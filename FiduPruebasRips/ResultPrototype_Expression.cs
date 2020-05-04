using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FiduPruebasRips
{
    /// <sumary>
    /// Clase para definición de funciones
    /// </sumary>
    public static class Helper
    {
        /// <sumary>
        /// Función principal RIPS
        /// </sumary> 
        /// <param name="parameters">parameters</param>
        public static dynamic MainRIPS(ENT_RipsParameters parameters)
        {
            try
            {

                if (parameters.CompanyId == 0) throw new ArgumentException($"El Id de la Empresa no puedo estar vacío");
                if (string.IsNullOrEmpty(parameters.CaseNumber)) throw new ArgumentException($"El número del caso no puede estar vacío");
                if (parameters.OperatorId == 0) throw new ArgumentException($"El id del operador no puede estar vacío");
                if (parameters.LibraryId == 0) throw new ArgumentException($"El Id de la librería no puede estar vacío");
                if (parameters.IdTypePopulation == 0) throw new ArgumentException($"El Id de tipo de población no puede estar vacío");
                if (string.IsNullOrEmpty(parameters.TrackingCode)) throw new ArgumentException($"El código de seguimiento no puede estar vacío");
                if (string.IsNullOrEmpty(parameters.UserCode)) throw new ArgumentException($"El código de usuario no puede estar vacío");
                if (string.IsNullOrEmpty(parameters.CTFileId)) throw new ArgumentException($"El id del archivo CT no puede estar vacío");
                if (parameters.InitDate == DateTime.MinValue) throw new ArgumentException($"La fecha de inicio no es válida");
                if (parameters.EndDate == DateTime.MinValue) throw new ArgumentException($"La fecha final no es válida");


                var sql = new StringBuilder();
                var listErrors = new List<string>();
                List<ENT_CTFile> listEntidadesCT = new List<ENT_CTFile>();
                Dictionary<string, decimal> dictionaryInvoices = new Dictionary<string, decimal>();

                Dictionary<string, List<string>> _dictionaryErrors = new Dictionary<string, List<string>>();
                Dictionary<string, string[]> _dictionaryHabilitationCodes = new Dictionary<string, string[]>();
                List<dynamic> filesInfo = new List<dynamic>();

                // Parametrización de los archivos
                filesInfo.Add(new { Code = "AF", FileId = parameters.AFFileId, ColumnLength = 17, Type = typeof(ENT_AFFile), List = new List<ENT_AFFile>() });
                filesInfo.Add(new { Code = "US", FileId = parameters.USFileId, ColumnLength = 14, Type = typeof(ENT_USFile), List = new List<ENT_USFile>() });
                filesInfo.Add(new { Code = "AD", FileId = parameters.ADFileId, ColumnLength = 6, Type = typeof(ENT_ADFile), List = new List<ENT_ADFile>() });
                filesInfo.Add(new { Code = "AT", FileId = parameters.ATFileId, ColumnLength = 11, Type = typeof(ENT_ATFile), List = new List<ENT_ATFile>() });
                filesInfo.Add(new { Code = "AN", FileId = parameters.ANFileId, ColumnLength = 14, Type = typeof(ENT_ANFile), List = new List<ENT_ANFile>() });
                filesInfo.Add(new { Code = "AP", FileId = parameters.APFileId, ColumnLength = 15, Type = typeof(ENT_APFile), List = new List<ENT_APFile>() });
                filesInfo.Add(new { Code = "AU", FileId = parameters.AUFileId, ColumnLength = 17, Type = typeof(ENT_AUFile), List = new List<ENT_AUFile>() });
                filesInfo.Add(new { Code = "AH", FileId = parameters.AHFileId, ColumnLength = 19, Type = typeof(ENT_AHFile), List = new List<ENT_AHFile>() });
                filesInfo.Add(new { Code = "AM", FileId = parameters.AMFileId, ColumnLength = 14, Type = typeof(ENT_AMFile), List = new List<ENT_AMFile>() });
                filesInfo.Add(new { Code = "AC", FileId = parameters.ACFileId, ColumnLength = 17, Type = typeof(ENT_ACFile), List = new List<ENT_ACFile>() });

                Dictionary<string, string> _dictionaryDocumentType = new Dictionary<string, string>();
                _dictionaryDocumentType.Add("CC", "1");
                _dictionaryDocumentType.Add("RC", "2");
                _dictionaryDocumentType.Add("TI", "3");
                _dictionaryDocumentType.Add("SC", "4");
                _dictionaryDocumentType.Add("CE", "6");
                _dictionaryDocumentType.Add("PA", "7");
                _dictionaryDocumentType.Add("CD", "9");
                _dictionaryDocumentType.Add("CN", "1423");
                _dictionaryDocumentType.Add("MS", "1424");
                _dictionaryDocumentType.Add("AS", "2825");
                _dictionaryDocumentType.Add("PE", "3271");

                List<string> listConceptCode = new List<string> { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14" };
                List<string> listZone = new List<string> { "U", "R" };
                List<string> listSex = new List<string> { "F", "M" };
                List<string> listSexNum = new List<string> { "1", "2" };

                DateTime initDate = parameters.InitDate;
                DateTime endDate = parameters.EndDate;

                int adapterId = 1;

                sql.Append(" SELECT Id FROM TypeDetail WITH(NOLOCK)");
                sql.AppendFormat(" WHERE Code = {0} AND IdTypeHead = 72", parameters.IdTypePopulation);

                var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
                if (resultExecute.IsError)
                {
                    return resultExecute;
                }
                var typePopulation = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());
                if (typePopulation == null || typePopulation.Count == 0)
                    return new ENT_ActionResult() { IsError = true, ErrorMessage = "El tipo de IPS del afiliado no existe" };

                sql = new StringBuilder();
                sql.Append(" SELECT IdOperator ");
                sql.Append(" FROM FileHead WITH(NOLOCK)");
                sql.AppendFormat(" WHERE InitDate = '{0}'", initDate.ToString("MM/dd/yyyy"));
                sql.AppendFormat(" AND EndDate = '{0}'", endDate.ToString("MM/dd/yyyy"));
                sql.AppendFormat(" AND IdOperator = {0} ", parameters.OperatorId);
                sql.AppendFormat(" AND IdTypePopulation = {0}", typePopulation.FirstOrDefault().Id.ToString());

                // Ejecutamos consulta para validar que no exista cargue para ese periodo
                resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
                if (resultExecute.IsError)
                {
                    return resultExecute;
                }

                var _head = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());
                if (_head == null || _head.Count() == 0)
                {
                    string lineSeparator = "\r\n";
                    string columnSeparator = ",";

                    // Obtenemos la data del archivo CT
                    listEntidadesCT = USR_ValidateFile(lineSeparator, columnSeparator, "CT", parameters.CompanyId, parameters.LibraryId, parameters.CTFileId, 4, typeof(ENT_CTFile), listErrors);

                    //Validamos que deban estar relacionados entre 3 y 10 archivos
                    if (listEntidadesCT.Count < 3 || listEntidadesCT.Count > 10)
                        listErrors.Add("La cantidad de archivos relacionados en el CT es incorrecto");

                    if (!listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("AF")))
                        listErrors.Add("El archivo AF no existe en el archivo CT");

                    if (!listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("US")))
                        listErrors.Add("El archivo US no existe en el archivo CT");

                    if (listErrors.Count > 0)
                    {
                        _dictionaryErrors.Add("CT", listErrors);
                    }
                    else
                    {
                        // Obtiene los codigos de habilitación de los archivos registrados en el CT
                        foreach (var entidadCT in listEntidadesCT)
                        {
                            _dictionaryHabilitationCodes.Add(entidadCT.FileCode.Trim().ToUpper().Substring(0, 2).ToUpper(), entidadCT.CodeProvider.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries));
                        }
                    }

                    // Valida la estructura de todos los archivos registrados en el CT
                    foreach (dynamic info in filesInfo.Where(file => listEntidadesCT.Any(ent => ent.FileCode.Trim().ToUpper().StartsWith(file.Code))).ToList())
                    {
                        listErrors = new List<string>();
                        info.List.AddRange(USR_ValidateFile(lineSeparator, columnSeparator, info.Code, parameters.CompanyId, parameters.LibraryId, info.FileId, info.ColumnLength, info.Type, listErrors));

                        if (listErrors.Count > 0)
                        {
                            _dictionaryErrors.Add(info.Code, listErrors);
                        }
                    }

                    // Valida si llega el id pero no esta en el CT
                    foreach (dynamic info in filesInfo.Where(file => !listEntidadesCT.Any(ent => ent.FileCode.Trim().ToUpper().StartsWith(file.Code))).ToList())
                    {
                        listErrors = new List<string>();
                        if (!string.IsNullOrEmpty(info.FileId))
                            listErrors.Add($"El archivo {info.Code} no se encuentra registrado en el archivo CT");

                        if (listErrors.Count > 0)
                        {
                            _dictionaryErrors.Add(info.Code, listErrors);
                        }
                    }

                    // Termina retornando ruta del archivo
                    if (_dictionaryErrors.Count > 0)
                    {
                        string pathFile = USR_SaveLog(_dictionaryErrors);
                        var attach = USR_WSAttachFileToProcess(pathFile, parameters.UserCode, parameters.CompanyId.ToString(), parameters.CaseNumber, parameters.TrackingCode);
                        if (attach.IsError)
                        {
                            attach.ErrorMessage = "No se pudo asociar el archivo al proceso. " + attach.ErrorMessage;
                            return attach;
                        }
                        return new ENT_ActionResult() { FileName = attach.FileName, IsError = true, ErrorMessage = "Hubo errores en la validación " };
                    }

                    // Consultas a bd necesarias para validación
                    var enabledFunctions = USR_GetEnabledFunctions(adapterId);
                    if (enabledFunctions.IsError)
                    {
                        return enabledFunctions;
                    }
                    // DocumentTypes
                    var documentTypes = USR_GetDocumentTypes(adapterId);
                    if (documentTypes.IsError)
                    {
                        return documentTypes;
                    }
                    // TypeDetailCity
                    var typeDetailsCity = USR_GetTypeDetail(adapterId);
                    if (typeDetailsCity.IsError)
                    {
                        return typeDetailsCity;
                    }
                    // Cups
                    var cups = USR_GetCups(adapterId);
                    if (cups.IsError)
                    {
                        return cups;
                    }
                    // TypeDetail
                    var typeDetailsConcept = USR_GetTypeDetailConcept(adapterId);
                    if (typeDetailsConcept.IsError)
                    {
                        return typeDetailsConcept;
                    }
                    // Diagnosis
                    var diagnosis = USR_GetDiagnosis(adapterId);
                    if (diagnosis.IsError)
                    {
                        return diagnosis;
                    }
                    // CUM
                    var cums = USR_GetCum(adapterId);
                    if (cums.IsError)
                    {
                        return cums;
                    }

                    var listDocumentTypesDB = JsonConvert.DeserializeObject<List<dynamic>>(documentTypes.Result.ToString());
                    var listEnabledFunctions = JsonConvert.DeserializeObject<List<dynamic>>(enabledFunctions.Result.ToString());

                    var listTypeDetails = JsonConvert.DeserializeObject<List<dynamic>>(typeDetailsCity.Result.ToString());
                    Dictionary<dynamic, dynamic> dictionaryTypeDetails = listTypeDetails.ToDictionary(x => x.Code.ToString(), x => x);

                    var listTypeDetailsConcept = JsonConvert.DeserializeObject<List<dynamic>>(typeDetailsConcept.Result.ToString());
                    Dictionary<dynamic, dynamic> dictionaryDetailsConcept = listTypeDetailsConcept.ToDictionary(x => x.Id.ToString(), x => x);

                    var listCups = JsonConvert.DeserializeObject<List<dynamic>>(cups.Result.ToString());
                    Dictionary<dynamic, dynamic> dictionaryCups = listCups.ToDictionary(x => x.Code.ToString(), x => x);

                    var listDiagnosis = JsonConvert.DeserializeObject<List<dynamic>>(diagnosis.Result.ToString());
                    Dictionary<dynamic, dynamic> dictionaryDiagnosis = listDiagnosis.ToDictionary(x => x.Code.ToString().Trim(), x => x);

                    var listCum = JsonConvert.DeserializeObject<List<dynamic>>(cums.Result.ToString());
                    Dictionary<dynamic, dynamic> dictionaryCum = listCum.ToDictionary(x => x.Code.ToString(), x => x);

                    // Listas de entidades
                    List<ENT_AFFile> listAF = filesInfo.Where(file => file.Code == "AF").Select(lst => lst.List).FirstOrDefault();
                    Dictionary<string, ENT_AFFile> _dictionaryAF = new Dictionary<string, ENT_AFFile>();
                    List<ENT_USFile> listUS = filesInfo.Where(file => file.Code == "US").Select(lst => lst.List).FirstOrDefault();
                    Dictionary<string, ENT_USFile> _dictionaryUS = new Dictionary<string, ENT_USFile>();
                    List<ENT_ACFile> listAC = filesInfo.Where(file => file.Code == "AC").Select(lst => lst.List).FirstOrDefault();
                    Dictionary<string, ENT_ACFile> _dictionaryAC = new Dictionary<string, ENT_ACFile>();
                    List<ENT_AUFile> listAU = filesInfo.Where(file => file.Code == "AU").Select(lst => lst.List).FirstOrDefault();
                    Dictionary<string, ENT_AUFile> _dictionaryAU = new Dictionary<string, ENT_AUFile>();
                    List<ENT_AHFile> listAH = filesInfo.Where(file => file.Code == "AH").Select(lst => lst.List).FirstOrDefault();
                    Dictionary<string, ENT_AHFile> _dictionaryAH = new Dictionary<string, ENT_AHFile>();
                    List<ENT_ADFile> listAD = filesInfo.Where(file => file.Code == "AD").Select(lst => lst.List).FirstOrDefault();
                    List<ENT_APFile> listAP = filesInfo.Where(file => file.Code == "AP").Select(lst => lst.List).FirstOrDefault();
                    List<ENT_AMFile> listAM = filesInfo.Where(file => file.Code == "AM").Select(lst => lst.List).FirstOrDefault();
                    List<ENT_ANFile> listAN = filesInfo.Where(file => file.Code == "AN").Select(lst => lst.List).FirstOrDefault();
                    List<ENT_ATFile> listAT = filesInfo.Where(file => file.Code == "AT").Select(lst => lst.List).FirstOrDefault();

                    var culture = new System.Globalization.CultureInfo("es-ES");
                    System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

                    // Validación CT
                    listErrors = new List<string>();
                    USR_ValidateCT(listEnabledFunctions, listEntidadesCT, filesInfo, _dictionaryHabilitationCodes.Values, adapterId, listErrors);
                    if (listErrors.Count > 0)
                    {
                        _dictionaryErrors.Add("CT", listErrors);
                    }

                    List<string> habilitationCodes = new List<string>();
                    // Validacion AF                   
                    if (listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("AF")))
                    {
                        habilitationCodes = new List<string>(_dictionaryHabilitationCodes.FirstOrDefault(di => di.Key == "AF").Value);
                        USR_ValidateAF(listEnabledFunctions, listAF, habilitationCodes, initDate, endDate, _dictionaryAF, _dictionaryErrors);

                    }
                    // Validacion US                 
                    if (listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("US")))
                        USR_ValidateUS(listEnabledFunctions, listUS, _dictionaryDocumentType, listDocumentTypesDB, dictionaryTypeDetails, listSex, listZone, adapterId, _dictionaryUS, _dictionaryErrors);

                    // Validacion AD                   
                    if (listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("AD")))
                    {
                        habilitationCodes = new List<string>(_dictionaryHabilitationCodes.FirstOrDefault(di => di.Key == "AD").Value);
                        USR_ValidateAD(listEnabledFunctions, listAD, habilitationCodes, listConceptCode, _dictionaryAF, _dictionaryErrors);
                    }
                    // Validacion AC                 
                    if (listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("AC")))
                    {
                        habilitationCodes = new List<string>(_dictionaryHabilitationCodes.FirstOrDefault(di => di.Key == "AC").Value);
                        USR_ValidateAC(listEnabledFunctions, listAC, habilitationCodes, _dictionaryAF, _dictionaryUS, _dictionaryDocumentType, dictionaryCups, dictionaryDetailsConcept, dictionaryInvoices, _dictionaryAC, _dictionaryErrors);
                    }
                    // Validacion AU                 
                    if (listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("AU")))
                    {
                        habilitationCodes = new List<string>(_dictionaryHabilitationCodes.FirstOrDefault(di => di.Key == "AU").Value);
                        USR_ValidateAU(listEnabledFunctions, listAU, habilitationCodes, _dictionaryAF, _dictionaryUS, _dictionaryDocumentType, dictionaryDiagnosis, _dictionaryAU, _dictionaryErrors);
                    }
                    // Validacion AP                    
                    if (listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("AP")))
                    {
                        habilitationCodes = new List<string>(_dictionaryHabilitationCodes.FirstOrDefault(di => di.Key == "AP").Value);
                        USR_ValidateAP(listEnabledFunctions, listAP, habilitationCodes, _dictionaryAF, _dictionaryUS, _dictionaryDocumentType, dictionaryCups, dictionaryDiagnosis, dictionaryDetailsConcept, dictionaryInvoices, _dictionaryErrors);
                    }
                    // Validacion AH                   
                    if (listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("AH")))
                    {
                        habilitationCodes = new List<string>(_dictionaryHabilitationCodes.FirstOrDefault(di => di.Key == "AH").Value);
                        USR_ValidateAH(listEnabledFunctions, listAH, habilitationCodes, _dictionaryAF, _dictionaryUS, _dictionaryAU, _dictionaryAC, _dictionaryDocumentType, dictionaryDiagnosis, _dictionaryAH, _dictionaryErrors);
                    }
                    // Validacion AM                   
                    if (listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("AM")))
                    {
                        habilitationCodes = new List<string>(_dictionaryHabilitationCodes.FirstOrDefault(di => di.Key == "AM").Value);
                        USR_ValidateAM(listEnabledFunctions, listAM, habilitationCodes, _dictionaryAF, _dictionaryUS, _dictionaryDocumentType, dictionaryCum, dictionaryDetailsConcept, dictionaryInvoices, _dictionaryErrors);
                    }
                    // Validacion AN                   
                    if (listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("AN")))
                    {
                        habilitationCodes = new List<string>(_dictionaryHabilitationCodes.FirstOrDefault(di => di.Key == "AN").Value);
                        USR_ValidateAN(listEnabledFunctions, listAN, habilitationCodes, _dictionaryAF, _dictionaryUS, _dictionaryDocumentType, dictionaryDiagnosis, _dictionaryAH, listSexNum, _dictionaryErrors);
                    }
                    // Validacion AT                
                    if (listEntidadesCT.Any(a => a.FileCode.Trim().ToUpper().StartsWith("AT")))
                    {
                        habilitationCodes = new List<string>(_dictionaryHabilitationCodes.FirstOrDefault(di => di.Key == "AT").Value);
                        USR_ValidateAT(listEnabledFunctions, listAT, habilitationCodes, _dictionaryAF, _dictionaryUS, _dictionaryDocumentType, dictionaryCups, dictionaryDetailsConcept, dictionaryInvoices, _dictionaryErrors);
                    }

                    if (parameters.Establishment == 1)
                        USR_ValidatePayValueAF(dictionaryInvoices, listAF, _dictionaryErrors);
                    // Termina retornando ruta del archivo
                    if (_dictionaryErrors.Count > 0)
                    {
                        string pathFile = USR_SaveLog(_dictionaryErrors);
                        var attach = USR_WSAttachFileToProcess(pathFile, parameters.UserCode, parameters.CompanyId.ToString(), parameters.CaseNumber, parameters.TrackingCode);
                        if (attach.IsError)
                        {
                            attach.ErrorMessage = "No se pudo asociar el archivo al proceso. " + attach.ErrorMessage;
                            return attach;
                        }
                        return new ENT_ActionResult() { FileName = attach.FileName, IsError = true, ErrorMessage = "Hubo errores en la validación " };

                    }


                    // Obtenemos los datos del archivo CT para guardar la cabecera
                    var resultCT = USR_WSGetFile(parameters.CompanyId, parameters.LibraryId, parameters.CTFileId);
                    if (resultCT.IsError)
                    {
                        throw new Exception(resultCT.ErrorMessage);
                    }
                    var dataCT = JsonConvert.DeserializeObject<dynamic>(resultCT.Result.ToString());

                    string reference = dataCT.FileName.ToString().Substring(2, dataCT.FileName.ToString().LastIndexOf(".") - 2);

                    // Guarda la cabecera
                    var resultSave = USR_SaveFileHead(reference, parameters.OperatorId.ToString(), initDate.ToLongDateString(), endDate.ToLongDateString(), parameters.CaseNumber, typePopulation.FirstOrDefault().Id.ToString());
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    var saveHead = JsonConvert.DeserializeObject<dynamic>(resultSave.Result.ToString());
                    string head = saveHead.Id.Value.ToString();

                    // Guardamos los archivos
                    resultSave = USR_SaveFileCT(listEntidadesCT, head);
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    resultSave = USR_SaveFileAF(listAF, head);
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    resultSave = USR_SaveFileUS(listUS, head);
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    resultSave = USR_SaveFileAC(listAC, head);
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    resultSave = USR_SaveFileAT(listAT, head);
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    resultSave = USR_SaveFileAU(listAU, head);
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    resultSave = USR_SaveFileAP(listAP, head);
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    resultSave = USR_SaveFileAH(listAH, head);
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    resultSave = USR_SaveFileAM(listAM, head);
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    resultSave = USR_SaveFileAN(listAN, head);
                    if (resultSave.IsError)
                    {
                        return resultSave;
                    }
                    if (listEntidadesCT.Count > 10)
                    {
                        resultSave = USR_SaveFileAD(listAD, head);
                        if (resultSave.IsError)
                        {
                            return resultSave;
                        }
                    }

                    return new ENT_ActionResult() { IsSuccessful = true, Result = head };
                }
                else
                {
                    return new ENT_ActionResult() { IsError = true, ErrorMessage = "Ya se realizo un cargue para este periodo" };
                }
            }
            catch (Exception ex)
            {
                return new ENT_ActionResult() { IsError = true, ErrorMessage = ex.Message };
            }
        }
        /// <sumary>
        /// Obtiene CUM
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        public static ENT_ActionResult USR_GetCum(long adapterId)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT DISTINCT Code, IdPos ");
            sql.Append("FROM Cum");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Obtiene CUPS
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        public static ENT_ActionResult USR_GetCups(long adapterId)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT TOP 1 Id  ");
            sql.Append("FROM EffectiveDatesCUPS WITH (NOLOCK)");
            sql.Append("WHERE InitialDatePeriod <= SYSDATETIME() AND EndDatePeriod >= SYSDATETIME() AND Status = 'S'");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            var listTypeDetails = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());

            sql = new StringBuilder();
            sql.Append("SELECT DISTINCT Code, IdServiceConcept, IdServicePurpose, IdProcedureType, IdProcedurePurpose ");
            sql.Append($"FROM Cups WITH (NOLOCK) where IdEffectiveDatesCUPS = '{listTypeDetails.FirstOrDefault().Id}'");
            resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }

            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Obtiene Diagnóstico
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        public static ENT_ActionResult USR_GetDiagnosis(long adapterId)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT DISTINCT Code ");
            sql.Append("FROM Diagnosis");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Obtiene tipos de documento
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        public static ENT_ActionResult USR_GetDocumentTypes(long adapterId)
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT Id, Code ");
            sql.Append(" FROM TypeDetail WITH (NOLOCK)");
            sql.Append(" WHERE IdTypeHead = 1");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Obtiene las funciones para validación de campos
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        public static ENT_ActionResult USR_GetEnabledFunctions(long adapterId)
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT IdFile,CodeFunction,Status ");
            sql.Append(" FROM ConfigurationDetail CD WITH (NOLOCK)");
            sql.Append(" INNER JOIN ConfigurationHead CH WITH (NOLOCK) ON (CD.IdConfigurationHead = CH.Id) ");
            sql.Append(" WHERE IdProccess = 1 ");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Obtiene el tipo detalle
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        public static ENT_ActionResult USR_GetTypeDetail(long adapterId)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT DISTINCT Code ");
            sql.Append("FROM TypeDetail WITH (NOLOCK)");
            sql.Append("WHERE IdTypeHead in (16, 17)");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Obtiene tipo detalle por concepto
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        public static ENT_ActionResult USR_GetTypeDetailConcept(long adapterId)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT Id, Code ");
            sql.Append("FROM TypeDetail WITH (NOLOCK)");
            sql.Append("WHERE IdTypeHead in (26, 27, 28, 29, 31)");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Guarda archivos AC
        /// </sumary> 
        /// <param name="listSaveAC">Listado de archivos AC</param>
        /// <param name="idFileHead">Id cabecera</param>
        public static ENT_ActionResult USR_SaveFileAC(List<ENT_ACFile> listSaveAC, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveAC)
            {
                dynamic fileAC = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.InvoiceNumber,
                    itemSave.CodeProvider,
                    itemSave.IdentificationType,
                    itemSave.IdentificationNumber,
                    ConsultationDateDTO = itemSave.ConsultationDate,
                    itemSave.AuthorizationNumber,
                    itemSave.ConsultationCode,
                    ConsultationPurposeDTO = itemSave.ConsultationPurpose,
                    ExternalCauseDTO = itemSave.ExternalCause,
                    itemSave.PrincipalDiagnosisCode,
                    itemSave.RelationDiagnosisCodeOne,
                    itemSave.RelationDiagnosisCodeTwo,
                    itemSave.RelationDiagnosisCodeThree,
                    TypeMainDiagnosisDTO = itemSave.TypeMainDiagnosis,
                    ConsultationValueDTO = itemSave.ConsultationValue,
                    ModeratorValueDTO = itemSave.ModeratorValue,
                    PayValueDTO = itemSave.PayValue
                };
                listSaveDB.Add(fileAC);
            }
            dynamic parameter = new
            {
                FileACDTO = listSaveDB
            };
            var resultSave = USR_SaveRipsFile(4, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo AC " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda archivos AD
        /// </sumary> 
        /// <param name="listSaveAD">Listado archivos AD</param>
        /// <param name="idFileHead">Id cabecera</param>
        public static ENT_ActionResult USR_SaveFileAD(List<ENT_ADFile> listSaveAD, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveAD)
            {
                dynamic fileAD = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.InvoiceNumber,
                    itemSave.CodeProvider,
                    itemSave.ConceptCode,
                    AmountDTO = itemSave.Amount,
                    UnitValueDTO = itemSave.UnitValue,
                    TotalValueByConceptDTO = itemSave.TotalValueByConcept
                };
                listSaveDB.Add(fileAD);
            }
            dynamic parameter = new
            {
                FileADDTO = listSaveDB
            };
            var resultSave = USR_SaveRipsFile(11, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo AD " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda archivo AF
        /// </sumary> 
        /// <param name="listSaveAF">Lista archivos AF</param>
        /// <param name="idFileHead">Id cabecera</param>
        public static ENT_ActionResult USR_SaveFileAF(List<ENT_AFFile> listSaveAF, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveAF)
            {
                dynamic fileAF = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.CodeProvider,
                    itemSave.NameProvider,
                    itemSave.IdentificationType,
                    IdentificatonProviderDTO = itemSave.IdentificatonProvider,
                    itemSave.InvoiceNumber,
                    ExpeditionInvoiceDTO = itemSave.ExpeditionInvoice,
                    PeriodValidityStartDateDTO = itemSave.PeriodValidityStartDate,
                    PeriodValidityEndDateDTO = itemSave.PeriodValidityEndDate,
                    itemSave.AdministeringEntityCode,
                    itemSave.AdministeringEntityName,
                    itemSave.ContractNumber,
                    itemSave.BenefitsPlan,
                    itemSave.PolicyNumber,
                    CoPaymentDTO = itemSave.CoPayment,
                    CommissionValueDTO = itemSave.CommissionValue,
                    DiscountValueDTO = itemSave.DiscountValue,
                    PayValueDTO = itemSave.PayValue
                };
                listSaveDB.Add(fileAF);
            }
            dynamic parameter = new
            {
                FileAFDTO = listSaveDB
            };
            var resultSave = USR_SaveRipsFile(2, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo AF " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda archivos AH
        /// </sumary> 
        /// <param name="listSaveAH">Listado archivos AH</param>
        /// <param name="idFileHead">Id cabecera</param>
        public static ENT_ActionResult USR_SaveFileAH(List<ENT_AHFile> listSaveAH, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveAH)
            {
                dynamic fileAH = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.InvoiceNumber,
                    itemSave.CodeProvider,
                    itemSave.IdentificationType,
                    itemSave.IdentificationNumber,
                    InstitutionEntryDTO = itemSave.InstitutionEntry,
                    InstitutionEntryDateDTO = itemSave.InstitutionEntryDate,
                    itemSave.InstitutionEntryHour,
                    itemSave.AuthorizationNumber,
                    ExternalCauseDTO = itemSave.ExternalCause,
                    itemSave.PrincipalDiagnosisCode,
                    itemSave.MainDiagnostic,
                    itemSave.RelationMainDiagnosisOne,
                    itemSave.RelationMainDiagnosisTwo,
                    itemSave.RelationMainDiagnosisThree,
                    itemSave.ComplicationDiagnosis,
                    StatusExitDTO = itemSave.StatusExit,
                    itemSave.DeathCauseDiagnosis,
                    InstitutionDateExitDTO = itemSave.InstitutionDateExit,
                    itemSave.InstitutionHourExit
                };
                listSaveDB.Add(fileAH);
            }
            dynamic parameter = new
            {
                FileAHDTO = listSaveDB
            };
            var resultSave = USR_SaveRipsFile(8, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo AH " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda archivos AM
        /// </sumary> 
        /// <param name="listSaveAM">Listado archivos AM</param>
        /// <param name="idFileHead">Id cabecera</param>
        public static ENT_ActionResult USR_SaveFileAM(List<ENT_AMFile> listSaveAM, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveAM)
            {
                dynamic fileAM = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.InvoiceNumber,
                    itemSave.CodeProvider,
                    itemSave.IdentificationType,
                    itemSave.IdentificationNumber,
                    itemSave.AuthorizationNumber,
                    itemSave.DrugsCode,
                    DrugsTypeDTO = itemSave.DrugsType,
                    itemSave.DrugsGeneric,
                    itemSave.PharmaceuticalForm,
                    itemSave.DrugsConcentration,
                    itemSave.DrugsUnitofMeasure,
                    DrugsAmountDTO = itemSave.DrugsAmount,
                    DrugsUnitValueDTO = itemSave.DrugsUnitValue,
                    DrugsTotalValueDTO = itemSave.DrugsTotalValue
                };
                listSaveDB.Add(fileAM);
            }
            dynamic parameter = new
            {
                FileAMDTO = listSaveDB
            };
            var resultSave = USR_SaveRipsFile(9, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo AM " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda archivos AN
        /// </sumary> 
        /// <param name="listSaveAN">Listado de archivos AN</param>
        /// <param name="idFileHead">Id cabecera</param>
        public static ENT_ActionResult USR_SaveFileAN(List<ENT_ANFile> listSaveAN, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveAN)
            {
                dynamic fileAN = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.InvoiceNumber,
                    itemSave.CodeProvider,
                    itemSave.IdentificationType,
                    itemSave.IdentificationNumber,
                    NewbornDateDTO = itemSave.NewbornDate,
                    itemSave.NewbornHour,
                    GestationalAgeDTO = itemSave.GestationalAge,
                    PrenatalControlDTO = itemSave.PrenatalControl,
                    itemSave.NewbornSex,
                    NewbornWeightDTO = itemSave.NewbornWeight,
                    itemSave.NewbornDiagnosis,
                    itemSave.DeathCauseDiagnosis,
                    DeathCauseDateDTO = itemSave.DeathCauseDate,
                    itemSave.DeathCauseHour
                };
                listSaveDB.Add(fileAN);
            }
            dynamic parameter = new
            {
                FileANDTO = listSaveDB
            };
            var resultSave = USR_SaveRipsFile(10, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo AN " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda archivos AP
        /// </sumary> 
        /// <param name="listSaveAP">Listado de archivos AP</param>
        /// <param name="idFileHead">Id cabecera</param>
        public static ENT_ActionResult USR_SaveFileAP(List<ENT_APFile> listSaveAP, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveAP)
            {
                dynamic fileAP = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.InvoiceNumber,
                    itemSave.CodeProvider,
                    itemSave.IdentificationType,
                    itemSave.IdentificationNumber,
                    ProcedureDateDTO = itemSave.ProcedureDate,
                    itemSave.AuthorizationNumber,
                    itemSave.ProcedureCode,
                    ProcedureScopeDTO = itemSave.ProcedureScope,
                    ProcedurePurposeDTO = itemSave.ProcedurePurpose,
                    AttendPersonalDTO = itemSave.AttendPersonal,
                    itemSave.PrincipalDiagnosticCode,
                    itemSave.RelationDiagnosisCodeOne,
                    itemSave.Complication,
                    ActSurgicalDTO = itemSave.ActSurgical,
                    PayProcedureDTO = itemSave.PayProcedure
                };
                listSaveDB.Add(fileAP);
            }
            dynamic parameter = new
            {
                FileAPDTO = listSaveDB
            };
            var resultSave = USR_SaveRipsFile(7, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo AP " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda archivos AT
        /// </sumary> 
        /// <param name="listSaveAT">Lista archivos AT</param>
        /// <param name="idFileHead">Id cabecera</param>
        public static ENT_ActionResult USR_SaveFileAT(List<ENT_ATFile> listSaveAT, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveAT)
            {
                dynamic fileAT = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.InvoiceNumber,
                    itemSave.CodeProvider,
                    itemSave.IdentificationType,
                    itemSave.IdentificationNumber,
                    itemSave.AuthorizationNumber,
                    ServiceTypeDTO = itemSave.ServiceType,
                    itemSave.ServiceCode,
                    itemSave.ServiceName,
                    ServiceAmountDTO = itemSave.ServiceAmount,
                    ServiceUnitValueDTO = itemSave.ServiceUnitValue,
                    ServiceTotalValueDTO = itemSave.ServiceTotalValue
                };
                listSaveDB.Add(fileAT);
            }
            dynamic parameter = new
            {
                FileATDTO = listSaveDB
            };

            var resultSave = USR_SaveRipsFile(5, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo AT " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda archivos AU
        /// </sumary> 
        /// <param name="listSaveAU">Listado de archivos AU</param>
        /// <param name="idFileHead">Id cabecera</param>
        public static ENT_ActionResult USR_SaveFileAU(List<ENT_AUFile> listSaveAU, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveAU)
            {
                dynamic fileAU = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.InvoiceNumber,
                    itemSave.CodeProvider,
                    itemSave.IdentificationType,
                    itemSave.IdentificationNumber,
                    ObservationDateDTO = itemSave.ObservationDate,
                    itemSave.ObservationHour,
                    itemSave.AuthorizationNumber,
                    ExternalCauseDTO = itemSave.ExternalCause,
                    itemSave.DiagnosisEnd,
                    itemSave.RelationDiagnosisExitOne,
                    itemSave.RelationDiagnosisExitTwo,
                    itemSave.RelationDiagnosisExitThree,
                    ObservationDestinationDTO = itemSave.ObservationDestination,
                    StatusExitDTO = itemSave.StatusExit,
                    itemSave.DeathCause,
                    ObservationDateExitDTO = itemSave.ObservationDateExit,
                    itemSave.ObservationHourExit
                };
                listSaveDB.Add(fileAU);
            }
            dynamic parameter = new
            {
                FileAUDTO = listSaveDB
            };

            var resultSave = USR_SaveRipsFile(6, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo AU " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda archivo CT
        /// </sumary> 
        /// <param name="listSaveCT">Lista archivos CT</param>
        /// <param name="idFileHead">Id Cabecera</param>
        public static ENT_ActionResult USR_SaveFileCT(List<ENT_CTFile> listSaveCT, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveCT)
            {
                dynamic fileCT = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.CodeProvider,
                    ReceivedDateDTO = itemSave.ReceivedDate,
                    itemSave.FileCode,
                    TotalRecordDTO = itemSave.TotalRecord
                };
                listSaveDB.Add(fileCT);
            }
            dynamic parameter = new
            {
                FileCTDTO = listSaveDB
            };

            var resultSave = USR_SaveRipsFile(1, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo CT " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda la cabecera de los archivos
        /// </sumary> 
        /// <param name="reference">Código de cabecera</param>
        /// <param name="idOperator">Id operador</param>
        /// <param name="initDate">Fecha inicio</param>
        /// <param name="endDate">Fecha fin</param>
        /// <param name="caseNumber">Número de caso</param>
        /// <param name="idTypePopulation">Id tipo población</param>
        public static ENT_ActionResult USR_SaveFileHead(string reference, string idOperator, string initDate, string endDate, string caseNumber, string idTypePopulation)
        {
            dynamic head = new
            {
                Code = reference,
                IdOperator = idOperator,
                InitDateDTO = initDate,
                EndDateDTO = endDate,
                ProcessDateDTO = DateTime.Now.ToLongDateString(),
                CaseNumber = caseNumber,
                IdTypePopulation = idTypePopulation
            };

            var resultSave = USR_SaveRipsFile(0, head);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar la cabecera " + resultSave.ErrorMessage;
                return resultSave;
            }

            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda archivo US
        /// </sumary> 
        /// <param name="listSaveUS">Lista archivos US</param>
        /// <param name="idFileHead">Id cabecera</param>
        public static ENT_ActionResult USR_SaveFileUS(List<ENT_USFile> listSaveUS, string idFileHead)
        {
            List<dynamic> listSaveDB = new List<dynamic>();
            foreach (var itemSave in listSaveUS)
            {
                dynamic fileUS = new
                {
                    IdFileHeadDTO = idFileHead,
                    itemSave.IdentificationType,
                    itemSave.IdentificationNumber,
                    itemSave.AdministeringEntityCode,
                    TypePersonDTO = itemSave.TypePerson,
                    itemSave.FirstLastName,
                    itemSave.SecondLastName,
                    itemSave.FirstName,
                    itemSave.SecondName,
                    AgePersonDTO = itemSave.AgePerson,
                    AgeUnitOfMeasurementDTO = itemSave.AgeUnitOfMeasurement,
                    itemSave.SexPerson,
                    itemSave.CodeTown,
                    itemSave.CodeCity,
                    itemSave.ZoneTown
                };
                listSaveDB.Add(fileUS);
            }
            dynamic parameter = new
            {
                FileUSDTO = listSaveDB
            };
            var resultSave = USR_SaveRipsFile(3, parameter);
            if (resultSave.IsError)
            {
                resultSave.ErrorMessage = "Ocurrio un error al guardar el archivo US " + resultSave.ErrorMessage;
                return resultSave;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultSave.Result };
        }
        /// <sumary>
        /// Guarda el archivo con la lista de errores RIPS
        /// </sumary> 
        /// <param name="dictionaryResult">Diccionario con resultado</param>
        public static string USR_SaveLog(dynamic dictionaryResult)
        {
            try
            {

                string pathName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Rips");

                if (!Directory.Exists(pathName))
                    Directory.CreateDirectory(pathName);

                pathName = Path.Combine(pathName, $"RIPSResult{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv");

                using (FileStream fs = new FileStream(pathName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine("ARCHIVO;MENSAJE");
                    foreach (var file in dictionaryResult)
                    {
                        for (int i = 0; i < file.Value.Count; i++)
                        {
                            sw.WriteLine($"{file.Key};{file.Value[i]}");
                        }
                        sw.Flush();
                    }
                }

                return pathName;
            }
            catch
            {
                throw;
            }
        }
        /// <sumary>
        /// Guarda archivos RIPS
        /// </sumary> 
        /// <param name="IdFile">Identificador de archivo Rips para obtener el controlador del servicio</param>
        /// <param name="listSave">Lista de tipo archivo Rips</param>
        public static ENT_ActionResult USR_SaveRipsFile(long IdFile, dynamic listSave)
        {
            try
            {

                string controller = string.Empty;
                string entity = string.Empty;

                switch (IdFile)
                {
                    case 0:
                        controller = "SaveFileRIPSFileHead";
                        break;
                    case 1:
                        controller = "SaveFileRIPSFileCT";
                        break;
                    case 2:
                        controller = "SaveFileRIPSFileAF";
                        entity = "FileAFDTO";
                        break;
                    case 3:
                        controller = "SaveFileRIPSFileUS";
                        entity = "FileUSDTO";
                        break;
                    case 4:
                        controller = "SaveFileRIPSFileAC";
                        entity = "FileACDTO";
                        break;
                    case 5:
                        controller = "SaveFileRIPSFileAT";
                        entity = "FileATDTO";
                        break;
                    case 6:
                        controller = "SaveFileRIPSFileAU";
                        entity = "FileAUDTO";
                        break;
                    case 7:
                        controller = "SaveFileRIPSFileAP";
                        entity = "FileAPDTO";
                        break;
                    case 8:
                        controller = "SaveFileRIPSFileAH";
                        entity = "FileAHDTO";
                        break;
                    case 9:
                        controller = "SaveFileRIPSFileAM";
                        entity = "FileAMDTO";
                        break;
                    case 10:
                        controller = "SaveFileRIPSFileAN";
                        entity = "FileANDTO";
                        break;
                    case 11:
                        controller = "SaveFileRIPSFileAD";
                        entity = "FileADDTO";
                        break;
                    default:
                        break;
                }

                ENT_ActionResult result = new ENT_ActionResult();
                string url = "http://190.217.17.108:8085/ASSURANCE/api/Rips/" + controller;

                if (IdFile > 1)
                {
                    var l = ((object)listSave).GetPropertyValue(entity);
                    var lstSave = ((List<dynamic>)l);
                    if (lstSave.Count < 6000)
                        result = SYS_WSPOST(url, listSave, null, null);
                    else
                    {
                        int startIndex = 0;
                        int top = 6000;
                        bool lastBulk = false;

                        do
                        {
                            List<dynamic> temp = lstSave.Skip(startIndex).Take(top).ToList();
                            dynamic objectTemp = new System.Dynamic.ExpandoObject();

                            #region Generate Object

                            var expandDic = objectTemp as IDictionary<string, object>;
                            if (expandDic.ContainsKey(entity))
                                expandDic[entity] = temp;
                            else
                                expandDic.Add(entity, temp);

                            #endregion

                            result = SYS_WSPOST(url, objectTemp, null, null);

                            if (!result.IsSuccessful)
                                return result;

                            startIndex = startIndex + top;
                            lastBulk = lstSave.Count < startIndex;
                        }
                        while (!lastBulk);
                    }
                }
                else
                {
                    result = SYS_WSPOST(url, listSave, null, null);
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ENT_ActionResult() { IsError = true, ErrorMessage = ex.Message + ex.StackTrace };
            }
        }
        /// <sumary>
        /// Valida archivo AC
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listAC">listAC</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="dictionaryAF">dictionaryAF</param>
        /// <param name="dictionaryUS">dictionaryUS</param>
        /// <param name="dictionaryDocumentType">dictionaryDocumentType</param>
        /// <param name="dictionaryCups">dictionaryCups</param>
        /// <param name="dictionaryDetailsConcept">dictionaryDetailsConcept</param>
        /// <param name="dictionaryInvoices">dictionaryInvoices</param>
        /// <param name="dictionaryAC">dictionaryAC</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidateAC(List<dynamic> listEnabledFunctions, List<ENT_ACFile> listAC, List<string> habilitationCodes, dynamic dictionaryAF, dynamic dictionaryUS, dynamic dictionaryDocumentType, dynamic dictionaryCups, dynamic dictionaryDetailsConcept, dynamic dictionaryInvoices, dynamic dictionaryAC, dynamic dictionaryErrors)
        {
            var listErrors = new List<string>();
            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "5" && f.Status == true);
            bool ValidateInvoiceNumberAC = functions.Any(f => f.CodeFunction == "ValidateInvoiceNumberAC");
            bool ValidateCodeProviderAC = functions.Any(f => f.CodeFunction == "ValidateCodeProviderAC");
            bool ValidateIdentificationNumberAC = functions.Any(f => f.CodeFunction == "ValidateIdentificationNumberAC");
            bool ValidateDocumentTypeAC = functions.Any(f => f.CodeFunction == "ValidateDocumentTypeAC");
            bool ValidateConsultationDateAC = functions.Any(f => f.CodeFunction == "ValidateConsultationDateAC");
            bool ValidateConsultationPurposeAC = functions.Any(f => f.CodeFunction == "ValidateConsultationPurposeAC");
            bool ValidateExternalCauseAC = functions.Any(f => f.CodeFunction == "ValidateExternalCauseAC");
            bool ValidateConsultationCodeAC = functions.Any(f => f.CodeFunction == "ValidateConsultationCodeAC");

            int index = 1;
            dynamic identification;
            int consultationPurpose = 0;
            DateTime consultationDate = new DateTime();

            foreach (ENT_ACFile file in listAC)
            {

                file.ConsultationValue = file.ConsultationValue.Replace('.', ',');
                file.ModeratorValue = file.ModeratorValue.Replace('.', ',');
                file.PayValue = file.PayValue.Replace('.', ',');
                identification = null;
                consultationPurpose = int.Parse(file.ConsultationPurpose);
                consultationDate = Convert.ToDateTime(file.ConsultationDate);

                if (ValidateInvoiceNumberAC)
                {
                    USR_ValidateInvoiceNumber(dictionaryAF, file.CodeProvider, file.InvoiceNumber, index, listErrors);
                }

                if (ValidateCodeProviderAC)
                    USR_ValidateCodeProvider(file.CodeProvider, habilitationCodes, index, listErrors);

                if (dictionaryUS.ContainsKey($"{file.IdentificationType}_{file.IdentificationNumber}"))
                {
                    identification = dictionaryUS[$"{file.IdentificationType}_{file.IdentificationNumber}"];
                }
                if (ValidateIdentificationNumberAC)
                    USR_ValidateIdentificationNumber(identification, index, listErrors);

                if (ValidateDocumentTypeAC)
                    USR_ValidateDocumentType(identification, dictionaryDocumentType, index, listErrors);

                if (ValidateConsultationDateAC)
                    USR_ValidateConsultationDateAC(consultationDate, index, listErrors);

                if (ValidateConsultationPurposeAC)
                    USR_ValidateConsultationPurposeAC(consultationPurpose, index, listErrors);

                if (ValidateExternalCauseAC)
                {
                    int? externalCause = null;
                    if (!string.IsNullOrEmpty(file.ExternalCause))
                    {
                        externalCause = int.Parse(file.ExternalCause);
                    }
                    USR_ValidateExternalCauseAC(externalCause, index, listErrors);
                }

                if (ValidateConsultationCodeAC)
                    USR_ValidateConsultationCodeAC(file.ConsultationCode, consultationPurpose, dictionaryCups, dictionaryDetailsConcept, index, listErrors);

                // Agrego a diccionario para consultas en otros archivos
                if (!dictionaryInvoices.ContainsKey($"{file.CodeProvider}_{file.InvoiceNumber}"))
                    dictionaryInvoices.Add($"{file.CodeProvider}_{file.InvoiceNumber}", Convert.ToDecimal(file.PayValue));
                else
                    dictionaryInvoices[$"{file.CodeProvider}_{file.InvoiceNumber}"] = dictionaryInvoices[$"{file.CodeProvider}_{file.InvoiceNumber}"] + Convert.ToDecimal(file.PayValue);

                // Agrego a diccionario para consultas en otros archivos
                if (!dictionaryAC.ContainsKey(file.IdentificationNumber))
                    dictionaryAC.Add(file.IdentificationNumber, file);

                index++;
            }

            if (listErrors.Count > 0)
            {

                dictionaryErrors.Add("AC", listErrors);
            }

            return listErrors;
        }
        /// <sumary>
        /// Valida archivo AD
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listAD">listAD</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="listConceptCode">listConceptCode</param>
        /// <param name="dictionaryAF">dictionaryAF</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidateAD(List<dynamic> listEnabledFunctions, List<ENT_ADFile> listAD, List<string> habilitationCodes, List<string> listConceptCode, dynamic dictionaryAF, dynamic dictionaryErrors)
        {
            List<string> listErrors = new List<string>();

            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "4" && f.Status == true);
            bool ValidateCodeProviderAD = functions.Any(f => f.CodeFunction == "ValidateCodeProviderAD");
            bool ValidateInvoiceNumberAD = functions.Any(f => f.CodeFunction == "ValidateInvoiceNumberAD");
            bool ValidateConceptCodeAD = functions.Any(f => f.CodeFunction == "ValidateConceptCodeAD");
            bool ValidateTotalValueByConceptAD = functions.Any(f => f.CodeFunction == "ValidateTotalValueByConceptAD");

            int index = 1;
            foreach (ENT_ADFile file in listAD)
            {
                file.UnitValue = file.UnitValue.Replace('.', ',');
                file.TotalValueByConcept = file.TotalValueByConcept.Replace('.', ',');

                if (ValidateInvoiceNumberAD)
                {
                    USR_ValidateInvoiceNumber(dictionaryAF, file.CodeProvider, file.InvoiceNumber, index, listErrors);
                }

                if (ValidateCodeProviderAD)
                    USR_ValidateCodeProvider(file.CodeProvider, habilitationCodes, index, listErrors);

                if (ValidateConceptCodeAD)
                    USR_ValidateConceptCodeAD(file.ConceptCode, listConceptCode, index, listErrors);

                if (ValidateTotalValueByConceptAD)
                    USR_ValidateTotalValueByConceptAD(file.TotalValueByConcept, file.Amount, file.UnitValue, index, listErrors);

                index++;
            }

            if (listErrors.Count > 0)
            {
                dictionaryErrors.Add("AD", listErrors);
            }

            return listErrors;
        }
        /// <sumary>
        /// Valida archivo AF
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listAF">listAF</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="initDate">initDate</param>
        /// <param name="endDate">endDate</param>
        /// <param name="dictionaryAF">dictionaryAF</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidateAF(List<dynamic> listEnabledFunctions, List<ENT_AFFile> listAF, List<string> habilitationCodes, DateTime initDate, DateTime endDate, dynamic dictionaryAF, dynamic dictionaryErrors)
        {
            var listErrorsResult = new List<string>();
            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "2" && f.Status == true);
            bool ValidateCodeProviderAF = functions.Any(f => f.CodeFunction == "ValidateCodeProviderAF");
            bool ValidateExpeditionInvoiceAF = functions.Any(f => f.CodeFunction == "ValidateExpeditionInvoiceAF");
            bool ValidatePeriodValidityStartDateAF = functions.Any(f => f.CodeFunction == "ValidatePeriodValidityStartDateAF");
            bool ValidatePeriodValidityEndDateAF = functions.Any(f => f.CodeFunction == "ValidatePeriodValidityEndDateAF");
            bool ValidateAdministeringEntityCodeAF = functions.Any(f => f.CodeFunction == "ValidateAdministeringEntityCodeAF");

            DateTime periodValidityStartDate = new DateTime();
            DateTime periodValidityEndDate = new DateTime();
            DateTime expeditionInvoice = new DateTime();
            int adapterId = 1;

            var sql = new StringBuilder();
            //Consulta Codigo ripscode
            sql.Append(" SELECT RipsCode ");
            sql.Append(" FROM Operator WITH(NOLOCK)");
            sql.Append(" WHERE RipsCode IS NOT NULL");

            var resultRipsCode = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());
            var listaresultRipsCode = JsonConvert.DeserializeObject<List<dynamic>>(resultRipsCode.Result.ToString());

            //Consulta code Divipola/Codigo Dane
            sql = new StringBuilder();
            sql.Append("SELECT DISTINCT Code  ");
            sql.Append("FROM TypeDetail WITH (NOLOCK)");
            sql.Append("WHERE IdTypeHead=17");

            var codeMunicipalyResul = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());
            var listacodeMunicipalyResul = JsonConvert.DeserializeObject<List<dynamic>>(codeMunicipalyResul.Result.ToString());

            List<string> listErrors = new List<string>();
            int index = 1;
            foreach (ENT_AFFile file in listAF)
            {
                // Agrego a diccionario para consultas en otros archivos
                if (!dictionaryAF.ContainsKey($"{file.CodeProvider}_{file.InvoiceNumber}"))
                    dictionaryAF.Add($"{file.CodeProvider}_{file.InvoiceNumber}", file);

                file.CoPayment = file.CoPayment.Replace('.', ',');
                file.CommissionValue = file.CommissionValue.Length > 0 ? file.CommissionValue.Replace('.', ',') : null;
                file.DiscountValue = file.DiscountValue.Length > 0 ? file.DiscountValue.Replace('.', ',') : null;
                file.PayValue = file.PayValue.Replace('.', ',');
                periodValidityStartDate = Convert.ToDateTime(file.PeriodValidityStartDate);
                periodValidityEndDate = Convert.ToDateTime(file.PeriodValidityEndDate);
                expeditionInvoice = Convert.ToDateTime(file.ExpeditionInvoice);

                if (ValidateCodeProviderAF)
                    USR_ValidateCodeProvider(file.CodeProvider, habilitationCodes, index, listErrors);

                if (ValidateExpeditionInvoiceAF)
                    USR_ValidateExpeditionInvoiceAF(expeditionInvoice, initDate, endDate, index, listErrors);

                if (ValidatePeriodValidityStartDateAF)
                    USR_ValidatePeriodValidityStartDateAF(periodValidityStartDate, periodValidityEndDate, initDate, index, listErrors);

                if (ValidatePeriodValidityEndDateAF)
                    USR_ValidatePeriodValidityEndDateAF(periodValidityStartDate, periodValidityEndDate, endDate, index, listErrors);

                if (ValidateAdministeringEntityCodeAF)
                    USR_ValidateAdministeringEntityCodeAF(file.AdministeringEntityCode, listaresultRipsCode, listacodeMunicipalyResul, index, listErrors);

                index++;
            }

            if (listErrors.Count > 0)
            {
                dictionaryErrors.Add("AF", listErrors);
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateAH
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listAH">listAH</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="dictionaryAF">dictionaryAF</param>
        /// <param name="dictionaryUS">dictionaryUS</param>
        /// <param name="dictionaryAU">dictionaryAU</param>
        /// <param name="dictionaryAC">dictionaryAC</param>
        /// <param name="dictionaryDocumentType">dictionaryDocumentType</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="dictionaryAH">dictionaryAH</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidateAH(List<dynamic> listEnabledFunctions, List<ENT_AHFile> listAH, List<string> habilitationCodes, dynamic dictionaryAF, dynamic dictionaryUS, dynamic dictionaryAU, dynamic dictionaryAC, dynamic dictionaryDocumentType, dynamic dictionaryDiagnosis, dynamic dictionaryAH, dynamic dictionaryErrors)
        {
            List<string> listErrors = new List<string>();
            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "8" && f.Status == true);
            bool ValidateInvoiceNumberAH = functions.Any(f => f.CodeFunction == "ValidateInvoiceNumberAH");
            bool ValidateCodeProviderAH = functions.Any(f => f.CodeFunction == "ValidateCodeProviderAH");
            bool ValidateIdentificationNumberAH = functions.Any(f => f.CodeFunction == "ValidateIdentificationNumberAH");
            bool ValidateDocumentTypeAH = functions.Any(f => f.CodeFunction == "ValidateDocumentTypeAH");
            bool ValidateInstitutionEntryAH = functions.Any(f => f.CodeFunction == "ValidateInstitutionEntryAH");
            bool ValidateInstitutionEntryDateAH = functions.Any(f => f.CodeFunction == "ValidateInstitutionEntryDateAH");
            bool ValidatePrincipalDiagnosisCodeAH = functions.Any(f => f.CodeFunction == "ValidatePrincipalDiagnosisCodeAH");
            bool ValidateMainDiagnosticAH = functions.Any(f => f.CodeFunction == "ValidateMainDiagnosticAH");
            bool ValidateRelationMainDiagnosisOneAH = functions.Any(f => f.CodeFunction == "ValidateRelationMainDiagnosisOneAH");
            bool ValidateRelationMainDiagnosisTwoAH = functions.Any(f => f.CodeFunction == "ValidateRelationMainDiagnosisTwoAH");
            bool ValidateRelationMainDiagnosisThreeAH = functions.Any(f => f.CodeFunction == "ValidateRelationMainDiagnosisThreeAH");
            bool ValidateComplicationDiagnosisAH = functions.Any(f => f.CodeFunction == "ValidateComplicationDiagnosisAH");
            bool ValidateStatusExitAH = functions.Any(f => f.CodeFunction == "ValidateStatusExitAH");
            bool ValidateDeathCauseDiagnosisAH = functions.Any(f => f.CodeFunction == "ValidateDeathCauseDiagnosisAH");
            bool ValidateInstitutionDateExitAH = functions.Any(f => f.CodeFunction == "ValidateInstitutionDateExitAH");

            int index = 1;
            dynamic identification;
            DateTime institutionEntryDate = new DateTime();
            DateTime institutionDateExit = new DateTime();
            int statusExit = 0;
            int institutionEntry = 0;
            foreach (ENT_AHFile file in listAH)
            {

                identification = null;
                institutionEntryDate = Convert.ToDateTime(file.InstitutionEntryDate);
                institutionDateExit = Convert.ToDateTime(file.InstitutionDateExit);
                statusExit = int.Parse(file.StatusExit);
                institutionEntry = int.Parse(file.InstitutionEntry);

                if (ValidateInvoiceNumberAH)
                {
                    USR_ValidateInvoiceNumber(dictionaryAF, file.CodeProvider, file.InvoiceNumber, index, listErrors);
                }

                if (ValidateCodeProviderAH)
                    USR_ValidateCodeProvider(file.CodeProvider, habilitationCodes, index, listErrors);


                if (dictionaryUS.ContainsKey($"{file.IdentificationType}_{file.IdentificationNumber}"))
                {
                    identification = dictionaryUS[$"{file.IdentificationType}_{file.IdentificationNumber}"];
                }
                if (ValidateIdentificationNumberAH)
                    USR_ValidateIdentificationNumber(identification, index, listErrors);

                if (ValidateDocumentTypeAH)
                    USR_ValidateDocumentType(identification, dictionaryDocumentType, index, listErrors);

                if (ValidateInstitutionEntryAH)
                {
                    USR_ValidateInstitutionEntryAH(institutionEntry, index, listErrors);
                    if (institutionEntry == 1)
                    {
                        if (!dictionaryAU.ContainsKey(file.IdentificationNumber))
                        {
                            if (!dictionaryAC.ContainsKey(file.IdentificationNumber))
                            {
                                listErrors.Add($"El numero de identificación del item {index} debe existir en el archivo AU o AC");
                            }
                        }
                    }
                    if (institutionEntry == 2)
                    {
                        if (!dictionaryAC.ContainsKey(file.IdentificationNumber))
                        {
                            listErrors.Add($"El numero de identificación del item {index} debe existir en el archivo AC");
                        }
                    }
                }

                if (ValidateInstitutionEntryDateAH)
                    USR_ValidateInstitutionEntryDateAH(institutionEntryDate, institutionDateExit, index, listErrors);

                if (ValidatePrincipalDiagnosisCodeAH)
                    USR_ValidatePrincipalDiagnosisCodeAH(file.PrincipalDiagnosisCode, dictionaryDiagnosis, index, listErrors);

                if (ValidateMainDiagnosticAH)
                    USR_ValidateMainDiagnosticAH(file.MainDiagnostic, dictionaryDiagnosis, index, listErrors);

                if (ValidateRelationMainDiagnosisOneAH)
                    USR_ValidateRelationMainDiagnosisOneAH(file.RelationMainDiagnosisOne, dictionaryDiagnosis, index, listErrors);

                if (ValidateRelationMainDiagnosisTwoAH)
                    USR_ValidateRelationMainDiagnosisTwoAH(file.RelationMainDiagnosisTwo, dictionaryDiagnosis, index, listErrors);

                if (ValidateRelationMainDiagnosisThreeAH)
                    USR_ValidateRelationMainDiagnosisThreeAH(file.RelationMainDiagnosisThree, dictionaryDiagnosis, index, listErrors);

                if (ValidateComplicationDiagnosisAH)
                    USR_ValidateComplicationDiagnosisAH(file.ComplicationDiagnosis, dictionaryDiagnosis, index, listErrors);

                if (ValidateStatusExitAH)
                    USR_ValidateStatusExitAH(statusExit, index, listErrors);

                if (ValidateDeathCauseDiagnosisAH)
                    USR_ValidateDeathCauseDiagnosisAH(statusExit, file.DeathCauseDiagnosis, dictionaryDiagnosis, index, listErrors);

                if (ValidateInstitutionDateExitAH)
                    USR_ValidateInstitutionDateExitAH(institutionEntryDate, institutionDateExit, index, listErrors);

                // Agrego a diccionario para consultas en otros archivos
                if (!dictionaryAH.ContainsKey(file.IdentificationNumber))
                    dictionaryAH.Add(file.IdentificationNumber, file);

                index++;
            }

            if (listErrors.Count > 0)
            {
                dictionaryErrors.Add("AH", listErrors);
            }

            return listErrors;
        }
        /// <sumary>
        /// Valida archivo AM
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listAM">listAM</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="dictionaryAF">dictionaryAF</param>
        /// <param name="dictionaryUS">dictionaryUS</param>
        /// <param name="dictionaryDocumentType">dictionaryDocumentType</param>
        /// <param name="dictionaryCum">dictionaryCum</param>
        /// <param name="dictionaryDetailsConcept">dictionaryDetailsConcept</param>
        /// <param name="dictionaryInvoices">dictionaryInvoices</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidateAM(List<dynamic> listEnabledFunctions, List<ENT_AMFile> listAM, List<string> habilitationCodes, dynamic dictionaryAF, dynamic dictionaryUS, dynamic dictionaryDocumentType, dynamic dictionaryCum, dynamic dictionaryDetailsConcept, dynamic dictionaryInvoices, dynamic dictionaryErrors)
        {
            List<string> listErrors = new List<string>();

            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "9" && f.Status == true);
            bool ValidateInvoiceNumberAM = functions.Any(f => f.CodeFunction == "ValidateInvoiceNumberAM");
            bool ValidateCodeProviderAM = functions.Any(f => f.CodeFunction == "ValidateCodeProviderAM");
            bool ValidateIdentificationNumberAM = functions.Any(f => f.CodeFunction == "ValidateIdentificationNumberAM");
            bool ValidateDocumentTypeAM = functions.Any(f => f.CodeFunction == "ValidateDocumentTypeAM");
            bool ValidateDrugsCodeAM = functions.Any(f => f.CodeFunction == "ValidateDrugsCodeAM");
            bool ValidateDrugsTypeAM = functions.Any(f => f.CodeFunction == "ValidateDrugsTypeAM");
            bool ValidateDrugsTotalValueAM = functions.Any(f => f.CodeFunction == "ValidateDrugsTotalValueAM");

            int index = 1;
            dynamic identification;
            foreach (ENT_AMFile file in listAM)
            {
                file.DrugsUnitValue = file.DrugsUnitValue.Replace('.', ',');
                file.DrugsTotalValue = file.DrugsTotalValue.Replace('.', ',');
                identification = null;

                if (ValidateInvoiceNumberAM)
                {
                    USR_ValidateInvoiceNumber(dictionaryAF, file.CodeProvider, file.InvoiceNumber, index, listErrors);
                }

                if (ValidateCodeProviderAM)
                    USR_ValidateCodeProvider(file.CodeProvider, habilitationCodes, index, listErrors);

                if (dictionaryUS.ContainsKey($"{file.IdentificationType}_{file.IdentificationNumber}"))
                {
                    identification = dictionaryUS[$"{file.IdentificationType}_{file.IdentificationNumber}"];
                }
                if (ValidateIdentificationNumberAM)
                    USR_ValidateIdentificationNumber(identification, index, listErrors);

                if (ValidateDocumentTypeAM)
                    USR_ValidateDocumentType(identification, dictionaryDocumentType, index, listErrors);

                if (ValidateDrugsCodeAM)
                    USR_ValidateDrugsCodeAM(dictionaryCum, file.DrugsCode, index, listErrors);

                if (ValidateDrugsTypeAM)
                    USR_ValidateDrugsTypeAM(dictionaryCum, dictionaryDetailsConcept, long.Parse(file.DrugsType), file.DrugsCode, index, listErrors);

                if (ValidateDrugsTotalValueAM)
                    USR_ValidateDrugsTotalValueAM(file.DrugsTotalValue, file.DrugsAmount, file.DrugsUnitValue, index, listErrors);

                // Agrego a diccionario para consultas en otros archivos
                if (!dictionaryInvoices.ContainsKey($"{file.CodeProvider}_{file.InvoiceNumber}"))
                    dictionaryInvoices.Add($"{file.CodeProvider}_{file.InvoiceNumber}", Convert.ToDecimal(file.DrugsTotalValue));
                else
                    dictionaryInvoices[$"{file.CodeProvider}_{file.InvoiceNumber}"] = dictionaryInvoices[$"{file.CodeProvider}_{file.InvoiceNumber}"] + Convert.ToDecimal(file.DrugsTotalValue);
                index++;

            }

            if (listErrors.Count > 0)
            {
                dictionaryErrors.Add("AM", listErrors);
            }

            return listErrors;
        }
        /// <sumary>
        /// Valida archivo AN
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listAN">listAN</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="dictionaryAF">dictionaryAF</param>
        /// <param name="dictionaryUS">dictionaryUS</param>
        /// <param name="dictionaryDocumentType">dictionaryDocumentType</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="dictionaryAH">dictionaryAH</param>
        /// <param name="listSexNum">listSexNum</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidateAN(List<dynamic> listEnabledFunctions, List<ENT_ANFile> listAN, List<string> habilitationCodes, dynamic dictionaryAF, dynamic dictionaryUS, dynamic dictionaryDocumentType, dynamic dictionaryDiagnosis, dynamic dictionaryAH, List<string> listSexNum, dynamic dictionaryErrors)
        {
            List<string> listErrors = new List<string>();

            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "10" && f.Status == true);
            bool ValidateInvoiceNumberAN = functions.Any(f => f.CodeFunction == "ValidateInvoiceNumberAN");
            bool ValidateCodeProviderAN = functions.Any(f => f.CodeFunction == "ValidateCodeProviderAN");
            bool ValidateIdentificationNumberAN = functions.Any(f => f.CodeFunction == "ValidateIdentificationNumberAN");
            bool ValidateDocumentTypeAN = functions.Any(f => f.CodeFunction == "ValidateDocumentTypeAN");
            bool ValidateNewbornDateAN = functions.Any(f => f.CodeFunction == "ValidateNewbornDateAN");
            bool ValidateGestationalAgeAN = functions.Any(f => f.CodeFunction == "ValidateGestationalAgeAN");
            bool ValidatePrenatalControlAN = functions.Any(f => f.CodeFunction == "ValidatePrenatalControlAN");
            bool ValidateNewbornSexAN = functions.Any(f => f.CodeFunction == "ValidateNewbornSexAN");
            bool ValidateNewbornDiagnosisAN = functions.Any(f => f.CodeFunction == "ValidateNewbornDiagnosisAN");
            bool ValidateDiagnosisAN = functions.Any(f => f.CodeFunction == "ValidateDiagnosisAN");
            bool ValidateDeathCauseDateAN = functions.Any(f => f.CodeFunction == "ValidateDeathCauseDateAN");

            int index = 1;
            dynamic identification;
            ENT_AHFile statusExit;
            DateTime newbornDate = new DateTime();
            DateTime? deathCauseDate;

            foreach (ENT_ANFile file in listAN)
            {
                identification = null;
                statusExit = null;
                newbornDate = Convert.ToDateTime(file.NewbornDate);

                if (ValidateInvoiceNumberAN)
                {
                    USR_ValidateInvoiceNumber(dictionaryAF, file.CodeProvider, file.InvoiceNumber, index, listErrors);
                }

                if (ValidateCodeProviderAN)
                    USR_ValidateCodeProvider(file.CodeProvider, habilitationCodes, index, listErrors);

                if (dictionaryUS.ContainsKey($"{file.IdentificationType}_{file.IdentificationNumber}"))
                {
                    identification = dictionaryUS[$"{file.IdentificationType}_{file.IdentificationNumber}"];
                }
                if (ValidateIdentificationNumberAN)
                    USR_ValidateIdentificationNumber(identification, index, listErrors);

                if (ValidateDocumentTypeAN)
                    USR_ValidateDocumentType(identification, dictionaryDocumentType, index, listErrors);

                if (ValidateNewbornDateAN)
                    USR_ValidateNewbornDateAN(newbornDate, index, listErrors);

                if (ValidateGestationalAgeAN)
                    USR_ValidateGestationalAgeAN(long.Parse(file.GestationalAge), index, listErrors);

                if (ValidatePrenatalControlAN)
                    USR_ValidatePrenatalControlAN(long.Parse(file.PrenatalControl), index, listErrors);

                if (ValidateNewbornSexAN)
                    USR_ValidateNewbornSexAN(file.NewbornSex, listSexNum, index, listErrors);

                if (ValidateNewbornDiagnosisAN)
                    USR_ValidateNewbornDiagnosisAN(file.NewbornDiagnosis, dictionaryDiagnosis, index, listErrors);

                if (dictionaryAH.ContainsKey(file.IdentificationNumber))
                    statusExit = dictionaryAH[file.IdentificationNumber];

                if (ValidateDiagnosisAN)
                    USR_ValidateDiagnosisAN(file.IdentificationNumber, file.DeathCauseDiagnosis, statusExit, dictionaryDiagnosis, index, listErrors);

                if (ValidateDeathCauseDateAN)
                {
                    deathCauseDate = null;
                    if (!string.IsNullOrEmpty(file.DeathCauseDate))
                        deathCauseDate = Convert.ToDateTime(file.DeathCauseDate);

                    USR_ValidateDeathCauseDateAN(deathCauseDate, newbornDate, statusExit, index, listErrors);
                }

                index++;
            }

            if (listErrors.Count > 0)
            {

                dictionaryErrors.Add("AN", listErrors);
            }

            return listErrors;
        }
        /// <sumary>
        /// Valida archivo AP
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listAP">listAP</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="dictionaryAF">dictionaryAF</param>
        /// <param name="dictionaryUS">dictionaryUS</param>
        /// <param name="dictionaryDocumentType">dictionaryDocumentType</param>
        /// <param name="dictionaryCups">dictionaryCups</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="dictionaryDetailsConcept">dictionaryDetailsConcept</param>
        /// <param name="dictionaryInvoices">dictionaryInvoices</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidateAP(List<dynamic> listEnabledFunctions, List<ENT_APFile> listAP, List<string> habilitationCodes, dynamic dictionaryAF, dynamic dictionaryUS, dynamic dictionaryDocumentType, dynamic dictionaryCups, dynamic dictionaryDiagnosis, dynamic dictionaryDetailsConcept, dynamic dictionaryInvoices, dynamic dictionaryErrors)
        {
            List<string> listErrors = new List<string>();
            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "6" && f.Status == true);
            bool ValidateInvoiceNumberAP = functions.Any(f => f.CodeFunction == "ValidateInvoiceNumberAP");
            bool ValidateCodeProviderAP = functions.Any(f => f.CodeFunction == "ValidateCodeProviderAP");
            bool ValidateIdentificationNumberAP = functions.Any(f => f.CodeFunction == "ValidateIdentificationNumberAP");
            bool ValidateDocumentTypeAP = functions.Any(f => f.CodeFunction == "ValidateDocumentTypeAP");
            bool ValidateProcedureDateAP = functions.Any(f => f.CodeFunction == "ValidateProcedureDateAP");
            bool ValidateCupAP = functions.Any(f => f.CodeFunction == "ValidateCupAP");
            bool ValidateProcedureScopeAP = functions.Any(f => f.CodeFunction == "ValidateProcedureScopeAP");
            bool ValidateProcedurePurposeAP = functions.Any(f => f.CodeFunction == "ValidateProcedurePurposeAP");
            bool ValidateAttendPersonalAP = functions.Any(f => f.CodeFunction == "ValidateAttendPersonalAP");
            bool ValidatePrincipalDiagnosticCodeAP = functions.Any(f => f.CodeFunction == "ValidatePrincipalDiagnosticCodeAP");
            bool ValidateRelationDiagnosisCodeOneAP = functions.Any(f => f.CodeFunction == "ValidateRelationDiagnosisCodeOneAP");
            bool ValidateComplicationAP = functions.Any(f => f.CodeFunction == "ValidateComplicationAP");
            bool ValidateActSurgicalAP = functions.Any(f => f.CodeFunction == "ValidateActSurgicalAP");

            int index = 1;
            dynamic identification;
            foreach (ENT_APFile file in listAP)
            {
                file.PayProcedure = file.PayProcedure.Replace('.', ',');
                identification = null; ;
                if (ValidateInvoiceNumberAP)
                {
                    USR_ValidateInvoiceNumber(dictionaryAF, file.CodeProvider, file.InvoiceNumber, index, listErrors);
                }

                if (ValidateCodeProviderAP)
                    USR_ValidateCodeProvider(file.CodeProvider, habilitationCodes, index, listErrors);


                if (dictionaryUS.ContainsKey($"{file.IdentificationType}_{file.IdentificationNumber}"))
                {
                    identification = dictionaryUS[$"{file.IdentificationType}_{file.IdentificationNumber}"];
                }
                if (ValidateIdentificationNumberAP)
                    USR_ValidateIdentificationNumber(identification, index, listErrors);

                if (ValidateDocumentTypeAP)
                    USR_ValidateDocumentType(identification, dictionaryDocumentType, index, listErrors);

                if (ValidateProcedureDateAP)
                    USR_ValidateProcedureDateAP(Convert.ToDateTime(file.ProcedureDate), index, listErrors);

                if (ValidateProcedureScopeAP)
                    USR_ValidateProcedureScopeAP(int.Parse(file.ProcedureScope), index, listErrors);

                dynamic cups = null;
                if (dictionaryCups.ContainsKey(file.ProcedureCode))
                    cups = dictionaryCups[file.ProcedureCode];

                if (ValidateCupAP)
                {
                    USR_ValidateCupAP(cups, index, listErrors);
                }
                // ProcedureType == 10
                if (cups != null)
                {
                    dynamic procedureType = null;
                    if (dictionaryDetailsConcept.ContainsKey(cups.IdProcedureType.ToString()))
                    {
                        procedureType = dictionaryDetailsConcept[cups.IdProcedureType.ToString()];
                    }

                    //se validan los diagnosticos solo si el tipo de procedimiento es quirurgico
                    if (procedureType != null && procedureType.Code == "10")
                    {
                        if (ValidateAttendPersonalAP)
                        {
                            int? attendPersonal = null;
                            if (!string.IsNullOrEmpty(file.AttendPersonal))
                            {
                                attendPersonal = int.Parse(file.AttendPersonal);
                            }
                            USR_ValidateAttendPersonalAP(attendPersonal, file.PrincipalDiagnosticCode, index, listErrors);
                        }

                        if (ValidatePrincipalDiagnosticCodeAP)
                            USR_ValidatePrincipalDiagnosticCodeAP(file.PrincipalDiagnosticCode, dictionaryDiagnosis, index, listErrors);

                        if (ValidateRelationDiagnosisCodeOneAP)
                            USR_ValidateRelationDiagnosisCodeOneAP(file.RelationDiagnosisCodeOne, dictionaryDiagnosis, index, listErrors);

                        if (ValidateComplicationAP)
                            USR_ValidateComplicationAP(file.Complication, dictionaryDiagnosis, index, listErrors);

                        if (ValidateActSurgicalAP)
                        {
                            long? actSurgical = null;
                            if (!string.IsNullOrEmpty(file.ActSurgical))
                            {
                                actSurgical = long.Parse(file.ActSurgical);
                            }
                            USR_ValidateActSurgicalAP(actSurgical, index, listErrors);
                        }
                    }

                    if (ValidateProcedurePurposeAP)
                        USR_ValidateProcedurePurposeAP(int.Parse(file.ProcedurePurpose), index, listErrors);
                }

                // Agrego a diccionario para consultas en otros archivos
                if (!dictionaryInvoices.ContainsKey($"{file.CodeProvider}_{file.InvoiceNumber}"))
                    dictionaryInvoices.Add($"{file.CodeProvider}_{file.InvoiceNumber}", Convert.ToDecimal(file.PayProcedure));
                else
                    dictionaryInvoices[$"{file.CodeProvider}_{file.InvoiceNumber}"] = dictionaryInvoices[$"{file.CodeProvider}_{file.InvoiceNumber}"] + Convert.ToDecimal(file.PayProcedure);
                index++;
            }

            if (listErrors.Count > 0)
            {
                dictionaryErrors.Add("AP", listErrors);
            }

            return listErrors;
        }
        /// <sumary>
        /// Valida archivo AT
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listAT">listAT</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="dictionaryAF">dictionaryAF</param>
        /// <param name="dictionaryUS">dictionaryUS</param>
        /// <param name="dictionaryDocumentType">dictionaryDocumentType</param>
        /// <param name="dictionaryCups">dictionaryCups</param>
        /// <param name="dictionaryDetailsConcept">dictionaryDetailsConcept</param>
        /// <param name="dictionaryInvoices">dictionaryInvoices</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidateAT(List<dynamic> listEnabledFunctions, List<ENT_ATFile> listAT, List<string> habilitationCodes, dynamic dictionaryAF, dynamic dictionaryUS, dynamic dictionaryDocumentType, dynamic dictionaryCups, dynamic dictionaryDetailsConcept, dynamic dictionaryInvoices, dynamic dictionaryErrors)
        {
            List<string> listErrors = new List<string>();
            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "11" && f.Status == true);
            bool ValidateInvoiceNumberAT = functions.Any(f => f.CodeFunction == "ValidateInvoiceNumberAT");
            bool ValidateCodeProviderAT = functions.Any(f => f.CodeFunction == "ValidateCodeProviderAT");
            bool ValidateIdentificationNumberAT = functions.Any(f => f.CodeFunction == "ValidateIdentificationNumberAT");
            bool ValidateDocumentTypeAT = functions.Any(f => f.CodeFunction == "ValidateDocumentTypeAT");
            bool ValidateServiceTypeAT = functions.Any(f => f.CodeFunction == "ValidateServiceTypeAT");
            bool ValidateCupAT = functions.Any(f => f.CodeFunction == "ValidateCupAT");
            bool ValidateServiceTotalValueAT = functions.Any(f => f.CodeFunction == "ValidateServiceTotalValueAT");

            int index = 1;
            dynamic identification;
            long? serviceType;

            foreach (ENT_ATFile file in listAT)
            {
                file.ServiceUnitValue = file.ServiceUnitValue.Replace('.', ',');
                file.ServiceTotalValue = file.ServiceTotalValue.Replace('.', ',');
                identification = null;
                serviceType = null;

                if (ValidateInvoiceNumberAT)
                {
                    USR_ValidateInvoiceNumber(dictionaryAF, file.CodeProvider, file.InvoiceNumber, index, listErrors);
                }

                if (ValidateCodeProviderAT)
                    USR_ValidateCodeProvider(file.CodeProvider, habilitationCodes, index, listErrors);

                if (dictionaryUS.ContainsKey($"{file.IdentificationType}_{file.IdentificationNumber}"))
                {
                    identification = dictionaryUS[$"{file.IdentificationType}_{file.IdentificationNumber}"];
                }
                if (ValidateIdentificationNumberAT)
                    USR_ValidateIdentificationNumber(identification, index, listErrors);

                if (ValidateDocumentTypeAT)
                    USR_ValidateDocumentType(identification, dictionaryDocumentType, index, listErrors);

                if (!string.IsNullOrEmpty(file.ServiceType))
                {
                    serviceType = long.Parse(file.ServiceType);
                }

                if (ValidateServiceTypeAT)
                {
                    USR_ValidateServiceTypeAT(serviceType, index, listErrors);
                }

                if (ValidateCupAT)
                {
                    USR_ValidateCupAT(serviceType, file.ServiceCode, dictionaryCups, dictionaryDetailsConcept, index, listErrors);
                }

                if (ValidateServiceTotalValueAT)
                    USR_ValidateServiceTotalValueAT(file.ServiceTotalValue, file.ServiceAmount, file.ServiceUnitValue, index, listErrors);

                // Agrego a diccionario para consultas en otros archivos
                if (!dictionaryInvoices.ContainsKey($"{file.CodeProvider}_{file.InvoiceNumber}"))
                    dictionaryInvoices.Add($"{file.CodeProvider}_{file.InvoiceNumber}", Convert.ToDecimal(file.ServiceTotalValue));
                else
                    dictionaryInvoices[$"{file.CodeProvider}_{file.InvoiceNumber}"] = dictionaryInvoices[$"{file.CodeProvider}_{file.InvoiceNumber}"] + Convert.ToDecimal(file.ServiceTotalValue);

                index++;
            }

            if (listErrors.Count > 0)
            {
                dictionaryErrors.Add("AT", listErrors);
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateAU
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listAU">listAU</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="dictionaryAF">dictionaryAF</param>
        /// <param name="dictionaryUS">dictionaryUS</param>
        /// <param name="dictionaryDocumentType">dictionaryDocumentType</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="dictionaryAU">dictionaryAU</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidateAU(List<dynamic> listEnabledFunctions, List<ENT_AUFile> listAU, List<string> habilitationCodes, dynamic dictionaryAF, dynamic dictionaryUS, dynamic dictionaryDocumentType, dynamic dictionaryDiagnosis, dynamic dictionaryAU, dynamic dictionaryErrors)
        {
            var listErrors = new List<string>();

            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "7" && f.Status == true);

            bool ValidateInvoiceNumberAU = functions.Any(f => f.CodeFunction == "ValidateInvoiceNumberAU");
            bool ValidateCodeProviderAU = functions.Any(f => f.CodeFunction == "ValidateCodeProviderAU");
            bool ValidateIdentificationNumberAU = functions.Any(f => f.CodeFunction == "ValidateIdentificationNumberAU");
            bool ValidateDocumentTypeAU = functions.Any(f => f.CodeFunction == "ValidateDocumentTypeAU");
            bool ValidateExternalCauseAU = functions.Any(f => f.CodeFunction == "ValidateExternalCauseAU");
            bool ValidateObservationDateAU = functions.Any(f => f.CodeFunction == "ValidateObservationDateAU");
            bool ValidateDiagnosisEndAU = functions.Any(f => f.CodeFunction == "ValidateDiagnosisEndAU");
            bool ValidateRelationDiagnosisExitOneAU = functions.Any(f => f.CodeFunction == "ValidateRelationDiagnosisExitOneAU");
            bool ValidateRelationDiagnosisExitTwoAU = functions.Any(f => f.CodeFunction == "ValidateRelationDiagnosisExitTwoAU");
            bool ValidateRelationDiagnosisExitThreeAU = functions.Any(f => f.CodeFunction == "ValidateRelationDiagnosisExitThreeAU");
            bool ValidateObservationDestinationAU = functions.Any(f => f.CodeFunction == "ValidateObservationDestinationAU");
            bool ValidateStatusExitAU = functions.Any(f => f.CodeFunction == "ValidateStatusExitAU");
            bool ValidateDeathCauseAU = functions.Any(f => f.CodeFunction == "ValidateDeathCauseAU");
            bool ValidateObservationDateExitAU = functions.Any(f => f.CodeFunction == "ValidateObservationDateExitAU");

            int index = 1;
            dynamic identification;
            DateTime observationDate = new DateTime();
            DateTime observationDateExit = new DateTime();
            int statusExit = 0;

            foreach (ENT_AUFile file in listAU)
            {
                identification = null;
                observationDate = Convert.ToDateTime(file.ObservationDate);
                observationDateExit = Convert.ToDateTime(file.ObservationDateExit);
                statusExit = int.Parse(file.StatusExit);

                if (ValidateInvoiceNumberAU)
                {
                    USR_ValidateInvoiceNumber(dictionaryAF, file.CodeProvider, file.InvoiceNumber, index, listErrors);
                }

                if (ValidateCodeProviderAU)
                    USR_ValidateCodeProvider(file.CodeProvider, habilitationCodes, index, listErrors);


                if (dictionaryUS.ContainsKey($"{file.IdentificationType}_{file.IdentificationNumber}"))
                {
                    identification = dictionaryUS[$"{file.IdentificationType}_{file.IdentificationNumber}"];
                }
                if (ValidateIdentificationNumberAU)
                    USR_ValidateIdentificationNumber(identification, index, listErrors);

                if (ValidateDocumentTypeAU)
                    USR_ValidateDocumentType(identification, dictionaryDocumentType, index, listErrors);

                if (ValidateExternalCauseAU)
                {
                    long? externalCause = null;
                    if (!string.IsNullOrEmpty(file.ExternalCause))
                    {
                        externalCause = long.Parse(file.ExternalCause);
                    }
                    USR_ValidateExternalCauseAU(externalCause, index, listErrors);
                }

                if (ValidateObservationDateAU)
                    USR_ValidateObservationDateAU(observationDate, observationDateExit, index, listErrors);

                if (ValidateDiagnosisEndAU)
                    USR_ValidateDiagnosisEndAU(file.DiagnosisEnd, dictionaryDiagnosis, index, listErrors);

                if (ValidateRelationDiagnosisExitOneAU)
                    USR_ValidateRelationDiagnosisExitOneAU(file.RelationDiagnosisExitOne, dictionaryDiagnosis, index, listErrors);

                if (ValidateRelationDiagnosisExitTwoAU)
                    USR_ValidateRelationDiagnosisExitTwoAU(file.RelationDiagnosisExitTwo, dictionaryDiagnosis, index, listErrors);

                if (ValidateRelationDiagnosisExitThreeAU)
                    USR_ValidateRelationDiagnosisExitThreeAU(file.RelationDiagnosisExitThree, dictionaryDiagnosis, index, listErrors);

                if (ValidateObservationDestinationAU)
                    USR_ValidateObservationDestinationAU(int.Parse(file.ObservationDestination), index, listErrors);

                if (ValidateStatusExitAU)
                    USR_ValidateStatusExitAU(statusExit, index, listErrors);

                if (ValidateDeathCauseAU)
                    USR_ValidateDeathCauseAU(statusExit, file.DeathCause, file.DiagnosisEnd, dictionaryDiagnosis, index, listErrors);

                if (ValidateObservationDateExitAU)
                    USR_ValidateObservationDateExitAU(observationDateExit, index, listErrors);

                // Agrego a diccionario para consultas en otros archivos
                if (!dictionaryAU.ContainsKey(file.IdentificationNumber))
                    dictionaryAU.Add(file.IdentificationNumber, file);

                index++;
            }

            if (listErrors.Count > 0)
            {
                dictionaryErrors.Add("AU", listErrors);
            }

            return listErrors;
        }
        /// <sumary>
        /// Validar Archivo CT
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listEntidadesCT">listEntidadesCT</param>
        /// <param name="filesInfo">filesInfo</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="adapterId">adapterId</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateCT(List<dynamic> listEnabledFunctions, List<ENT_CTFile> listEntidadesCT, List<dynamic> filesInfo, dynamic habilitationCodes, long adapterId, List<string> listErrors)
        {

            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "1" && f.Status == true);

            // Validar fechas de entrada
            foreach (var file in listEntidadesCT.Select((v, i) => new { Value = v, Index = i }))
            {
                if (Convert.ToDateTime(file.Value.ReceivedDate) > DateTime.Now)
                    listErrors.Add($"La fecha de recibido en el item {file.Index + 1} es mayor a la fecha actual");
            }

            if (functions.Any(f => f.CodeFunction == "ValidateTotalRecordsCT"))
            {
                // Valida que coincidan el total de registros con el total en CT
                foreach (dynamic info in filesInfo.Where(file => listEntidadesCT.Any(ent => ent.FileCode.Trim().ToUpper().StartsWith(file.Code))).ToList())
                {
                    // Total de registros informados en el CT
                    int totalRecord = int.Parse(listEntidadesCT.FirstOrDefault(x => x.FileCode.Trim().ToUpper().StartsWith(info.Code)).TotalRecord);

                    if (info.List.Count != totalRecord)
                        listErrors.Add($"En el archivo {info.Code} no coinciden el total de registros con el total en CT");
                }
            }
            if (functions.Any(f => f.CodeFunction == "ValidateHabilitationCodes"))
            {
                // Validamos los códigos de habilitación en la base de datos
                ENT_ActionResult qualificationCodes = USR_GetValidQualificationCodes(adapterId, habilitationCodes);
                if (qualificationCodes.IsError)
                {
                    throw new Exception(qualificationCodes.ErrorMessage);
                }
                List<dynamic> listQualificationCodesDB = JsonConvert.DeserializeObject<List<dynamic>>(qualificationCodes.Result.ToString());

                foreach (string[] codes in habilitationCodes)
                {
                    foreach (string code in codes)
                    {
                        if (!listQualificationCodesDB.Any(f => f.QualificationCode == code))
                            listErrors.Add($"El código de habilitación {code} no existe en la base de datos");
                    }
                }

            }

            return listErrors;
        }
        /// <sumary>
        /// Valida la cantidad de columnas y la estructura del  archivo RIPS
        /// </sumary> 
        /// <param name="lineSeparator">Separador de línea</param>
        /// <param name="columnSeparator">Separador de columna</param>
        /// <param name="codeFile">codeFile</param>
        /// <param name="company">Compañia</param>
        /// <param name="libraryId">Id Librería</param>
        /// <param name="fileId">Id del archivo RIPS</param>
        /// <param name="columnLength">Cantidad de columnas</param>
        /// <param name="entity">Tipo de la entidad</param>
        /// <param name="listErrors">Listado de errores</param>
        public static dynamic USR_ValidateFile(string lineSeparator, string columnSeparator, string codeFile, long company, long libraryId, string fileId, long columnLength, dynamic entity, List<string> listErrors)
        {
            try
            {
                Type typeEntity = (Type)entity;
                Type genericListType = typeof(List<>).MakeGenericType(typeEntity);
                IList lstEntities = (IList)Activator.CreateInstance(genericListType);
                if (!string.IsNullOrEmpty(fileId))
                {
                    // Obtenemos los datos del archivo
                    var result = USR_WSGetFile(company, libraryId, fileId);
                    if (!result.IsError && result.IsSuccessful)
                    {
                        var data = JsonConvert.DeserializeObject<dynamic>(result.Result.ToString());
                        byte[] fileBody = data.FileBody;
                        if (fileBody != null && fileBody.Length > 0)
                        {
                            using (Stream stream = new MemoryStream(fileBody))
                            {
                                using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                                {
                                    // Validamos estructura 
                                    var text = streamReader.ReadToEnd();
                                    string[] lines = text.Split(new string[] { lineSeparator }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] columns;
                                    for (var i = 0; i < lines.Length; i++)
                                    {
                                        columns = lines[i].Split(columnSeparator[0]);
                                        if (columns.Length != columnLength)
                                        {
                                            listErrors.Add($"La estructura del archivo no corresponde con la longitud, item {i + 1}");
                                        }
                                    }
                                    if (listErrors.Count == 0)
                                        // Convertimos archivo a lista de entidades
                                        lstEntities = SYS_FileToEntities(text, lineSeparator, columnSeparator, entity);
                                }
                            }
                            //Asignamos el item de error a los mensajes de validacion de los atributos
                            int index = 0;
                            PropertyInfo[] properties = typeEntity.GetProperties();

                            foreach (dynamic ent in lstEntities)
                            {
                                if (ent.ValidationErrorsList.Count > 0)
                                {
                                    string mensajeItem = $" Línea {index + 1}";
                                    foreach (string msg in ent.ValidationErrorsList)
                                    {
                                        var p = properties.Select((Value, Index) => new { Value, Index })
                                                 .Single(pro => Regex.IsMatch(msg, string.Format(@"\b{0}\b", Regex.Escape(pro.Value.Name))));
                                        string mensajeColumn = $" Columna {p.Index + 1}";
                                        string msgError = ((RegexAttribute)p.Value.GetCustomAttribute(typeof(RegexAttribute)))?.Message;
                                        msgError = string.Concat(msgError, ",", mensajeItem, ",", mensajeColumn);
                                        listErrors.Add(msgError);
                                    }
                                }
                                ++index;
                            }
                        }
                        else
                        {
                            listErrors.Add($"La estructura del archivo {codeFile} no corresponde a un formato válido de RIPS");
                        }
                    }
                    else
                    {
                        if (codeFile == "CT")
                            throw new Exception("No se encontró el archivo CT");

                        listErrors.Add($"No se encontró el archivo {codeFile} o su estructura no corresponde al formato RIPS");
                    }
                }
                else
                {
                    listErrors.Add($"No se envió el id correspondiente al archivo {codeFile} relacionado en el CT");
                }
                return lstEntities;
            }
            catch
            {
                throw;
            }
        }
        /// <sumary>
        /// ValidatePayValueAF
        /// </sumary> 
        /// <param name="dictionaryInvoices">dictionaryInvoices</param>
        /// <param name="listAF">listAF</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidatePayValueAF(dynamic dictionaryInvoices, List<ENT_AFFile> listAF, dynamic dictionaryErrors)
        {
            var listErrors = new List<string>();
            int index = 0;
            decimal totalInvoices = 0;
            foreach (ENT_AFFile file in listAF)
            {
                index = index + 1;
                totalInvoices = 0;

                if (dictionaryInvoices.ContainsKey($"{file.CodeProvider}_{file.InvoiceNumber}"))
                    totalInvoices = dictionaryInvoices[$"{file.CodeProvider}_{file.InvoiceNumber}"];

                if (decimal.Parse(file.PayValue) != totalInvoices)
                    listErrors.Add(string.Format("El valor total de la factura reportado en el archivo AF es invalido para el item {0}", index));
            }
            if (listErrors.Count > 0)
            {
                if (!dictionaryErrors.ContainsKey("AF"))
                    dictionaryErrors.Add("AF", listErrors);
                else
                    dictionaryErrors["AF"].AddRange(listErrors);
            }
            return listErrors;
        }
        /// <sumary>
        /// Valida archivo US
        /// </sumary> 
        /// <param name="listEnabledFunctions">listEnabledFunctions</param>
        /// <param name="listUS">listUS</param>
        /// <param name="dictionaryDocumentType">dictionaryDocumentType</param>
        /// <param name="listDocumentTypesDB">listDocumentTypesDB</param>
        /// <param name="dictionaryTypeDetails">dictionaryTypeDetails</param>
        /// <param name="listSex">listSex</param>
        /// <param name="listZone">listZone</param>
        /// <param name="adapterId">adapterId</param>
        /// <param name="dictionaryUS">dictionaryUS</param>
        /// <param name="dictionaryErrors">dictionaryErrors</param>
        public static List<string> USR_ValidateUS(List<dynamic> listEnabledFunctions, List<ENT_USFile> listUS, dynamic dictionaryDocumentType, List<dynamic> listDocumentTypesDB, dynamic dictionaryTypeDetails, List<string> listSex, List<string> listZone, long adapterId, dynamic dictionaryUS, dynamic dictionaryErrors)
        {
            List<string> listErrors = new List<string>();
            List<dynamic> personList = new List<dynamic>();

            // Obtiene solo funciones pertenecientes al archivo y que esten activas
            var functions = listEnabledFunctions.Where(f => f.IdFile == "3" && f.Status == true);
            bool ValidateDocumentTypeUS = functions.Any(f => f.CodeFunction == "ValidateDocumentTypeUS");
            bool ValidateIdentificationNumberUS = functions.Any(f => f.CodeFunction == "ValidateIdentificationNumberUS");
            bool ValidateAgeUnitOfMeasurementUS = functions.Any(f => f.CodeFunction == "ValidateAgeUnitOfMeasurementUS");
            bool ValidateSexPersonUS = functions.Any(f => f.CodeFunction == "ValidateSexPersonUS");
            bool ValidateCodeTownUS = functions.Any(f => f.CodeFunction == "ValidateCodeTownUS");
            bool ValidateCodeCityUS = functions.Any(f => f.CodeFunction == "ValidateCodeCityUS");
            bool ValidateZoneTownUS = functions.Any(f => f.CodeFunction == "ValidateZoneTownUS");
            bool ValidateAffiliatePerson = functions.Any(f => f.CodeFunction == "ValidateAffiliatePerson");
            bool ValidateAdministeringEntityCodeUS = functions.Any(f => f.CodeFunction == "ValidateAdministeringEntityCodeUS");

            int index = 1;
            var sql = new StringBuilder();
            //Consulta Codigo ripscode
            sql.Append(" SELECT RipsCode ");
            sql.Append(" FROM Operator WITH(NOLOCK)");
            sql.Append(" WHERE RipsCode IS NOT NULL");

            var resultRipsCode = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());
            var listaresultRipsCode = JsonConvert.DeserializeObject<List<dynamic>>(resultRipsCode.Result.ToString());

            //Consulta code Divipola/Codigo Dane
            sql = new StringBuilder();
            sql.Append("SELECT DISTINCT Code  ");
            sql.Append("FROM TypeDetail WITH (NOLOCK)");
            sql.Append("WHERE IdTypeHead=17");

            var codeMunicipalyResul = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());
            var listacodeMunicipalyResul = JsonConvert.DeserializeObject<List<dynamic>>(codeMunicipalyResul.Result.ToString());

            foreach (ENT_USFile file in listUS)
            {

                // Agrego a diccionario para consultas en otros archivos
                if (!dictionaryUS.ContainsKey($"{file.IdentificationType}_{file.IdentificationNumber}"))
                    dictionaryUS.Add($"{file.IdentificationType}_{file.IdentificationNumber}", file);

                if (ValidateDocumentTypeUS)
                    USR_ValidateDocumentType(file, dictionaryDocumentType, index, listErrors);

                if (ValidateIdentificationNumberUS)
                {
                    if (dictionaryDocumentType.ContainsKey(file.IdentificationType))
                    {
                        var documentTypeDictionary = dictionaryDocumentType[file.IdentificationType];

                        var documentTypeBD = listDocumentTypesDB.Where(d => d.Id == documentTypeDictionary).FirstOrDefault();
                        // Agrega personas para consultarlas posteriormente
                        personList.Add(new { DocumentType = documentTypeBD.Id.ToString(), Identification = file.IdentificationNumber, Index = index });
                    }
                }

                if (ValidateAgeUnitOfMeasurementUS)
                    USR_ValidateAgeUnitOfMeasurementUS(int.Parse(file.AgeUnitOfMeasurement), int.Parse(file.AgePerson), index, listErrors);

                if (ValidateSexPersonUS)
                    USR_ValidateSexPersonUS(listSex, file.SexPerson, index, listErrors);

                if (ValidateCodeTownUS)
                    USR_ValidateCodeTownUS(dictionaryTypeDetails, file.CodeTown, index, listErrors);

                if (ValidateCodeCityUS)
                    USR_ValidateCodeCityUS(dictionaryTypeDetails, file.CodeTown, file.CodeCity, index, listErrors);

                if (ValidateZoneTownUS)
                    USR_ValidateZoneTownUS(file.ZoneTown, listZone, index, listErrors);

                if (ValidateAdministeringEntityCodeUS)
                    USR_ValidateAdministeringEntityCodeUS(file.AdministeringEntityCode, listaresultRipsCode, listacodeMunicipalyResul, index, listErrors);

                index++;
            }

            if (ValidateAffiliatePerson)
                USR_ValidateAffiliatePerson(personList, adapterId, listErrors);

            if (listErrors.Count > 0)
            {
                dictionaryErrors.Add("US", listErrors);
            }

            return listErrors;
        }
        /// <sumary>
        /// Adjunta el archivo al proceso
        /// </sumary> 
        /// <param name="fileFullPath">fileFullPath</param>
        /// <param name="pCodUsu">Código de usuario</param>
        /// <param name="pCodEmpr">Id de empresa</param>
        /// <param name="pCodCas">Número de caso</param>
        /// <param name="pCodSeg">Código de seguridad</param>
        public static ENT_ActionResult USR_WSAttachFileToProcess(string fileFullPath, string pCodUsu, string pCodEmpr, string pCodCas, string pCodSeg)
        {
            try
            {
                if (string.IsNullOrEmpty(pCodUsu) || string.IsNullOrEmpty(pCodEmpr) || string.IsNullOrEmpty(pCodCas) || string.IsNullOrEmpty(pCodSeg))
                {
                    throw new ArgumentNullException("pCodUsu,pCodEmpr,pCodCas,pCodSeg");
                }

                // Sube al ftp
                string remotePath = $"WORKFLOW/{pCodEmpr}/{DateTime.Now.Year}/WF_CDOCU";
                var fileName = $"{pCodEmpr}_{pCodCas}_1_{Path.GetFileName(fileFullPath)}";
                remotePath += $"/{fileName}";
                using (FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    var r = USR_FtpUpload(remotePath, fs);
                    fs.Close();
                }

                return new ENT_ActionResult() { IsSuccessful = true, FileName = Path.GetFileName(Convert.ToString(fileFullPath)) };
            }
            catch (Exception ex)
            {
                return new ENT_ActionResult() { IsError = true, ErrorMessage = ex.Message + ex.StackTrace };
            }
        }
        /// <sumary>
        /// Obtiene un archivo del repositorio de archivos según el tipo
        /// </sumary> 
        /// <param name="company">Empresa</param>
        /// <param name="libraryId">Id de librería</param>
        /// <param name="fileId">Id del archivo</param>
        public static ENT_ActionResult USR_WSGetFile(long company, long libraryId, string fileId)
        {
            try
            {
                if (string.IsNullOrEmpty(fileId))
                {
                    throw new ArgumentNullException("fileId");
                }

                ENT_ActionResult result = new ENT_ActionResult();
                string url = "http://190.217.17.108:8080/api/api/Upload/GetDocument";
                url = url + "?company=" + company + "&libraryId=" + libraryId + "&fileId=" + fileId;
                result = SYS_WSGET(url, null);
                return result;
            }
            catch (Exception ex)
            {
                return new ENT_ActionResult() { IsError = true, ErrorMessage = ex.Message + ex.StackTrace };
            }
        }
        /// <sumary>
        /// Ejecuta una consulta Sql con el adaptador
        /// </sumary> 
        /// <param name="adapterId">Id del adaptador</param>
        /// <param name="queryBD">Consulta Sql</param>
        public static ENT_ActionResult SYS_WSExecuteQuery(long adapterId, string queryBD)
        {
            try
            {
                if (string.IsNullOrEmpty(queryBD))
                {
                    throw new ArgumentNullException("queryBD");
                }

                var plainTextBytes = Encoding.UTF8.GetBytes(queryBD);
                var query = Convert.ToBase64String(plainTextBytes);

                ENT_ActionResult result = new ENT_ActionResult();
                string url = "http://190.217.17.108:8080/api/api/Adapter/ExecuteQuery";
                dynamic jsonObject = new JObject();
                jsonObject.adapterId = adapterId;
                jsonObject.queryBD = query;
                result = SYS_WSPOST(url, jsonObject, null, null);
                return result;
            }
            catch (Exception ex)
            {
                return new ENT_ActionResult() { IsError = true, ErrorMessage = ex.Message + ex.StackTrace };
            }
        }
        /// <sumary>
        /// Función estándar para el consumo de un servicio web POST
        /// </sumary> 
        /// <param name="url">Url del Servicio Web</param>
        /// <param name="parameters">Parámetros del servicio</param>
        /// <param name="headers">Cabecera del servicio</param>
        /// <param name="fileFullPath">fileFullPath</param>
        public static ENT_ActionResult SYS_WSPOST(string url, object parameters, dynamic headers, string fileFullPath)
        {
            ENT_ActionResult result = new ENT_ActionResult();
            result = SYS_WSRequest("POST", url, parameters, headers, fileFullPath, null);
            return result;
        }
        /// <sumary>
        /// Función estándar para el consumo de un servicio web
        /// </sumary> 
        /// <param name="method">Método del servicio</param>
        /// <param name="url">Url del servicio web</param>
        /// <param name="parameters">Parámetros del método a consumir</param>
        /// <param name="headers">Cabecera del servicio</param>
        /// <param name="fileFullPath">fileFullPath</param>
        /// <param name="minTimeout">Minutos de timeout</param>
        public static ENT_ActionResult SYS_WSRequest(string method, string url, object parameters, dynamic headers, string fileFullPath, double? minTimeout)
        {
            try
            {
                if (string.IsNullOrEmpty(method) || string.IsNullOrEmpty(url))
                {
                    throw new ArgumentNullException("method or url");
                }

                HttpMethod httpMethod = new HttpMethod(method);
                double min = minTimeout != null && minTimeout > 0 ? (double)minTimeout : 3;
                using (HttpClient Client = new HttpClient { Timeout = TimeSpan.FromMinutes(min) })
                {
                    using (var request = new HttpRequestMessage(httpMethod, url))
                    {
                        if (parameters != null)
                        {
                            request.Content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
                        }

                        if (headers != null)
                        {
                            foreach (KeyValuePair<String, String> header in headers)
                            {
                                request.Headers.Add(header.Key, header.Value);
                            }
                        }

                        using (HttpResponseMessage response = Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result)
                        {
                            using (HttpContent content = response.Content)
                            {
                                string result = content.ReadAsStringAsync().Result;

                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    var resultWs = JsonConvert.DeserializeObject<dynamic>(result);
                                    if ((bool)resultWs.IsError)
                                    {
                                        return new ENT_ActionResult() { IsError = true, ErrorMessage = resultWs.ErrorMessage };
                                    }
                                    return new ENT_ActionResult() { IsSuccessful = resultWs.IsSucessfull, Result = resultWs.Result };

                                }

                                else
                                    return new ENT_ActionResult() { IsError = true, ErrorMessage = result };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ENT_ActionResult() { IsError = true, ErrorMessage = ex.InnerException.Message + ex.StackTrace };
            }
        }
        /// <sumary>
        /// ValidateCodeProvider
        /// </sumary> 
        /// <param name="codeProvider">codeProvider</param>
        /// <param name="habilitationCodes">habilitationCodes</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateCodeProvider(string codeProvider, List<string> habilitationCodes, long index, List<string> listErrors)
        {
            if (!habilitationCodes.Contains(codeProvider))
                listErrors.Add($"El prestador de servicio de salud del item {index} no existe en el archivo CT");
            return listErrors;
        }
        /// <sumary>
        /// ValidateConsultationCodeAC
        /// </sumary> 
        /// <param name="consultationCode">consultationCode</param>
        /// <param name="consultationPurpose">consultationPurpose</param>
        /// <param name="dictionaryCups">dictionaryCups</param>
        /// <param name="dictionaryDetailsConcept">dictionaryDetailsConcept</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateConsultationCodeAC(string consultationCode, long consultationPurpose, dynamic dictionaryCups, dynamic dictionaryDetailsConcept, long index, List<string> listErrors)
        {
            if (!dictionaryCups.ContainsKey(consultationCode))
            {
                listErrors.Add($"El CUPS del item {index} no existe en la base de datos");
            }
            else
            {
                var cups = dictionaryCups[consultationCode];
                dynamic serviceType = null;
                if (dictionaryDetailsConcept.ContainsKey(cups.IdProcedureType.ToString()))
                {
                    serviceType = dictionaryDetailsConcept[cups.IdProcedureType.ToString()];
                }

                //se valiada que el cup sea de tipo consulta
                if (serviceType != null && serviceType.Code != null && serviceType.Code.ToString() != "1")
                {
                    listErrors.Add($"El codigo CUPS del item {index} no es de tipo consulta");
                }
                //se valida que el proposito sea igual al que viene en el registro
                if (consultationPurpose < 1 || consultationPurpose > 10)
                {
                    listErrors.Add($"La finalidad del servicio del item {index} es diferente a la del CUPS");
                }
            }


            return listErrors;
        }
        /// <sumary>
        /// ValidateConsultationDateAC
        /// </sumary> 
        /// <param name="consultationDate">consultationDate</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateConsultationDateAC(DateTime consultationDate, long index, List<string> listErrors)
        {
            if (consultationDate > DateTime.Now)
            {
                listErrors.Add($"La fecha de consulta del item {index} no puede ser mayor a la fecha actual");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateConsultationPurposeAC
        /// </sumary> 
        /// <param name="consultationPurpose">consultationPurpose</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateConsultationPurposeAC(long consultationPurpose, long index, List<string> listErrors)
        {
            if (consultationPurpose < 1 || consultationPurpose > 10)
            {
                listErrors.Add($"La finalidad de consulta del item {index} es invalido");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateDocumentType
        /// </sumary> 
        /// <param name="documentUS">documentUS</param>
        /// <param name="dictionaryDocumentType">dictionaryDocumentType</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateDocumentType(ENT_USFile documentUS, dynamic dictionaryDocumentType, long index, List<string> listErrors)
        {
            if (documentUS != null)
            {
                USR_ValidateDocument(documentUS.IdentificationType, long.Parse(documentUS.AgePerson), long.Parse(documentUS.AgeUnitOfMeasurement), index, listErrors);
                if (!dictionaryDocumentType.ContainsKey(documentUS.IdentificationType))
                    listErrors.Add($"El tipo de documento de identificación del item {index} no existe en la base de datos");
            }
            return listErrors;
        }
        /// <sumary>
        /// ValidateDocument
        /// </sumary> 
        /// <param name="documentType">documentType</param>
        /// <param name="age">age</param>
        /// <param name="measurementUnit">measurementUnit</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateDocument(string documentType, long age, long measurementUnit, long index, List<string> listErrors)
        {
            bool valid = true;
            switch (documentType)
            {

                case "CE":
                    if (!(age >= 7 && measurementUnit == 1))
                    {
                        valid = false;
                    }
                    break;
                case "PE":
                case "CD":
                case "PA":
                case "SC":
                case "CC":
                    if (!(age >= 18 && measurementUnit == 1))
                    {
                        valid = false;
                    }

                    break;
                case "TI":
                    if (!(age >= 7 && age <= 17 && measurementUnit == 1))
                    {
                        valid = false;
                    }

                    break;
                case "AS":
                    if (!(age >= 17 && measurementUnit == 1))
                    {
                        valid = false;
                    }

                    break;

                case "MS":
                    if (!(age > 0 && age <= 31 && measurementUnit == 3))
                    {
                        valid = false;
                    }
                    else if (!(age > 0 && age <= 12 && measurementUnit == 2))
                    {
                        valid = false;
                    }
                    else if (!(age > 0 && age <= 17 && measurementUnit == 1))
                    {
                        valid = false;
                    }
                    break;
                case "RC":
                    if (!(age <= 7 && measurementUnit == 1))
                    {
                        valid = false;
                    }
                    break;

                case "CN":
                    if (!(age <= 3 && measurementUnit == 2))
                    {
                        valid = false;
                    }
                    break;
            }
            if (!valid)
            {
                listErrors.Add($"El tipo de documento del item {index} no es valido para la edad y la unidad de medida");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateExternalCauseAC
        /// </sumary> 
        /// <param name="externalCause">externalCause</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateExternalCauseAC(long? externalCause, long index, List<string> listErrors)
        {
            if (externalCause != null && externalCause > 15)
            {
                listErrors.Add($"La causa externa del item {index} es invalido");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateIdentificationNumber
        /// </sumary> 
        /// <param name="documentUS">documentUS</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateIdentificationNumber(ENT_USFile documentUS, long index, List<string> listErrors)
        {
            if (documentUS == null)
                listErrors.Add($"El documento de identificación del item {index} no existe en el archivo US");

            return listErrors;
        }
        /// <sumary>
        /// ValidateInvoiceNumber
        /// </sumary> 
        /// <param name="dictionaryAF">dictionaryAF</param>
        /// <param name="codeProvider">codeProvider</param>
        /// <param name="invoiceNumber">invoiceNumber</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateInvoiceNumber(dynamic dictionaryAF, string codeProvider, string invoiceNumber, long index, List<string> listErrors)
        {
            if (!dictionaryAF.ContainsKey($"{codeProvider}_{invoiceNumber}"))
            {
                listErrors.Add($"La factura del item {index} no existe en el archivo AF");
            }
            return listErrors;
        }
        /// <sumary>
        /// ValidateConceptCodeAD
        /// </sumary> 
        /// <param name="conceptCode">conceptCode</param>
        /// <param name="listConceptCode">listConceptCode</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateConceptCodeAD(string conceptCode, List<string> listConceptCode, long index, List<string> listErrors)
        {
            if (!listConceptCode.Contains(conceptCode))
            {
                listErrors.Add($"El código del concepto del item {index} no existe en la base de datos");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateTotalValueByConceptAD
        /// </sumary> 
        /// <param name="totalValueByConcept">totalValueByConcept</param>
        /// <param name="amount">amount</param>
        /// <param name="unitValue">unitValue</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateTotalValueByConceptAD(string totalValueByConcept, string amount, string unitValue, long index, List<string> listErrors)
        {
            if (decimal.Parse(totalValueByConcept) != (int.Parse(amount) * decimal.Parse(unitValue)))
            {
                listErrors.Add($"El valor total del item {index} es diferente a la operación entre valor unitario por cantidad");
            }

            return listErrors;
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="AdministeringEntityCode">AdministeringEntityCode</param>
        /// <param name="resultRipsCode">resultRipsCode</param>
        /// <param name="codeMunicipalyResul">codeMunicipalyResul</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateAdministeringEntityCodeAF(string AdministeringEntityCode, List<dynamic> resultRipsCode, List<dynamic> codeMunicipalyResul, long index, List<string> listErrors)
        {
            //Valida Divipola/CodigoRips
            if (AdministeringEntityCode != "" && AdministeringEntityCode != null)
            {
                if (resultRipsCode.FirstOrDefault(x => x.RipsCode == AdministeringEntityCode) == null)
                {
                    if (codeMunicipalyResul.FirstOrDefault(x => x.Code == AdministeringEntityCode) == null) listErrors.Add($"No existe el codigo del item {index} (Divipola/CodigoRips) o es invalido archivo AF");
                }
            }
            else
            {
                listErrors.Add($"El codigo del item {index} (Divipola/CodigoRips) es nulo o es invalido archivo AF");
            }

            return listErrors;
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="expeditionInvoice">expeditionInvoice</param>
        /// <param name="initDate">initDate</param>
        /// <param name="endDate">endDate</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateExpeditionInvoiceAF(DateTime expeditionInvoice, DateTime initDate, DateTime endDate, long index, List<string> listErrors)
        {
            if (expeditionInvoice > DateTime.Now)
                listErrors.Add($"La fecha de la factura en el item {index} es mayor a la fecha actual");

            if (expeditionInvoice < initDate || expeditionInvoice > endDate)
                listErrors.Add($"Linea {index} la fecha de factura fuera del periodo que se esta reportando");

            return listErrors;
        }
        /// <sumary>
        /// ValidatePeriodValidityEndDateAF
        /// </sumary> 
        /// <param name="periodValidityStartDate">periodValidityStartDate</param>
        /// <param name="periodValidityEndDate">periodValidityEndDate</param>
        /// <param name="endDate">endDate</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidatePeriodValidityEndDateAF(DateTime periodValidityStartDate, DateTime periodValidityEndDate, DateTime endDate, long index, List<string> listErrors)
        {
            if (periodValidityEndDate < periodValidityStartDate)
                listErrors.Add($"La fecha de final en el item {index} es menor a la fecha inicial");

            if (periodValidityEndDate != endDate)
            {
                listErrors.Add($"La fecha de inicio o de fin en el item {index} no coinciden con las fechas del periodo proporcionado");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidatePeriodValidityStartDateAF
        /// </sumary> 
        /// <param name="periodValidityStartDate">periodValidityStartDate</param>
        /// <param name="periodValidityEndDate">periodValidityEndDate</param>
        /// <param name="initDate">initDate</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidatePeriodValidityStartDateAF(DateTime periodValidityStartDate, DateTime periodValidityEndDate, DateTime initDate, long index, List<string> listErrors)
        {
            if (periodValidityStartDate > periodValidityEndDate)
                listErrors.Add($"La fecha de inicio en el item {index} es mayor a la fecha final");

            if (periodValidityStartDate != initDate)
                listErrors.Add($"La fecha de inicio en el item {index} no coinciden con las fechas del periodo proporcionado");

            return listErrors;
        }
        /// <sumary>
        /// ValidateComplicationDiagnosisAH
        /// </sumary> 
        /// <param name="complicationDiagnosis">complicationDiagnosis</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateComplicationDiagnosisAH(string complicationDiagnosis, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!string.IsNullOrEmpty(complicationDiagnosis))
            {
                if (!dictionaryDiagnosis.ContainsKey(complicationDiagnosis))
                {
                    listErrors.Add($"El diagnostico de complicación del item {index} no existe en la base de datos");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateDeathCauseDiagnosisAH
        /// </sumary> 
        /// <param name="statusExit">statusExit</param>
        /// <param name="deathCauseDiagnosis">deathCauseDiagnosis</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateDeathCauseDiagnosisAH(long statusExit, string deathCauseDiagnosis, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (statusExit == 2)
            {
                if (string.IsNullOrEmpty(deathCauseDiagnosis) || !dictionaryDiagnosis.ContainsKey(deathCauseDiagnosis))
                {
                    listErrors.Add($"El diagnostico de muerte del item {index} no existe en la base de datos");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateInstitutionDateExitAH
        /// </sumary> 
        /// <param name="institutionEntryDate">institutionEntryDate</param>
        /// <param name="institutionDateExit">institutionDateExit</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateInstitutionDateExitAH(DateTime institutionEntryDate, DateTime institutionDateExit, long index, List<string> listErrors)
        {
            if (institutionDateExit < institutionEntryDate)
            {
                listErrors.Add($"La fecha de salida del item {index} no puede ser menor a la fecha de ingreso");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateInstitutionEntryAH
        /// </sumary> 
        /// <param name="institutionEntry">institutionEntry</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateInstitutionEntryAH(long institutionEntry, long index, List<string> listErrors)
        {
            if (institutionEntry < 1 || institutionEntry > 4)
            {
                listErrors.Add($"El ingreso del item {index} no existe");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateInstitutionEntryDateAH
        /// </sumary> 
        /// <param name="institutionEntryDate">institutionEntryDate</param>
        /// <param name="institutionDateExit">institutionDateExit</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateInstitutionEntryDateAH(DateTime institutionEntryDate, DateTime institutionDateExit, long index, List<string> listErrors)
        {
            if (institutionEntryDate > DateTime.Now || institutionEntryDate > institutionDateExit)
            {
                listErrors.Add($"La fecha del item {index} no puede ser mayor a la fecha de salida ni a la fecha actual");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateMainDiagnosticAH
        /// </sumary> 
        /// <param name="MainDiagnostic">MainDiagnostic</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateMainDiagnosticAH(string MainDiagnostic, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!dictionaryDiagnosis.ContainsKey(MainDiagnostic))
            {
                listErrors.Add($"El diagnostico principal de egreso del item {index} no existe en la base de datos");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidatePrincipalDiagnosisCodeAH
        /// </sumary> 
        /// <param name="principalDiagnosisCode">principalDiagnosisCode</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidatePrincipalDiagnosisCodeAH(string principalDiagnosisCode, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!dictionaryDiagnosis.ContainsKey(principalDiagnosisCode))
            {
                listErrors.Add($"El diagnostico principal de ingreso del item {index} no existe en la base de datos");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateRelationMainDiagnosisOneAH
        /// </sumary> 
        /// <param name="relationMainDiagnosisOne">relationMainDiagnosisOne</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateRelationMainDiagnosisOneAH(string relationMainDiagnosisOne, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!string.IsNullOrEmpty(relationMainDiagnosisOne))
            {
                if (!dictionaryDiagnosis.ContainsKey(relationMainDiagnosisOne))
                {
                    listErrors.Add($"El diagnostico uno del item {index} no existe en la base de datos");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateRelationMainDiagnosisThreeAH
        /// </sumary> 
        /// <param name="relationMainDiagnosisThree">relationMainDiagnosisThree</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateRelationMainDiagnosisThreeAH(string relationMainDiagnosisThree, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!string.IsNullOrEmpty(relationMainDiagnosisThree))
            {
                if (!dictionaryDiagnosis.ContainsKey(relationMainDiagnosisThree))
                {
                    listErrors.Add($"El diagnostico tres del item {index} no existe en la base de datos");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateRelationMainDiagnosisTwoAH
        /// </sumary> 
        /// <param name="relationMainDiagnosisTwo">relationMainDiagnosisTwo</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateRelationMainDiagnosisTwoAH(string relationMainDiagnosisTwo, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!string.IsNullOrEmpty(relationMainDiagnosisTwo))
            {
                if (!dictionaryDiagnosis.ContainsKey(relationMainDiagnosisTwo))
                {
                    listErrors.Add($"El diagnostico dos del item {index} no existe en la base de datos");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateStatusExitAH
        /// </sumary> 
        /// <param name="statusExit">statusExit</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateStatusExitAH(long statusExit, long index, List<string> listErrors)
        {
            if (statusExit < 1 || statusExit > 2)
            {
                listErrors.Add($"El estado de salida del item {index} no existe");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateDrugsCodeAM
        /// </sumary> 
        /// <param name="dictionaryCum">dictionaryCum</param>
        /// <param name="drugsCode">drugsCode</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateDrugsCodeAM(dynamic dictionaryCum, string drugsCode, long index, List<string> listErrors)
        {
            if (!dictionaryCum.ContainsKey(drugsCode))
                listErrors.Add($"El CUM de medicamentos del item {index} no existe en la base de datos");

            return listErrors;
        }
        /// <sumary>
        /// ValidateDrugsTotalValueAM
        /// </sumary> 
        /// <param name="drugsTotalValue">drugsTotalValue</param>
        /// <param name="drugsAmount">drugsAmount</param>
        /// <param name="drugsUnitValue">drugsUnitValue</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateDrugsTotalValueAM(string drugsTotalValue, string drugsAmount, string drugsUnitValue, long index, List<string> listErrors)
        {
            if (decimal.Parse(drugsTotalValue) != (int.Parse(drugsAmount) * decimal.Parse(drugsUnitValue)))
            {
                listErrors.Add($"El valor total del item {index} es diferente a la operación entre valor unitario por cantidad");
            }
            return listErrors;
        }
        /// <sumary>
        /// ValidateDrugsTypeAM
        /// </sumary> 
        /// <param name="dictionaryCum">dictionaryCum</param>
        /// <param name="dictionaryDetailsConcept">dictionaryDetailsConcept</param>
        /// <param name="drugsType">drugsType</param>
        /// <param name="drugsCode">drugsCode</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateDrugsTypeAM(dynamic dictionaryCum, dynamic dictionaryDetailsConcept, long drugsType, string drugsCode, long index, List<string> listErrors)
        {
            if (dictionaryCum.ContainsKey(drugsCode))
            {
                //se valida que sea pos o no pos
                if (drugsType == 0 || drugsType > 2)
                {
                    listErrors.Add($"El tipo de medicamento del item {index} no existe");
                }
                else
                {
                    var cum = dictionaryCum[drugsCode];

                    dynamic serviceType = null;
                    if (dictionaryDetailsConcept.ContainsKey(cum.IdPos.ToString()))
                    {
                        serviceType = dictionaryDetailsConcept[cum.IdPos.ToString()];
                    }
                    //se valiada que el cup sea de tipo consulta
                    if (serviceType != null && serviceType.Code != null && serviceType.Code.ToString() != drugsType.ToString())
                    {
                        listErrors.Add($"El tipo de medicamento del item {index} no coincide con el CUM");
                    }
                }
            }
            return listErrors;
        }
        /// <sumary>
        /// ValidateDeathCauseDateAN
        /// </sumary> 
        /// <param name="deathCauseDate">deathCauseDate</param>
        /// <param name="newbornDate">Fecha de nacimiento</param>
        /// <param name="statusExit">Estado de salida</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateDeathCauseDateAN(DateTime? deathCauseDate, DateTime newbornDate, ENT_AHFile statusExit, long index, List<string> listErrors)
        {
            if ((statusExit != null && int.Parse(statusExit.StatusExit) == 2) && deathCauseDate == null)
            {
                listErrors.Add($"La fecha de muerte del item {index} no puede estar vacía cuando hay causa de muerte");
            }

            if (deathCauseDate != null)
            {
                if (deathCauseDate > DateTime.Now)
                    listErrors.Add($"La fecha de muerte del item {index} no puede ser mayor a la fecha actual");

                if (deathCauseDate < newbornDate)
                    listErrors.Add($"La fecha de muerte del item {index} no puede ser menor a la fecha de nacimiento");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateDiagnosisAN
        /// </sumary> 
        /// <param name="identificationNumber">identificationNumber</param>
        /// <param name="deathCauseDiagnosis">deathCauseDiagnosis</param>
        /// <param name="statusExit">statusExit</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateDiagnosisAN(string identificationNumber, string deathCauseDiagnosis, ENT_AHFile statusExit, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (statusExit != null)
            {
                if (int.Parse(statusExit.StatusExit) == 2)
                {
                    if (string.IsNullOrEmpty(deathCauseDiagnosis) || !dictionaryDiagnosis.ContainsKey(deathCauseDiagnosis))
                    {
                        listErrors.Add($"El diagnostico de muerte del item {index} no existe en la base de datos");
                    }
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateGestationalAgeAN
        /// </sumary> 
        /// <param name="gestationalAge">gestationalAge</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateGestationalAgeAN(long gestationalAge, long index, List<string> listErrors)
        {
            if (gestationalAge < 1 || gestationalAge > 99)
            {
                listErrors.Add($"La edad gestacional del item {index} es invalida");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateNewbornDateAN
        /// </sumary> 
        /// <param name="newbornDate">newbornDate</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateNewbornDateAN(DateTime newbornDate, long index, List<string> listErrors)
        {
            if (newbornDate > DateTime.Now)
            {
                listErrors.Add($"La fecha del item {index} no puede ser mayor a la fecha actual");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateNewbornDiagnosisAN
        /// </sumary> 
        /// <param name="newbornDiagnosis">newbornDiagnosis</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateNewbornDiagnosisAN(string newbornDiagnosis, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!dictionaryDiagnosis.ContainsKey(newbornDiagnosis))
                listErrors.Add($"El diagnostico de recien nacido del item {index} no existe en la base de datos");

            return listErrors;
        }
        /// <sumary>
        /// ValidateNewbornSexAN
        /// </sumary> 
        /// <param name="newbornSex">newbornSex</param>
        /// <param name="listSexNum">listSexNum</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateNewbornSexAN(string newbornSex, List<string> listSexNum, long index, List<string> listErrors)
        {
            if (!listSexNum.Contains(newbornSex))
            {
                listErrors.Add($"El genero del item {index} no existe en la base de datos");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidatePrenatalControlAN
        /// </sumary> 
        /// <param name="prenatalControl">prenatalControl</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidatePrenatalControlAN(long prenatalControl, long index, List<string> listErrors)
        {
            if (prenatalControl != 1 && prenatalControl != 2)
            {
                listErrors.Add($"El control prenatal del item {index} no existe");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateActSurgicalAP
        /// </sumary> 
        /// <param name="actSurgical">actSurgical</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateActSurgicalAP(long? actSurgical, long index, List<string> listErrors)
        {
            if (actSurgical == null)
            {
                listErrors.Add($"La forma de liquidación quirúrgica del item {index} no puede estar vacio");
            }
            else
            {
                if (actSurgical < 1 || actSurgical > 5)
                {
                    listErrors.Add($"La forma de liquidación quirúrgica del item {index} no se encuentra en el rango permitido");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateAttendPersonalAP
        /// </sumary> 
        /// <param name="attendPersonal">attendPersonal</param>
        /// <param name="principalDiagnosticCode">principalDiagnosticCode</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateAttendPersonalAP(long? attendPersonal, string principalDiagnosticCode, long index, List<string> listErrors)
        {
            if (principalDiagnosticCode == "O829" && attendPersonal == null)
            {
                listErrors.Add($"La atención personal del item {index} no puede estar vacio");
            }
            else
            {
                if (attendPersonal < 1 || attendPersonal > 5)
                {
                    listErrors.Add($"La atención personal del item {index} no se encuentra en el rango permitido");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateComplicationAP
        /// </sumary> 
        /// <param name="complication">complication</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateComplicationAP(string complication, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!string.IsNullOrEmpty(complication))
            {
                if (!dictionaryDiagnosis.ContainsKey(complication))
                {
                    listErrors.Add($"El diagnostico complicación del item {index} no existe en la base de datos");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateCupAP
        /// </sumary> 
        /// <param name="cup">cup</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateCupAP(dynamic cup, long index, List<string> listErrors)
        {
            if (cup == null)
                listErrors.Add($"El CUPS del item {index} no existe en la base de datos");

            return listErrors;
        }
        /// <sumary>
        /// ValidatePrincipalDiagnosticCodeAP
        /// </sumary> 
        /// <param name="principalDiagnosticCode">principalDiagnosticCode</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidatePrincipalDiagnosticCodeAP(string principalDiagnosticCode, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (string.IsNullOrEmpty(principalDiagnosticCode))
            {
                listErrors.Add($"El diagnostico principal del item {index} no puede estar vacio");
            }
            else
            {
                if (!dictionaryDiagnosis.ContainsKey(principalDiagnosticCode))
                {
                    listErrors.Add($"El diagnostico principal del item {index} no existe en la base de datos");
                }
            }
            return listErrors;
        }
        /// <sumary>
        /// ValidateProcedureDateAP
        /// </sumary> 
        /// <param name="procedureDate">procedureDate</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateProcedureDateAP(DateTime procedureDate, long index, List<string> listErrors)
        {
            if (procedureDate > DateTime.Now)
            {
                listErrors.Add($"La fecha del procedimiento del item {index} no puede ser mayor que la fecha actual");
            }
            return listErrors;
        }
        /// <sumary>
        /// ValidateProcedurePurposeAP
        /// </sumary> 
        /// <param name="procedurePurpose">procedurePurpose</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateProcedurePurposeAP(long procedurePurpose, long index, List<string> listErrors)
        {
            if (procedurePurpose < 1 || procedurePurpose > 5)
            {
                listErrors.Add($"La finalidad del servicio del item {index} es diferente a la del CUPS");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateProcedureScopeAP
        /// </sumary> 
        /// <param name="procedureScope">procedureScope</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateProcedureScopeAP(long procedureScope, long index, List<string> listErrors)
        {
            if (procedureScope < 1 || procedureScope > 3)
            {
                listErrors.Add($"El ámbito del item {index} no se encuentra dentro del rango permitido");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateRelationDiagnosisCodeOneAP
        /// </sumary> 
        /// <param name="relationDiagnosisCodeOne">relationDiagnosisCodeOne</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateRelationDiagnosisCodeOneAP(string relationDiagnosisCodeOne, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            //diagnostico relacionado
            if (!string.IsNullOrEmpty(relationDiagnosisCodeOne))
            {
                if (!dictionaryDiagnosis.ContainsKey(relationDiagnosisCodeOne))
                {
                    listErrors.Add($"El diagnostico relacionado del item {index} no existe en la base de datos");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateCupAT
        /// </sumary> 
        /// <param name="serviceType">serviceType</param>
        /// <param name="serviceCode">serviceCode</param>
        /// <param name="dictionaryCups">dictionaryCups</param>
        /// <param name="dictionaryDetailsConcept">dictionaryDetailsConcept</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateCupAT(long? serviceType, string serviceCode, dynamic dictionaryCups, dynamic dictionaryDetailsConcept, long index, List<string> listErrors)
        {
            if (serviceType != null && (serviceType == 2 || serviceType == 3))
            {
                if (!dictionaryCups.ContainsKey(serviceCode))
                {
                    listErrors.Add($"El CUPS del item {index} no existe en la base de datos");
                }
                else
                {
                    var cups = dictionaryCups[serviceCode];
                    dynamic procedureType = null;
                    if (dictionaryDetailsConcept.ContainsKey(cups.IdProcedureType.ToString()))
                    {
                        procedureType = dictionaryDetailsConcept[cups.IdProcedureType.ToString()];
                    }

                    //tipo de procedimieto
                    if (procedureType == null || procedureType.Code.ToString() != "4" && procedureType.Code.ToString() != "15")
                    {
                        listErrors.Add($"El tipo de procedimiento del item {index} no es valido para el archivo");
                    }
                }
            }
            return listErrors;
        }
        /// <sumary>
        /// ValidateServiceTotalValueAT
        /// </sumary> 
        /// <param name="serviceTotalValue">serviceTotalValue</param>
        /// <param name="serviceAmount">serviceAmount</param>
        /// <param name="serviceUnitValue">serviceUnitValue</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateServiceTotalValueAT(string serviceTotalValue, string serviceAmount, string serviceUnitValue, long index, List<string> listErrors)
        {
            if (decimal.Parse(serviceTotalValue) != (int.Parse(serviceAmount) * decimal.Parse(serviceUnitValue)))
            {
                listErrors.Add($"El valor total del item {index} es diferente a la operación entre valor unitario por cantidad");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateServiceTypeAT
        /// </sumary> 
        /// <param name="serviceType">serviceType</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateServiceTypeAT(long? serviceType, long index, List<string> listErrors)
        {
            if (serviceType != null && (serviceType < 1 || serviceType > 4))
            {
                listErrors.Add($"El tipo de servicio del item {index} no existe");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateDeathCauseAU
        /// </sumary> 
        /// <param name="statusExit">statusExit</param>
        /// <param name="deathCause">deathCause</param>
        /// <param name="diagnosisEnd">diagnosisEnd</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateDeathCauseAU(long statusExit, string deathCause, string diagnosisEnd, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (statusExit == 2)
            {
                if (deathCause == null || (!dictionaryDiagnosis.ContainsKey(deathCause)))
                {
                    listErrors.Add($"El diagnostico de muerte del item {index} no existe en la base de datos");
                }
                else if (deathCause != diagnosisEnd)
                {
                    listErrors.Add($"El diagnostico de muerte del item {index} es diferente al diagnostico final");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateDiagnosisEndAU
        /// </sumary> 
        /// <param name="diagnosisEnd">diagnosisEnd</param>
        /// <param name="dicitonaryDiagnosis">dicitonaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateDiagnosisEndAU(string diagnosisEnd, dynamic dicitonaryDiagnosis, long index, List<string> listErrors)
        {
            if (!dicitonaryDiagnosis.ContainsKey(diagnosisEnd))
                listErrors.Add($"El diagnostico de salida del item {index} no existe en la base de datos");

            return listErrors;
        }
        /// <sumary>
        /// ValidateExternalCauseAU
        /// </sumary> 
        /// <param name="externalCause">externalCause</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateExternalCauseAU(long? externalCause, long index, List<string> listErrors)
        {
            if (externalCause != null && externalCause < 1 || externalCause > 15)
            {
                listErrors.Add($"La causa externa del item {index} es invalido");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateObservationDateAU
        /// </sumary> 
        /// <param name="observationDate">observationDate</param>
        /// <param name="observationDateExit">observationDateExit</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateObservationDateAU(DateTime observationDate, DateTime observationDateExit, long index, List<string> listErrors)
        {
            if (observationDate > DateTime.Now || observationDate > observationDateExit)
            {
                listErrors.Add($"La fecha del item {index} no puede ser mayor a la fecha de salida ni a la fecha actual");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateObservationDateExitAU
        /// </sumary> 
        /// <param name="observationDateExit">observationDateExit</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateObservationDateExitAU(DateTime observationDateExit, long index, List<string> listErrors)
        {
            if (observationDateExit > DateTime.Now)
                listErrors.Add($"La fecha de salida del item {index} no puede ser mayor a la fecha actual");

            return listErrors;
        }
        /// <sumary>
        /// ValidateObservationDestinationAU
        /// </sumary> 
        /// <param name="observationDestination">observationDestination</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateObservationDestinationAU(long observationDestination, long index, List<string> listErrors)
        {
            if (observationDestination < 1 || observationDestination > 3)
                listErrors.Add($"La observación de destino del item {index} no existe");

            return listErrors;
        }
        /// <sumary>
        /// ValidateRelationDiagnosisExitOneAU
        /// </sumary> 
        /// <param name="relationDiagnosisExitOne">relationDiagnosisExitOne</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateRelationDiagnosisExitOneAU(string relationDiagnosisExitOne, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!string.IsNullOrEmpty(relationDiagnosisExitOne) && !dictionaryDiagnosis.ContainsKey(relationDiagnosisExitOne))
                listErrors.Add($"El diagnostico uno del item {index} no existe en la base de datos");

            return listErrors;
        }
        /// <sumary>
        /// ValidateRelationDiagnosisExitThreeAU
        /// </sumary> 
        /// <param name="relationDiagnosisExitThree">relationDiagnosisExitThree</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateRelationDiagnosisExitThreeAU(string relationDiagnosisExitThree, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!string.IsNullOrEmpty(relationDiagnosisExitThree) && !dictionaryDiagnosis.ContainsKey(relationDiagnosisExitThree))
                listErrors.Add($"El diagnostico tres del item {index} no existe en la base de datos");

            return listErrors;
        }
        /// <sumary>
        /// ValidateRelationDiagnosisExitTwoAU
        /// </sumary> 
        /// <param name="relationDiagnosisExitTwo">relationDiagnosisExitTwo</param>
        /// <param name="dictionaryDiagnosis">dictionaryDiagnosis</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateRelationDiagnosisExitTwoAU(string relationDiagnosisExitTwo, dynamic dictionaryDiagnosis, long index, List<string> listErrors)
        {
            if (!string.IsNullOrEmpty(relationDiagnosisExitTwo) && !dictionaryDiagnosis.ContainsKey(relationDiagnosisExitTwo))
                listErrors.Add($"El diagnostico dos del item {index} no existe en la base de datos");

            return listErrors;
        }
        /// <sumary>
        /// ValidateStatusExitAU
        /// </sumary> 
        /// <param name="statusExit">statusExit</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateStatusExitAU(long statusExit, long index, List<string> listErrors)
        {
            if (statusExit < 1 || statusExit > 2)
                listErrors.Add($"El estado de salida del item {index} no existe");

            return listErrors;
        }
        /// <sumary>
        /// Obtiene los códigos de habilitación filtrados
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        /// <param name="qualificationCodes">qualificationCodes</param>
        public static ENT_ActionResult USR_GetValidQualificationCodes(long adapterId, dynamic qualificationCodes)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" DECLARE @x xml; ");
            sql.Append(" SET @x = '<root><ids> ");
            foreach (string[] codes in qualificationCodes)
            {
                foreach (string code in codes)
                {
                    sql.Append($"<id>{code}</id>");
                }
            }
            sql.Append(" </ids></root> ';");
            sql.Append("  SELECT QualificationCode  FROM Operator WITH (NOLOCK) WHERE IdOperatorType = 70 AND QualificationCode IN(select T.X.value('(text())[1]', 'varchar(15)') as id from @X.nodes('/root/ids/id') as T(X)); ");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Convierte los registros de una archivo a una lista de entidades
        /// </sumary> 
        /// <param name="textFile">Ruta del archivo</param>
        /// <param name="lineSeparator">Separador de lineas en el archivo</param>
        /// <param name="columnSeparator">Separador de columnas en el archivo</param>
        /// <param name="type">Tipo de entidad</param>
        public static dynamic SYS_FileToEntities(string textFile, string lineSeparator, string columnSeparator, object type)
        {
            if (string.IsNullOrEmpty(lineSeparator) || string.IsNullOrEmpty(columnSeparator))
            {
                throw new ArgumentNullException("lineSeparator,columnSeparator");
            }

            if (type == null)
                return new List<dynamic>();

            Type typeEntity = (Type)type;
            Type genericListType = typeof(List<>).MakeGenericType(typeEntity);
            IList lstEntities = (IList)Activator.CreateInstance(genericListType);
            try
            {

                string[] lines = textFile.Split(new string[] { lineSeparator }, StringSplitOptions.RemoveEmptyEntries);
                var properties = typeEntity.GetProperties().OrderBy(p => ((OrderAttribute)p.GetCustomAttributes(typeof(OrderAttribute), false)[0]).Order);
                string[] columns;
                object instance;
                int index = 0;
                foreach (string line in lines)
                {
                    instance = Activator.CreateInstance(typeEntity);

                    columns = line.Split(columnSeparator[0]);

                    index = 0;
                    foreach (var property in properties)
                    {
                        property.SetValue(instance, columns[index]);
                        index++;
                    }
                    lstEntities.Add(instance);

                }

                return lstEntities;
            }
            catch
            {
                throw;
            }
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="AdministeringEntityCode">Variable de parámetro de función vacía</param>
        /// <param name="resultRipsCode">resultRipsCode</param>
        /// <param name="codeMunicipalyResul">codeMunicipalyResul</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateAdministeringEntityCodeUS(string AdministeringEntityCode, List<dynamic> resultRipsCode, List<dynamic> codeMunicipalyResul, long index, List<string> listErrors)
        {
            if (AdministeringEntityCode != "" && AdministeringEntityCode != null)
            {
                if (resultRipsCode.FirstOrDefault(x => x.RipsCode == AdministeringEntityCode) == null)
                {
                    if (codeMunicipalyResul.FirstOrDefault(x => x.Code == AdministeringEntityCode) == null) listErrors.Add($"No existe el codigo del item {index} (Divipola/CodigoRips) o es invalido archivo US");
                }
            }
            else
            {
                listErrors.Add($"El codigo del item {index} (Divipola/CodigoRips) es nulo o es invalido archivo US");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateAffiliatePerson
        /// </sumary> 
        /// <param name="personList">personList</param>
        /// <param name="adapterId">adapterId</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateAffiliatePerson(List<dynamic> personList, long adapterId, List<string> listErrors)
        {
            ENT_ActionResult persons = USR_GetPersonByDocumentNumber(adapterId, personList);
            if (persons.IsError)
            {
                throw new Exception(persons.ErrorMessage);
            }
            List<dynamic> listpersonByDocumentNumber = JsonConvert.DeserializeObject<List<dynamic>>(persons.Result.ToString());
            Dictionary<string, dynamic> dictionarypersonByDocumentNumber = listpersonByDocumentNumber.ToDictionary(x => $"{x.IdDocumentType}_{x.DocumentNumber}", x => x);


            List<dynamic> personsNoExist = new List<dynamic>();

            foreach (var person in personList)
            {
                if (!dictionarypersonByDocumentNumber.ContainsKey($"{person.DocumentType}_{person.Identification}"))
                {
                    personsNoExist.Add(person);

                }
            }
            if (personsNoExist.Count > 0)
            {

                ENT_ActionResult novelty = USR_GetNoveltyDetail(adapterId, personsNoExist);
                if (novelty.IsError)
                {
                    throw new Exception(novelty.ErrorMessage);
                }
                List<dynamic> novChangeNumDoc = JsonConvert.DeserializeObject<List<dynamic>>(novelty.Result.ToString());
                if (novChangeNumDoc.Count > 0)
                {
                    foreach (var itemNoveltyDetail in novChangeNumDoc)
                    {
                        var novelties = USR_GetNoveltiesTypeDocumentByPerson(adapterId, itemNoveltyDetail.IdNovelty.ToString());
                        if (novelties.IsError)
                        {
                            throw new Exception(novelties.ErrorMessage);
                        }
                        List<dynamic> listNovelties = JsonConvert.DeserializeObject<List<dynamic>>(novelties.Result.ToString());
                        foreach (var itemNovelty in listNovelties)
                        {
                            var person = USR_GetPersonByTypeAndDocumentNumber(adapterId, itemNovelty.NewValue.ToString(), itemNoveltyDetail.NewValue.ToString());
                            if (person.IsError)
                            {
                                throw new Exception(person.ErrorMessage);
                            }
                            List<dynamic> personBD = JsonConvert.DeserializeObject<List<dynamic>>(person.Result.ToString());
                            if (personBD != null && personBD.Count > 0)
                            {
                                personsNoExist.RemoveAll(d => d.Identification == itemNoveltyDetail.OldValue.ToString());
                                break;
                            }
                        }
                    }

                }
                //Valida cambio de tipo de documento       

                var novelties2 = USR_GetNoveltiesAffiliate(adapterId, personsNoExist);
                if (novelties2.IsError)
                {
                    throw new Exception(novelties2.ErrorMessage);
                }
                List<dynamic> listNovelties2 = JsonConvert.DeserializeObject<List<dynamic>>(novelties2.Result.ToString());
                Dictionary<string, dynamic> dictionaryNovelties2 = listNovelties2.ToDictionary(x => $"{x.OldValue}_{x.DocumentNumber}", x => x);
                List<Tuple<int, string>> ForDelete = new List<Tuple<int, string>>();

                foreach (var itemPerson in personsNoExist)
                {
                    if (dictionaryNovelties2.ContainsKey($"{itemPerson.DocumentType}_{itemPerson.Identification}"))
                    {
                        Tuple<int, string> tuple = new Tuple<int, string>(int.Parse(itemPerson.DocumentType), itemPerson.Identification);

                        ForDelete.Add(tuple);
                    }
                }

                foreach (var item in ForDelete)
                {
                    personsNoExist.RemoveAll(d => d.Identification == item.Item2);
                }

                foreach (var item in personsNoExist)
                {
                    listErrors.Add($"La persona identificada con tipo de documento {item.DocumentType} Número {item.Identification} registrada el item {item.Index} no existe");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// Obtiene las novedades por afiliado
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        /// <param name="personList">personList</param>
        public static ENT_ActionResult USR_GetNoveltiesAffiliate(long adapterId, dynamic personList)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" DECLARE @x xml; ");
            sql.Append(" SET @x = '<root><ids> ");
            foreach (var documentNumber in personList)
                sql.Append($"<id>{documentNumber.Identification}</id>");
            sql.Append(" </ids></root> ';");

            sql.Append("select NoveltyDetail.IdNovelty, Person.IdDocumentType, Person.DocumentNumber, NoveltyDetail.FieldName, NoveltyDetail.OldValue, NoveltyDetail.NewValue, Novelty.FiscalEffectDate ");
            sql.Append("from NoveltyDetail inner join Novelty on Novelty.Id = NoveltyDetail.IdNovelty inner join Affiliate on Novelty.IdAffiliate = Affiliate.Id inner join Person on Person.Id = Affiliate.IdPerson ");
            sql.Append($"where FieldName = 'ID Tipo Documento' and IdNovelty in (select Id from Novelty where IdAffiliate in (select Id from Affiliate where IdPerson in (select Id from Person where DocumentNumber in (select T.X.value('(text())[1]', 'varchar(15)') as id from @X.nodes('/root/ids/id') as T(X)))))");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Obtiene novedades por tipo de documento
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        /// <param name="idNovelty">idNovelty</param>
        public static ENT_ActionResult USR_GetNoveltiesTypeDocumentByPerson(long adapterId, string idNovelty)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($"select * from NoveltyDetail where FieldName = 'ID Tipo Documento' and IdNovelty in (select Id from Novelty where IdAffiliate = (select IdAffiliate from Novelty where Id = '{idNovelty}'))");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Obtiene detalles de novedad
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        /// <param name="personsNoExist">personsNoExist</param>
        public static ENT_ActionResult USR_GetNoveltyDetail(long adapterId, dynamic personsNoExist)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" DECLARE @x xml; ");
            sql.Append(" SET @x = '<root><ids> ");
            foreach (var documentNumber in personsNoExist)
                sql.Append($"<id>{documentNumber.Identification}</id>");
            sql.Append(" </ids></root> ';");
            sql.Append("  SELECT IdNovelty, OldValue, NewValue  FROM NoveltyDetail WITH (NOLOCK) WHERE OldValue IN(select T.X.value('(text())[1]', 'varchar(15)') as id from @X.nodes('/root/ids/id') as T(X)) AND FieldName='Número de Documento'; ");

            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// Obtiene persona por tipo de documento
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        /// <param name="documentsNumbers">documentsNumbers</param>
        public static ENT_ActionResult USR_GetPersonByDocumentNumber(long adapterId, dynamic documentsNumbers)
        {
            StringBuilder sbPersonByNumber = new StringBuilder();
            sbPersonByNumber.Append(" DECLARE @XmlDocumentHandle int; ");
            sbPersonByNumber.Append(" DECLARE @XmlDocument xml; ");
            sbPersonByNumber.Append(" SET @XmlDocument = '<root><ids> ");
            foreach (var documentNumber in documentsNumbers)
                sbPersonByNumber.Append($"<id>{documentNumber.Identification}</id>");
            sbPersonByNumber.Append(" </ids></root> ';");
            sbPersonByNumber.Append("  EXEC sp_xml_preparedocument @XmlDocumentHandle OUTPUT, @XmlDocument; ");
            sbPersonByNumber.Append(" SELECT  DISTINCT IdDocumentType, DocumentNumber FROM Person WITH (NOLOCK) WHERE DocumentNumber IN( ");
            sbPersonByNumber.Append(" SELECT id FROM OPENXML (@XmlDocumentHandle, '/root/ids/id',1) WITH (id  varchar(15) '.')); ");
            sbPersonByNumber.Append(" EXEC sp_xml_removedocument @XmlDocumentHandle; ");

            var resultExecute = SYS_WSExecuteQuery(adapterId, sbPersonByNumber.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };

        }
        /// <sumary>
        /// Obtiene persona por tipo y numero de identificación
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        /// <param name="idDocumentType">idDocumentType</param>
        /// <param name="documentNumber">documentNumber</param>
        public static ENT_ActionResult USR_GetPersonByTypeAndDocumentNumber(long adapterId, string idDocumentType, string documentNumber)
        {
            var sql = new StringBuilder();
            sql.Append(" SELECT IdDocumentType, DocumentNumber ");
            sql.Append(" FROM Person WITH (NOLOCK)");
            sql.Append($" WHERE IdDocumentType = {idDocumentType} AND DocumentNumber = '{documentNumber}'");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return resultExecute;
            }
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultExecute.Result };
        }
        /// <sumary>
        /// ValidateAgeUnitOfMeasurementUS
        /// </sumary> 
        /// <param name="ageUnitOfMeasurement">ageUnitOfMeasurement</param>
        /// <param name="agePerson">agePerson</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateAgeUnitOfMeasurementUS(long ageUnitOfMeasurement, long agePerson, long index, List<string> listErrors)
        {
            if (ageUnitOfMeasurement < 1 || ageUnitOfMeasurement > 3)
            {
                listErrors.Add($"El tipo de unidad de medida de la edad del item {index} no es válido");
            }
            else
            {
                if (ageUnitOfMeasurement == 1) //Años
                {
                    if (agePerson < 1 || agePerson > 120)
                        listErrors.Add($"La edad {agePerson} del item {index} no corresponde a la unidad de medida {ageUnitOfMeasurement}");
                }
                if (ageUnitOfMeasurement == 2) //Meses
                {
                    if (agePerson < 1 || agePerson > 11)
                        listErrors.Add($"La edad {agePerson} del item {index} no corresponde a la unidad de medida {ageUnitOfMeasurement}");
                }
                if (ageUnitOfMeasurement == 3) //Dias
                {
                    if (agePerson < 1 || agePerson > 29)
                        listErrors.Add($"La edad {agePerson} del item {index} no corresponde a la unidad de medida {ageUnitOfMeasurement}");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateCodeCityUS
        /// </sumary> 
        /// <param name="dictionaryTypeDetails">dictionaryTypeDetails</param>
        /// <param name="codeTown">codeTown</param>
        /// <param name="codeCity">codeCity</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateCodeCityUS(dynamic dictionaryTypeDetails, string codeTown, string codeCity, long index, List<string> listErrors)
        {
            if (!dictionaryTypeDetails.ContainsKey($"{codeTown}{codeCity}"))
                listErrors.Add($"La ciudad del item {index} no existe en la base de datos");

            return listErrors;
        }
        /// <sumary>
        /// ValidateCodeTownUS
        /// </sumary> 
        /// <param name="dictionaryTypeDetails">dictionaryTypeDetails</param>
        /// <param name="codeTown">codeTown</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateCodeTownUS(dynamic dictionaryTypeDetails, string codeTown, long index, List<string> listErrors)
        {
            if (!dictionaryTypeDetails.ContainsKey(codeTown))
                listErrors.Add($"El departamento del item {index} no existe en la base de datos");

            return listErrors;
        }
        /// <sumary>
        /// ValidateSexPersonUS
        /// </sumary> 
        /// <param name="listSex">listSex</param>
        /// <param name="sexPerson">sexPerson</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateSexPersonUS(List<string> listSex, string sexPerson, long index, List<string> listErrors)
        {
            if (!listSex.Contains(sexPerson))
            {
                listErrors.Add($"El genero del item {index} no existe en la base de datos");
            }

            return listErrors;
        }
        /// <sumary>
        /// ValidateZoneTownUS
        /// </sumary> 
        /// <param name="zoneTown">zoneTown</param>
        /// <param name="listZone">listZone</param>
        /// <param name="index">index</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateZoneTownUS(string zoneTown, List<string> listZone, long index, List<string> listErrors)
        {
            if (!listZone.Contains(zoneTown))
            {
                listErrors.Add($"LA zona del item {index} no existe en la base de datos");
            }

            return listErrors;
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="remoteFile">Ruta destino de archivo a subir</param>
        /// <param name="localFile">FileStream de archivo a subir</param>
        public static string USR_FtpUpload(string remoteFile, dynamic localFile)
        {
            try
            {
                string ftp = "190.217.17.108";
                string userName = "OpheliaDcom";
                string password = "F1dupr3v1s0r4*!/#";

                string host = ("ftp://" + ftp);

                if (string.IsNullOrEmpty(ftp) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException("ftp,userName,password");
                }
                int bufferSize = 2048;
                /* Create an FTP Request */
                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(userName, password);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */
                Stream ftpStream = ftpRequest.GetRequestStream();
                /* Buffer for the Downloaded Data */
                localFile.Position = 0;
                byte[] byteBuffer = new byte[bufferSize];
                int bytesSent = localFile.Read(byteBuffer, 0, bufferSize);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                while (bytesSent != 0)
                {
                    ftpStream.Write(byteBuffer, 0, bytesSent);
                    bytesSent = localFile.Read(byteBuffer, 0, bufferSize);
                }

                /* Resource Cleanup */
                localFile.Close();
                ftpStream.Close();
                using (FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    ftpRequest = null;
                    return response.StatusDescription;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <sumary>
        /// Función estándar para el consumo de un servicio web GET
        /// </sumary> 
        /// <param name="url">Url del servicio Web</param>
        /// <param name="headers">Cabecera del servicio</param>
        public static ENT_ActionResult SYS_WSGET(string url, dynamic headers)
        {
            ENT_ActionResult result = new ENT_ActionResult();
            result = SYS_WSRequest("GET", url, null, headers, null, null);
            return result;
        }

        #region Extention
        /// <sumary>
        /// Obtiene el valor de una propieda de un objecto específico
        /// </sumary>
        public static TResult GetPropertyValue<TResult>(this object obj, string propertyName)
        {
            propertyName = propertyName?.Trim() ?? string.Empty;
            if (obj != null && propertyName != string.Empty)
            {
                var propInfo = obj.GetType().GetProperty(propertyName);
                if (propInfo != null)
                    if (propInfo.PropertyType.IsAssignableFrom(typeof(TResult)))
                        return ((TResult)propInfo.GetValue(obj));
            }

            return default(TResult);
        }
        /// <sumary>
        /// Obtiene el valor de una propieda de un objecto específico
        /// </sumary>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            propertyName = propertyName?.Trim() ?? string.Empty;
            if (obj != null && propertyName != string.Empty)
            {
                var propInfo = obj.GetType().GetProperty(propertyName);
                if (propInfo != null)
                    return propInfo.GetValue(obj);
            }

            return null;
        }
        #endregion
    }


    #region Entities
    /// <sumary>
    /// Archivo AC
    /// </sumary>
    public class ENT_ACFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// InvoiceNumber
        /// </sumary>
        private string _InvoiceNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string InvoiceNumber { get { return _InvoiceNumber; } set { _InvoiceNumber = ValidateValue<string>(value, nameof(InvoiceNumber)); } }
        /// <sumary>
        /// CodeProvider
        /// </sumary>
        private string _CodeProvider;
        [Order]
        [Regex(@"^[0-9]{12}$", "Sólo se permiten números y la longitud válida son 12 caracteres")] public string CodeProvider { get { return _CodeProvider; } set { _CodeProvider = ValidateValue<string>(value, nameof(CodeProvider)); } }
        /// <sumary>
        /// IdentificationType
        /// </sumary>
        private string _IdentificationType;
        [Order]
        [Regex(@"^(CC|CE|CD|PA|PE|RC|TI|CN|SC|AS|MS)$", "El valor ingresado no es válido. Valores válidos: CC, CE, CD, PA,PE, RC, TI, CN, SC, AS, MS")] public string IdentificationType { get { return _IdentificationType; } set { _IdentificationType = ValidateValue<string>(value, nameof(IdentificationType)); } }
        /// <sumary>
        /// IdentificationNumber
        /// </sumary>
        private string _IdentificationNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string IdentificationNumber { get { return _IdentificationNumber; } set { _IdentificationNumber = ValidateValue<string>(value, nameof(IdentificationNumber)); } }
        /// <sumary>
        /// ConsultationDate
        /// </sumary>
        private string _ConsultationDate;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string ConsultationDate { get { return _ConsultationDate; } set { _ConsultationDate = ValidateValue<string>(value, nameof(ConsultationDate)); } }
        /// <sumary>
        /// AuthorizationNumber
        /// </sumary>
        private string _AuthorizationNumber;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9 _-]{1,15}$", "La longitud debe estar entre 1 y 15 caracteres")] public string AuthorizationNumber { get { return _AuthorizationNumber; } set { _AuthorizationNumber = ValidateValue<string>(value, nameof(AuthorizationNumber)); } }
        /// <sumary>
        /// ConsultationCode
        /// </sumary>
        private string _ConsultationCode;
        [Order]
        [Regex(@"^.{1,8}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 8 caracteres")] public string ConsultationCode { get { return _ConsultationCode; } set { _ConsultationCode = ValidateValue<string>(value, nameof(ConsultationCode)); } }
        /// <sumary>
        /// ConsultationPurpose
        /// </sumary>
        private string _ConsultationPurpose;
        [Order]
        [Regex(@"^[0-9]{2}$", "Sólo se permiten números y la longitud válida son 2 caracteres")] public string ConsultationPurpose { get { return _ConsultationPurpose; } set { _ConsultationPurpose = ValidateValue<string>(value, nameof(ConsultationPurpose)); } }
        /// <sumary>
        /// ExternalCause
        /// </sumary>
        private string _ExternalCause;
        [Order]
        [Regex(@"^[0-9]{2}$", "Sólo se permiten números y la longitud válida son 2 caracteres")] public string ExternalCause { get { return _ExternalCause; } set { _ExternalCause = ValidateValue<string>(value, nameof(ExternalCause)); } }
        /// <sumary>
        /// PrincipalDiagnosisCode
        /// </sumary>
        private string _PrincipalDiagnosisCode;
        [Order]
        [Regex(@"^.{4}$", "La longitud válida son 4 caracteres")] public string PrincipalDiagnosisCode { get { return _PrincipalDiagnosisCode; } set { _PrincipalDiagnosisCode = ValidateValue<string>(value, nameof(PrincipalDiagnosisCode)); } }
        /// <sumary>
        /// RelationDiagnosisCodeOne
        /// </sumary>
        private string _RelationDiagnosisCodeOne;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud debe ser igual a 4 caracteres")] public string RelationDiagnosisCodeOne { get { return _RelationDiagnosisCodeOne; } set { _RelationDiagnosisCodeOne = ValidateValue<string>(value, nameof(RelationDiagnosisCodeOne)); } }
        /// <sumary>
        /// RelationDiagnosisCodeTwo
        /// </sumary>
        private string _RelationDiagnosisCodeTwo;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud debe ser igual a 4 caracteres")] public string RelationDiagnosisCodeTwo { get { return _RelationDiagnosisCodeTwo; } set { _RelationDiagnosisCodeTwo = ValidateValue<string>(value, nameof(RelationDiagnosisCodeTwo)); } }
        /// <sumary>
        /// RelationDiagnosisCodeThree
        /// </sumary>
        private string _RelationDiagnosisCodeThree;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud debe ser igual a 4 caracteres")] public string RelationDiagnosisCodeThree { get { return _RelationDiagnosisCodeThree; } set { _RelationDiagnosisCodeThree = ValidateValue<string>(value, nameof(RelationDiagnosisCodeThree)); } }
        /// <sumary>
        /// TypeMainDiagnosis
        /// </sumary>
        private string _TypeMainDiagnosis;
        [Order]
        [Regex(@"^[0-9]{1}$", "Sólo se permiten números y la longitud válida son 1 caracter")] public string TypeMainDiagnosis { get { return _TypeMainDiagnosis; } set { _TypeMainDiagnosis = ValidateValue<string>(value, nameof(TypeMainDiagnosis)); } }
        /// <sumary>
        /// ConsultationValue
        /// </sumary>
        private string _ConsultationValue;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string ConsultationValue { get { return _ConsultationValue; } set { _ConsultationValue = ValidateValue<string>(value, nameof(ConsultationValue)); } }
        /// <sumary>
        /// ModeratorValue
        /// </sumary>
        private string _ModeratorValue;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string ModeratorValue { get { return _ModeratorValue; } set { _ModeratorValue = ValidateValue<string>(value, nameof(ModeratorValue)); } }
        /// <sumary>
        /// PayValue
        /// </sumary>
        private string _PayValue;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string PayValue { get { return _PayValue; } set { _PayValue = ValidateValue<string>(value, nameof(PayValue)); } }
        #endregion

        #region Builders
        public ENT_ACFile() : base(null) { ExtrictValidation = false; }
        public ENT_ACFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Archivo AD
    /// </sumary>
    public class ENT_ADFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// InvoiceNumber
        /// </sumary>
        private string _InvoiceNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string InvoiceNumber { get { return _InvoiceNumber; } set { _InvoiceNumber = ValidateValue<string>(value, nameof(InvoiceNumber)); } }
        /// <sumary>
        /// CodeProvider
        /// </sumary>
        private string _CodeProvider;
        [Order]
        [Regex(@"^[0-9]{12}$", "Sólo se permiten números y la longitud válida son 12 caracteres")] public string CodeProvider { get { return _CodeProvider; } set { _CodeProvider = ValidateValue<string>(value, nameof(CodeProvider)); } }
        /// <sumary>
        /// ConceptCode
        /// </sumary>
        private string _ConceptCode;
        [Order]
        [Regex(@"^(01|02|03|04|05|06|07|08|09|10|11|12|13|14)$", "El valor ingresado no es válido. Valores válidos: 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14")] public string ConceptCode { get { return _ConceptCode; } set { _ConceptCode = ValidateValue<string>(value, nameof(ConceptCode)); } }
        /// <sumary>
        /// Amount
        /// </sumary>
        private string _Amount;
        [Order]
        [Regex(@"^[0-9]{1,15}$", "Solo se permiten números y la longitud debe estar entre 1 y 15 caracteres")] public string Amount { get { return _Amount; } set { _Amount = ValidateValue<string>(value, nameof(Amount)); } }
        /// <sumary>
        /// UnitValue
        /// </sumary>
        private string _UnitValue;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string UnitValue { get { return _UnitValue; } set { _UnitValue = ValidateValue<string>(value, nameof(UnitValue)); } }
        /// <sumary>
        /// TotalValueByConcept
        /// </sumary>
        private string _TotalValueByConcept;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string TotalValueByConcept { get { return _TotalValueByConcept; } set { _TotalValueByConcept = ValidateValue<string>(value, nameof(TotalValueByConcept)); } }
        #endregion

        #region Builders
        public ENT_ADFile() : base(null) { ExtrictValidation = false; }
        public ENT_ADFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Archivo AF
    /// </sumary>
    public class ENT_AFFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// CodeProvider
        /// </sumary>
        private string _CodeProvider;
        [Order]
        [Regex(@"[0-9*$]", "El campo no puede estar vacío y debe contener un caracter numérico")] public string CodeProvider { get { return _CodeProvider; } set { _CodeProvider = ValidateValue<string>(value, nameof(CodeProvider)); } }
        /// <sumary>
        /// NameProvider
        /// </sumary>
        private string _NameProvider;
        [Order]
        [Regex(@"^.{1,60}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 60 caracteres")] public string NameProvider { get { return _NameProvider; } set { _NameProvider = ValidateValue<string>(value, nameof(NameProvider)); } }
        /// <sumary>
        /// IdentificationType
        /// </sumary>
        private string _IdentificationType;
        [Order]
        [Regex(@"^(CC|CE|CD|PA|PE|NI)$", "El valor ingresado no es válido. Valores válidos: CC, CE, CD, PA, PE, NI")] public string IdentificationType { get { return _IdentificationType; } set { _IdentificationType = ValidateValue<string>(value, nameof(IdentificationType)); } }
        /// <sumary>
        /// IdentificatonProvider
        /// </sumary>
        private string _IdentificatonProvider;
        [Order]
        [Regex(@"^[0-9]{1,16}$", "Solo se permiten números y la longitud debe estar entre 1 y 16 caracteres")] public string IdentificatonProvider { get { return _IdentificatonProvider; } set { _IdentificatonProvider = ValidateValue<string>(value, nameof(IdentificatonProvider)); } }
        /// <sumary>
        /// InvoiceNumber
        /// </sumary>
        private string _InvoiceNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string InvoiceNumber { get { return _InvoiceNumber; } set { _InvoiceNumber = ValidateValue<string>(value, nameof(InvoiceNumber)); } }
        /// <sumary>
        /// ExpeditionInvoice
        /// </sumary>
        private string _ExpeditionInvoice;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string ExpeditionInvoice { get { return _ExpeditionInvoice; } set { _ExpeditionInvoice = ValidateValue<string>(value, nameof(ExpeditionInvoice)); } }
        /// <sumary>
        /// PeriodValidityStartDate
        /// </sumary>
        private string _PeriodValidityStartDate;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string PeriodValidityStartDate { get { return _PeriodValidityStartDate; } set { _PeriodValidityStartDate = ValidateValue<string>(value, nameof(PeriodValidityStartDate)); } }
        /// <sumary>
        /// PeriodValidityEndDate
        /// </sumary>
        private string _PeriodValidityEndDate;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string PeriodValidityEndDate { get { return _PeriodValidityEndDate; } set { _PeriodValidityEndDate = ValidateValue<string>(value, nameof(PeriodValidityEndDate)); } }
        /// <sumary>
        /// AdministeringEntityCode
        /// </sumary>
        private string _AdministeringEntityCode;
        [Order]
        [Regex(@"^.{5,6}$", "La longitud del campo debe ser 6 caracteres")] public string AdministeringEntityCode { get { return _AdministeringEntityCode; } set { _AdministeringEntityCode = ValidateValue<string>(value, nameof(AdministeringEntityCode)); } }
        /// <sumary>
        /// AdministeringEntityName
        /// </sumary>
        private string _AdministeringEntityName;
        [Order]
        [Regex(@"^.{1,30}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 30 caracteres")] public string AdministeringEntityName { get { return _AdministeringEntityName; } set { _AdministeringEntityName = ValidateValue<string>(value, nameof(AdministeringEntityName)); } }
        /// <sumary>
        /// ContractNumber
        /// </sumary>
        private string _ContractNumber;
        [Order]
        [Regex(@"^$|^.{1,15}$", "La longitud debe estar entre 1 y 15 caracteres")] public string ContractNumber { get { return _ContractNumber; } set { _ContractNumber = ValidateValue<string>(value, nameof(ContractNumber)); } }
        /// <sumary>
        /// BenefitsPlan
        /// </sumary>
        private string _BenefitsPlan;
        [Order]
        [Regex(@"^$|^.{1,30}$", "La longitud debe estar entre 1 y 30 caracteres")] public string BenefitsPlan { get { return _BenefitsPlan; } set { _BenefitsPlan = ValidateValue<string>(value, nameof(BenefitsPlan)); } }
        /// <sumary>
        /// PolicyNumber
        /// </sumary>
        private string _PolicyNumber;
        [Order]
        [Regex(@"^$|^.{1,10}$", "La longitud debe estar entre 1 y 10 caracteres")] public string PolicyNumber { get { return _PolicyNumber; } set { _PolicyNumber = ValidateValue<string>(value, nameof(PolicyNumber)); } }
        /// <sumary>
        /// CoPayment
        /// </sumary>
        private string _CoPayment;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string CoPayment { get { return _CoPayment; } set { _CoPayment = ValidateValue<string>(value, nameof(CoPayment)); } }
        /// <sumary>
        /// CommissionValue
        /// </sumary>
        private string _CommissionValue;
        [Order]
        [Regex(@"^$|^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string CommissionValue { get { return _CommissionValue; } set { _CommissionValue = ValidateValue<string>(value, nameof(CommissionValue)); } }
        /// <sumary>
        /// DiscountValue
        /// </sumary>
        private string _DiscountValue;
        [Order]
        [Regex(@"^$|^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string DiscountValue { get { return _DiscountValue; } set { _DiscountValue = ValidateValue<string>(value, nameof(DiscountValue)); } }
        /// <sumary>
        /// PayValue
        /// </sumary>
        private string _PayValue;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string PayValue { get { return _PayValue; } set { _PayValue = ValidateValue<string>(value, nameof(PayValue)); } }
        #endregion

        #region Builders
        public ENT_AFFile() : base(null) { ExtrictValidation = false; }
        public ENT_AFFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Archivo AF
    /// </sumary>
    public class ENT_AHFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// InvoiceNumber
        /// </sumary>
        private string _InvoiceNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string InvoiceNumber { get { return _InvoiceNumber; } set { _InvoiceNumber = ValidateValue<string>(value, nameof(InvoiceNumber)); } }
        /// <sumary>
        /// CodeProvider
        /// </sumary>
        private string _CodeProvider;
        [Order]
        [Regex(@"^[0-9]{12}$", "Sólo se permiten números y la longitud válida son 12 caracteres")] public string CodeProvider { get { return _CodeProvider; } set { _CodeProvider = ValidateValue<string>(value, nameof(CodeProvider)); } }
        /// <sumary>
        /// IdentificationType
        /// </sumary>
        private string _IdentificationType;
        [Order]
        [Regex(@"^(CC|CE|CD|PA|PE|RC|TI|CN|SC|AS|MS)$", "El valor ingresado no es válido. Valores válidos: CC, CE, CD, PA,PE, RC, TI, CN, SC, AS, MS")] public string IdentificationType { get { return _IdentificationType; } set { _IdentificationType = ValidateValue<string>(value, nameof(IdentificationType)); } }
        /// <sumary>
        /// IdentificationNumber
        /// </sumary>
        private string _IdentificationNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string IdentificationNumber { get { return _IdentificationNumber; } set { _IdentificationNumber = ValidateValue<string>(value, nameof(IdentificationNumber)); } }
        /// <sumary>
        /// InstitutionEntry
        /// </sumary>
        private string _InstitutionEntry;
        [Order]
        [Regex(@"^[0-9]{1}$", "Sólo se permiten números y la longitud válida son 1 caracter")] public string InstitutionEntry { get { return _InstitutionEntry; } set { _InstitutionEntry = ValidateValue<string>(value, nameof(InstitutionEntry)); } }
        /// <sumary>
        /// InstitutionEntryDate
        /// </sumary>
        private string _InstitutionEntryDate;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string InstitutionEntryDate { get { return _InstitutionEntryDate; } set { _InstitutionEntryDate = ValidateValue<string>(value, nameof(InstitutionEntryDate)); } }
        /// <sumary>
        /// InstitutionEntryHour
        /// </sumary>
        private string _InstitutionEntryHour;
        [Order]
        [Regex(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$", "Formato de Hora Inválido")] public string InstitutionEntryHour { get { return _InstitutionEntryHour; } set { _InstitutionEntryHour = ValidateValue<string>(value, nameof(InstitutionEntryHour)); } }
        /// <sumary>
        /// AuthorizationNumber
        /// </sumary>
        private string _AuthorizationNumber;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9 _-]{1,15}$", "La longitud debe estar entre 1 y 15 caracteres")] public string AuthorizationNumber { get { return _AuthorizationNumber; } set { _AuthorizationNumber = ValidateValue<string>(value, nameof(AuthorizationNumber)); } }
        /// <sumary>
        /// ExternalCause
        /// </sumary>
        private string _ExternalCause;
        [Order]
        [Regex(@"^[0-9]{2}$", "Sólo se permiten números y la longitud válida son 2 caracteres")] public string ExternalCause { get { return _ExternalCause; } set { _ExternalCause = ValidateValue<string>(value, nameof(ExternalCause)); } }
        /// <sumary>
        /// PrincipalDiagnosisCode
        /// </sumary>
        private string _PrincipalDiagnosisCode;
        [Order]
        [Regex(@"^[A-Za-z0-9]{4}$", "El campo no puede estar vacío y la longitud válida son 4 caracteres")] public string PrincipalDiagnosisCode { get { return _PrincipalDiagnosisCode; } set { _PrincipalDiagnosisCode = ValidateValue<string>(value, nameof(PrincipalDiagnosisCode)); } }
        /// <sumary>
        /// MainDiagnostic
        /// </sumary>
        private string _MainDiagnostic;
        [Order]
        [Regex(@"^[A-Za-z0-9]{4}$", "El campo no puede estar vacío y la longitud válida son 4 caracteres")] public string MainDiagnostic { get { return _MainDiagnostic; } set { _MainDiagnostic = ValidateValue<string>(value, nameof(MainDiagnostic)); } }
        /// <sumary>
        /// RelationMainDiagnosisOne
        /// </sumary>
        private string _RelationMainDiagnosisOne;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud debe ser igual a 4 caracteres")] public string RelationMainDiagnosisOne { get { return _RelationMainDiagnosisOne; } set { _RelationMainDiagnosisOne = ValidateValue<string>(value, nameof(RelationMainDiagnosisOne)); } }
        /// <sumary>
        /// RelationMainDiagnosisTwo
        /// </sumary>
        private string _RelationMainDiagnosisTwo;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud debe ser igual a 4 caracteres")] public string RelationMainDiagnosisTwo { get { return _RelationMainDiagnosisTwo; } set { _RelationMainDiagnosisTwo = ValidateValue<string>(value, nameof(RelationMainDiagnosisTwo)); } }
        /// <sumary>
        /// RelationMainDiagnosisThree
        /// </sumary>
        private string _RelationMainDiagnosisThree;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud debe ser igual a 4 caracteres")] public string RelationMainDiagnosisThree { get { return _RelationMainDiagnosisThree; } set { _RelationMainDiagnosisThree = ValidateValue<string>(value, nameof(RelationMainDiagnosisThree)); } }
        /// <sumary>
        /// ComplicationDiagnosis
        /// </sumary>
        private string _ComplicationDiagnosis;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud debe ser igual a 4 caracteres")] public string ComplicationDiagnosis { get { return _ComplicationDiagnosis; } set { _ComplicationDiagnosis = ValidateValue<string>(value, nameof(ComplicationDiagnosis)); } }
        /// <sumary>
        /// StatusExit
        /// </sumary>
        private string _StatusExit;
        [Order]
        [Regex(@"^[0-9]{1}$", "Sólo se permiten números y la longitud válida son 1 caracter")] public string StatusExit { get { return _StatusExit; } set { _StatusExit = ValidateValue<string>(value, nameof(StatusExit)); } }
        /// <sumary>
        /// DeathCauseDiagnosis
        /// </sumary>
        private string _DeathCauseDiagnosis;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud debe ser igual a 4 caracteres")] public string DeathCauseDiagnosis { get { return _DeathCauseDiagnosis; } set { _DeathCauseDiagnosis = ValidateValue<string>(value, nameof(DeathCauseDiagnosis)); } }
        /// <sumary>
        /// InstitutionDateExit
        /// </sumary>
        private string _InstitutionDateExit;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string InstitutionDateExit { get { return _InstitutionDateExit; } set { _InstitutionDateExit = ValidateValue<string>(value, nameof(InstitutionDateExit)); } }
        /// <sumary>
        /// InstitutionHourExit
        /// </sumary>
        private string _InstitutionHourExit;
        [Order]
        [Regex(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$", "Formato de Hora Inválido")] public string InstitutionHourExit { get { return _InstitutionHourExit; } set { _InstitutionHourExit = ValidateValue<string>(value, nameof(InstitutionHourExit)); } }
        #endregion

        #region Builders
        public ENT_AHFile() : base(null) { ExtrictValidation = false; }
        public ENT_AHFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Archivo AM
    /// </sumary>
    public class ENT_AMFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// InvoiceNumber
        /// </sumary>
        private string _InvoiceNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string InvoiceNumber { get { return _InvoiceNumber; } set { _InvoiceNumber = ValidateValue<string>(value, nameof(InvoiceNumber)); } }
        /// <sumary>
        /// CodeProvider
        /// </sumary>
        private string _CodeProvider;
        [Order]
        [Regex(@"^[0-9]{12}$", "Sólo se permiten números y la longitud válida son 12 caracteres")] public string CodeProvider { get { return _CodeProvider; } set { _CodeProvider = ValidateValue<string>(value, nameof(CodeProvider)); } }
        /// <sumary>
        /// IdentificationType
        /// </sumary>
        private string _IdentificationType;
        [Order]
        [Regex(@"^(CC|CE|CD|PA|PE|RC|TI|CN|SC|AS|MS)$", "El valor ingresado no es válido. Valores válidos: CC, CE, CD, PA,PE, RC, TI, CN, SC, AS, MS")] public string IdentificationType { get { return _IdentificationType; } set { _IdentificationType = ValidateValue<string>(value, nameof(IdentificationType)); } }
        /// <sumary>
        /// IdentificationNumber
        /// </sumary>
        private string _IdentificationNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string IdentificationNumber { get { return _IdentificationNumber; } set { _IdentificationNumber = ValidateValue<string>(value, nameof(IdentificationNumber)); } }
        /// <sumary>
        /// AuthorizationNumber
        /// </sumary>
        private string _AuthorizationNumber;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9 _-]{1,15}$", "La longitud debe estar entre 1 y 15 caracteres")] public string AuthorizationNumber { get { return _AuthorizationNumber; } set { _AuthorizationNumber = ValidateValue<string>(value, nameof(AuthorizationNumber)); } }
        /// <sumary>
        /// DrugsCode
        /// </sumary>
        private string _DrugsCode;
        [Order]
        [Regex(@"^[A-Za-z0-9 _-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string DrugsCode { get { return _DrugsCode; } set { _DrugsCode = ValidateValue<string>(value, nameof(DrugsCode)); } }
        /// <sumary>
        /// DrugsType
        /// </sumary>
        private string _DrugsType;
        [Order]
        [Regex(@"^[0-9]{1}$", "Sólo se permiten números y la longitud válida son 1 caracter")] public string DrugsType { get { return _DrugsType; } set { _DrugsType = ValidateValue<string>(value, nameof(DrugsType)); } }
        /// <sumary>
        /// DrugsGeneric
        /// </sumary>
        private string _DrugsGeneric;
        [Order]
        [Regex(@"^.{1,30}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 30 caracteres")] public string DrugsGeneric { get { return _DrugsGeneric; } set { _DrugsGeneric = ValidateValue<string>(value, nameof(DrugsGeneric)); } }
        /// <sumary>
        /// PharmaceuticalForm
        /// </sumary>
        private string _PharmaceuticalForm;
        [Order]
        [Regex(@"^.{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string PharmaceuticalForm { get { return _PharmaceuticalForm; } set { _PharmaceuticalForm = ValidateValue<string>(value, nameof(PharmaceuticalForm)); } }
        /// <sumary>
        /// DrugsConcentration
        /// </sumary>
        private string _DrugsConcentration;
        [Order]
        [Regex(@"^.{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string DrugsConcentration { get { return _DrugsConcentration; } set { _DrugsConcentration = ValidateValue<string>(value, nameof(DrugsConcentration)); } }
        /// <sumary>
        /// DrugsUnitofMeasure
        /// </sumary>
        private string _DrugsUnitofMeasure;
        [Order]
        [Regex(@"^.{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string DrugsUnitofMeasure { get { return _DrugsUnitofMeasure; } set { _DrugsUnitofMeasure = ValidateValue<string>(value, nameof(DrugsUnitofMeasure)); } }
        /// <sumary>
        /// DrugsAmount
        /// </sumary>
        private string _DrugsAmount;
        [Order]
        [Regex(@"^[0-9]{1,5}$", "Solo se permiten números y la longitud debe estar entre 1 y 5 caracteres")] public string DrugsAmount { get { return _DrugsAmount; } set { _DrugsAmount = ValidateValue<string>(value, nameof(DrugsAmount)); } }
        /// <sumary>
        /// DrugsUnitValue
        /// </sumary>
        private string _DrugsUnitValue;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string DrugsUnitValue { get { return _DrugsUnitValue; } set { _DrugsUnitValue = ValidateValue<string>(value, nameof(DrugsUnitValue)); } }
        /// <sumary>
        /// DrugsTotalValue
        /// </sumary>
        private string _DrugsTotalValue;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string DrugsTotalValue { get { return _DrugsTotalValue; } set { _DrugsTotalValue = ValidateValue<string>(value, nameof(DrugsTotalValue)); } }
        #endregion

        #region Builders
        public ENT_AMFile() : base(null) { ExtrictValidation = false; }
        public ENT_AMFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Archivo AN
    /// </sumary>
    public class ENT_ANFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// InvoiceNumber
        /// </sumary>
        private string _InvoiceNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string InvoiceNumber { get { return _InvoiceNumber; } set { _InvoiceNumber = ValidateValue<string>(value, nameof(InvoiceNumber)); } }
        /// <sumary>
        /// CodeProvider
        /// </sumary>
        private string _CodeProvider;
        [Order]
        [Regex(@"^[0-9]{12}$", "Sólo se permiten números y la longitud válida son 12 caracteres")] public string CodeProvider { get { return _CodeProvider; } set { _CodeProvider = ValidateValue<string>(value, nameof(CodeProvider)); } }
        /// <sumary>
        /// IdentificationType
        /// </sumary>
        private string _IdentificationType;
        [Order]
        [Regex(@"^(CC|CE|CD|PA|PE|RC|TI|CN|SC|AS|MS)$", "El valor ingresado no es válido. Valores válidos: CC, CE, CD, PA,PE, RC, TI, CN, SC, AS, MS")] public string IdentificationType { get { return _IdentificationType; } set { _IdentificationType = ValidateValue<string>(value, nameof(IdentificationType)); } }
        /// <sumary>
        /// IdentificationNumber
        /// </sumary>
        private string _IdentificationNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string IdentificationNumber { get { return _IdentificationNumber; } set { _IdentificationNumber = ValidateValue<string>(value, nameof(IdentificationNumber)); } }
        /// <sumary>
        /// NewbornDate
        /// </sumary>
        private string _NewbornDate;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string NewbornDate { get { return _NewbornDate; } set { _NewbornDate = ValidateValue<string>(value, nameof(NewbornDate)); } }
        /// <sumary>
        /// NewbornHour
        /// </sumary>
        private string _NewbornHour;
        [Order]
        [Regex(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$", "Formato de Hora Inválido")] public string NewbornHour { get { return _NewbornHour; } set { _NewbornHour = ValidateValue<string>(value, nameof(NewbornHour)); } }
        /// <sumary>
        /// GestationalAge
        /// </sumary>
        private string _GestationalAge;
        [Order]
        [Regex(@"^[0-9]{2}$", "Sólo se permiten números y la longitud válida son 2 caracteres")] public string GestationalAge { get { return _GestationalAge; } set { _GestationalAge = ValidateValue<string>(value, nameof(GestationalAge)); } }
        /// <sumary>
        /// PrenatalControl
        /// </sumary>
        private string _PrenatalControl;
        [Order]
        [Regex(@"^[0-9]{1}$", "Sólo se permiten números y la longitud válida son 1 caracter")] public string PrenatalControl { get { return _PrenatalControl; } set { _PrenatalControl = ValidateValue<string>(value, nameof(PrenatalControl)); } }
        /// <sumary>
        /// NewbornSex
        /// </sumary>
        private string _NewbornSex;
        [Order]
        [Regex(@"^[12]$", "El valor ingresado no es válido. Valores válidos: 1 = Masculino : 2 = Femenino")] public string NewbornSex { get { return _NewbornSex; } set { _NewbornSex = ValidateValue<string>(value, nameof(NewbornSex)); } }
        /// <sumary>
        /// NewbornWeight
        /// </sumary>
        private string _NewbornWeight;
        [Order]
        [Regex(@"^[0-9]{1,4}$", "Solo se permiten números y la longitud debe estar entre 1 y 4 caracteres")] public string NewbornWeight { get { return _NewbornWeight; } set { _NewbornWeight = ValidateValue<string>(value, nameof(NewbornWeight)); } }
        /// <sumary>
        /// NewbornDiagnosis
        /// </sumary>
        private string _NewbornDiagnosis;
        [Order]
        [Regex(@"^[A-Za-z0-9]{4}$", "Sólo se permiten números y letras, la longitud válida son 4 caracteres")] public string NewbornDiagnosis { get { return _NewbornDiagnosis; } set { _NewbornDiagnosis = ValidateValue<string>(value, nameof(NewbornDiagnosis)); } }
        /// <sumary>
        /// DeathCauseDiagnosis
        /// </sumary>
        private string _DeathCauseDiagnosis;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud debe ser igual a 4 caracteres")] public string DeathCauseDiagnosis { get { return _DeathCauseDiagnosis; } set { _DeathCauseDiagnosis = ValidateValue<string>(value, nameof(DeathCauseDiagnosis)); } }
        /// <sumary>
        /// DeathCauseDate
        /// </sumary>
        private string _DeathCauseDate;
        [Order]
        [Regex(@"^$|^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string DeathCauseDate { get { return _DeathCauseDate; } set { _DeathCauseDate = ValidateValue<string>(value, nameof(DeathCauseDate)); } }
        /// <sumary>
        /// DeathCauseHour
        /// </sumary>
        private string _DeathCauseHour;
        [Order]
        [Regex(@"^$|^([01]?[0-9]|2[0-3]):[0-5][0-9]$", "Formato de Hora Inválido")] public string DeathCauseHour { get { return _DeathCauseHour; } set { _DeathCauseHour = ValidateValue<string>(value, nameof(DeathCauseHour)); } }
        #endregion

        #region Builders
        public ENT_ANFile() : base(null) { ExtrictValidation = false; }
        public ENT_ANFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Archivo AP
    /// </sumary>
    public class ENT_APFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// InvoiceNumber
        /// </sumary>
        private string _InvoiceNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string InvoiceNumber { get { return _InvoiceNumber; } set { _InvoiceNumber = ValidateValue<string>(value, nameof(InvoiceNumber)); } }
        /// <sumary>
        /// CodeProvider
        /// </sumary>
        private string _CodeProvider;
        [Order]
        [Regex(@"^[0-9]{12}$", "Sólo se permiten números y la longitud válida son 12 caracteres")] public string CodeProvider { get { return _CodeProvider; } set { _CodeProvider = ValidateValue<string>(value, nameof(CodeProvider)); } }
        /// <sumary>
        /// IdentificationType
        /// </sumary>
        private string _IdentificationType;
        [Order]
        [Regex(@"^(CC|CE|CD|PA|PE|RC|TI|CN|SC|AS|MS)$", "El valor ingresado no es válido. Valores válidos: CC, CE, CD, PA,PE, RC, TI, CN, SC, AS, MS")] public string IdentificationType { get { return _IdentificationType; } set { _IdentificationType = ValidateValue<string>(value, nameof(IdentificationType)); } }
        /// <sumary>
        /// IdentificationNumber
        /// </sumary>
        private string _IdentificationNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string IdentificationNumber { get { return _IdentificationNumber; } set { _IdentificationNumber = ValidateValue<string>(value, nameof(IdentificationNumber)); } }
        /// <sumary>
        /// ProcedureDate
        /// </sumary>
        private string _ProcedureDate;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string ProcedureDate { get { return _ProcedureDate; } set { _ProcedureDate = ValidateValue<string>(value, nameof(ProcedureDate)); } }
        /// <sumary>
        /// AuthorizationNumber
        /// </sumary>
        private string _AuthorizationNumber;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9 _-]{1,15}$", "La longitud debe estar entre 1 y 15 caracteres")] public string AuthorizationNumber { get { return _AuthorizationNumber; } set { _AuthorizationNumber = ValidateValue<string>(value, nameof(AuthorizationNumber)); } }
        /// <sumary>
        /// ProcedureCode
        /// </sumary>
        private string _ProcedureCode;
        [Order]
        [Regex(@"^.{1,8}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 8 caracteres")] public string ProcedureCode { get { return _ProcedureCode; } set { _ProcedureCode = ValidateValue<string>(value, nameof(ProcedureCode)); } }
        /// <sumary>
        /// ProcedureScope
        /// </sumary>
        private string _ProcedureScope;
        [Order]
        [Regex(@"^[0-9]{1}$", "Sólo se permiten números y la longitud válida son 1 caracter")] public string ProcedureScope { get { return _ProcedureScope; } set { _ProcedureScope = ValidateValue<string>(value, nameof(ProcedureScope)); } }
        /// <sumary>
        /// ProcedurePurpose
        /// </sumary>
        private string _ProcedurePurpose;
        [Order]
        [Regex(@"^[0-9]{1}$", "Sólo se permiten números y la longitud válida son 1 caracter")] public string ProcedurePurpose { get { return _ProcedurePurpose; } set { _ProcedurePurpose = ValidateValue<string>(value, nameof(ProcedurePurpose)); } }
        /// <sumary>
        /// AttendPersonal
        /// </sumary>
        private string _AttendPersonal;
        [Order]
        [Regex(@"^$|^[0-9]{1}$", "Solo se permiten números y un caracter")] public string AttendPersonal { get { return _AttendPersonal; } set { _AttendPersonal = ValidateValue<string>(value, nameof(AttendPersonal)); } }
        /// <sumary>
        /// PrincipalDiagnosticCode
        /// </sumary>
        private string _PrincipalDiagnosticCode;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud válida son 4 caracteres")] public string PrincipalDiagnosticCode { get { return _PrincipalDiagnosticCode; } set { _PrincipalDiagnosticCode = ValidateValue<string>(value, nameof(PrincipalDiagnosticCode)); } }
        /// <sumary>
        /// RelationDiagnosisCodeOne
        /// </sumary>
        private string _RelationDiagnosisCodeOne;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud válida son 4 caracteres")] public string RelationDiagnosisCodeOne { get { return _RelationDiagnosisCodeOne; } set { _RelationDiagnosisCodeOne = ValidateValue<string>(value, nameof(RelationDiagnosisCodeOne)); } }
        /// <sumary>
        /// Complication
        /// </sumary>
        private string _Complication;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud válida son 4 caracteres")] public string Complication { get { return _Complication; } set { _Complication = ValidateValue<string>(value, nameof(Complication)); } }
        /// <sumary>
        /// ActSurgical
        /// </sumary>
        private string _ActSurgical;
        [Order]
        [Regex(@"^$|^[0-9]{1}$", "Sólo se permiten números y la longitud válida son 1 caracter")] public string ActSurgical { get { return _ActSurgical; } set { _ActSurgical = ValidateValue<string>(value, nameof(ActSurgical)); } }
        /// <sumary>
        /// PayProcedure
        /// </sumary>
        private string _PayProcedure;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string PayProcedure { get { return _PayProcedure; } set { _PayProcedure = ValidateValue<string>(value, nameof(PayProcedure)); } }
        #endregion

        #region Builders
        public ENT_APFile() : base(null) { ExtrictValidation = false; }
        public ENT_APFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// AT File
    /// </sumary>
    public class ENT_ATFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// InvoiceNumber
        /// </sumary>
        private string _InvoiceNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string InvoiceNumber { get { return _InvoiceNumber; } set { _InvoiceNumber = ValidateValue<string>(value, nameof(InvoiceNumber)); } }
        /// <sumary>
        /// CodeProvider
        /// </sumary>
        private string _CodeProvider;
        [Order]
        [Regex(@"^[0-9]{12}$", "Sólo se permiten números y la longitud válida son 12 caracteres")] public string CodeProvider { get { return _CodeProvider; } set { _CodeProvider = ValidateValue<string>(value, nameof(CodeProvider)); } }
        /// <sumary>
        /// IdentificationType
        /// </sumary>
        private string _IdentificationType;
        [Order]
        [Regex(@"^(CC|CE|CD|PA|PE|RC|TI|CN|SC|AS|MS)$", "El valor ingresado no es válido. Valores válidos: CC, CE, CD, PA,PE, RC, TI, CN, SC, AS, MS")] public string IdentificationType { get { return _IdentificationType; } set { _IdentificationType = ValidateValue<string>(value, nameof(IdentificationType)); } }
        /// <sumary>
        /// IdentificationNumber
        /// </sumary>
        private string _IdentificationNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string IdentificationNumber { get { return _IdentificationNumber; } set { _IdentificationNumber = ValidateValue<string>(value, nameof(IdentificationNumber)); } }
        /// <sumary>
        /// AuthorizationNumber
        /// </sumary>
        private string _AuthorizationNumber;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9 _-]{1,15}$", "La longitud debe estar entre 1 y 15 caracteres")] public string AuthorizationNumber { get { return _AuthorizationNumber; } set { _AuthorizationNumber = ValidateValue<string>(value, nameof(AuthorizationNumber)); } }
        /// <sumary>
        /// ServiceType
        /// </sumary>
        private string _ServiceType;
        [Order]
        [Regex(@"^(1|2|3|4)$", "El valor ingresado no es válido. Valores válidos: 1,2,3,4")] public string ServiceType { get { return _ServiceType; } set { _ServiceType = ValidateValue<string>(value, nameof(ServiceType)); } }
        /// <sumary>
        /// ServiceCode
        /// </sumary>
        private string _ServiceCode;
        [Order]
        [Regex(@"^[A-Za-z0-9 _-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string ServiceCode { get { return _ServiceCode; } set { _ServiceCode = ValidateValue<string>(value, nameof(ServiceCode)); } }
        /// <sumary>
        /// ServiceName
        /// </sumary>
        private string _ServiceName;
        [Order]
        [Regex(@"^.{1,60}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 60 caracteres")] public string ServiceName { get { return _ServiceName; } set { _ServiceName = ValidateValue<string>(value, nameof(ServiceName)); } }
        /// <sumary>
        /// ServiceAmount
        /// </sumary>
        private string _ServiceAmount;
        [Order]
        [Regex(@"^[0-9]{1,5}$", "Solo se permiten números y la longitud debe estar entre 1 y 5 caracteres")] public string ServiceAmount { get { return _ServiceAmount; } set { _ServiceAmount = ValidateValue<string>(value, nameof(ServiceAmount)); } }
        /// <sumary>
        /// ServiceUnitValue
        /// </sumary>
        private string _ServiceUnitValue;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string ServiceUnitValue { get { return _ServiceUnitValue; } set { _ServiceUnitValue = ValidateValue<string>(value, nameof(ServiceUnitValue)); } }
        /// <sumary>
        /// ServiceTotalValue
        /// </sumary>
        private string _ServiceTotalValue;
        [Order]
        [Regex(@"^(?=[0-9.]{1,15}$)[0-9]+([.][0-9]{1,4})?$", "Solo se permiten números con 4 decimales y la longitud debe estar entre 1 y 15 caracteres")] public string ServiceTotalValue { get { return _ServiceTotalValue; } set { _ServiceTotalValue = ValidateValue<string>(value, nameof(ServiceTotalValue)); } }
        #endregion

        #region Builders
        public ENT_ATFile() : base(null) { ExtrictValidation = false; }
        public ENT_ATFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Archivo AU
    /// </sumary>
    public class ENT_AUFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// InvoiceNumber
        /// </sumary>
        private string _InvoiceNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string InvoiceNumber { get { return _InvoiceNumber; } set { _InvoiceNumber = ValidateValue<string>(value, nameof(InvoiceNumber)); } }
        /// <sumary>
        /// CodeProvider
        /// </sumary>
        private string _CodeProvider;
        [Order]
        [Regex(@"^[0-9]{12}$", "Sólo se permiten números y la longitud válida son 12 caracteres")] public string CodeProvider { get { return _CodeProvider; } set { _CodeProvider = ValidateValue<string>(value, nameof(CodeProvider)); } }
        /// <sumary>
        /// IdentificationType
        /// </sumary>
        private string _IdentificationType;
        [Order]
        [Regex(@"^(CC|CE|CD|PA|PE|RC|TI|CN|SC|AS|MS)$", "El valor ingresado no es válido. Valores válidos: CC, CE, CD, PA,PE, RC, TI, CN, SC, AS, MS")] public string IdentificationType { get { return _IdentificationType; } set { _IdentificationType = ValidateValue<string>(value, nameof(IdentificationType)); } }
        /// <sumary>
        /// IdentificationNumber
        /// </sumary>
        private string _IdentificationNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string IdentificationNumber { get { return _IdentificationNumber; } set { _IdentificationNumber = ValidateValue<string>(value, nameof(IdentificationNumber)); } }
        /// <sumary>
        /// ObservationDate
        /// </sumary>
        private string _ObservationDate;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string ObservationDate { get { return _ObservationDate; } set { _ObservationDate = ValidateValue<string>(value, nameof(ObservationDate)); } }
        /// <sumary>
        /// ObservationHour
        /// </sumary>
        private string _ObservationHour;
        [Order]
        [Regex(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$", "Formato de Hora Inválido")] public string ObservationHour { get { return _ObservationHour; } set { _ObservationHour = ValidateValue<string>(value, nameof(ObservationHour)); } }
        /// <sumary>
        /// AuthorizationNumber
        /// </sumary>
        private string _AuthorizationNumber;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9 _-]{1,15}$", "La longitud debe estar entre 1 y 15 caracteres")] public string AuthorizationNumber { get { return _AuthorizationNumber; } set { _AuthorizationNumber = ValidateValue<string>(value, nameof(AuthorizationNumber)); } }
        /// <sumary>
        /// ExternalCause
        /// </sumary>
        private string _ExternalCause;
        [Order]
        [Regex(@"^[0-9]{2}$", "Sólo se permiten números y la longitud válida son 2 caracteres")] public string ExternalCause { get { return _ExternalCause; } set { _ExternalCause = ValidateValue<string>(value, nameof(ExternalCause)); } }
        /// <sumary>
        /// DiagnosisEnd
        /// </sumary>
        private string _DiagnosisEnd;
        [Order]
        [Regex(@"^[A-Za-z0-9]{4}$", "El campo no puede estar vacío y la longitud válida son 4 caracteres")] public string DiagnosisEnd { get { return _DiagnosisEnd; } set { _DiagnosisEnd = ValidateValue<string>(value, nameof(DiagnosisEnd)); } }
        /// <sumary>
        /// RelationDiagnosisExitOne
        /// </sumary>
        private string _RelationDiagnosisExitOne;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud válida son 4 caracteres")] public string RelationDiagnosisExitOne { get { return _RelationDiagnosisExitOne; } set { _RelationDiagnosisExitOne = ValidateValue<string>(value, nameof(RelationDiagnosisExitOne)); } }
        /// <sumary>
        /// RelationDiagnosisExitTwo
        /// </sumary>
        private string _RelationDiagnosisExitTwo;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud válida son 4 caracteres")] public string RelationDiagnosisExitTwo { get { return _RelationDiagnosisExitTwo; } set { _RelationDiagnosisExitTwo = ValidateValue<string>(value, nameof(RelationDiagnosisExitTwo)); } }
        /// <sumary>
        /// RelationDiagnosisExitThree
        /// </sumary>
        private string _RelationDiagnosisExitThree;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud válida son 4 caracteres")] public string RelationDiagnosisExitThree { get { return _RelationDiagnosisExitThree; } set { _RelationDiagnosisExitThree = ValidateValue<string>(value, nameof(RelationDiagnosisExitThree)); } }
        /// <sumary>
        /// ObservationDestination
        /// </sumary>
        private string _ObservationDestination;
        [Order]
        [Regex(@"^[0-9]{1}$", "Sólo se permiten números y la longitud válida son 1 caracter")] public string ObservationDestination { get { return _ObservationDestination; } set { _ObservationDestination = ValidateValue<string>(value, nameof(ObservationDestination)); } }
        /// <sumary>
        /// StatusExit
        /// </sumary>
        private string _StatusExit;
        [Order]
        [Regex(@"^[0-9]$", "Sólo se permiten números y la longitud válida es 1 caracter")] public string StatusExit { get { return _StatusExit; } set { _StatusExit = ValidateValue<string>(value, nameof(StatusExit)); } }
        /// <sumary>
        /// DeathCause
        /// </sumary>
        private string _DeathCause;
        [Order]
        [Regex(@"^$|^[A-Za-z0-9]{4}$", "La longitud debe ser igual a 4 caracteres")] public string DeathCause { get { return _DeathCause; } set { _DeathCause = ValidateValue<string>(value, nameof(DeathCause)); } }
        /// <sumary>
        /// ObservationDateExit
        /// </sumary>
        private string _ObservationDateExit;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresada no es correcto")] public string ObservationDateExit { get { return _ObservationDateExit; } set { _ObservationDateExit = ValidateValue<string>(value, nameof(ObservationDateExit)); } }
        /// <sumary>
        /// ObservationHourExit
        /// </sumary>
        private string _ObservationHourExit;
        [Order]
        [Regex(@"^$|^([01]?[0-9]|2[0-3]):[0-5][0-9]$", "El formato de hora es inválido")] public string ObservationHourExit { get { return _ObservationHourExit; } set { _ObservationHourExit = ValidateValue<string>(value, nameof(ObservationHourExit)); } }
        #endregion

        #region Builders
        public ENT_AUFile() : base(null) { ExtrictValidation = false; }
        public ENT_AUFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Encapsula los datos de respuesta de la ejecución de un servicio web
    /// </sumary>
    public class ENT_ActionResult : EntityBase
    {
        #region Properties
        /// <sumary>
        /// IsSuccessful
        /// </sumary>
        private bool _IsSuccessful;
        [Order]
        public bool IsSuccessful { get { return _IsSuccessful; } set { _IsSuccessful = ValidateValue<bool>(value, nameof(IsSuccessful)); } }
        /// <sumary>
        /// IsError
        /// </sumary>
        private bool _IsError;
        [Order]
        public bool IsError { get { return _IsError; } set { _IsError = ValidateValue<bool>(value, nameof(IsError)); } }
        /// <sumary>
        /// ErrorMessage
        /// </sumary>
        private string _ErrorMessage;
        [Order]
        public string ErrorMessage { get { return _ErrorMessage; } set { _ErrorMessage = ValidateValue<string>(value, nameof(ErrorMessage)); } }
        /// <sumary>
        /// Messages
        /// </sumary>
        private List<string> _Messages;
        [Order]
        public List<string> Messages { get { return _Messages; } set { _Messages = ValidateValue<List<string>>(value, nameof(Messages)); } }
        /// <sumary>
        /// Result
        /// </sumary>
        private object _Result;
        [Order]
        public object Result { get { return _Result; } set { _Result = ValidateValue<object>(value, nameof(Result)); } }
        /// <sumary>
        /// IsSucessfull
        /// </sumary>
        private bool _IsSucessfull;
        [Order]
        public bool IsSucessfull { get { return _IsSucessfull; } set { _IsSucessfull = ValidateValue<bool>(value, nameof(IsSucessfull)); } }
        /// <sumary>
        /// FileName
        /// </sumary>
        private string _FileName;
        [Order]
        public string FileName { get { return _FileName; } set { _FileName = ValidateValue<string>(value, nameof(FileName)); } }
        #endregion

        #region Builders
        public ENT_ActionResult() : base(null) { ExtrictValidation = true; }
        public ENT_ActionResult(object obj) : base(obj) { ExtrictValidation = true; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Archivo CT
    /// </sumary>
    public class ENT_CTFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// CodeProvider
        /// </sumary>
        private string _CodeProvider;
        [Order]
        [Regex(@"^[0-9]{12}(\|[0-9]{12})*$", "Sólo se permiten números, separados o no por (|) y la longitud válida son 12 caracteres por valor")] public string CodeProvider { get { return _CodeProvider; } set { _CodeProvider = ValidateValue<string>(value, nameof(CodeProvider)); } }
        /// <sumary>
        /// ReceivedDate
        /// </sumary>
        private string _ReceivedDate;
        [Order]
        [Regex(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", "El formato de la fecha ingresado no es correcto")] public string ReceivedDate { get { return _ReceivedDate; } set { _ReceivedDate = ValidateValue<string>(value, nameof(ReceivedDate)); } }
        /// <sumary>
        /// FileCode
        /// </sumary>
        private string _FileCode;
        [Order]
        [Regex(@"^.{1,10}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 10 caracteres")] public string FileCode { get { return _FileCode; } set { _FileCode = ValidateValue<string>(value, nameof(FileCode)); } }
        /// <sumary>
        /// TotalRecord
        /// </sumary>
        private string _TotalRecord;
        [Order]
        [Regex(@"^[0-9]{1,9}$", "Solo se permiten números y la longitud debe estar entre 1 y 9 caracteres")] public string TotalRecord { get { return _TotalRecord; } set { _TotalRecord = ValidateValue<string>(value, nameof(TotalRecord)); } }
        #endregion

        #region Builders
        public ENT_CTFile() : base(null) { ExtrictValidation = false; }
        public ENT_CTFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Entidad con parámetros de entrada utilizados en MainRIPS
    /// </sumary>
    public class ENT_RipsParameters : EntityBase
    {
        #region Properties
        /// <sumary>
        /// CompanyId
        /// </sumary>
        private long _CompanyId;
        [Order]
        public long CompanyId { get { return _CompanyId; } set { _CompanyId = ValidateValue<long>(value, nameof(CompanyId)); } }
        /// <sumary>
        /// OperatorId
        /// </sumary>
        private long _OperatorId;
        [Order]
        public long OperatorId { get { return _OperatorId; } set { _OperatorId = ValidateValue<long>(value, nameof(OperatorId)); } }
        /// <sumary>
        /// CaseNumber
        /// </sumary>
        private string _CaseNumber;
        [Order]
        public string CaseNumber { get { return _CaseNumber; } set { _CaseNumber = ValidateValue<string>(value, nameof(CaseNumber)); } }
        /// <sumary>
        /// LibraryId
        /// </sumary>
        private long _LibraryId;
        [Order]
        public long LibraryId { get { return _LibraryId; } set { _LibraryId = ValidateValue<long>(value, nameof(LibraryId)); } }
        /// <sumary>
        /// InitDate
        /// </sumary>
        private DateTime _InitDate;
        [Order]
        public DateTime InitDate { get { return _InitDate; } set { _InitDate = ValidateValue<DateTime>(value, nameof(InitDate)); } }
        /// <sumary>
        /// EndDate
        /// </sumary>
        private DateTime _EndDate;
        [Order]
        public DateTime EndDate { get { return _EndDate; } set { _EndDate = ValidateValue<DateTime>(value, nameof(EndDate)); } }
        /// <sumary>
        /// IdTypePopulation
        /// </sumary>
        private long _IdTypePopulation;
        [Order]
        public long IdTypePopulation { get { return _IdTypePopulation; } set { _IdTypePopulation = ValidateValue<long>(value, nameof(IdTypePopulation)); } }
        /// <sumary>
        /// Establishment
        /// </sumary>
        private long _Establishment;
        [Order]
        public long Establishment { get { return _Establishment; } set { _Establishment = ValidateValue<long>(value, nameof(Establishment)); } }
        /// <sumary>
        /// Código de seguimiento
        /// </sumary>
        private string _TrackingCode;
        [Order]
        public string TrackingCode { get { return _TrackingCode; } set { _TrackingCode = ValidateValue<string>(value, nameof(TrackingCode)); } }
        /// <sumary>
        /// UserCode
        /// </sumary>
        private string _UserCode;
        [Order]
        public string UserCode { get { return _UserCode; } set { _UserCode = ValidateValue<string>(value, nameof(UserCode)); } }
        /// <sumary>
        /// ACFileId
        /// </sumary>
        private string _ACFileId;
        [Order]
        public string ACFileId { get { return _ACFileId; } set { _ACFileId = ValidateValue<string>(value, nameof(ACFileId)); } }
        /// <sumary>
        /// AFFileId
        /// </sumary>
        private string _AFFileId;
        [Order]
        public string AFFileId { get { return _AFFileId; } set { _AFFileId = ValidateValue<string>(value, nameof(AFFileId)); } }
        /// <sumary>
        /// AHFileId
        /// </sumary>
        private string _AHFileId;
        [Order]
        public string AHFileId { get { return _AHFileId; } set { _AHFileId = ValidateValue<string>(value, nameof(AHFileId)); } }
        /// <sumary>
        /// AMFileId
        /// </sumary>
        private string _AMFileId;
        [Order]
        public string AMFileId { get { return _AMFileId; } set { _AMFileId = ValidateValue<string>(value, nameof(AMFileId)); } }
        /// <sumary>
        /// ANFileId
        /// </sumary>
        private string _ANFileId;
        [Order]
        public string ANFileId { get { return _ANFileId; } set { _ANFileId = ValidateValue<string>(value, nameof(ANFileId)); } }
        /// <sumary>
        /// APFileId
        /// </sumary>
        private string _APFileId;
        [Order]
        public string APFileId { get { return _APFileId; } set { _APFileId = ValidateValue<string>(value, nameof(APFileId)); } }
        /// <sumary>
        /// ATFileId
        /// </sumary>
        private string _ATFileId;
        [Order]
        public string ATFileId { get { return _ATFileId; } set { _ATFileId = ValidateValue<string>(value, nameof(ATFileId)); } }
        /// <sumary>
        /// AUFileId
        /// </sumary>
        private string _AUFileId;
        [Order]
        public string AUFileId { get { return _AUFileId; } set { _AUFileId = ValidateValue<string>(value, nameof(AUFileId)); } }
        /// <sumary>
        /// CTFileId
        /// </sumary>
        private string _CTFileId;
        [Order]
        public string CTFileId { get { return _CTFileId; } set { _CTFileId = ValidateValue<string>(value, nameof(CTFileId)); } }
        /// <sumary>
        /// USFileId
        /// </sumary>
        private string _USFileId;
        [Order]
        public string USFileId { get { return _USFileId; } set { _USFileId = ValidateValue<string>(value, nameof(USFileId)); } }
        /// <sumary>
        /// ADFileId
        /// </sumary>
        private string _ADFileId;
        [Order]
        public string ADFileId { get { return _ADFileId; } set { _ADFileId = ValidateValue<string>(value, nameof(ADFileId)); } }
        #endregion

        #region Builders
        public ENT_RipsParameters() : base(null) { ExtrictValidation = true; }
        public ENT_RipsParameters(object obj) : base(obj) { ExtrictValidation = true; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Archivo US
    /// </sumary>
    public class ENT_USFile : EntityBase
    {
        #region Properties
        /// <sumary>
        /// IdentificationType
        /// </sumary>
        private string _IdentificationType;
        [Order]
        [Regex(@"^(CC|CE|CD|PA|PE|RC|TI|CN|SC|AS|MS)$", "El valor ingresado no es válido. Valores válidos: CC, CE, CD, PA, PE, RC, TI, CN, SC, AS, MS")] public string IdentificationType { get { return _IdentificationType; } set { _IdentificationType = ValidateValue<string>(value, nameof(IdentificationType)); } }
        /// <sumary>
        /// IdentificationNumber
        /// </sumary>
        private string _IdentificationNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string IdentificationNumber { get { return _IdentificationNumber; } set { _IdentificationNumber = ValidateValue<string>(value, nameof(IdentificationNumber)); } }
        /// <sumary>
        /// AdministeringEntityCode
        /// </sumary>
        private string _AdministeringEntityCode;
        [Order]
        [Regex(@"^.{5,6}$", "La longitud válida son 5 a 6 caracteres")] public string AdministeringEntityCode { get { return _AdministeringEntityCode; } set { _AdministeringEntityCode = ValidateValue<string>(value, nameof(AdministeringEntityCode)); } }
        /// <sumary>
        /// TypePerson
        /// </sumary>
        private string _TypePerson;
        [Order]
        [Regex(@"^[1-8]{1}$", "Solo se permite números entre 1 y 8, y la longitud debe ser 1 carácter")] public string TypePerson { get { return _TypePerson; } set { _TypePerson = ValidateValue<string>(value, nameof(TypePerson)); } }
        /// <sumary>
        /// FirstLastName
        /// </sumary>
        private string _FirstLastName;
        [Order]
        [Regex(@"^.{1,30}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 30 caracteres")] public string FirstLastName { get { return _FirstLastName; } set { _FirstLastName = ValidateValue<string>(value, nameof(FirstLastName)); } }
        /// <sumary>
        /// SecondLastName
        /// </sumary>
        private string _SecondLastName;
        [Order]
        [Regex(@"^$|^.{1,30}$", "La longitud debe estar entre 1 y 30 caracteres")] public string SecondLastName { get { return _SecondLastName; } set { _SecondLastName = ValidateValue<string>(value, nameof(SecondLastName)); } }
        /// <sumary>
        /// FirstName
        /// </sumary>
        private string _FirstName;
        [Order]
        [Regex(@"^.{1,20}$", "El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string FirstName { get { return _FirstName; } set { _FirstName = ValidateValue<string>(value, nameof(FirstName)); } }
        /// <sumary>
        /// SecondName
        /// </sumary>
        private string _SecondName;
        [Order]
        [Regex(@"^$|^.{1,20}$", "La longitud debe estar entre 1 y 20 caracteres")] public string SecondName { get { return _SecondName; } set { _SecondName = ValidateValue<string>(value, nameof(SecondName)); } }
        /// <sumary>
        /// AgePerson
        /// </sumary>
        private string _AgePerson;
        [Order]
        [Regex(@"^([1-9]|[1-8][0-9]|9[0-9]|1[01][0-9]|120)$", "Sólo se permite números entre 1 y 120, y la longitud debe ser 3 caracteres")] public string AgePerson { get { return _AgePerson; } set { _AgePerson = ValidateValue<string>(value, nameof(AgePerson)); } }
        /// <sumary>
        /// AgeUnitOfMeasurement
        /// </sumary>
        private string _AgeUnitOfMeasurement;
        [Order]
        [Regex(@"^[1-3]{1}$", "Solo se permite números entre 1 y 3, y la longitud debe ser 1 carácter")] public string AgeUnitOfMeasurement { get { return _AgeUnitOfMeasurement; } set { _AgeUnitOfMeasurement = ValidateValue<string>(value, nameof(AgeUnitOfMeasurement)); } }
        /// <sumary>
        /// SexPerson
        /// </sumary>
        private string _SexPerson;
        [Order]
        [Regex(@"^[FMfm]$", "El valor ingresado no es válido. Valores válidos: F,M ó f,m")] public string SexPerson { get { return _SexPerson; } set { _SexPerson = ValidateValue<string>(value, nameof(SexPerson)); } }
        /// <sumary>
        /// CodeTown
        /// </sumary>
        private string _CodeTown;
        [Order]
        [Regex(@"^[0-9]{2}$", "Sólo se permiten números y la longitud válida son 2 caracteres")] public string CodeTown { get { return _CodeTown; } set { _CodeTown = ValidateValue<string>(value, nameof(CodeTown)); } }
        /// <sumary>
        /// CodeCity
        /// </sumary>
        private string _CodeCity;
        [Order]
        [Regex(@"^[0-9]{3}$", "Sólo se permiten números y la longitud válida son 3 caracteres")] public string CodeCity { get { return _CodeCity; } set { _CodeCity = ValidateValue<string>(value, nameof(CodeCity)); } }
        /// <sumary>
        /// ZoneTown
        /// </sumary>
        private string _ZoneTown;
        [Order]
        [Regex(@"^[URur]$", "El valor ingresado no es válido. Valores válidos: U,R ó u,r")] public string ZoneTown { get { return _ZoneTown; } set { _ZoneTown = ValidateValue<string>(value, nameof(ZoneTown)); } }
        #endregion

        #region Builders
        public ENT_USFile() : base(null) { ExtrictValidation = false; }
        public ENT_USFile(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    #endregion

    #region Base
    public class EntityBase
    {
        public bool ExtrictValidation;
        public List<string> ValidationErrorsList = new List<string>();
        public EntityBase(object obj)
        {
            if (obj == null) return;

            try
            {
                var properties = this.GetType().GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    bool vlid = true;
                    switch (GetDataType(properties[i].PropertyType.Name, properties[i].PropertyType.FullName, out bool isNulleable))
                    {
                        case "Boolean":
                            vlid = bool.TryParse((obj as dynamic)[properties[i].Name].ToString(), out bool vb);
                            if (vlid || (isNulleable && (obj as dynamic)[properties[i].Name] == null)) properties[i].SetValue(this, vb);
                            else if (isNulleable && (obj as dynamic)[properties[i].Name] == null) { properties[i].SetValue(this, null); }
                            break;
                        case "Int16":
                            vlid = short.TryParse((obj as dynamic)[properties[i].Name].ToString(), out short vs);
                            if (vlid || (isNulleable && (obj as dynamic)[properties[i].Name] == null)) properties[i].SetValue(this, vs);
                            else if (isNulleable && (obj as dynamic)[properties[i].Name] == null) { properties[i].SetValue(this, null); }
                            break;
                        case "Int32":
                            vlid = int.TryParse((obj as dynamic)[properties[i].Name].ToString(), out int vi);
                            if (vlid || (isNulleable && (obj as dynamic)[properties[i].Name] == null)) properties[i].SetValue(this, vi);
                            else if (isNulleable && (obj as dynamic)[properties[i].Name] == null) { properties[i].SetValue(this, null); }
                            break;
                        case "Int64":
                            vlid = long.TryParse((obj as dynamic)[properties[i].Name].ToString(), out long vl);
                            if (vlid || (isNulleable && (obj as dynamic)[properties[i].Name] == null)) properties[i].SetValue(this, vl);
                            else if (isNulleable && (obj as dynamic)[properties[i].Name] == null) { properties[i].SetValue(this, null); }
                            break;
                        case "Double":
                            vlid = double.TryParse((obj as dynamic)[properties[i].Name].ToString(), out double vd);
                            if (vlid) properties[i].SetValue(this, vd);
                            else if (isNulleable && (obj as dynamic)[properties[i].Name] == null) { properties[i].SetValue(this, null); }
                            break;
                        case "DateTime":
                            vlid = DateTime.TryParse((obj as dynamic)[properties[i].Name].ToString(), out DateTime vt);
                            if (vlid || (isNulleable && (obj as dynamic)[properties[i].Name] == null)) properties[i].SetValue(this, vt);
                            else if (isNulleable && (obj as dynamic)[properties[i].Name] == null) { properties[i].SetValue(this, null); }
                            break;
                        case "String":
                            properties[i].SetValue(this, ((obj as dynamic)[properties[i].Name] as JValue)?.ToObject(properties[i].PropertyType));
                            break;
                        case "IEnumerable":
                            properties[i].SetValue(this, ((obj as dynamic)[properties[i].Name] as JArray)?.ToObject(properties[i].PropertyType));
                            break;
                        default:
                            properties[i].SetValue(this, Activator.CreateInstance(properties[i].PropertyType, new object[] { (obj as dynamic)[properties[i].Name] }));
                            break;
                    }

                    if (!vlid)
                        ValidationErrorsList.Add(string.Format("El valor de la propiedad '{0}' no es válido.", properties[i].Name));
                }
            }
            catch (Exception ex) { throw ex; }

            if (ExtrictValidation && ValidationErrorsList.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, ValidationErrorsList));
        }

        /// <summary>
        /// Define el tipo de dato usado por la propiedad
        /// </summary>
        /// <param name="name">Nombre de la propiedad</param>
        /// <param name="fullName">Nombre completo de la propiedad</param>
        /// <param name="isNulleable">salida que define si es poosible ser nula la propiedad</param>
        /// <returns>Nombre del listado</returns>
        private string GetDataType(string name, string fullName, out bool isNulleable)
        {
            isNulleable = false;

            if (name.Contains("Nullable"))
            {
                isNulleable = true;
                name = Regex.Match(fullName, "Int16|Int32|Int64|Double|DateTime|String").ToString();
            }
            else if (name.Contains("List") || name.Contains("IEnumerable") || name.Contains("ICollection") || name.Contains("IList"))
            {
                name = "IEnumerable";
            }

            return name;
        }

        /// <summary>
        /// Valida los valores de un Listado de tipo T
        /// </summary>
        /// <typeparam name="T">Tipo de objeto</typeparam>
        /// <param name="value">valor asignable al objeto</param>
        /// <param name="propName">nombre</param>
        /// <returns>Listado de valores</returns>
        public IEnumerable<T> ValidateValue<T>(IEnumerable<T> values, string propName)
        {
            foreach (var value in values)
                ValidateValue<T>(value, propName);
            return values;
        }

        /// <summary>
        /// Valida el valor de un tipo T
        /// </summary>
        /// <typeparam name="T">Tipo de objeto</typeparam>
        /// <param name="value">valor asignable al objeto</param>
        /// <param name="propName">nombre</param>
        /// <returns>Valor</returns>
        public T ValidateValue<T>(T value, string propName)
        {
            var dataType = typeof(T).Name;
            var attrs = this.GetType().GetProperty(propName).GetCustomAttributes(true);

            for (int i = 0; i < attrs.Length; i++)
            {
                var attrName = attrs[i].GetType().Name;

                if (attrName == nameof(FunctionAttribute))
                {
                    if (!InvokeFunction(((FunctionAttribute)attrs[i]).Value, value))
                    {
                        ValidationErrorsList.Add(string.Format("El valor de la propiedad '{0}' no cumple los parámetros de validación {1}.", propName, "No se cumple la función de validación "));
                    }
                }

                switch (dataType)
                {
                    case "Int64":
                        if (attrName == nameof(MinValueAttribute))
                            if (Convert.ToInt64(value) < ((MinValueAttribute)attrs[i]).Value)
                                ValidationErrorsList.Add(string.Format("El valor de la propiedad '{0}' no cumple los parámetros de validación {1}.", propName, "Valor mínimo "));

                        if (attrName == nameof(MaxValueAttribute))
                            if (Convert.ToInt64(value) > ((MaxValueAttribute)attrs[i]).Value)
                                ValidationErrorsList.Add(string.Format("El valor de la propiedad '{0}' no cumple los parámetros de validación {1}.", propName, "Valor máximo "));
                        break;
                    case "String":
                        if (attrName == nameof(LengthAttribute))
                            if (Convert.ToString(value).Length > ((LengthAttribute)attrs[i]).Value)
                                ValidationErrorsList.Add(string.Format("El valor de la propiedad '{0}' no cumple los parámetros de validación {1}.", propName, "Longitud "));

                        if (attrName == nameof(RegexAttribute))
                            if (!Regex.IsMatch(Convert.ToString(value), ((RegexAttribute)attrs[i]).Value))
                                ValidationErrorsList.Add(string.Format("El valor de la propiedad '{0}' no cumple los parámetros de validación {1}.", propName, ((RegexAttribute)attrs[i]).Message));
                        break;
                    case "Double":
                        if (attrName == nameof(MinValueAttribute))
                            if (Convert.ToInt64(value) < ((MinValueAttribute)attrs[i]).Value)
                                ValidationErrorsList.Add(string.Format("El valor de la propiedad '{0}' no cumple los parámetros de validación {1}.", propName, "Valor mínimo "));

                        if (attrName == nameof(MaxValueAttribute))
                            if (Convert.ToInt64(value) > ((MaxValueAttribute)attrs[i]).Value)
                                ValidationErrorsList.Add(string.Format("El valor de la propiedad '{0}' no cumple los parámetros de validación {1}.", propName, "Valor máximo "));

                        if (attrName == nameof(DecimalCountAttribute))
                        {
                            var match = Regex.Match(Convert.ToDouble(value).ToString(), "(?<=[\\.|,])[0-9]+");
                            if (match.ToString().Length != ((DecimalCountAttribute)attrs[i]).Value)
                                ValidationErrorsList.Add(string.Format("El valor de la propiedad '{0}' no cumple los parámetros de validación {1}.", propName, "Cantidad de decimales "));
                        }
                        break;
                }
            }

            if (ExtrictValidation && ValidationErrorsList.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, ValidationErrorsList));

            return value;
        }

        /// <summary>
        /// Intenta convertir un JSON a un objeto específico
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">JSON</param>
        /// <param name="result">Objeto resultado</param>
        /// <param name="messages">mensajes de respuesta</param>
        /// <returns>boleano que define si fue válido a no la conversión</returns>
        public bool TryParseFromJson<T>(string json, out T result, out string messages)
        {
            result = Activator.CreateInstance<T>();
            messages = string.Empty;

            try
            {
                var jsonObj = JsonConvert.DeserializeObject(json);
                JsonSerializer serializer = new JsonSerializer();
                result = (T)serializer.Deserialize(new JTokenReader(jsonObj as JObject), typeof(T));
                return true;
            }
            catch (Exception ex)
            {
                messages = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Ejecuta una función
        /// </summary>
        /// <param name="commandString"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool InvokeFunction(string commandString, object parameter)
        {
            string functClass = "Helper";
            Type thisType = Type.GetType($"{this.GetType().Namespace}.{ functClass}");
            MethodInfo theMethod = thisType.GetMethod(commandString);
            var rst = theMethod.Invoke(this, new object[] { parameter });
            return (bool)rst;
        }
    }

    /// <summary>
    /// Provee métodos para indexar una colección de datos tipo T
    /// y realizar búsquedas más rapidas
    /// </summary>
    /// <typeparam name="T">Tipo de dato de la colección</typeparam>
    public sealed class IndexedCollection<T>
    {
        #region Fields

        /// <summary>
        /// Lista de datos no indexados
        /// </summary>
        internal IList<T> NonIndexedList;
        /// <summary>
        /// Lista de datos indexados
        /// </summary>
        internal readonly IDictionary<string, ILookup<object, T>> IndexedList;
        /// <summary>
        /// Indices
        /// </summary>
        internal readonly IList<Expression<Func<T, object>>> Indexes;

        #endregion

        #region Builders

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// </summary>
        /// <param name="source">Fuente de datos</param>
        /// <param name="indexes">Indices usados para organizar los datos</param>
        public IndexedCollection(IEnumerable<T> source, params Expression<Func<T, object>>[] indexes)
        {
            NonIndexedList = new List<T>(source);
            IndexedList = new Dictionary<string, ILookup<object, T>>();
            Indexes = new List<Expression<Func<T, object>>>();
            BuildIndexes(indexes);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Busca un valor sobre una propiedad
        /// </summary>
        /// <param name="property">Expresión de la propiedad sobre la que se realiza la busqueda</param>
        /// <param name="value">Valor a buscar</param>
        /// <returns>Conjunto de resultados</returns>
        public IndexedResult FindValue(Expression<Func<T, object>> property, object value)
        {
            return new IndexedResult(this, new List<T>()).And(property, value);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Construye los indices
        /// </summary>
        /// <param name="indexes">Indices como expresiones</param>
        private void BuildIndexes(Expression<Func<T, object>>[] indexes)
        {
            for (int i = 0; i < indexes.Length; i++)
            {
                string indexName = Convert.ToBase64String(Encoding.UTF8.GetBytes(PropertyName(indexes[i])));
                if (IndexedList.ContainsKey(indexName))
                {
                    continue;
                }

                Indexes.Add(indexes[i]);
                IndexedList.Add(indexName, NonIndexedList.ToLookup(indexes[i].Compile()));
            }
            NonIndexedList = NonIndexedList.Except(IndexedList.SelectMany(x => x.Value).SelectMany(r => r)).ToList();
        }

        /// <summary>
        /// Obtiene el nombre de la propiedad en la expresión
        /// </summary>
        /// <param name="expression">Expresión a evaluar</param>
        /// <returns>Nombre de la propiedad en base64</returns>
        internal string PropertyName(Expression<Func<T, object>> expression)
        {
            if (!(expression.Body is MemberExpression body))
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        #endregion

        #region IEnumerable

        /// <summary>
        /// Obtiene una lista de la colección
        /// </summary>
        /// <returns>Lista de tipo T</returns>
        public IList<T> ToList()
        {
            List<T> res = new List<T>(NonIndexedList);
            res.AddRange(IndexedList.SelectMany(x => x.Value).SelectMany(r => r));

            return res;
        }

        #endregion

        #region Classes

        /// <summary>
        /// Encapsula un conjunto de resultados de la busqueda sobre una colección indexada
        /// </summary>
        public class IndexedResult
        {
            #region Fields

            /// <summary>
            /// Instancia a la colección de datos
            /// </summary>
            private readonly IndexedCollection<T> _indexedCollection;
            /// <summary>
            /// Conjunto de resultados
            /// </summary>
            private readonly IEnumerable<T> _resultSet;

            #endregion

            #region Builders

            /// <summary>
            /// Inicializa una nueva instancia de la clase
            /// </summary>
            /// <param name="indexedCollection">Instancia a la colección de datos</param>
            /// <param name="resultSet">Conjunto de resultados</param>
            internal IndexedResult(IndexedCollection<T> indexedCollection, IEnumerable<T> resultSet)
            {
                _indexedCollection = indexedCollection;
                _resultSet = resultSet;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Retorna un subconjunto de resultados donde los elementos cumplan cualquiera de los dos criterios
            /// </summary>
            /// <param name="property">Expresión de la propiedad sobre la que se realiza la busqueda</param>
            /// <param name="value">Valor a buscar</param>
            /// <returns>Subconjunto de resultados</returns>
            public IndexedResult Or(Expression<Func<T, object>> property, object value)
            {
                string indexName = Convert.ToBase64String(Encoding.UTF8.GetBytes(_indexedCollection.PropertyName(property)));
                if (_indexedCollection.IndexedList.ContainsKey(indexName))
                {
                    return new IndexedResult(_indexedCollection, (_resultSet.Count() == 0 ? (_indexedCollection.IndexedList[indexName].Contains(value) ? _indexedCollection.IndexedList[indexName][value] : new T[0]) : (_indexedCollection.IndexedList[indexName].Contains(value) ? _resultSet.Union(_indexedCollection.IndexedList[indexName][value]) : _resultSet)));
                }

                var c = property.Compile();
                return new IndexedResult(_indexedCollection, _resultSet.Except(_indexedCollection.NonIndexedList.Where(x => c(x).Equals(value))));
            }

            /// <summary>
            /// Retorna un subconjunto de resultados donde los elementos cumplan con ambos criterios
            /// </summary>
            /// <param name="property">Expresión de la propiedad sobre la que se realiza la busqueda</param>
            /// <param name="value">Valor a buscar</param>
            /// <returns>Subconjunto de resultados</returns>
            public IndexedResult And(Expression<Func<T, object>> property, object value)
            {
                string indexName = Convert.ToBase64String(Encoding.UTF8.GetBytes(_indexedCollection.PropertyName(property)));
                if (_indexedCollection.IndexedList.ContainsKey(indexName))
                {
                    return new IndexedResult(_indexedCollection, _resultSet != null ? (_resultSet.Count() == 0 ? (_indexedCollection.IndexedList[indexName].Contains(value) ? _indexedCollection.IndexedList[indexName][value] : null) : (_indexedCollection.IndexedList[indexName].Contains(value) ? _resultSet.Intersect(_indexedCollection.IndexedList[indexName][value]) : new T[0])) : new T[0]);
                }

                var c = property.Compile();
                return new IndexedResult(_indexedCollection, _resultSet.Intersect(_indexedCollection.NonIndexedList.Where(x => c(x).Equals(value))));
            }

            #endregion

            #region IEnumerable

            /// <summary>
            /// Obtiene una lista de la colección
            /// </summary>
            /// <returns>Lista de tipo T</returns>
            public IList<T> ToList()
            {
                return _resultSet != null ? _resultSet.ToList() : new List<T>();
            }

            #endregion
        }

        #endregion
    }
    #endregion

    #region Attributes
    /// <summary>
    /// Longitud permitida
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LengthAttribute : Attribute
    {
        private long _Value;
        public long Value { get { return _Value; } set { _Value = value; } }
        public LengthAttribute(long Value) { this.Value = Value; }
    }
    /// <summary>
    /// Expresión regular a aplicar
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RegexAttribute : Attribute
    {
        private string _Value;
        private string _Message;
        public string Value { get { return _Value; } set { _Value = value; } }
        public string Message { get { return _Message; } set { _Message = value; } }

        public RegexAttribute(string Value, string Message) { this._Value = Value; this._Message = Message; }
    }
    /// <summary>
    /// Cantidad de decimales
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DecimalCountAttribute : Attribute
    {
        private long _Value;
        public long Value { get { return _Value; } set { _Value = value; } }
        public DecimalCountAttribute(long Value) { this._Value = Value; }
    }
    /// <summary>
    /// Valor máximo
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxValueAttribute : Attribute
    {
        private long _Value;
        public long Value { get { return _Value; } set { _Value = value; } }
        public MaxValueAttribute(long Value) { this._Value = Value; }
    }
    /// <summary>
    /// Valor mínimo
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MinValueAttribute : Attribute
    {
        private long _Value;
        public long Value { get { return _Value; } set { _Value = value; } }
        public MinValueAttribute(long Value) { this._Value = Value; }
    }
    /// <summary>
    /// Función
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FunctionAttribute : Attribute
    {
        private string _Value;
        public string Value { get { return _Value; } set { _Value = value; } }
        public FunctionAttribute(string Value) { this._Value = Value; }
    }
    /// <summary>
    /// Orden
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        private readonly int order_;
        public OrderAttribute([CallerLineNumber]int order = 0)
        {
            order_ = order;
        }

        public int Order { get { return order_; } }
    }
    #endregion
}