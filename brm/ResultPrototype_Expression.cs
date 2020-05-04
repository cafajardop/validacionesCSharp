using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Globalization;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace brm
{
    /// <sumary>
	/// ResultPrototype_Expression
	/// </sumary> 
	public sealed class ResultPrototype_Expression
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// LibraryId
        /// </sumary>
        private long LibraryIdIn;
        /// <sumary>
        /// CompanyId
        /// </sumary>
        private long CompanyIdIn;
        /// <sumary>
        /// CaseNumber
        /// </sumary>
        private string CaseNumberIn;
        /// <sumary>
        /// UserCode
        /// </sumary>
        private string UserCodeIn;
        /// <sumary>
        /// FileId
        /// </sumary>
        private string FileIdIn;
        /// <sumary>
        /// CodeFile
        /// </sumary>
        private string CodeFileIn;
        /// <sumary>
        /// period
        /// </sumary>
        private long periodln;
        /// <sumary>
        /// year
        /// </sumary>
        private long yearln;
        /// <sumary>
        /// operatorId
        /// </sumary>
        private long operatorIdln;
        /// <sumary>
        /// IdTypePopulation
        /// </sumary>
        private long IdTypePopulationln;
        /// <sumary>
        /// ResultMessage
        /// </sumary>
        private string ResultMessage;
        /// <sumary>
        /// Resultado prototipo detalle 4725
        /// </sumary>
        private bool ResultPrototype;
        #endregion

        #region Members
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        private readonly RUL_TreatmentAntituberculoso RUL_TreatmentAntituberculoso = new RUL_TreatmentAntituberculoso();
        /// <sumary>
        /// Resolución_4725_de_2011
        /// </sumary>
        private readonly RUL_StudySyphilis RUL_StudySyphilis = new RUL_StudySyphilis();
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        private readonly RUL_Prophylaxis RUL_Prophylaxis = new RUL_Prophylaxis();
        /// <sumary>
        /// Regla Presente-Resolución 4725 de 2011
        /// </sumary>
        private readonly RUL_Present RUL_Present = new RUL_Present();
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        private readonly RUL_PathologiesThatDefineVIH RUL_PathologiesThatDefineVIH = new RUL_PathologiesThatDefineVIH();
        /// <sumary>
        /// Resolucion 4725 de 2011
        /// </sumary>
        private readonly RUL_Past_ RUL_Past_ = new RUL_Past_();
        /// <sumary>
        /// Regla para validar identidad y demografía resolución 4725
        /// </sumary>
        private readonly RUL_IdentityDemography4725 RUL_IdentityDemography4725 = new RUL_IdentityDemography4725();
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        private readonly RUL_CurrentAntirretroviralTherapy RUL_CurrentAntirretroviralTherapy = new RUL_CurrentAntirretroviralTherapy();
        /// <sumary>
        /// TERAPIA ANTIRRETROVIRAL DE INICIO (TAR)
        /// </sumary>
        private readonly RUL_Antiretroviral_therapy_begin RUL_Antiretroviral_therapy_begin = new RUL_Antiretroviral_therapy_begin();
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        private readonly RUL_AdministrativePreviousContribution RUL_AdministrativePreviousContribution = new RUL_AdministrativePreviousContribution();
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public ResultPrototype_Expression() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// ResultPrototype_Expression
        /// </sumary>
        /// <param name="LibraryIdIn">LibraryId</param>
        /// <param name="CompanyIdIn">CompanyId</param>
        /// <param name="CaseNumberIn">CaseNumber</param>
        /// <param name="UserCodeIn">UserCode</param>
        /// <param name="FileIdIn">FileId</param>
        /// <param name="CodeFileIn">CodeFile</param>
        /// <param name="periodln">period</param>
        /// <param name="yearln">year</param>
        /// <param name="operatorIdln">operatorId</param>
        /// <param name="IdTypePopulationln">IdTypePopulation</param>
        public RuntimeResult<bool> Execute(long LibraryIdIn, long CompanyIdIn, string CaseNumberIn, string UserCodeIn, string FileIdIn, string CodeFileIn, long periodln, long yearln, long operatorIdln, long IdTypePopulationln)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.LibraryIdIn = LibraryIdIn;
                this.CompanyIdIn = CompanyIdIn;
                this.CaseNumberIn = CaseNumberIn;
                this.UserCodeIn = UserCodeIn;
                this.FileIdIn = FileIdIn;
                this.CodeFileIn = CodeFileIn;
                this.periodln = periodln;
                this.yearln = yearln;
                this.operatorIdln = operatorIdln;
                this.IdTypePopulationln = IdTypePopulationln;
                this.ResultMessage = FUNC_ResultMessage();
                this.ResultPrototype = FUNC_ResultPrototype();
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<bool>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_ResultMessage()
        {
            return "";
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_ResultPrototype()
        {
            ENT_parameters4725 parameters4725 = new ENT_parameters4725()
            {
                LibraryId = LibraryIdIn,
                CompanyId = CompanyIdIn,
                CaseNumber = CaseNumberIn,
                UserCode = UserCodeIn,
                FileId = FileIdIn,
                year = yearln,
                operatorId = operatorIdln,
                IdTypePopulation = IdTypePopulationln
            };

            var result = Helper.USR_Main4725(parameters4725);

            if (result.IsError)
            {
                this.FileName = result.FileName;
                this.ResultMessage = result.ErrorMessage;
                return false;
            }

            if (periodln < 1 || periodln > 2) throw new ArgumentException($"El periodo de Cargue no es valido  debe ser (1 o 2) {periodln}");
            if (result.Result.Count == 0) throw new ArgumentException($"El archivo no contiene registros a esta incorrecto favor validar");

            int adapterId = 1;
            #region Valida Tipo de poblacion
            var sql = new StringBuilder();

            sql.Append(" SELECT Id ");
            sql.Append(" FROM TypeDetail WITH(NOLOCK)");
            sql.Append(" WHERE IdTypeHead = 72");
            sql.AppendFormat(" AND  Code = {0} ", parameters4725.IdTypePopulation);

            var resultTypepopulation = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (!resultTypepopulation.IsSuccessful)
            {
                this.FileName = resultTypepopulation.FileName;
                this.ResultMessage = resultTypepopulation.ErrorMessage;
                return false;
            }
            dynamic idtypepopulation = JsonConvert.DeserializeObject<dynamic>(resultTypepopulation.Result.ToString());
            int info = 0;
            if (idtypepopulation != null && idtypepopulation.Count > 0)
                info = (int)((JProperty)((JContainer)((JContainer)idtypepopulation).First).First).Value;
            else
            {
                this.ResultMessage = "Id tipo de poblacion invalido";
                return false;
            }

            #endregion

            #region ValidaTipoOperador
            sql = new StringBuilder();

            sql.Append(" SELECT Id ");
            sql.Append(" FROM Operator WITH(NOLOCK)");
            sql.AppendFormat(" WHERE  Id = {0} ", parameters4725.operatorId);

            var resultIdOperator = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (!resultIdOperator.IsSuccessful)
            {
                this.FileName = resultIdOperator.FileName;
                this.ResultMessage = resultIdOperator.ErrorMessage;
                return false;
            }
            var _idOperator = JsonConvert.DeserializeObject<List<dynamic>>(resultIdOperator.Result.ToString());

            if (_idOperator.Count() == 0)
            {
                this.ResultMessage = "Id operador invalido";
                return false;
            }

            #endregion

            #region Validate Dates
            var resulDate = Helper.USR_GetDatePeriod(periodln, parameters4725.year);
            if (!resulDate.IsSuccessful)
            {
                this.ResultMessage = resulDate.ErrorMessage;
                return false;
            }

            sql = new StringBuilder();

            sql.Append(" SELECT IdOperator ");
            sql.Append(" FROM FileHead4725 WITH(NOLOCK)");
            sql.AppendFormat(" WHERE InitialDate = '{0}'", resulDate.Result.InitPeriod.ToString("MM/dd/yyyy"));
            sql.AppendFormat(" AND EndDate = '{0}'", resulDate.Result.EndPeriod.ToString("MM/dd/yyyy"));
            sql.AppendFormat(" AND  IdOperator = {0} ", parameters4725.operatorId);
            sql.AppendFormat(" AND IdTypePopulation = {0}", info);

            var resultExecute = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (resultExecute.IsError)
            {
                this.ResultMessage = resultExecute.ErrorMessage;
                return false;
            }
            var _head = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());

            if (_head != null && _head.Count() > 0)
            {
                this.ResultMessage = "Ya existe un Cargue para este periodo";
                return false;
            }

            #endregion


            var entity = result.Result;
            List<ENT_StructureRes4725> _entity = entity;
            var listErrors = new List<string>();

            const string folder = "Resolucion4725ResultData";
            int index = 0;


            sql = new StringBuilder();

            // consulta todo los operadores  para validar en memoria
            sql.Append(" SELECT DISTINCT  QualificationCode");
            sql.Append(" FROM Operator WITH(NOLOCK)");
            sql.Append("where IdOperatorType = 70");

            var operatorResul = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (operatorResul.IsError)
            {
                this.FileName = operatorResul.FileName;
                this.ResultMessage = operatorResul.ErrorMessage;
                return false;
            }
            var listaQualification = JsonConvert.DeserializeObject<List<dynamic>>(operatorResul.Result.ToString());

            //Consulta la tabla typedetail para rendimiento optimo
            sql = new StringBuilder();
            sql.Append("SELECT DISTINCT Code  ");
            sql.Append("FROM TypeDetail WITH (NOLOCK)");
            sql.Append("WHERE IdTypeHead=17");

            var codeMunicipalyResul = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (codeMunicipalyResul.IsError)
            {
                this.FileName = operatorResul.FileName;
                this.ResultMessage = operatorResul.ErrorMessage;
                return false;
            }
            var listaCodeMunicipaly = JsonConvert.DeserializeObject<List<dynamic>>(codeMunicipalyResul.Result.ToString());

            //Consulta Codigo CUM
            sql = new StringBuilder();
            sql.Append("SELECT DISTINCT Code  ");
            sql.Append("FROM Cum WITH (NOLOCK)");

            var codeCUM = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (codeCUM.IsError)
            {
                this.FileName = codeCUM.FileName;
                this.ResultMessage = codeCUM.ErrorMessage;
                return false;
            }
            var listaCum = JsonConvert.DeserializeObject<List<dynamic>>(codeCUM.Result.ToString());

            //Valida si el usuario existe en la BD 
            var documentTypes = Helper.USR_GetDocumentTypes(adapterId);
            var listDocumentTypesDB = JsonConvert.DeserializeObject<List<dynamic>>(documentTypes.Result.ToString());
            Helper.USR_ValidateDocumentNumber(adapterId, _entity, listErrors, index, listDocumentTypesDB);

            index = 0;
            foreach (var ent in _entity)
            {
                //Valida si el operador es correcto
                Helper.USR_ValidateQualificationCodeAndCodeMunicipaly(listErrors, index, listaQualification, listaCodeMunicipaly, listaCum, ent);
                //Valida Fechas
                Helper.USR_ValidateDatesTar(listErrors, index, ent);
                //Valida conteo linfocitos y resultado de Genotipificacion
                Helper.USR_ValidateCountLinfocitos(listErrors, index, ent);
                //Ejecuta las reglas del BRM
                var res = Helper.SYS_VerificationPrototype(new Func<object>[]
                {
                        () => RUL_IdentityDemography4725.Execute(ent.CodigoEPS,ent.Regimen,ent.GrupoPoblacional,ent.PrimerNombre,ent.SegundoNombre,ent.PrimerApellido, ent.SegundoApellido, ent.TipoIdentificacion, ent.NumeroDocumento, ent.FechaNacimiento, ent.Sexo, ent.PertenenciaEtnica,ent.DireccionResidencia, ent.TelefonoContacto,ent.CodigoMunicipioRes,ent.FechaAfiliacionEPS, ent.PersonaGestante,ent.PersonaConTuberculosis,ent.PersonaMenor18MesesVIH, ent.CondicionConRespectoDiagnosticoVIH)
                 });

                //Recorre los registros para insertar en la lista de errores 
                string replaceCharacters = res.GetPropertyValue<string>("Messages").Replace("\r\n", "*").Replace("\n", "*").Replace("\r", "*");
                string[] dataRules = replaceCharacters.Split('*');

                foreach (var fields in dataRules)
                {
                    if (fields != "" && fields != " ")
                    {
                        string mensajeItem = $" Línea {index + 1}";
                        listErrors.Add(string.Concat(fields.Trim(), ",", mensajeItem));
                    }
                }
                index++;
            }

            //Inserta a la lista de errores
            if (listErrors.Count > 0)
            {
                string pathFile = Helper.USR_GenericSaveLog(new Dictionary<string, List<string>>() { ["4725"] = listErrors }, folder);
                var attach = Helper.USR_WSAttachFileToProcess(pathFile, UserCodeIn, CompanyIdIn.ToString(), CaseNumberIn, "4725");
                if (attach.IsError)
                {
                    this.ResultMessage = "No se pudo asociar el archivo al proceso. " + attach.ErrorMessage;
                    return false;
                }
                this.FileName = attach.FileName;
                this.ResultMessage = "Hubo errores en la validación ";
                return false;
            }

            #region savefile4725
            //Guarda la informacion en la Base de datos
            string cutOffDate = resulDate.Result.EndPeriod.ToString("MM/dd/yyyy");
            string code = "4725";
            string InitialDate = resulDate.Result.InitPeriod.ToString("MM/dd/yyyy");
            string endDate = resulDate.Result.EndPeriod.ToString("MM/dd/yyyy");

            var resultSave = Helper.USR_Save4725File(cutOffDate, code, parameters4725.operatorId, parameters4725.CaseNumber, InitialDate, endDate, info, _entity);
            if (resultSave.IsError)
            {
                this.ResultMessage = "Ocurrio un error al guardar el archivo " + resultSave.ErrorMessage;
                return false;
            }
            #endregion

            return true;
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<bool> EvaluateCombinations()
        {
            return RuntimeResult<bool>.SetError("No cumple ninguna condición");
        }
        #endregion
    }
    /// <sumary>
    /// Plantilla de reglas
    /// </sumary> 
    public sealed class RUL_TreatmentAntituberculoso
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Campo valida Recibe tratamiento antituberculoso Res 4725
        /// </sumary>
        private string v93_AntituberculosisTreatment;
        /// <sumary>
        /// Campo valida fecha de inicio del tratamiento antituberculoso que recibe actualmente Res 4725
        /// </sumary>
        private string v94_StartDateCuurentTuberculosisTreatment;
        /// <sumary>
        /// AMIKACINA
        /// </sumary>
        private long v95_1_CurrentAmikacina;
        /// <sumary>
        /// CIPROFLOXACINA
        /// </sumary>
        private long v95_2_CurrentCiprofloxacina;
        /// <sumary>
        /// ESTREPTOMICINA
        /// </sumary>
        private long v95_3_CurrentEstreptomicina;
        /// <sumary>
        /// ETHAMBUTOL
        /// </sumary>
        private long v95_4_CurrentEthambutol;
        /// <sumary>
        /// ETHIONAMIDA
        /// </sumary>
        private long v95_5_CurrentEthionamida;
        /// <sumary>
        /// ISONIACIDA
        /// </sumary>
        private long v95_6_CurrentIsoniacida;
        /// <sumary>
        /// PIRAZINAMIDA
        /// </sumary>
        private long v95_7_CurrentPirazinamida;
        /// <sumary>
        /// RIFAMPICINA
        /// </sumary>
        private long v95_8_CurrentRifampicina;
        /// <sumary>
        /// Actualmente recibe RIFABUTINA
        /// </sumary>
        private long v95_9_CurrentRifabutina;
        /// <sumary>
        /// Actualmente recibe Medicamento NO POS (medicamento antituberculoso 1)
        /// </sumary>
        private string v95_10_CurrentNonposMedication1tuber;
        /// <sumary>
        /// Actualmente recibe Medicamento NO POS (medicamento antituberculoso 2)
        /// </sumary>
        private string v95_11_CurrentNonposMedication2tuber;
        /// <sumary>
        /// Actualmente recibe Medicamento NO POS (medicamento antituberculoso 3)
        /// </sumary>
        private string v95_12_CurrentNonposMedication3tuber;
        /// <sumary>
        /// Actualmente recibe Medicamento NO POS (medicamento antituberculoso 4)
        /// </sumary>
        private string v95_13_CurrentNonposMedication4tuber;
        /// <sumary>
        /// Actualmente recibe Medicamento NO POS (medicamento antituberculoso 5)
        /// </sumary>
        private string v95_14_CurrentNonposMedication5tuber;
        /// <sumary>
        /// Campo valida Fecha de inicio del tratamiento antituberculoso que recibe actualmente Res 4725
        /// </sumary>
        private string VC_v94_DateTreatment;
        /// <sumary>
        /// v95_10_v95_10_CurrentNonposMedication1tuber
        /// </sumary>
        private string VC_v95_10_TBmedicine1;
        /// <sumary>
        /// v95_11_TBmedicine2
        /// </sumary>
        private string VC_v95_11_TBmedicine2;
        /// <sumary>
        /// v95_11_TBmedicine2
        /// </sumary>
        private string VC_v95_12_TBmedicine3;
        /// <sumary>
        /// v95_13_TBmedicine4
        /// </sumary>
        private string VC_v95_13_TBmedicine4;
        /// <sumary>
        /// v95_14_TBmedicine5
        /// </sumary>
        private string VC_v95_14_TBmedicine5;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_TreatmentAntituberculoso() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        /// <param name="v93_AntituberculosisTreatment">Campo valida Recibe tratamiento antituberculoso Res 4725</param>
        /// <param name="v94_StartDateCuurentTuberculosisTreatment">Campo valida fecha de inicio del tratamiento antituberculoso que recibe actualmente Res 4725</param>
        /// <param name="v95_1_CurrentAmikacina">AMIKACINA</param>
        /// <param name="v95_2_CurrentCiprofloxacina">CIPROFLOXACINA</param>
        /// <param name="v95_3_CurrentEstreptomicina">ESTREPTOMICINA</param>
        /// <param name="v95_4_CurrentEthambutol">ETHAMBUTOL</param>
        /// <param name="v95_5_CurrentEthionamida">ETHIONAMIDA</param>
        /// <param name="v95_6_CurrentIsoniacida">ISONIACIDA</param>
        /// <param name="v95_7_CurrentPirazinamida">PIRAZINAMIDA</param>
        /// <param name="v95_8_CurrentRifampicina">RIFAMPICINA</param>
        /// <param name="v95_9_CurrentRifabutina">Actualmente recibe RIFABUTINA</param>
        /// <param name="v95_10_CurrentNonposMedication1tuber">Actualmente recibe Medicamento NO POS (medicamento antituberculoso 1)</param>
        /// <param name="v95_11_CurrentNonposMedication2tuber">Actualmente recibe Medicamento NO POS (medicamento antituberculoso 2)</param>
        /// <param name="v95_12_CurrentNonposMedication3tuber">Actualmente recibe Medicamento NO POS (medicamento antituberculoso 3)</param>
        /// <param name="v95_13_CurrentNonposMedication4tuber">Actualmente recibe Medicamento NO POS (medicamento antituberculoso 4)</param>
        /// <param name="v95_14_CurrentNonposMedication5tuber">Actualmente recibe Medicamento NO POS (medicamento antituberculoso 5)</param>
        public RuntimeResult<string> Execute(string v93_AntituberculosisTreatment, string v94_StartDateCuurentTuberculosisTreatment, long v95_1_CurrentAmikacina, long v95_2_CurrentCiprofloxacina, long v95_3_CurrentEstreptomicina, long v95_4_CurrentEthambutol, long v95_5_CurrentEthionamida, long v95_6_CurrentIsoniacida, long v95_7_CurrentPirazinamida, long v95_8_CurrentRifampicina, long v95_9_CurrentRifabutina, string v95_10_CurrentNonposMedication1tuber, string v95_11_CurrentNonposMedication2tuber, string v95_12_CurrentNonposMedication3tuber, string v95_13_CurrentNonposMedication4tuber, string v95_14_CurrentNonposMedication5tuber)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.v93_AntituberculosisTreatment = v93_AntituberculosisTreatment;
                this.v94_StartDateCuurentTuberculosisTreatment = v94_StartDateCuurentTuberculosisTreatment;
                this.v95_1_CurrentAmikacina = v95_1_CurrentAmikacina;
                this.v95_2_CurrentCiprofloxacina = v95_2_CurrentCiprofloxacina;
                this.v95_3_CurrentEstreptomicina = v95_3_CurrentEstreptomicina;
                this.v95_4_CurrentEthambutol = v95_4_CurrentEthambutol;
                this.v95_5_CurrentEthionamida = v95_5_CurrentEthionamida;
                this.v95_6_CurrentIsoniacida = v95_6_CurrentIsoniacida;
                this.v95_7_CurrentPirazinamida = v95_7_CurrentPirazinamida;
                this.v95_8_CurrentRifampicina = v95_8_CurrentRifampicina;
                this.v95_9_CurrentRifabutina = v95_9_CurrentRifabutina;
                this.v95_10_CurrentNonposMedication1tuber = v95_10_CurrentNonposMedication1tuber;
                this.v95_11_CurrentNonposMedication2tuber = v95_11_CurrentNonposMedication2tuber;
                this.v95_12_CurrentNonposMedication3tuber = v95_12_CurrentNonposMedication3tuber;
                this.v95_13_CurrentNonposMedication4tuber = v95_13_CurrentNonposMedication4tuber;
                this.v95_14_CurrentNonposMedication5tuber = v95_14_CurrentNonposMedication5tuber;
                this.VC_v94_DateTreatment = FUNC_VC_v94_DateTreatment();
                this.VC_v95_10_TBmedicine1 = FUNC_VC_v95_10_TBmedicine1();
                this.VC_v95_11_TBmedicine2 = FUNC_VC_v95_11_TBmedicine2();
                this.VC_v95_12_TBmedicine3 = FUNC_VC_v95_12_TBmedicine3();
                this.VC_v95_13_TBmedicine4 = FUNC_VC_v95_13_TBmedicine4();
                this.VC_v95_14_TBmedicine5 = FUNC_VC_v95_14_TBmedicine5();
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<string>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v94_DateTreatment()
        {
            return Helper.USR_DateValidate(v94_StartDateCuurentTuberculosisTreatment);
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v95_10_TBmedicine1()
        {
            return Helper.USR_ValidateCUM_v95("95.10", v95_10_CurrentNonposMedication1tuber, "medicamento antituberculoso 1");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v95_11_TBmedicine2()
        {
            return Helper.USR_ValidateCUM_v95("95.11", v95_11_CurrentNonposMedication2tuber, "medicamento antituberculoso 2");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v95_12_TBmedicine3()
        {
            return Helper.USR_ValidateCUM_v95("95.12", v95_12_CurrentNonposMedication3tuber, "medicamento antituberculoso 3");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v95_13_TBmedicine4()
        {
            return Helper.USR_ValidateCUM_v95("95.13", v95_13_CurrentNonposMedication4tuber, "medicamento antituberculoso 4");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v95_14_TBmedicine5()
        {
            return Helper.USR_ValidateCUM_v95("95.14", v95_14_CurrentNonposMedication5tuber, "medicamento antituberculoso 5");
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!((new string[] { "0", "1", "2", "3", "9" }).Contains(v93_AntituberculosisTreatment))) NonValidMessages.Add($"Debe ingresar un dato valido validando si la persona recibe tratamiento antituberculoso.Validar la variable 93 'Recibe  tratamiento antituberculoso'");
            if (!(VC_v94_DateTreatment == "OK")) NonValidMessages.Add($"{this.VC_v94_DateTreatment}");
            if (!((new long[] { 0, 1 }).Contains(v95_1_CurrentAmikacina))) NonValidMessages.Add($"Debe ingresar un dato valido teniendo en cuenta si la persona actualmente recibe AMIKACINA.Validar la variable  95.1 Actualmente recibe AMIKACINA");
            if (!((new long[] { 0, 1 }).Contains(v95_2_CurrentCiprofloxacina))) NonValidMessages.Add($"Debe ingresar un dato valido teniendo en cuenta si la persona actualmente recibe.Validar la variable 95.2 Actualmente recibe CIPROFLOXACINA");
            if (!((new long[] { 0, 1 }).Contains(v95_3_CurrentEstreptomicina))) NonValidMessages.Add($"Debe ingresar un dato valido teniendo en cuenta si la persona actualmente recibe.Validar la variable 95.3 ESTREPTOMICINA ");
            if (!((new long[] { 0, 1 }).Contains(v95_4_CurrentEthambutol))) NonValidMessages.Add($"Debe ingresar un dato valido teniendo en cuenta si la persona actualmente recibe.Validar la variable 95.3 ESTREPTOMICINA");
            if (!((new long[] { 0, 1 }).Contains(v95_5_CurrentEthionamida))) NonValidMessages.Add($"Debe ingresar un dato valido teniendo en cuenta si la persona actualmente recibe.Validar la variable 95.5 Actualmente recibe ETHIONAMIDA");
            if (!((new long[] { 0, 1 }).Contains(v95_6_CurrentIsoniacida))) NonValidMessages.Add($"Debe ingresar un dato valido teniendo en cuenta si la persona actualmente recibe.Validar la variable 95.6 Actualmente recibe ISONIACIDA");
            if (!((new long[] { 0, 1 }).Contains(v95_7_CurrentPirazinamida))) NonValidMessages.Add($"Debe ingresar un dato valido teniendo en cuenta si la persona actualmente recibe.Validar la variable 95.7 Actualmente recibe PIRAZINAMIDA");
            if (!((new long[] { 0, 1 }).Contains(v95_8_CurrentRifampicina))) NonValidMessages.Add($"Debe ingresar un dato valido teniendo en cuenta si la persona actualmente recibe.Validar la variable 95.8 Actualmente recibe RIFAMPICINA");
            if (!((new long[] { 0, 1 }).Contains(v95_9_CurrentRifabutina))) NonValidMessages.Add($"Debe ingresar un dato valido teniendo en cuenta si la persona actualmente recibe.Validar la variable 95.9 RIFABUTINA");
            if (!(VC_v95_10_TBmedicine1 == "OK")) NonValidMessages.Add($"{this.VC_v95_10_TBmedicine1}");
            if (!(VC_v95_11_TBmedicine2 == "OK")) NonValidMessages.Add($"{this.VC_v95_11_TBmedicine2}");
            if (!(VC_v95_12_TBmedicine3 == "OK")) NonValidMessages.Add($"{this.VC_v95_12_TBmedicine3}");
            if (!(VC_v95_13_TBmedicine4 == "OK")) NonValidMessages.Add($"{this.VC_v95_13_TBmedicine4}");
            if (!(VC_v95_14_TBmedicine5 == "OK")) NonValidMessages.Add($"{this.VC_v95_14_TBmedicine5}");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetInvalid(() => { return ""; }, $"", this.FileName);
        }
        #endregion
    }
    /// <sumary>
    /// Resolución_4725_de_2011
    /// </sumary> 
    public sealed class RUL_StudySyphilis
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Resultado de Serología para Sífilis en primer trimestre de gestación 
        /// </sumary>
        private long v96_SerologySyphilis;
        /// <sumary>
        /// Resultado de Serología para Sífilis en segundo trimestre de gestación
        /// </sumary>
        private long v97_ResultSerology;
        /// <sumary>
        /// Resultado de Serología para Sífilis en tercer trimestre de gestación
        /// </sumary>
        private long v98_ResultSerologyTrimester;
        /// <sumary>
        /// Resultado de Serología al momento del parto o aborto (para gestantes sin serología del tercer trimestre)
        /// </sumary>
        private long v99_SerologyResult;
        /// <sumary>
        /// Fecha de primer tratamiento de la Sífilis
        /// </sumary>
        private string v100_DateFirstTreatmentSifilis;
        /// <sumary>
        /// Fecha de segundo tratamiento de la Sífilis 
        /// </sumary>
        private string v101_SecondSyphilis;
        /// <sumary>
        /// Fecha de tercero tratamiento de la Sífilis
        /// </sumary>
        private string v102_3rdDateSyphilis;
        /// <sumary>
        /// Fecha de primer tratamiento de la Sífilis
        /// </sumary>
        private string VC_v100_DateFirstTreatmentSifilis;
        /// <sumary>
        /// v101_SecondSyphilis
        /// </sumary>
        private string VC_v101_SecondSyphilis;
        /// <sumary>
        /// v102_3rdDateSyphilis
        /// </sumary>
        private string VC_v102_3rdDateSyphilis;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_StudySyphilis() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Resolución_4725_de_2011
        /// </sumary>
        /// <param name="v96_SerologySyphilis">Resultado de Serología para Sífilis en primer trimestre de gestación </param>
        /// <param name="v97_ResultSerology">Resultado de Serología para Sífilis en segundo trimestre de gestación</param>
        /// <param name="v98_ResultSerologyTrimester">Resultado de Serología para Sífilis en tercer trimestre de gestación</param>
        /// <param name="v99_SerologyResult">Resultado de Serología al momento del parto o aborto (para gestantes sin serología del tercer trimestre)</param>
        /// <param name="v100_DateFirstTreatmentSifilis">Fecha de primer tratamiento de la Sífilis</param>
        /// <param name="v101_SecondSyphilis">Fecha de segundo tratamiento de la Sífilis </param>
        /// <param name="v102_3rdDateSyphilis">Fecha de tercero tratamiento de la Sífilis</param>
        public RuntimeResult<string> Execute(long v96_SerologySyphilis, long v97_ResultSerology, long v98_ResultSerologyTrimester, long v99_SerologyResult, string v100_DateFirstTreatmentSifilis, string v101_SecondSyphilis, string v102_3rdDateSyphilis)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.v96_SerologySyphilis = v96_SerologySyphilis;
                this.v97_ResultSerology = v97_ResultSerology;
                this.v98_ResultSerologyTrimester = v98_ResultSerologyTrimester;
                this.v99_SerologyResult = v99_SerologyResult;
                this.v100_DateFirstTreatmentSifilis = v100_DateFirstTreatmentSifilis;
                this.v101_SecondSyphilis = v101_SecondSyphilis;
                this.v102_3rdDateSyphilis = v102_3rdDateSyphilis;
                this.VC_v100_DateFirstTreatmentSifilis = FUNC_VC_v100_DateFirstTreatmentSifilis();
                this.VC_v101_SecondSyphilis = FUNC_VC_v101_SecondSyphilis();
                this.VC_v102_3rdDateSyphilis = FUNC_VC_v102_3rdDateSyphilis();
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<string>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v100_DateFirstTreatmentSifilis()
        {
            return Helper.USR_ValidateDateYYYYmm(v100_DateFirstTreatmentSifilis, "Error en la opcion registrada,debe validar que el numero ingresado sea una opcion corrrecta.Validar la variable 100 Fecha de primer tratamiento de la sifilis", "Error en el formato de fecha,debe registrar año y mes.Validar la variable 100 Fecha de primer tratamiento de la sifilis");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v101_SecondSyphilis()
        {
            return Helper.USR_ValidateDateYYYYmm(v101_SecondSyphilis, "Debe introducir un valor correcto con respecto a la fecha del segundo tratamiendo para la sifilis.Validar la variable 101 Fecha de segundo tratamiento de la sifilis", "Error en el formato de fecha,debe registrar año y mes.Validar la variable 101 Fecha de segundo tratamiento de la sifilis");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v102_3rdDateSyphilis()
        {
            return Helper.USR_ValidateDateYYYYmm(v102_3rdDateSyphilis, "Debe introducir un valor correcto de acuerdo a la fecha del tercer tratamiendo de la sifilis.Validar la variable 102 Fecha de tercer tratamiento de la sifilis", "Error en el formato de fecha,debe registrar año y mes.Validar la variable 102 Fecha de tercer tratamiento de la sifilis");
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!((new long[] { 1, 2, 3, 9 }).Contains(v96_SerologySyphilis))) NonValidMessages.Add($"Debe ingresar un dato valido correspondiente al resultado de serologia para sifilis en primer trimestre de gestacion.Validar la variable 96 Resultado de Serología para Sífilis en primer trimestre de gestación");
            if (!((new long[] { 1, 2, 3, 99 }).Contains(v97_ResultSerology))) NonValidMessages.Add($"Debe ingresar un dato valido correspondiente al resultado de serologia para sifilis en segundo trimestre de gestacion.Validar la variable 97 Resultado de Serología para Sífilis en segundo trimestre de gestación");
            if (!((new long[] { 1, 2, 3, 99 }).Contains(v98_ResultSerologyTrimester))) NonValidMessages.Add($"Debe ingresar un dato valido correspondiente al resultado de serologia para sifilis en tercer trimestre de gestacion.Validar la variable 98 Resultado de Serología para Sífilis en tercer trimestre de gestación");
            if (!((new long[] { 1, 2, 3, 99 }).Contains(v99_SerologyResult))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto al resultado de serologia al momento del parto o aborto (para gestantes sin serologia del tercer trimestre).Validar la variable 99");
            if (!(VC_v100_DateFirstTreatmentSifilis == "OK")) NonValidMessages.Add($"{this.VC_v100_DateFirstTreatmentSifilis }");
            if (!(VC_v101_SecondSyphilis == "OK")) NonValidMessages.Add($"{this.VC_v101_SecondSyphilis}");
            if (!(VC_v102_3rdDateSyphilis == "OK")) NonValidMessages.Add($"{this.VC_v102_3rdDateSyphilis}");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return ""; }, $"");
        }
        #endregion
    }
    /// <sumary>
    /// Plantilla de reglas
    /// </sumary> 
    public sealed class RUL_Prophylaxis
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Campo valida Profilaxis ARV para el recién nacido expuesto al VIH(hijo de la madre con infeccion por VIH) Res 4725
        /// </sumary>
        private long v92_ProfilaxisARV;
        /// <sumary>
        /// Campo valida Profilaxispara MAC (Mycobacterium Avium Complex) Res 4725
        /// </sumary>
        private long v92_1_ProfilaxisMAC;
        /// <sumary>
        /// Campo valida Profilaxis con Fluconazol Res 4725
        /// </sumary>
        private long v92_2_ProfilaxisFluconazol;
        /// <sumary>
        /// Campo valida Profilaxis con Trimetoprim Sulfa Res 4725
        /// </sumary>
        private long v92_3_ProfilaxisTrimetoprimSulfa;
        /// <sumary>
        /// Campo valida Profilaxis con inmunoglobulina IV Res 4725
        /// </sumary>
        private long v92_4_ProfilaxisInmunoglobulinaIV;
        /// <sumary>
        /// Campo valida Profilaxis con Isoniacida Res 4725
        /// </sumary>
        private long v92_5_ProfilaxisIsoniacida;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_Prophylaxis() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        /// <param name="v92_ProfilaxisARV">Campo valida Profilaxis ARV para el recién nacido expuesto al VIH(hijo de la madre con infeccion por VIH) Res 4725</param>
        /// <param name="v92_1_ProfilaxisMAC">Campo valida Profilaxispara MAC (Mycobacterium Avium Complex) Res 4725</param>
        /// <param name="v92_2_ProfilaxisFluconazol">Campo valida Profilaxis con Fluconazol Res 4725</param>
        /// <param name="v92_3_ProfilaxisTrimetoprimSulfa">Campo valida Profilaxis con Trimetoprim Sulfa Res 4725</param>
        /// <param name="v92_4_ProfilaxisInmunoglobulinaIV">Campo valida Profilaxis con inmunoglobulina IV Res 4725</param>
        /// <param name="v92_5_ProfilaxisIsoniacida">Campo valida Profilaxis con Isoniacida Res 4725</param>
        public RuntimeResult<string> Execute(long v92_ProfilaxisARV, long v92_1_ProfilaxisMAC, long v92_2_ProfilaxisFluconazol, long v92_3_ProfilaxisTrimetoprimSulfa, long v92_4_ProfilaxisInmunoglobulinaIV, long v92_5_ProfilaxisIsoniacida)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.v92_ProfilaxisARV = v92_ProfilaxisARV;
                this.v92_1_ProfilaxisMAC = v92_1_ProfilaxisMAC;
                this.v92_2_ProfilaxisFluconazol = v92_2_ProfilaxisFluconazol;
                this.v92_3_ProfilaxisTrimetoprimSulfa = v92_3_ProfilaxisTrimetoprimSulfa;
                this.v92_4_ProfilaxisInmunoglobulinaIV = v92_4_ProfilaxisInmunoglobulinaIV;
                this.v92_5_ProfilaxisIsoniacida = v92_5_ProfilaxisIsoniacida;
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<string>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(v92_ProfilaxisARV < 3)) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si el recien nacido recibio profilaxis ARV.Validar la variable 92 'Profilaxis ARV para el recien nacido expuesto al VIH (Hijo de madre con infeccion por VIH)'");
            if (!(v92_1_ProfilaxisMAC < 3)) NonValidMessages.Add($"Debe ingresar un dato valido verificando si la persona recibio profilaxispara.Validar la variable 92.1 'Profilaxispara MAC (Mycobacterium Avium Complex'");
            if (!(v92_3_ProfilaxisTrimetoprimSulfa < 2)) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si la persona tuvo profilaxis con Trimetoprim sulfa.Validar la variable 92.3 'Profilaxis con trimetoprim Sulfa'");
            if (!(v92_4_ProfilaxisInmunoglobulinaIV < 2)) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si la persona tuvo profilaxis con inmunoglobulina IV.Validar la variable 92.4 'Profilaxis con inmunoblobulina IV'");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetInvalid(() => { return ""; }, $"", this.FileName);
        }
        #endregion
    }
    /// <sumary>
    /// Regla Presente-Resolución 4725 de 2011
    /// </sumary> 
    public sealed class RUL_Present
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Variable de codigo de habilitacion
        /// </sumary>
        private string v54_CodeHabilitation;
        /// <sumary>
        /// Fecha de ingreso a la IPS actual, para seguimiento y atención de la infección por el VIH 
        /// </sumary>
        private string v55_IPS;
        /// <sumary>
        /// Codigo de Municipio
        /// </sumary>
        private string v56_Municipality;
        /// <sumary>
        /// Quién hace la atención clínica y formulación para la infección por el VIH al paciente actualmente
        /// </sumary>
        private long v57_Atention;
        /// <sumary>
        /// Valoracion por infectologo
        /// </sumary>
        private long v58_Infectologist;
        /// <sumary>
        /// Ha tenido al menos un resultado de genotipificación
        /// </sumary>
        private long v59_Genotyping;
        /// <sumary>
        /// Momento de la genotipificacion
        /// </sumary>
        private long v60_MomentGenotyping;
        /// <sumary>
        /// Situación clínica actual 
        /// </sumary>
        private long v61_ClinicalSituation;
        /// <sumary>
        /// Estado Clinico Actual
        /// </sumary>
        private long V62_ClinicalSituationState;
        /// <sumary>
        /// Tiene dislipidemia
        /// </sumary>
        private long v62_dyslipidemia;
        /// <sumary>
        /// neuropatía periférica 
        /// </sumary>
        private long v64_NeuropathyPeripheral;
        /// <sumary>
        /// Tiene lipoatrofia o lipodistrofia 
        /// </sumary>
        private long v65_Lipoatrophy_Lipodystrophy;
        /// <sumary>
        /// Tiene coinfección con el VHB
        /// </sumary>
        private long v66_CoinfectionVHB;
        /// <sumary>
        /// coinfección con el VHC
        /// </sumary>
        private long v67_CoinfectionVHC;
        /// <sumary>
        /// Tiene Anemia
        /// </sumary>
        private long v68_Anemia;
        /// <sumary>
        /// Tiene Cirrosis hepática 
        /// </sumary>
        private long v69_LiverCirrhosis;
        /// <sumary>
        /// Renal Cronica
        /// </sumary>
        private long v70_ChronicRenal;
        /// <sumary>
        /// Tiene Enfermedad coronaria 
        /// </sumary>
        private long v71_DiseaseCoronary;
        /// <sumary>
        /// Tiene o ha tenido otras Infecciones de Transmisión Sexual en los últimos 12 meses 
        /// </sumary>
        private long v72_InfectionsSexualTransmission;
        /// <sumary>
        ///  Neoplasia no relacionada con SIDA 
        /// </sumary>
        private long v73_NeoplasmSIDA;
        /// <sumary>
        /// Discapacidad funcional 
        /// </sumary>
        private long v74_FunctionalDisability;
        /// <sumary>
        /// Fecha último conteo de linfocitos T CD4
        /// </sumary>
        private string v75_CountLymphocytes;
        /// <sumary>
        /// Valor del último conteo de linfocitos
        /// </sumary>
        private long v76_ValueLymphocyte;
        /// <sumary>
        /// Fecha último conteo de linfocitos totales
        /// </sumary>
        private string v77_DateTotalLymphocyte;
        /// <sumary>
        /// Valor del último conteo de linfocitos totales 
        /// </sumary>
        private long v78_LastValueLymphocyte;
        /// <sumary>
        /// Fecha última carga viral reportada 
        /// </sumary>
        private string v79_DateViralReported;
        /// <sumary>
        /// Resultado de la última Carga Viral reportada 
        /// </sumary>
        private long v80_LoadViral;
        /// <sumary>
        /// Suministro de condones en los últimos 3 meses 
        /// </sumary>
        private long v81_CondomSupply;
        /// <sumary>
        /// Número de condones suministrados en los últimos 3 meses
        /// </sumary>
        private long v81_1_NumberCondom;
        /// <sumary>
        /// Método de planificación familiar (diferente al condon como metodo de planificacion)
        /// </sumary>
        private long v82_FamilyPlanningMethod;
        /// <sumary>
        /// Vacuna Hepatitis B
        /// </sumary>
        private long v83_HepatitisBVaccine;
        /// <sumary>
        /// Se le realizó PPD en los últimos 12 meses
        /// </sumary>
        private long v84_PPD;
        /// <sumary>
        /// Estudio con carga viral para menores de 18 meses, hijos de madre con infección por VIH
        /// </sumary>
        private long v85_StudyLoadViral;
        /// <sumary>
        /// Estudio con segunda carga viral para menores de 18 meses, hijos de madres con infección por VIH
        /// </sumary>
        private long v86_SecondLoadViral;
        /// <sumary>
        /// Número de cargas virales que se le han realizado al menor de 18 meses para su estudio desde su nacimiento
        /// </sumary>
        private long v87_NumberLoadViral;
        /// <sumary>
        /// Suministro Formula Lactea
        /// </sumary>
        private long v88_FormuleSupply;
        /// <sumary>
        /// Variable de codigo de habilitacion
        /// </sumary>
        private string VC_v54_CodeHabilitation;
        /// <sumary>
        /// Valida la fecha
        /// </sumary>
        private string VC_v55_IPS;
        /// <sumary>
        /// VC_v75_CountLymphocytes
        /// </sumary>
        private string VC_v75_CountLymphocytes;
        /// <sumary>
        /// v77_DateTotalLymphocyte
        /// </sumary>
        private string VC_v77_DateTotalLymphocyte;
        /// <sumary>
        /// v78_LastValueLymphocyte
        /// </sumary>
        private string VC_v78_LastValueLymphocyte;
        /// <sumary>
        /// v79_DateViralReported
        /// </sumary>
        private string VC_v79_DateViralReported;
        /// <sumary>
        /// v87_NumberLoadViral
        /// </sumary>
        private string VC_v87_NumberLoadViral;
        /// <sumary>
        /// v76_ValueLymphocyte
        /// </sumary>
        private string VC_v76_ValueLymphocyte;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_Present() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Regla Presente-Resolución 4725 de 2011
        /// </sumary>
        /// <param name="v54_CodeHabilitation">Variable de codigo de habilitacion</param>
        /// <param name="v55_IPS">Fecha de ingreso a la IPS actual, para seguimiento y atención de la infección por el VIH </param>
        /// <param name="v56_Municipality">Codigo de Municipio</param>
        /// <param name="v57_Atention">Quién hace la atención clínica y formulación para la infección por el VIH al paciente actualmente</param>
        /// <param name="v58_Infectologist">Valoracion por infectologo</param>
        /// <param name="v59_Genotyping">Ha tenido al menos un resultado de genotipificación</param>
        /// <param name="v60_MomentGenotyping">Momento de la genotipificacion</param>
        /// <param name="v61_ClinicalSituation">Situación clínica actual </param>
        /// <param name="V62_ClinicalSituationState">Estado Clinico Actual</param>
        /// <param name="v62_dyslipidemia">Tiene dislipidemia</param>
        /// <param name="v64_NeuropathyPeripheral">neuropatía periférica </param>
        /// <param name="v65_Lipoatrophy_Lipodystrophy">Tiene lipoatrofia o lipodistrofia </param>
        /// <param name="v66_CoinfectionVHB">Tiene coinfección con el VHB</param>
        /// <param name="v67_CoinfectionVHC">coinfección con el VHC</param>
        /// <param name="v68_Anemia">Tiene Anemia</param>
        /// <param name="v69_LiverCirrhosis">Tiene Cirrosis hepática </param>
        /// <param name="v70_ChronicRenal">Renal Cronica</param>
        /// <param name="v71_DiseaseCoronary">Tiene Enfermedad coronaria </param>
        /// <param name="v72_InfectionsSexualTransmission">Tiene o ha tenido otras Infecciones de Transmisión Sexual en los últimos 12 meses </param>
        /// <param name="v73_NeoplasmSIDA"> Neoplasia no relacionada con SIDA </param>
        /// <param name="v74_FunctionalDisability">Discapacidad funcional </param>
        /// <param name="v75_CountLymphocytes">Fecha último conteo de linfocitos T CD4</param>
        /// <param name="v76_ValueLymphocyte">Valor del último conteo de linfocitos</param>
        /// <param name="v77_DateTotalLymphocyte">Fecha último conteo de linfocitos totales</param>
        /// <param name="v78_LastValueLymphocyte">Valor del último conteo de linfocitos totales </param>
        /// <param name="v79_DateViralReported">Fecha última carga viral reportada </param>
        /// <param name="v80_LoadViral">Resultado de la última Carga Viral reportada </param>
        /// <param name="v81_CondomSupply">Suministro de condones en los últimos 3 meses </param>
        /// <param name="v81_1_NumberCondom">Número de condones suministrados en los últimos 3 meses</param>
        /// <param name="v82_FamilyPlanningMethod">Método de planificación familiar (diferente al condon como metodo de planificacion)</param>
        /// <param name="v83_HepatitisBVaccine">Vacuna Hepatitis B</param>
        /// <param name="v84_PPD">Se le realizó PPD en los últimos 12 meses</param>
        /// <param name="v85_StudyLoadViral">Estudio con carga viral para menores de 18 meses, hijos de madre con infección por VIH</param>
        /// <param name="v86_SecondLoadViral">Estudio con segunda carga viral para menores de 18 meses, hijos de madres con infección por VIH</param>
        /// <param name="v87_NumberLoadViral">Número de cargas virales que se le han realizado al menor de 18 meses para su estudio desde su nacimiento</param>
        /// <param name="v88_FormuleSupply">Suministro Formula Lactea</param>
        public RuntimeResult<string> Execute(string v54_CodeHabilitation, string v55_IPS, string v56_Municipality, long v57_Atention, long v58_Infectologist, long v59_Genotyping, long v60_MomentGenotyping, long v61_ClinicalSituation, long V62_ClinicalSituationState, long v62_dyslipidemia, long v64_NeuropathyPeripheral, long v65_Lipoatrophy_Lipodystrophy, long v66_CoinfectionVHB, long v67_CoinfectionVHC, long v68_Anemia, long v69_LiverCirrhosis, long v70_ChronicRenal, long v71_DiseaseCoronary, long v72_InfectionsSexualTransmission, long v73_NeoplasmSIDA, long v74_FunctionalDisability, string v75_CountLymphocytes, long v76_ValueLymphocyte, string v77_DateTotalLymphocyte, long v78_LastValueLymphocyte, string v79_DateViralReported, long v80_LoadViral, long v81_CondomSupply, long v81_1_NumberCondom, long v82_FamilyPlanningMethod, long v83_HepatitisBVaccine, long v84_PPD, long v85_StudyLoadViral, long v86_SecondLoadViral, long v87_NumberLoadViral, long v88_FormuleSupply)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.v54_CodeHabilitation = v54_CodeHabilitation;
                this.v55_IPS = v55_IPS;
                this.v56_Municipality = v56_Municipality;
                this.v57_Atention = v57_Atention;
                this.v58_Infectologist = v58_Infectologist;
                this.v59_Genotyping = v59_Genotyping;
                this.v60_MomentGenotyping = v60_MomentGenotyping;
                this.v61_ClinicalSituation = v61_ClinicalSituation;
                this.V62_ClinicalSituationState = V62_ClinicalSituationState;
                this.v62_dyslipidemia = v62_dyslipidemia;
                this.v64_NeuropathyPeripheral = v64_NeuropathyPeripheral;
                this.v65_Lipoatrophy_Lipodystrophy = v65_Lipoatrophy_Lipodystrophy;
                this.v66_CoinfectionVHB = v66_CoinfectionVHB;
                this.v67_CoinfectionVHC = v67_CoinfectionVHC;
                this.v68_Anemia = v68_Anemia;
                this.v69_LiverCirrhosis = v69_LiverCirrhosis;
                this.v70_ChronicRenal = v70_ChronicRenal;
                this.v71_DiseaseCoronary = v71_DiseaseCoronary;
                this.v72_InfectionsSexualTransmission = v72_InfectionsSexualTransmission;
                this.v73_NeoplasmSIDA = v73_NeoplasmSIDA;
                this.v74_FunctionalDisability = v74_FunctionalDisability;
                this.v75_CountLymphocytes = v75_CountLymphocytes;
                this.v76_ValueLymphocyte = v76_ValueLymphocyte;
                this.v77_DateTotalLymphocyte = v77_DateTotalLymphocyte;
                this.v78_LastValueLymphocyte = v78_LastValueLymphocyte;
                this.v79_DateViralReported = v79_DateViralReported;
                this.v80_LoadViral = v80_LoadViral;
                this.v81_CondomSupply = v81_CondomSupply;
                this.v81_1_NumberCondom = v81_1_NumberCondom;
                this.v82_FamilyPlanningMethod = v82_FamilyPlanningMethod;
                this.v83_HepatitisBVaccine = v83_HepatitisBVaccine;
                this.v84_PPD = v84_PPD;
                this.v85_StudyLoadViral = v85_StudyLoadViral;
                this.v86_SecondLoadViral = v86_SecondLoadViral;
                this.v87_NumberLoadViral = v87_NumberLoadViral;
                this.v88_FormuleSupply = v88_FormuleSupply;
                this.VC_v54_CodeHabilitation = FUNC_VC_v54_CodeHabilitation();
                this.VC_v55_IPS = FUNC_VC_v55_IPS();
                this.VC_v75_CountLymphocytes = FUNC_VC_v75_CountLymphocytes();
                this.VC_v77_DateTotalLymphocyte = FUNC_VC_v77_DateTotalLymphocyte();
                this.VC_v78_LastValueLymphocyte = FUNC_VC_v78_LastValueLymphocyte();
                this.VC_v79_DateViralReported = FUNC_VC_v79_DateViralReported();
                this.VC_v87_NumberLoadViral = FUNC_VC_v87_NumberLoadViral();
                this.VC_v76_ValueLymphocyte = FUNC_VC_v76_ValueLymphocyte();
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<string>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v54_CodeHabilitation()
        {
            return Helper.USR_ValidateIPS(v54_CodeHabilitation);
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v55_IPS()
        {
            return Helper.USR_ValidateDate(v55_IPS, "Debe introducir un valor correcto, el unico valor correcto es 9999-99-99.Validar la variable 55, Fecha de ingreso a la IPS actual, para seguimiento y atención de la infección por el VIH", "Debe ingresar una fecha correcta,validar que la fecha sea de la siguiente manera: Año-Mes-Dia.Validar la variable 55 Fecha de ingreso a la IPS actual, para seguimiento y atención de la infección por el VIH");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v75_CountLymphocytes()
        {
            return Helper.USR_ValidateDate(v75_CountLymphocytes, "Error en la fecha,debe verificar que sea registrada de la siguiente manera: Año-Mes-Dia", "Debe introducir un valor correcto.Validar  la variable 75");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v77_DateTotalLymphocyte()
        {
            return Helper.USR_ValidateDate(v77_DateTotalLymphocyte, "Debe introducir un valor correcto.Validar la variable 77", "Error en la fecha,debe verificar que la fecha sea registrada de la siguiente manera: AAAA-MM-DD. Validar la variable 77 Fecha último conteo de linfocitos totales");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v78_LastValueLymphocyte()
        {
            return Helper.USR_F_v78_LastValueLymphocyte(v78_LastValueLymphocyte);
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v79_DateViralReported()
        {
            return Helper.USR_ValidateDate(v79_DateViralReported, "Debe introducir un valor correcto,debe verificar que el dato registrado sea  9999-99-99.Validar la variable 79 Fecha última carga viral reportada", "Error en la fecha, debe verificar que la fecha venga de la siguiente manera: Año-Mes-Dia.Validar la variable 79 Fecha última carga viral reportada");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v87_NumberLoadViral()
        {
            if (v87_NumberLoadViral > 0 && v87_NumberLoadViral <= 12)
                return "OK";

            if (v87_NumberLoadViral != 99)
                return "Debe ingresar un dato valido,verificar si el menor tiene mas de 18 meses de vida.Validar la variable 87 Numero de cargas virales que se la han realizado al menor de 18 meses para su estudio desde su nacimiento y variable 10 Fecha de nacimiento";

            return "Debe ingresar un dato valido con respecto al rango de valores o verificar si el menor de edad tenga 18 meses o menos de vida.Validar la variable 87  Numero de cargas virales que se la han realizado al menor de 18 meses para su estudio desde su nacimiento y variable 10 Fecha de nacimiento";


        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v76_ValueLymphocyte()
        {
            if ((v76_ValueLymphocyte >= 0 && v76_ValueLymphocyte <= 5000) || (v76_ValueLymphocyte == 99999))
                return "OK";

            if (v76_ValueLymphocyte != 99999)
                return "Debe introducir un valor correcto,debe tener en cuenta que el unico valor valido es 99999.Valdiar la variable 76 Valor del último conteo de linfocitos T CD4+";

            return "Debe introducir un valor con respecto al rango teniendo en cuenta el numero de linfocitos T CD4+ del uitmo conteo.Validar la variable 76 Valor del último conteo de linfocitos T CD4+";

        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_v54_CodeHabilitation == "OK")) NonValidMessages.Add($"{this.VC_v54_CodeHabilitation}");
            if (!(VC_v55_IPS == "OK")) NonValidMessages.Add($"{this.VC_v55_IPS}");
            if (!((new long[] { 0, 1, 2, 3, 4, 5, 6, 9 }).Contains(v57_Atention))) NonValidMessages.Add($"Debe ingresar un dato valido, con respecto a la persona que realiza la atencion medica.Validar la variable 57 Quién hace la atención clínica y formulación para la infección por el VIH al paciente actualmente");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v58_Infectologist))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto a la valoracion por el infectologo.Validar la variable 58 Valoración por Infectólogo en los últimos 6 meses");
            if (!((new long[] { 0, 1, 2, 9 }).Contains(v59_Genotyping))) NonValidMessages.Add($"Debe ingresar un dato valido de acuerdo si ha tenido un resultado de genotipificacion.Validar la variable 59 Ha tenido al menos un resultado de genotipificación");
            if (!((new long[] { 0, 1, 2, 3, 4, 5, 6, 9 }).Contains(v60_MomentGenotyping))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto al momento de la genotipificacion.Validar la variable 60, Momento de la genotipificacion");
            if (!((new long[] { 0, 1, 2, 9 }).Contains(v61_ClinicalSituation))) NonValidMessages.Add($"Debe ingresar un dato correcto con respecto a la situacion de la clinica actual.Validar la variable 61 Situación clínica actual");
            if (!((new long[] { 1, 2, 3, 9 }).Contains(V62_ClinicalSituationState))) NonValidMessages.Add($"Debe ingresar un dato valido de acuerdo al estadio clinico actual.Validar la variable 62, Estadio clínico actual");
            if (!((new long[] { 0, 1 }).Contains(v62_dyslipidemia))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si la persona tiene dislipidemia.Validar la variable 63 Tiene dislipidemia");
            if (!((new long[] { 0, 1 }).Contains(v64_NeuropathyPeripheral))) NonValidMessages.Add($"Debe ingresar un dato valido de acuerdo si la persona tiene neuropatia periferica.Validar la variable 64 Tiene neuropatía periférica");
            if (!((new long[] { 0, 1 }).Contains(v65_Lipoatrophy_Lipodystrophy))) NonValidMessages.Add($"Debe ingresar un dato valido validando si la persona tiene lipoatrofia o lipodistrofia.Validar la variable 65 Tiene lipoatrofia o lipodistrofia");
            if (!((new long[] { 0, 1 }).Contains(v66_CoinfectionVHB))) NonValidMessages.Add($"Debe ingresar un dato valido verificando si la persona tiene coinfeccion con el VHB.Validar la variable 66 Tiene coinfección con el VHB");
            if (!((new long[] { 0, 1 }).Contains(v67_CoinfectionVHC))) NonValidMessages.Add($"Debe ingresar un dato valido verificando si la persona tiene coinfeccion con el VHC.Validar la variable 67 Tiene coinfección con el VHC");
            if (!((new long[] { 0, 1 }).Contains(v68_Anemia))) NonValidMessages.Add($"Debe ingresar un dato valido verficando que la persona tiene anemia.Validar la variable 68,Tiene Anemia");
            if (!((new long[] { 0, 1 }).Contains(v69_LiverCirrhosis))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si la persona tiene cirrosis hepatica.Validar la variable 69 Tiene Cirrosis hepática");
            if (!((new long[] { 0, 1 }).Contains(v70_ChronicRenal))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si la persona tiene enfermedad renal cronica.Validar la variable 70 Tiene Enfermedad renal crónica");
            if (!((new long[] { 1, 0 }).Contains(v71_DiseaseCoronary))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si la persona tiene enfermedad coro naria.Validar la variable 71 Tiene Enfermedad coronaria");
            if (!((new long[] { 0, 1 }).Contains(v72_InfectionsSexualTransmission))) NonValidMessages.Add($"Debe ingresar un dato valido validando si tiene o ha tenido otras infecciones de transmision sexual en los ultimos 12 meses.Validar la variable 72 Tiene o ha tenido otras infecciones de transmision sexual en los ultimos 12 meses");
            if (!((new long[] { 0, 1 }).Contains(v73_NeoplasmSIDA))) NonValidMessages.Add($"Error de dato,verifique que haya digitado las opciones correctas.Validar la variable 73 Tiene o ha tenido otra neoplasia no relacionada con SIDA");
            if (!((new long[] { 0, 1, 2 }).Contains(v74_FunctionalDisability))) NonValidMessages.Add($"Debe ingresar un dato valido,verifique que las opciones son correctas teniendo en cuenta la discapacidad funcional.Validar la variable 74 Discapacidad funcional");
            if (!(VC_v75_CountLymphocytes == "OK")) NonValidMessages.Add($"{this.VC_v75_CountLymphocytes}");
            if (!(VC_v77_DateTotalLymphocyte == "OK")) NonValidMessages.Add($"{this.VC_v77_DateTotalLymphocyte}");
            if (!(VC_v79_DateViralReported == "OK")) NonValidMessages.Add($"{this.VC_v79_DateViralReported}");
            if (!(VC_v78_LastValueLymphocyte == "OK")) NonValidMessages.Add($"{this.VC_v78_LastValueLymphocyte}");
            if (!((new long[] { 0, 1, 2, 9 }).Contains(v80_LoadViral))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto al resultado de la ultima carga vrial reportada.Validar la variable 80 Resultado de la ultima carga viral reportada");
            if (!((new long[] { 0, 1, 3, 9 }).Contains(v81_CondomSupply))) NonValidMessages.Add($"Debe registrar una opcion correcta verificando el suministro de condones en los ultimos 3 meses.Validar la variable 81 Suministros de condones en los ultimos 3 meses");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v81_1_NumberCondom))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto al numero de condones suministrados en los ultimos 3 meses.Validar la variable 81.1 Numero de condones suministrados en los ultimos 3 meses");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v82_FamilyPlanningMethod))) NonValidMessages.Add($"Debe ingresar una opcion correcta con respecto al metodo de planificacion farmiliar.Validar la variable 82 Método de planificación familiar (diferente al condon como metodo de planificacion)");
            if (!((new long[] { 0, 1, 2, 3, 9 }).Contains(v83_HepatitisBVaccine))) NonValidMessages.Add($"Debe ingresar un dato valido teniendo en cuenta si la persona tiene vacuna hepatitis B.Validar la variable 83 Vacuna Hepatitis B");
            if (!((new long[] { 0, 1, 2, 9 }).Contains(v84_PPD))) NonValidMessages.Add($"Debe ingresar una opcion correcta validando si se le realizo PPD en los ultimos 12 meses.Validar la variable 84 Se le realizo PPD en los ultimos 12 meses");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v85_StudyLoadViral))) NonValidMessages.Add($"Debe ingresar un dato valido,verificar si el menor tiene 18 o menos meses de vida.Validar la variable 85 Estudio con carga viral para menores de 18 meses,hijos de madre con infeccion por VIH y variable 10 Fecha de nacimiento");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v86_SecondLoadViral))) NonValidMessages.Add($"Debe ingresar un dato valido,verificar si el menor tiene 18 o menos meses de vida.Validar la variable 86 Estudio con segunda carga viral para menores de 18 meses,hijos de madre con infeccion por VIH y variable 10 Fecha de nacimiento");
            if (!(VC_v87_NumberLoadViral == "OK")) NonValidMessages.Add($"{this.VC_v87_NumberLoadViral}");
            if (!((new long[] { 0, 1, 9 }).Contains(v88_FormuleSupply))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto al suministro de formula lactea.Validar la variable 88 Formula lactea");
            if (!(VC_v76_ValueLymphocyte == "OK")) NonValidMessages.Add($"{this.VC_v76_ValueLymphocyte}");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetInvalid(() => { return ""; }, $"", this.FileName);
        }
        #endregion
    }
    /// <sumary>
    /// Plantilla de reglas
    /// </sumary> 
    public sealed class RUL_PathologiesThatDefineVIH
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Candidiasis esofágica, traqueal, bronquial o pulmonar Res 4725
        /// </sumary>
        private long v53_1Candiesotraqbronqpul;
        /// <sumary>
        /// Campo Valida si tiene o ha tenido Tuberculosis extra pulmonar o pulmonar Res 4725
        /// </sumary>
        private long v53_2PulmonaryTuberculosis;
        /// <sumary>
        /// Campo Valida si tiene o ha tenido Cáncer de cérvix invasivo Res 4725
        /// </sumary>
        private long v53_3InvasiveCervicalCancer;
        /// <sumary>
        /// Campo Valida si tiene o ha tenido demencia asociada al VIH (deterioro cognitivo o de otras funciones que interfiere con las actividades laborales Res 4725
        /// </sumary>
        private long v53_4DementiaAssociatedvih;
        /// <sumary>
        /// Campo Valida si tiene o ha tenido Coccidioidomicosisextrapulmonar Res 4725
        /// </sumary>
        private long v53_5ExtrapulmonaryCoccidioidomycosis;
        /// <sumary>
        /// Campo Valida si tiene o ha tenido Infección por Citomegalovírus (CMV) de cualquier órgano excepto hígado, bazo, o ganglios linfáticos Res 4725
        /// </sumary>
        private long v53_6CmvInfection;
        /// <sumary>
        /// Campo Valida o ha tenido Síndrome de desgaste o caquexia asociada a VIH Res 4725
        /// </sumary>
        private long v53_7SimpleHerpes;
        /// <sumary>
        /// Campo Valida o ha tenido Diarrea por Isospora belli o Cryptosporidium de más de un mes de duración Res 4725
        /// </sumary>
        private long v53_8DiarrheaIsospora;
        /// <sumary>
        /// Campo Valida tiene o ha tenido Histoplasmosis extra pulmonar Res 4725
        /// </sumary>
        private long v53_9ExtrapulmonaryHistoplasmosis;
        /// <sumary>
        /// Campo Valida tiene o ha tenido Linfoma de Burkitt, inmunoblástico, o primario del sistema nervioso central Res 4725
        /// </sumary>
        private long v53_10Burkittlymphoma;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Neumonía por Pneumocystiscarinii (jirovecii) Res 4725
        /// </sumary>
        private long v53_11PneumoniapNeumocystiscarinii;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Neumonía bacteriana recurrente (2 o más episodios en 1 año) Res 4725
        /// </sumary>
        private long v53_12RecurrentBacterialpNeumonia;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Septicemia por Salmonella (no tifoidea) Res 4725
        /// </sumary>
        private long v53_13SalmonellaSepticemia;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Infección diseminada por Micobacteriumavium (MAC) o Kansasii (MAI) Res 4725
        /// </sumary>
        private long v53_14DisseminatedInfectionmacmai;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Criptococosisextrapulmonar Res 4725
        /// </sumary>
        private long v53_15CriptococosiSexTrapulmonar;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Sarcoma de Kaposi Res 4725
        /// </sumary>
        private long v53_16Kaposisarcoma;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Síndrome de desgaste o caquexia asociada a VIH Res 4725
        /// </sumary>
        private long v53_17Wearsyndrome;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Leucoencefalopatia multifocal progresiva o encefalopatía por VIH Res 4725
        /// </sumary>
        private long v53_18Leukoencefelopatia;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Neumonía intersticial linfoidea Res 4725
        /// </sumary>
        private long v53_19LymphoidinterstitialpNeumonia;
        /// <sumary>
        /// Campo Valida Tiene o ha tenido Toxoplasmosis cerebral Res 4725
        /// </sumary>
        private long v53_20CerebralToxoplasmosis;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_PathologiesThatDefineVIH() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        /// <param name="v53_1Candiesotraqbronqpul">Campo Valida Tiene o ha tenido Candidiasis esofágica, traqueal, bronquial o pulmonar Res 4725</param>
        /// <param name="v53_2PulmonaryTuberculosis">Campo Valida si tiene o ha tenido Tuberculosis extra pulmonar o pulmonar Res 4725</param>
        /// <param name="v53_3InvasiveCervicalCancer">Campo Valida si tiene o ha tenido Cáncer de cérvix invasivo Res 4725</param>
        /// <param name="v53_4DementiaAssociatedvih">Campo Valida si tiene o ha tenido demencia asociada al VIH (deterioro cognitivo o de otras funciones que interfiere con las actividades laborales Res 4725</param>
        /// <param name="v53_5ExtrapulmonaryCoccidioidomycosis">Campo Valida si tiene o ha tenido Coccidioidomicosisextrapulmonar Res 4725</param>
        /// <param name="v53_6CmvInfection">Campo Valida si tiene o ha tenido Infección por Citomegalovírus (CMV) de cualquier órgano excepto hígado, bazo, o ganglios linfáticos Res 4725</param>
        /// <param name="v53_7SimpleHerpes">Campo Valida o ha tenido Síndrome de desgaste o caquexia asociada a VIH Res 4725</param>
        /// <param name="v53_8DiarrheaIsospora">Campo Valida o ha tenido Diarrea por Isospora belli o Cryptosporidium de más de un mes de duración Res 4725</param>
        /// <param name="v53_9ExtrapulmonaryHistoplasmosis">Campo Valida tiene o ha tenido Histoplasmosis extra pulmonar Res 4725</param>
        /// <param name="v53_10Burkittlymphoma">Campo Valida tiene o ha tenido Linfoma de Burkitt, inmunoblástico, o primario del sistema nervioso central Res 4725</param>
        /// <param name="v53_11PneumoniapNeumocystiscarinii">Campo Valida Tiene o ha tenido Neumonía por Pneumocystiscarinii (jirovecii) Res 4725</param>
        /// <param name="v53_12RecurrentBacterialpNeumonia">Campo Valida Tiene o ha tenido Neumonía bacteriana recurrente (2 o más episodios en 1 año) Res 4725</param>
        /// <param name="v53_13SalmonellaSepticemia">Campo Valida Tiene o ha tenido Septicemia por Salmonella (no tifoidea) Res 4725</param>
        /// <param name="v53_14DisseminatedInfectionmacmai">Campo Valida Tiene o ha tenido Infección diseminada por Micobacteriumavium (MAC) o Kansasii (MAI) Res 4725</param>
        /// <param name="v53_15CriptococosiSexTrapulmonar">Campo Valida Tiene o ha tenido Criptococosisextrapulmonar Res 4725</param>
        /// <param name="v53_16Kaposisarcoma">Campo Valida Tiene o ha tenido Sarcoma de Kaposi Res 4725</param>
        /// <param name="v53_17Wearsyndrome">Campo Valida Tiene o ha tenido Síndrome de desgaste o caquexia asociada a VIH Res 4725</param>
        /// <param name="v53_18Leukoencefelopatia">Campo Valida Tiene o ha tenido Leucoencefalopatia multifocal progresiva o encefalopatía por VIH Res 4725</param>
        /// <param name="v53_19LymphoidinterstitialpNeumonia">Campo Valida Tiene o ha tenido Neumonía intersticial linfoidea Res 4725</param>
        /// <param name="v53_20CerebralToxoplasmosis">Campo Valida Tiene o ha tenido Toxoplasmosis cerebral Res 4725</param>
        public RuntimeResult<string> Execute(long v53_1Candiesotraqbronqpul, long v53_2PulmonaryTuberculosis, long v53_3InvasiveCervicalCancer, long v53_4DementiaAssociatedvih, long v53_5ExtrapulmonaryCoccidioidomycosis, long v53_6CmvInfection, long v53_7SimpleHerpes, long v53_8DiarrheaIsospora, long v53_9ExtrapulmonaryHistoplasmosis, long v53_10Burkittlymphoma, long v53_11PneumoniapNeumocystiscarinii, long v53_12RecurrentBacterialpNeumonia, long v53_13SalmonellaSepticemia, long v53_14DisseminatedInfectionmacmai, long v53_15CriptococosiSexTrapulmonar, long v53_16Kaposisarcoma, long v53_17Wearsyndrome, long v53_18Leukoencefelopatia, long v53_19LymphoidinterstitialpNeumonia, long v53_20CerebralToxoplasmosis)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.v53_1Candiesotraqbronqpul = v53_1Candiesotraqbronqpul;
                this.v53_2PulmonaryTuberculosis = v53_2PulmonaryTuberculosis;
                this.v53_3InvasiveCervicalCancer = v53_3InvasiveCervicalCancer;
                this.v53_4DementiaAssociatedvih = v53_4DementiaAssociatedvih;
                this.v53_5ExtrapulmonaryCoccidioidomycosis = v53_5ExtrapulmonaryCoccidioidomycosis;
                this.v53_6CmvInfection = v53_6CmvInfection;
                this.v53_7SimpleHerpes = v53_7SimpleHerpes;
                this.v53_8DiarrheaIsospora = v53_8DiarrheaIsospora;
                this.v53_9ExtrapulmonaryHistoplasmosis = v53_9ExtrapulmonaryHistoplasmosis;
                this.v53_10Burkittlymphoma = v53_10Burkittlymphoma;
                this.v53_11PneumoniapNeumocystiscarinii = v53_11PneumoniapNeumocystiscarinii;
                this.v53_12RecurrentBacterialpNeumonia = v53_12RecurrentBacterialpNeumonia;
                this.v53_13SalmonellaSepticemia = v53_13SalmonellaSepticemia;
                this.v53_14DisseminatedInfectionmacmai = v53_14DisseminatedInfectionmacmai;
                this.v53_15CriptococosiSexTrapulmonar = v53_15CriptococosiSexTrapulmonar;
                this.v53_16Kaposisarcoma = v53_16Kaposisarcoma;
                this.v53_17Wearsyndrome = v53_17Wearsyndrome;
                this.v53_18Leukoencefelopatia = v53_18Leukoencefelopatia;
                this.v53_19LymphoidinterstitialpNeumonia = v53_19LymphoidinterstitialpNeumonia;
                this.v53_20CerebralToxoplasmosis = v53_20CerebralToxoplasmosis;
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<string>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(v53_4DementiaAssociatedvih < 2)) NonValidMessages.Add($"Debe ingresar un dato valido, las únicas opciones correctas son 0 y 1 .Validar la variable 53.4 'Tiene o ha tenido demencia asociada al VIH (deterioro cognitivo o de otras funciones que interfiere con las actividades laborales'");
            if (!(v53_5ExtrapulmonaryCoccidioidomycosis < 2)) NonValidMessages.Add($"Error en el dato registrado, las opciones correctas son 0 y 1.Validar la variable 53.5 'Tiene o ha tenido Coccidioidomicosisextrapulmonar'");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetInvalid(() => { return ""; }, $"", this.FileName);
        }
        #endregion
    }
    /// <sumary>
    /// Resolucion 4725 de 2011
    /// </sumary> 
    public sealed class RUL_Past_
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Fecha PruebaPresuntiva
        /// </sumary>
        private string v21_DatePresuntive;
        /// <sumary>
        /// Fecha Diagnostico  Infeccion
        /// </sumary>
        private string v22_DateInfectionDiagnostic;
        /// <sumary>
        /// Prueba Presuntiva
        /// </sumary>
        private long v23_TestPresuntive;
        /// <sumary>
        /// Aseguramiento al momento del diagnostico
        /// </sumary>
        private long v24_AsuranceDiagnostic;
        /// <sumary>
        /// Codigo EPS o DANE
        /// </sumary>
        private string v25_EPS_DANE;
        /// <sumary>
        /// Mecanismo de Transmision
        /// </sumary>
        private string v26_TransmissionMechanism;
        /// <sumary>
        /// Estudio Clínico al momento del diagnostico
        /// </sumary>
        private long v27_StudyClinicDiagnostic;
        /// <sumary>
        /// Conteo de linfocitos T CD4+ al momento del diagnóstico
        /// </sumary>
        private long v28_LymphocyteCount;
        /// <sumary>
        /// Conteo de linfocitos totales al momento del diagnóstico
        /// </sumary>
        private long v29_TotalLymphocytes;
        /// <sumary>
        /// Obtiene la respuesta de la validacion 21
        /// </sumary>
        private string VC_v21_DatePresuntive;
        /// <sumary>
        /// Validacion de Fecha Diagnostico infeccion
        /// </sumary>
        private string VC_v22_DateDiagnostic;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_Past_() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Resolucion 4725 de 2011
        /// </sumary>
        /// <param name="v21_DatePresuntive">Fecha PruebaPresuntiva</param>
        /// <param name="v22_DateInfectionDiagnostic">Fecha Diagnostico  Infeccion</param>
        /// <param name="v23_TestPresuntive">Prueba Presuntiva</param>
        /// <param name="v24_AsuranceDiagnostic">Aseguramiento al momento del diagnostico</param>
        /// <param name="v25_EPS_DANE">Codigo EPS o DANE</param>
        /// <param name="v26_TransmissionMechanism">Mecanismo de Transmision</param>
        /// <param name="v27_StudyClinicDiagnostic">Estudio Clínico al momento del diagnostico</param>
        /// <param name="v28_LymphocyteCount">Conteo de linfocitos T CD4+ al momento del diagnóstico</param>
        /// <param name="v29_TotalLymphocytes">Conteo de linfocitos totales al momento del diagnóstico</param>
        public RuntimeResult<string> Execute(string v21_DatePresuntive, string v22_DateInfectionDiagnostic, long v23_TestPresuntive, long v24_AsuranceDiagnostic, string v25_EPS_DANE, string v26_TransmissionMechanism, long v27_StudyClinicDiagnostic, long v28_LymphocyteCount, long v29_TotalLymphocytes)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.v21_DatePresuntive = v21_DatePresuntive;
                this.v22_DateInfectionDiagnostic = v22_DateInfectionDiagnostic;
                this.v23_TestPresuntive = v23_TestPresuntive;
                this.v24_AsuranceDiagnostic = v24_AsuranceDiagnostic;
                this.v25_EPS_DANE = v25_EPS_DANE;
                this.v26_TransmissionMechanism = v26_TransmissionMechanism;
                this.v27_StudyClinicDiagnostic = v27_StudyClinicDiagnostic;
                this.v28_LymphocyteCount = v28_LymphocyteCount;
                this.v29_TotalLymphocytes = v29_TotalLymphocytes;
                this.VC_v21_DatePresuntive = FUNC_VC_v21_DatePresuntive();
                this.VC_v22_DateDiagnostic = FUNC_VC_v22_DateDiagnostic();
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<string>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v21_DatePresuntive()
        {
            return Helper.USR_Validate21(v21_DatePresuntive);
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v22_DateDiagnostic()
        {
            return Helper.USR_InfectionDiagnost(v22_DateInfectionDiagnostic);
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_v21_DatePresuntive == "OK")) NonValidMessages.Add($"{this.VC_v21_DatePresuntive}");
            if (!(VC_v22_DateDiagnostic == "OK")) NonValidMessages.Add($"{this.VC_v22_DateDiagnostic}");
            if (!(v23_TestPresuntive <= 10)) NonValidMessages.Add($"Debe registrar una opcion correcta.Validar variable 23, Como llego a la prueba presuntiva");
            if (!((new long[] { 1, 2, 3, 4, 5, 6, 9 }).Contains(v24_AsuranceDiagnostic))) NonValidMessages.Add($"Debe registrar una opcion valida.Validar la variable 24 ,Aseguramiento al momento del diagnostico");
            if (!((new string[] { "0", "1", "2", "3", "4", "5", "6", "9" }).Contains(v26_TransmissionMechanism))) NonValidMessages.Add($"Debe registrar una opcion valida, validando el mecanimo de transmision.Validar variable 26 Mecanismo de transmision");
            if (!(v27_StudyClinicDiagnostic <= 11)) NonValidMessages.Add($"Debe registrar un valor valido.Validar la variable 27 Estadio clinico al momento del diagnostico(Adolecentes de 13 años en adelante y adultos)");
            if (!(v28_LymphocyteCount >= 0 && v28_LymphocyteCount <= 99005)) NonValidMessages.Add($"Error en el dato, verifique que esté en el rango disponible. Validar variable 28 Conteo de linfocitos T CD4+ al momento del diagnóstico");
            if (!(v29_TotalLymphocytes >= 0 && v29_TotalLymphocytes <= 99005)) NonValidMessages.Add($"Error en el dato, verifique que el valor ingresado se encuentre dentro del rango de valores.Validar variable 29 Conteo de linfocitos totales al momento del diagnóstico");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return "1"; }, $"");
        }
        #endregion
    }
    /// <sumary>
    /// Regla para validar identidad y demografía resolución 4725
    /// </sumary> 
    public sealed class RUL_IdentityDemography4725
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Validar el campo exacto en la tabla operator
        /// </sumary>
        private string v1CodeEps;
        /// <sumary>
        /// Tipo de regimen Resolucion 4725
        /// </sumary>
        private string v2RegimeType;
        /// <sumary>
        /// Campo grupo poblacional 4725
        /// </sumary>
        private string v3PopulationGroup;
        /// <sumary>
        /// Campo primer nombre Res 4725
        /// </sumary>
        private string v4FirstName;
        /// <sumary>
        /// Segundo Nombre Res 4725
        /// </sumary>
        private string v5SecondName;
        /// <sumary>
        /// Campo primer Apellido Res 4725
        /// </sumary>
        private string v6FirstLastName;
        /// <sumary>
        /// Campo Segundo Apellido Res 4725
        /// </sumary>
        private string v7SecondLastName;
        /// <sumary>
        /// Campo Tipo de Documento Res 4725
        /// </sumary>
        private string v8IdentificationType;
        /// <sumary>
        /// Campo Numero de Documento Res 4725
        /// </sumary>
        private string v9DocumentNumber;
        /// <sumary>
        /// Campo Fecha de nacimiento Res 4725
        /// </sumary>
        private string v10BirthDate;
        /// <sumary>
        /// Campo Validar Sexo Res 4725
        /// </sumary>
        private string v11IdSex;
        /// <sumary>
        /// Campo Valida pertenencia etnica Res 4725
        /// </sumary>
        private string v12CodeEthnic;
        /// <sumary>
        /// Campo Valida direccion de residencia Res 4725
        /// </sumary>
        private string v13ResidenceAddress;
        /// <sumary>
        /// Campo Valida Telefono de contacto Res 4725
        /// </sumary>
        private string v14TelephoneContact;
        /// <sumary>
        /// Campo Valida Código municipio de residencia Res 4725
        /// </sumary>
        private string v15MunicipalityResidence;
        /// <sumary>
        /// Campo Valida FECHA DE AFILIACION A EPS Res 4725
        /// </sumary>
        private string v16AffiliationDate;
        /// <sumary>
        /// Campo Valida Persona Gestan Res 4725
        /// </sumary>
        private string v17PregnantPerson;
        /// <sumary>
        /// Campo Valida Persona con Tuberculosis Res 4725
        /// </sumary>
        private string v18TuberculosisPerson;
        /// <sumary>
        /// Hijo menor de 18 meses con madre con VIH
        /// </sumary>
        private string v19PersonUnder18MonthMotherhiv;
        /// <sumary>
        /// Campo Valida Condicion del diagnostico por VIH Res 4725
        /// </sumary>
        private string v20ConditionDiagnosishivInfection;
        /// <sumary>
        /// Primer Nombre
        /// </sumary>
        private long VC_V4FirstName;
        /// <sumary>
        /// Valida longitud segundo nombre
        /// </sumary>
        private long VC_V5SecondName;
        /// <sumary>
        /// Valida longitud Primer Apellido
        /// </sumary>
        private long VC_V6LastName;
        /// <sumary>
        /// Valida longitud segundo apellido
        /// </sumary>
        private long VC_V7SecondSurname;
        /// <sumary>
        /// Valida si la variable numero de documento es diferente de null y el regimen es subsidiado
        /// </sumary>
        private bool VC_V9NumberDocument;
        /// <sumary>
        /// Valida longitud direccion de residencia
        /// </sumary>
        private long VC_v13ResidenceAddress;
        /// <sumary>
        /// Valida longitud telefono de contacto
        /// </sumary>
        private long VC_v14TelephoneContact;
        /// <sumary>
        /// Valida longitud Código municipio de residencia
        /// </sumary>
        private long VC_v15CodeMunicipaly;
        /// <sumary>
        /// Funcion integrada USR_ValidatePregnantMother4725
        /// </sumary>
        private string VC_v17PregnantMother;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_IdentityDemography4725() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Regla para validar identidad y demografía resolución 4725
        /// </sumary>
        /// <param name="v1CodeEps">Validar el campo exacto en la tabla operator</param>
        /// <param name="v2RegimeType">Tipo de regimen Resolucion 4725</param>
        /// <param name="v3PopulationGroup">Campo grupo poblacional 4725</param>
        /// <param name="v4FirstName">Campo primer nombre Res 4725</param>
        /// <param name="v5SecondName">Segundo Nombre Res 4725</param>
        /// <param name="v6FirstLastName">Campo primer Apellido Res 4725</param>
        /// <param name="v7SecondLastName">Campo Segundo Apellido Res 4725</param>
        /// <param name="v8IdentificationType">Campo Tipo de Documento Res 4725</param>
        /// <param name="v9DocumentNumber">Campo Numero de Documento Res 4725</param>
        /// <param name="v10BirthDate">Campo Fecha de nacimiento Res 4725</param>
        /// <param name="v11IdSex">Campo Validar Sexo Res 4725</param>
        /// <param name="v12CodeEthnic">Campo Valida pertenencia etnica Res 4725</param>
        /// <param name="v13ResidenceAddress">Campo Valida direccion de residencia Res 4725</param>
        /// <param name="v14TelephoneContact">Campo Valida Telefono de contacto Res 4725</param>
        /// <param name="v15MunicipalityResidence">Campo Valida Código municipio de residencia Res 4725</param>
        /// <param name="v16AffiliationDate">Campo Valida FECHA DE AFILIACION A EPS Res 4725</param>
        /// <param name="v17PregnantPerson">Campo Valida Persona Gestan Res 4725</param>
        /// <param name="v18TuberculosisPerson">Campo Valida Persona con Tuberculosis Res 4725</param>
        /// <param name="v19PersonUnder18MonthMotherhiv">Hijo menor de 18 meses con madre con VIH</param>
        /// <param name="v20ConditionDiagnosishivInfection">Campo Valida Condicion del diagnostico por VIH Res 4725</param>
        public RuntimeResult<string> Execute(string v1CodeEps, string v2RegimeType, string v3PopulationGroup, string v4FirstName, string v5SecondName, string v6FirstLastName, string v7SecondLastName, string v8IdentificationType, string v9DocumentNumber, string v10BirthDate, string v11IdSex, string v12CodeEthnic, string v13ResidenceAddress, string v14TelephoneContact, string v15MunicipalityResidence, string v16AffiliationDate, string v17PregnantPerson, string v18TuberculosisPerson, string v19PersonUnder18MonthMotherhiv, string v20ConditionDiagnosishivInfection)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.v1CodeEps = v1CodeEps;
                this.v2RegimeType = v2RegimeType;
                this.v3PopulationGroup = v3PopulationGroup;
                this.v4FirstName = v4FirstName;
                this.v5SecondName = v5SecondName;
                this.v6FirstLastName = v6FirstLastName;
                this.v7SecondLastName = v7SecondLastName;
                this.v8IdentificationType = v8IdentificationType;
                this.v9DocumentNumber = v9DocumentNumber;
                this.v10BirthDate = v10BirthDate;
                this.v11IdSex = v11IdSex;
                this.v12CodeEthnic = v12CodeEthnic;
                this.v13ResidenceAddress = v13ResidenceAddress;
                this.v14TelephoneContact = v14TelephoneContact;
                this.v15MunicipalityResidence = v15MunicipalityResidence;
                this.v16AffiliationDate = v16AffiliationDate;
                this.v17PregnantPerson = v17PregnantPerson;
                this.v18TuberculosisPerson = v18TuberculosisPerson;
                this.v19PersonUnder18MonthMotherhiv = v19PersonUnder18MonthMotherhiv;
                this.v20ConditionDiagnosishivInfection = v20ConditionDiagnosishivInfection;
                this.VC_V4FirstName = FUNC_VC_V4FirstName();
                this.VC_V5SecondName = FUNC_VC_V5SecondName();
                this.VC_V6LastName = FUNC_VC_V6LastName();
                this.VC_V7SecondSurname = FUNC_VC_V7SecondSurname();
                this.VC_V9NumberDocument = FUNC_VC_V9NumberDocument();
                this.VC_v13ResidenceAddress = FUNC_VC_v13ResidenceAddress();
                this.VC_v14TelephoneContact = FUNC_VC_v14TelephoneContact();
                this.VC_v15CodeMunicipaly = FUNC_VC_v15CodeMunicipaly();
                this.VC_v17PregnantMother = FUNC_VC_v17PregnantMother();
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<string>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        /// <sumary>
        ///	
        /// </sumary>
        private long FUNC_VC_V4FirstName()
        {
            return v4FirstName.ToString().Length;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private long FUNC_VC_V5SecondName()
        {
            return v5SecondName.Length;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private long FUNC_VC_V6LastName()
        {
            return v6FirstLastName.Length;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private long FUNC_VC_V7SecondSurname()
        {
            return v7SecondLastName.Length;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_V9NumberDocument()
        {
            return Helper.USR_ValidateDocumentRes4725(v9DocumentNumber, v2RegimeType);
        }
        /// <sumary>
        ///	
        /// </sumary>
        private long FUNC_VC_v13ResidenceAddress()
        {
            return v13ResidenceAddress.Length;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private long FUNC_VC_v14TelephoneContact()
        {
            return v14TelephoneContact.Length;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private long FUNC_VC_v15CodeMunicipaly()
        {
            return v15MunicipalityResidence.Length;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v17PregnantMother()
        {
            return Helper.USR_ValidatePregnantMother4725(v11IdSex, v17PregnantPerson);
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!((new string[] { "M", "F" }).Contains(v11IdSex))) NonValidMessages.Add($"Debe ingresar un sexo valido. Validar variable 11 'Sexo'");
            if (!(VC_v17PregnantMother == "")) NonValidMessages.Add($"{this.VC_v17PregnantMother}");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetInvalid(() => { return "Codigo de regimen no valido.Validar la variable 2 regimen"; }, $"", this.FileName);
        }
        #endregion
    }
    /// <sumary>
    /// Plantilla de reglas
    /// </sumary> 
    public sealed class RUL_CurrentAntirretroviralTherapy
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Campo Valida Recibe (TAR) Res 4725
        /// </sumary>
        private long v89_Receive;
        /// <sumary>
        /// Campo Fecha de inicio de los medicamentos de la TAR que recibe actualmente Res 4725
        /// </sumary>
        private string v90_DateMedicineTAR;
        /// <sumary>
        /// Campo Valida Actualmente recibe ABACAVIR Res 4725
        /// </sumary>
        private long v91_1_Avacavir;
        /// <sumary>
        /// Campo Valida Actualmente recibe ATAZANAVIR Res 4725
        /// </sumary>
        private long v91_2_Atazanavir;
        /// <sumary>
        /// Campo Valida Actualmente recibe DIDANOSINA Res 4725
        /// </sumary>
        private long v91_3_Didanosina;
        /// <sumary>
        /// Campo Valida Actualmente recibe EFAVIRENZ Res 4725
        /// </sumary>
        private long v91_4_Efavirenz;
        /// <sumary>
        /// Campo Actualmente recibe ESTAVUDINA Res 4725
        /// </sumary>
        private long v91_5_Estavudina;
        /// <sumary>
        /// Campo Actualmente recibe Fosamprenavir Res 4725
        /// </sumary>
        private long v91_6_Fosamprenavir;
        /// <sumary>
        /// Campo Actualmente recibe Indinavir Res 4725
        /// </sumary>
        private long v91_7_Indinavir;
        /// <sumary>
        /// Campo Actualmente recibe Lamivudina Res 4725
        /// </sumary>
        private long v91_8_Lamivudina;
        /// <sumary>
        /// Campo Actualmente recibe Lopinavir Res 4725
        /// </sumary>
        private long v91_9_Lopinavir;
        /// <sumary>
        /// Campo Actualmente recibe Nevirapina Res 4725
        /// </sumary>
        private long v91_10_Nevirapina;
        /// <sumary>
        /// Campo Actualmente recibe Nelfinavir Res 4725
        /// </sumary>
        private long v91_11_Nelfinavir;
        /// <sumary>
        /// Campo Actualmente recibe Ritonavir Res 4725
        /// </sumary>
        private long v91_12_Ritonavir;
        /// <sumary>
        /// Campo Actualmente recibe Saquinavir Res 4725
        /// </sumary>
        private long v91_13_Saquinavir;
        /// <sumary>
        /// Campo Actualmente recibe Zidovudina Res 4725
        /// </sumary>
        private long v91_14_Zidovudina;
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS (medicamento 1)
        /// </sumary>
        private string v91_15_Medicamento1;
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS (medicamento 2)
        /// </sumary>
        private string v91_16_Medicamento2;
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS (medicamento 3)
        /// </sumary>
        private string v91_17_Medicamento3;
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS (medicamento 4)
        /// </sumary>
        private string v91_18_Medicamento4;
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS(medicamento 5)
        /// </sumary>
        private string v91_19_Medicamento5;
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS (medicamento 6)
        /// </sumary>
        private string v91_20_Medicamento6;
        /// <sumary>
        /// Funcion integrada USR_GenerateCUM Variable 91.15
        /// </sumary>
        private string VC_v91_15_Medicamento1;
        /// <sumary>
        /// Funcion integrada USR_GenerateCUM 91.16
        /// </sumary>
        private string VC_v91_16_Medicamento2;
        /// <sumary>
        /// Funcion integrada USR_GenerateCUM 91.17
        /// </sumary>
        private string VC_v91_17_Medicamento3;
        /// <sumary>
        /// Funcion integrada USR_GenerateCUM 91.18
        /// </sumary>
        private string VC_v91_18_Medicamento4;
        /// <sumary>
        /// Funcion integrada USR_GenerateCUM 91.19
        /// </sumary>
        private string VC_v91_19_Medicamento5;
        /// <sumary>
        /// Funcion integrada USR_GenerateCUM 91.20
        /// </sumary>
        private string VC_v91_20_Medicamento6;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_CurrentAntirretroviralTherapy() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        /// <param name="v89_Receive">Campo Valida Recibe (TAR) Res 4725</param>
        /// <param name="v90_DateMedicineTAR">Campo Fecha de inicio de los medicamentos de la TAR que recibe actualmente Res 4725</param>
        /// <param name="v91_1_Avacavir">Campo Valida Actualmente recibe ABACAVIR Res 4725</param>
        /// <param name="v91_2_Atazanavir">Campo Valida Actualmente recibe ATAZANAVIR Res 4725</param>
        /// <param name="v91_3_Didanosina">Campo Valida Actualmente recibe DIDANOSINA Res 4725</param>
        /// <param name="v91_4_Efavirenz">Campo Valida Actualmente recibe EFAVIRENZ Res 4725</param>
        /// <param name="v91_5_Estavudina">Campo Actualmente recibe ESTAVUDINA Res 4725</param>
        /// <param name="v91_6_Fosamprenavir">Campo Actualmente recibe Fosamprenavir Res 4725</param>
        /// <param name="v91_7_Indinavir">Campo Actualmente recibe Indinavir Res 4725</param>
        /// <param name="v91_8_Lamivudina">Campo Actualmente recibe Lamivudina Res 4725</param>
        /// <param name="v91_9_Lopinavir">Campo Actualmente recibe Lopinavir Res 4725</param>
        /// <param name="v91_10_Nevirapina">Campo Actualmente recibe Nevirapina Res 4725</param>
        /// <param name="v91_11_Nelfinavir">Campo Actualmente recibe Nelfinavir Res 4725</param>
        /// <param name="v91_12_Ritonavir">Campo Actualmente recibe Ritonavir Res 4725</param>
        /// <param name="v91_13_Saquinavir">Campo Actualmente recibe Saquinavir Res 4725</param>
        /// <param name="v91_14_Zidovudina">Campo Actualmente recibe Zidovudina Res 4725</param>
        /// <param name="v91_15_Medicamento1">Actualmente en la TAR recibe Medicamento NO POS (medicamento 1)</param>
        /// <param name="v91_16_Medicamento2">Actualmente en la TAR recibe Medicamento NO POS (medicamento 2)</param>
        /// <param name="v91_17_Medicamento3">Actualmente en la TAR recibe Medicamento NO POS (medicamento 3)</param>
        /// <param name="v91_18_Medicamento4">Actualmente en la TAR recibe Medicamento NO POS (medicamento 4)</param>
        /// <param name="v91_19_Medicamento5">Actualmente en la TAR recibe Medicamento NO POS(medicamento 5)</param>
        /// <param name="v91_20_Medicamento6">Actualmente en la TAR recibe Medicamento NO POS (medicamento 6)</param>
        public RuntimeResult<string> Execute(long v89_Receive, string v90_DateMedicineTAR, long v91_1_Avacavir, long v91_2_Atazanavir, long v91_3_Didanosina, long v91_4_Efavirenz, long v91_5_Estavudina, long v91_6_Fosamprenavir, long v91_7_Indinavir, long v91_8_Lamivudina, long v91_9_Lopinavir, long v91_10_Nevirapina, long v91_11_Nelfinavir, long v91_12_Ritonavir, long v91_13_Saquinavir, long v91_14_Zidovudina, string v91_15_Medicamento1, string v91_16_Medicamento2, string v91_17_Medicamento3, string v91_18_Medicamento4, string v91_19_Medicamento5, string v91_20_Medicamento6)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.v89_Receive = v89_Receive;
                this.v90_DateMedicineTAR = v90_DateMedicineTAR;
                this.v91_1_Avacavir = v91_1_Avacavir;
                this.v91_2_Atazanavir = v91_2_Atazanavir;
                this.v91_3_Didanosina = v91_3_Didanosina;
                this.v91_4_Efavirenz = v91_4_Efavirenz;
                this.v91_5_Estavudina = v91_5_Estavudina;
                this.v91_6_Fosamprenavir = v91_6_Fosamprenavir;
                this.v91_7_Indinavir = v91_7_Indinavir;
                this.v91_8_Lamivudina = v91_8_Lamivudina;
                this.v91_9_Lopinavir = v91_9_Lopinavir;
                this.v91_10_Nevirapina = v91_10_Nevirapina;
                this.v91_11_Nelfinavir = v91_11_Nelfinavir;
                this.v91_12_Ritonavir = v91_12_Ritonavir;
                this.v91_13_Saquinavir = v91_13_Saquinavir;
                this.v91_14_Zidovudina = v91_14_Zidovudina;
                this.v91_15_Medicamento1 = v91_15_Medicamento1;
                this.v91_16_Medicamento2 = v91_16_Medicamento2;
                this.v91_17_Medicamento3 = v91_17_Medicamento3;
                this.v91_18_Medicamento4 = v91_18_Medicamento4;
                this.v91_19_Medicamento5 = v91_19_Medicamento5;
                this.v91_20_Medicamento6 = v91_20_Medicamento6;
                this.VC_v91_15_Medicamento1 = FUNC_VC_v91_15_Medicamento1();
                this.VC_v91_16_Medicamento2 = FUNC_VC_v91_16_Medicamento2();
                this.VC_v91_17_Medicamento3 = FUNC_VC_v91_17_Medicamento3();
                this.VC_v91_18_Medicamento4 = FUNC_VC_v91_18_Medicamento4();
                this.VC_v91_19_Medicamento5 = FUNC_VC_v91_19_Medicamento5();
                this.VC_v91_20_Medicamento6 = FUNC_VC_v91_20_Medicamento6();
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<string>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v91_15_Medicamento1()
        {
            return Helper.USR_GenerateCUM(v91_15_Medicamento1, "91.15", "Medicamento 1");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v91_16_Medicamento2()
        {
            return Helper.USR_GenerateCUM(v91_16_Medicamento2, "91.16", "Medicamento 2");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v91_17_Medicamento3()
        {
            return Helper.USR_GenerateCUM(v91_17_Medicamento3, "91.17", "Medicamento 3");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v91_18_Medicamento4()
        {
            return Helper.USR_GenerateCUM(v91_18_Medicamento4, "91.18", "Medicamento 4");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v91_19_Medicamento5()
        {
            return Helper.USR_GenerateCUM(v91_19_Medicamento5, "91.19", "Medicamento 5");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v91_20_Medicamento6()
        {
            return Helper.USR_GenerateCUM(v91_20_Medicamento6, "91.20", "Medicamento 6");
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_v91_15_Medicamento1 == "OK")) NonValidMessages.Add($"{this.VC_v91_15_Medicamento1}");
            if (!(VC_v91_16_Medicamento2 == "OK")) NonValidMessages.Add($"{this.VC_v91_16_Medicamento2}");
            if (!(VC_v91_17_Medicamento3 == "OK")) NonValidMessages.Add($"{this.VC_v91_17_Medicamento3}");
            if (!(VC_v91_18_Medicamento4 == "OK")) NonValidMessages.Add($"{this.VC_v91_18_Medicamento4}");
            if (!(VC_v91_19_Medicamento5 == "OK")) NonValidMessages.Add($"{this.VC_v91_19_Medicamento5}");
            if (!(VC_v91_20_Medicamento6 == "OK")) NonValidMessages.Add($"{this.VC_v91_20_Medicamento6}");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetInvalid(() => { return ""; }, $"", this.FileName);
        }
        #endregion
    }
    /// <sumary>
    /// TERAPIA ANTIRRETROVIRAL DE INICIO (TAR)
    /// </sumary> 
    public sealed class RUL_Antiretroviral_therapy_begin
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Fecha de Inicio de TAR
        /// </sumary>
        private string v30_DateTAR;
        /// <sumary>
        /// Conteo de linfocitos T CD4  al momento de inicio de la TAR
        /// </sumary>
        private long v31_LymphocytesCount;
        /// <sumary>
        /// Conteo de linfocitos totales al momento de inicio de la TAR
        /// </sumary>
        private long v32_lymphocytesTotals;
        /// <sumary>
        /// Carga Viral al inicio de TAR
        /// </sumary>
        private long v33_LoadTAR;
        /// <sumary>
        /// motivo de Inicio TAR
        /// </sumary>
        private long v34_BeginTAR;
        /// <sumary>
        /// Tenia anemia al iniciar el TAR
        /// </sumary>
        private long v35_AnemiaTAR;
        /// <sumary>
        /// Tenia enfermedad renal cronica al iniciar TAR
        /// </sumary>
        private long v36_RenalTAR;
        /// <sumary>
        /// Tenia coinfeccion con VHB al iniciar el TAR
        /// </sumary>
        private long v37_VHB_TAR;
        /// <sumary>
        /// Tenia Coinfección con el VHC al iniciar TAR
        /// </sumary>
        private long v38_VHC_TAR;
        /// <sumary>
        /// Tenia Tuberculosis al iniciar TAR
        /// </sumary>
        private long v39_Tuberculosis_TAR;
        /// <sumary>
        /// Tenía Cirugía cardiovascular o infarto previo al inicio de la TAR
        /// </sumary>
        private long v40_Attack_surgery;
        /// <sumary>
        /// Tenia Sarcoma de Kaposi al iniciar TAR
        /// </sumary>
        private long v41_Caposi_sarcoma;
        /// <sumary>
        /// Estaba embarazada al iniciar TAR
        /// </sumary>
        private long v42_PregnancyTAR;
        /// <sumary>
        /// Tenía Enfermedad psiquiátrica al iniciar TAR
        /// </sumary>
        private long v43_Psychiatry;
        /// <sumary>
        /// Al inicio de la TAR recibió ABACAVIR
        /// </sumary>
        private long v44_1_ABACAVIR;
        /// <sumary>
        /// Al inicio de la TAR recibio ATANAZAVIR
        /// </sumary>
        private long v44_2_ATAZANAVIR;
        /// <sumary>
        /// Al inicio de la TAR recibió DIDANOSINA
        /// </sumary>
        private long v44_3_DIDANOSINA;
        /// <sumary>
        /// Al inicio de la TAR recibió EFAVIRENZ
        /// </sumary>
        private long v44_4_EFAVIRENZ;
        /// <sumary>
        /// Al inicio de la TAR recibió ESTAVUDINA
        /// </sumary>
        private long v44_5_ESTAVUDINA;
        /// <sumary>
        /// Al inicio de la TAR recibió FOSAMPRENAVIR
        /// </sumary>
        private long v44_6_FOSAMPRENAVIR;
        /// <sumary>
        /// Al inicio de la TAR recibió INDINAVIR
        /// </sumary>
        private long v44_7_INDINAVIR;
        /// <sumary>
        /// Al inicio de la TAR recibió LAMIVUDINA
        /// </sumary>
        private long v44_8_LAMIVUDINA;
        /// <sumary>
        /// Al inicio de la TAR recibió LOPINAVIR
        /// </sumary>
        private long v44_9_LOPINAVIR;
        /// <sumary>
        /// Al inicio de la TAR recibió NEVIRAPINA
        /// </sumary>
        private long v44_10_NEVIRAPINA;
        /// <sumary>
        /// Al inicio de la TAR recibió NELFINAVIR
        /// </sumary>
        private long v44_11_NELFINAVIR;
        /// <sumary>
        /// Al inicio de la TAR recibió RITONAVIR
        /// </sumary>
        private long v44_12_RITONAVIR;
        /// <sumary>
        /// Al inicio de la TAR recibió SQUINAVIR
        /// </sumary>
        private long v44_13_SQUINAVIR;
        /// <sumary>
        /// Al inicio de la TAR recibió ZIDOVUDINA
        /// </sumary>
        private long v44_14_ZIDOVUDINA;
        /// <sumary>
        /// En la TAR inicial recibió Medicamento NO POS (medicamento 1) 
        /// </sumary>
        private string v44_15_Medicine1;
        /// <sumary>
        /// En la TAR inicial recibió Medicamento NO POS (medicamento 2) 
        /// </sumary>
        private string v44_16_Medicine2;
        /// <sumary>
        /// En la TAR inicial recibió Medicamento NO POS (medicamento 3) 
        /// </sumary>
        private string v44_17_Medicine3;
        /// <sumary>
        /// En la TAR inicial recibió Medicamento NO POS (medicamento 4) 
        /// </sumary>
        private string v44_18_Medicine4;
        /// <sumary>
        /// En la TAR inicial recibió Medicamento NO POS (medicamento 5) 
        /// </sumary>
        private string v44_19_Medicine5;
        /// <sumary>
        /// En la TAR inicial recibió Medicamento NO POS (medicamento 6) 
        /// </sumary>
        private string v44_20_Medicine6;
        /// <sumary>
        /// Recibió asesoría antes de iniciar TAR 
        /// </sumary>
        private string v45_AdvisoryTAR;
        /// <sumary>
        /// VAR_46 Meses dispensados
        /// </sumary>
        private long v46_MonthsDispensed;
        /// <sumary>
        /// Cantidad de Citas Medicas
        /// </sumary>
        private long v47_Medical_Appointment;
        /// <sumary>
        /// Alguno de los medicamentos con los que inició TAR ha sido cambiado, por cualquier motivo 
        /// </sumary>
        private long v48_ChangeMedicine;
        /// <sumary>
        /// Fecha del primer cambio de cualquier medicamento del esquema inicial de TAR 
        /// </sumary>
        private string v49_MedicationChange;
        /// <sumary>
        /// Causa del cambio de medicamento
        /// </sumary>
        private long v50_CauseMedication_Change;
        /// <sumary>
        /// Numero de Fallas TAR
        /// </sumary>
        private long v51_NumberFails;
        /// <sumary>
        /// Numero de veces de cambio hasta el reporte actual
        /// </sumary>
        private long v52_NumberChange;
        /// <sumary>
        /// Variable de fecha de inicio de TAR
        /// </sumary>
        private string VC_30_DATE_TAR;
        /// <sumary>
        /// 44.15
        /// </sumary>
        private string VC_v44_15_Medicine1;
        /// <sumary>
        /// 44.16
        /// </sumary>
        private string VC_v44_16_Medicine2;
        /// <sumary>
        /// 44.17
        /// </sumary>
        private string VC_v44_17_Medicine3;
        /// <sumary>
        /// 44.18
        /// </sumary>
        private string VC_v44_18_Medicine4;
        /// <sumary>
        /// 44.19
        /// </sumary>
        private string VC_v44_19_Medicine5;
        /// <sumary>
        /// 44.20
        /// </sumary>
        private string VC_v44_20_Medicine6;
        /// <sumary>
        /// Variable meses dispensados
        /// </sumary>
        private string VC_v46_MonthDispensed;
        /// <sumary>
        /// Variable de cantidad de citas medicas
        /// </sumary>
        private string VC_v47_Medical_Appointment;
        /// <sumary>
        /// Cambio de Medicamento
        /// </sumary>
        private string VC_v49_MedicationChange;
        /// <sumary>
        /// Variable numero de Fallas TAR
        /// </sumary>
        private string VC_v51_NumberFails;
        /// <sumary>
        /// Valida las veces de cambio hasta el reporte actual
        /// </sumary>
        private string VC_v52_NumberChange;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_Antiretroviral_therapy_begin() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// TERAPIA ANTIRRETROVIRAL DE INICIO (TAR)
        /// </sumary>
        /// <param name="v30_DateTAR">Fecha de Inicio de TAR</param>
        /// <param name="v31_LymphocytesCount">Conteo de linfocitos T CD4  al momento de inicio de la TAR</param>
        /// <param name="v32_lymphocytesTotals">Conteo de linfocitos totales al momento de inicio de la TAR</param>
        /// <param name="v33_LoadTAR">Carga Viral al inicio de TAR</param>
        /// <param name="v34_BeginTAR">motivo de Inicio TAR</param>
        /// <param name="v35_AnemiaTAR">Tenia anemia al iniciar el TAR</param>
        /// <param name="v36_RenalTAR">Tenia enfermedad renal cronica al iniciar TAR</param>
        /// <param name="v37_VHB_TAR">Tenia coinfeccion con VHB al iniciar el TAR</param>
        /// <param name="v38_VHC_TAR">Tenia Coinfección con el VHC al iniciar TAR</param>
        /// <param name="v39_Tuberculosis_TAR">Tenia Tuberculosis al iniciar TAR</param>
        /// <param name="v40_Attack_surgery">Tenía Cirugía cardiovascular o infarto previo al inicio de la TAR</param>
        /// <param name="v41_Caposi_sarcoma">Tenia Sarcoma de Kaposi al iniciar TAR</param>
        /// <param name="v42_PregnancyTAR">Estaba embarazada al iniciar TAR</param>
        /// <param name="v43_Psychiatry">Tenía Enfermedad psiquiátrica al iniciar TAR</param>
        /// <param name="v44_1_ABACAVIR">Al inicio de la TAR recibió ABACAVIR</param>
        /// <param name="v44_2_ATAZANAVIR">Al inicio de la TAR recibio ATANAZAVIR</param>
        /// <param name="v44_3_DIDANOSINA">Al inicio de la TAR recibió DIDANOSINA</param>
        /// <param name="v44_4_EFAVIRENZ">Al inicio de la TAR recibió EFAVIRENZ</param>
        /// <param name="v44_5_ESTAVUDINA">Al inicio de la TAR recibió ESTAVUDINA</param>
        /// <param name="v44_6_FOSAMPRENAVIR">Al inicio de la TAR recibió FOSAMPRENAVIR</param>
        /// <param name="v44_7_INDINAVIR">Al inicio de la TAR recibió INDINAVIR</param>
        /// <param name="v44_8_LAMIVUDINA">Al inicio de la TAR recibió LAMIVUDINA</param>
        /// <param name="v44_9_LOPINAVIR">Al inicio de la TAR recibió LOPINAVIR</param>
        /// <param name="v44_10_NEVIRAPINA">Al inicio de la TAR recibió NEVIRAPINA</param>
        /// <param name="v44_11_NELFINAVIR">Al inicio de la TAR recibió NELFINAVIR</param>
        /// <param name="v44_12_RITONAVIR">Al inicio de la TAR recibió RITONAVIR</param>
        /// <param name="v44_13_SQUINAVIR">Al inicio de la TAR recibió SQUINAVIR</param>
        /// <param name="v44_14_ZIDOVUDINA">Al inicio de la TAR recibió ZIDOVUDINA</param>
        /// <param name="v44_15_Medicine1">En la TAR inicial recibió Medicamento NO POS (medicamento 1) </param>
        /// <param name="v44_16_Medicine2">En la TAR inicial recibió Medicamento NO POS (medicamento 2) </param>
        /// <param name="v44_17_Medicine3">En la TAR inicial recibió Medicamento NO POS (medicamento 3) </param>
        /// <param name="v44_18_Medicine4">En la TAR inicial recibió Medicamento NO POS (medicamento 4) </param>
        /// <param name="v44_19_Medicine5">En la TAR inicial recibió Medicamento NO POS (medicamento 5) </param>
        /// <param name="v44_20_Medicine6">En la TAR inicial recibió Medicamento NO POS (medicamento 6) </param>
        /// <param name="v45_AdvisoryTAR">Recibió asesoría antes de iniciar TAR </param>
        /// <param name="v46_MonthsDispensed">VAR_46 Meses dispensados</param>
        /// <param name="v47_Medical_Appointment">Cantidad de Citas Medicas</param>
        /// <param name="v48_ChangeMedicine">Alguno de los medicamentos con los que inició TAR ha sido cambiado, por cualquier motivo </param>
        /// <param name="v49_MedicationChange">Fecha del primer cambio de cualquier medicamento del esquema inicial de TAR </param>
        /// <param name="v50_CauseMedication_Change">Causa del cambio de medicamento</param>
        /// <param name="v51_NumberFails">Numero de Fallas TAR</param>
        /// <param name="v52_NumberChange">Numero de veces de cambio hasta el reporte actual</param>
        public RuntimeResult<string> Execute(string v30_DateTAR, long v31_LymphocytesCount, long v32_lymphocytesTotals, long v33_LoadTAR, long v34_BeginTAR, long v35_AnemiaTAR, long v36_RenalTAR, long v37_VHB_TAR, long v38_VHC_TAR, long v39_Tuberculosis_TAR, long v40_Attack_surgery, long v41_Caposi_sarcoma, long v42_PregnancyTAR, long v43_Psychiatry, long v44_1_ABACAVIR, long v44_2_ATAZANAVIR, long v44_3_DIDANOSINA, long v44_4_EFAVIRENZ, long v44_5_ESTAVUDINA, long v44_6_FOSAMPRENAVIR, long v44_7_INDINAVIR, long v44_8_LAMIVUDINA, long v44_9_LOPINAVIR, long v44_10_NEVIRAPINA, long v44_11_NELFINAVIR, long v44_12_RITONAVIR, long v44_13_SQUINAVIR, long v44_14_ZIDOVUDINA, string v44_15_Medicine1, string v44_16_Medicine2, string v44_17_Medicine3, string v44_18_Medicine4, string v44_19_Medicine5, string v44_20_Medicine6, string v45_AdvisoryTAR, long v46_MonthsDispensed, long v47_Medical_Appointment, long v48_ChangeMedicine, string v49_MedicationChange, long v50_CauseMedication_Change, long v51_NumberFails, long v52_NumberChange)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.v30_DateTAR = v30_DateTAR;
                this.v31_LymphocytesCount = v31_LymphocytesCount;
                this.v32_lymphocytesTotals = v32_lymphocytesTotals;
                this.v33_LoadTAR = v33_LoadTAR;
                this.v34_BeginTAR = v34_BeginTAR;
                this.v35_AnemiaTAR = v35_AnemiaTAR;
                this.v36_RenalTAR = v36_RenalTAR;
                this.v37_VHB_TAR = v37_VHB_TAR;
                this.v38_VHC_TAR = v38_VHC_TAR;
                this.v39_Tuberculosis_TAR = v39_Tuberculosis_TAR;
                this.v40_Attack_surgery = v40_Attack_surgery;
                this.v41_Caposi_sarcoma = v41_Caposi_sarcoma;
                this.v42_PregnancyTAR = v42_PregnancyTAR;
                this.v43_Psychiatry = v43_Psychiatry;
                this.v44_1_ABACAVIR = v44_1_ABACAVIR;
                this.v44_2_ATAZANAVIR = v44_2_ATAZANAVIR;
                this.v44_3_DIDANOSINA = v44_3_DIDANOSINA;
                this.v44_4_EFAVIRENZ = v44_4_EFAVIRENZ;
                this.v44_5_ESTAVUDINA = v44_5_ESTAVUDINA;
                this.v44_6_FOSAMPRENAVIR = v44_6_FOSAMPRENAVIR;
                this.v44_7_INDINAVIR = v44_7_INDINAVIR;
                this.v44_8_LAMIVUDINA = v44_8_LAMIVUDINA;
                this.v44_9_LOPINAVIR = v44_9_LOPINAVIR;
                this.v44_10_NEVIRAPINA = v44_10_NEVIRAPINA;
                this.v44_11_NELFINAVIR = v44_11_NELFINAVIR;
                this.v44_12_RITONAVIR = v44_12_RITONAVIR;
                this.v44_13_SQUINAVIR = v44_13_SQUINAVIR;
                this.v44_14_ZIDOVUDINA = v44_14_ZIDOVUDINA;
                this.v44_15_Medicine1 = v44_15_Medicine1;
                this.v44_16_Medicine2 = v44_16_Medicine2;
                this.v44_17_Medicine3 = v44_17_Medicine3;
                this.v44_18_Medicine4 = v44_18_Medicine4;
                this.v44_19_Medicine5 = v44_19_Medicine5;
                this.v44_20_Medicine6 = v44_20_Medicine6;
                this.v45_AdvisoryTAR = v45_AdvisoryTAR;
                this.v46_MonthsDispensed = v46_MonthsDispensed;
                this.v47_Medical_Appointment = v47_Medical_Appointment;
                this.v48_ChangeMedicine = v48_ChangeMedicine;
                this.v49_MedicationChange = v49_MedicationChange;
                this.v50_CauseMedication_Change = v50_CauseMedication_Change;
                this.v51_NumberFails = v51_NumberFails;
                this.v52_NumberChange = v52_NumberChange;
                this.VC_30_DATE_TAR = FUNC_VC_30_DATE_TAR();
                this.VC_v44_15_Medicine1 = FUNC_VC_v44_15_Medicine1();
                this.VC_v44_16_Medicine2 = FUNC_VC_v44_16_Medicine2();
                this.VC_v44_17_Medicine3 = FUNC_VC_v44_17_Medicine3();
                this.VC_v44_18_Medicine4 = FUNC_VC_v44_18_Medicine4();
                this.VC_v44_19_Medicine5 = FUNC_VC_v44_19_Medicine5();
                this.VC_v44_20_Medicine6 = FUNC_VC_v44_20_Medicine6();
                this.VC_v46_MonthDispensed = FUNC_VC_v46_MonthDispensed();
                this.VC_v47_Medical_Appointment = FUNC_VC_v47_Medical_Appointment();
                this.VC_v49_MedicationChange = FUNC_VC_v49_MedicationChange();
                this.VC_v51_NumberFails = FUNC_VC_v51_NumberFails();
                this.VC_v52_NumberChange = FUNC_VC_v52_NumberChange();
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<string>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_30_DATE_TAR()
        {
            return Helper.USR_ValidateTAR(v30_DateTAR);
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v44_15_Medicine1()
        {
            return Helper.USR_GenerateCUM(v44_15_Medicine1, "44.15", "Medicamento 1");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v44_16_Medicine2()
        {
            return Helper.USR_GenerateCUM(v44_16_Medicine2, "44.16", "Medicamento2");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v44_17_Medicine3()
        {
            return Helper.USR_GenerateCUM(v44_17_Medicine3, "44.17", "Documento 3");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v44_18_Medicine4()
        {
            return Helper.USR_GenerateCUM(v44_18_Medicine4, "44.18", "Medicamento 4");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v44_19_Medicine5()
        {
            return Helper.USR_GenerateCUM(v44_19_Medicine5, "44.19", "Documento 5");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v44_20_Medicine6()
        {
            return Helper.USR_GenerateCUM(v44_20_Medicine6, "44.20", "Medicamento 6");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v46_MonthDispensed()
        {
            return Helper.USR_ValidateMonthDispensed(v46_MonthsDispensed);
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v47_Medical_Appointment()
        {
            return Helper.USR_Medical_Appoinment(v47_Medical_Appointment);
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v49_MedicationChange()
        {
            return Helper.USR_ValidateDateChangeMedicament(v49_MedicationChange);
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v51_NumberFails()
        {
            return Helper.USR_ValidateNumberFail(v51_NumberFails);
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v52_NumberChange()
        {
            return Helper.USR_ValidateNumberChange(v52_NumberChange);
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_30_DATE_TAR == "OK")) NonValidMessages.Add($"{this.VC_30_DATE_TAR}");
            if (!(v31_LymphocytesCount >= 0 && v31_LymphocytesCount <= 99055)) NonValidMessages.Add($"Error en el dato, verifique que esté en el rango de valores. Validar variable 31 Conteo de linfocitos T CD4+ al momento de inicio de la TAR");
            if (!(v32_lymphocytesTotals <= 1 && v32_lymphocytesTotals <= 99005)) NonValidMessages.Add($"Debe introducir un valor que este dentro del rango establecido.Validar variable 32 Conteo de linfocitos totales al momento de inicio de la TAR");
            if (!((new long[] { 0, 1, 2, 3, 4, 5, 6, 9 }).Contains(v33_LoadTAR))) NonValidMessages.Add($"Debe registrar una opcion valida.Validar la variable 33 Carga Viral al inicio de TAR");
            if (!(v34_BeginTAR <= 10)) NonValidMessages.Add($"Debe registrar una opcion valida.Validar variable 34 Motivo de inicio de la TAR");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v35_AnemiaTAR))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si la persona tenía anemia al iniciar la TAR.Validar variable 35 Tenía Anemia al iniciar TAR");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v36_RenalTAR))) NonValidMessages.Add($"Debe registrar una opcion valida con respecto si la persona tenia enfermedad renal cronica al iniciar la TAR.Validar variable 36 Tenía Enfermedad renal crónica al iniciar TAR");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v37_VHB_TAR))) NonValidMessages.Add($"Debe ingresar una opcion valida con respecto si la persona tenia coinfeccion con VHB al iniciar la TAR.Validar la variable 37 Tenía Coinfección con el VHB al iniciar TAR");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v38_VHC_TAR))) NonValidMessages.Add($"Debe registrar una opcion correcta de acuerdo si la persona tenia coinfeccion con el VHC al iniciar la TAR.Validar variable 38 Tenía Coinfección con el VHC al iniciar TAR");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v39_Tuberculosis_TAR))) NonValidMessages.Add($"Debe ingresar una opcion valida con respecto si tenia tuberculosis al iniciar la TAR, Validar variable 39 Tenía Tuberculosis al iniciar TAR ");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v40_Attack_surgery))) NonValidMessages.Add($"Debe de ingresar un dato valido con respecto a que si tenia cirugia cardiovascular al inicio de la TAR. Validar variable 40 Tenía Cirugía cardiovascular o infarto previo al inicio de la TAR");
            if (!((new long[] { 0, 1, 2, 3, 4, 9 }).Contains(v41_Caposi_sarcoma))) NonValidMessages.Add($"Debe introducir un valor valido con respecto si al inicio de la TAR tenia Sarcoma de kaposi. Validar la variable 41 Tenía Sarcoma de Kaposi al iniciar TAR");
            if (!((new long[] { 0, 1, 2, 3, 4, 5, 9 }).Contains(v42_PregnancyTAR))) NonValidMessages.Add($"Debe ingresar un valor valido con respecto si estaba embarazada al iniciar la TAR.Validar la varaible 42 Estaba embarazada al iniciar TAR");
            if (!((new long[] { 0, 1, 2, 3, 4, 5, 9 }).Contains(v43_Psychiatry))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR tenia enfermeda psiquiatrica.Validar variable 43");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_1_ABACAVIR))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio ABACAVIR.Validar la variable 44.1 Al inicio de la TAR recibió ABACAVIR");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_2_ATAZANAVIR))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio ATAZANAVIR.Validar la variable 44.2 ");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_3_DIDANOSINA))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio DIDANOSINA.Validar variable 44.3 Al inicio de la TAR recibió DIDANOSINA");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_4_EFAVIRENZ))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio EFAVIRENZ.Validar variable 44.4 Al inicio de la TAR recibió EFAVIRENZ");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_5_ESTAVUDINA))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio ESTAVUDINA.Validar variable 44.5 Al inicio de la TAR recibió ESTAVUDINA");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_6_FOSAMPRENAVIR))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio FOSAMPRENAVIR.Validar variable 44.6 Al inicio de la TAR recibió FOSAMPRENAVIR");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_7_INDINAVIR))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio INDINAVIR.Validar la variable 44.7 Al inicio de la TAR recibió INDINAVIR");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_8_LAMIVUDINA))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio LAMIVUDINA.Validar la variable 44.8 Al inicio de la TAR recibió LAMIVUDINA");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_9_LOPINAVIR))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio LOPINAVIR.Variable la variable 44.9 Al inicio de la TAR recibió LOPINAVIR");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_10_NEVIRAPINA))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio NEVIRAPINA.Validar la variable 44.10 Al inicio de la TAR recibió NEVIRAPINA");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_11_NELFINAVIR))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio NELFINAVIR.Validar la variable 44.11 Al inicio de la TAR recibió NELFINAVIR");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_12_RITONAVIR))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio RITONAVIR. Validar la variable 44.12 Al inicio de la TAR recibió RITONAVIR");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_13_SQUINAVIR))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio SQUINAVIR. Validar la variable 44.13 Al inicio de la TAR recibió SQUINAVIR");
            if (!((new long[] { 0, 1, 9 }).Contains(v44_14_ZIDOVUDINA))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto si al iniciar la TAR recibio ZIDOVUDINA.Validar la variable 44.14 Al inicio de la TAR recibió ZIDOVUDINA");
            if (!(VC_v44_15_Medicine1 == "OK")) NonValidMessages.Add($"{this.VC_v44_15_Medicine1}");
            if (!(VC_v44_16_Medicine2 == "OK")) NonValidMessages.Add($"{this.VC_v44_16_Medicine2}");
            if (!(VC_v44_17_Medicine3 == "OK")) NonValidMessages.Add($"{this.VC_v44_17_Medicine3}");
            if (!(VC_v44_18_Medicine4 == "OK")) NonValidMessages.Add($"{this.VC_v44_18_Medicine4}");
            if (!(VC_v44_19_Medicine5 == "OK")) NonValidMessages.Add($"{this.VC_v44_19_Medicine5}");
            if (!(VC_v44_20_Medicine6 == "OK")) NonValidMessages.Add($"{this.VC_v44_20_Medicine6}");
            if (!((new string[] { "0", "1", "2", "3", "4", "9" }).Contains(v45_AdvisoryTAR))) NonValidMessages.Add($"Debe ingresar una opcion valida teniendo en cuenta si la persona recibio asesoria antes de iniciar el TAR.Validar la variable 45 Recibió asesoría antes de iniciar TAR");
            if (!(VC_v47_Medical_Appointment == "OK")) NonValidMessages.Add($"{this.VC_v47_Medical_Appointment}");
            if (!(VC_v49_MedicationChange == "OK")) NonValidMessages.Add($"{this.VC_v49_MedicationChange}");
            if (!((new long[] { 0, 1, 2, 3, 4, 5, 6, 9 }).Contains(v50_CauseMedication_Change))) NonValidMessages.Add($"Debe ingresar un dato valido con respecto a la causa del cambio de medicamento con el que inicio la TAR. Validar la variable 50 Causa de cambio de medicamento con el que inicio la TAR");
            if (!(VC_v51_NumberFails == "OK")) NonValidMessages.Add($"{this.VC_v51_NumberFails}");
            if (!(VC_v52_NumberChange == "OK")) NonValidMessages.Add($"{this.VC_v52_NumberChange }");
            if (!(VC_v46_MonthDispensed == "OK")) NonValidMessages.Add($"{this.VC_v46_MonthDispensed}");
            if (!((new long[] { 1, 2, 3, 4, 9 }).Contains(v48_ChangeMedicine))) NonValidMessages.Add($"Debe registrar una de las opciones si el medicamento con el que inicio el TAR fue cambiado.Validar la variable 48 Alguno de los medicamentos con los que inició TAR ha sido cambiado, por cualquier motivo");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return "OK"; }, $"");
        }
        #endregion
    }
    /// <sumary>
    /// Plantilla de reglas
    /// </sumary> 
    public sealed class RUL_AdministrativePreviousContribution
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Campo Valida Novedad del paciente respecto al reporte anterior Res 4725
        /// </sumary>
        private long v103_Noveltypatient;
        /// <sumary>
        /// Fecha de desafiliación de la EPS
        /// </sumary>
        private string v104_DisenrollmentDateEPS;
        /// <sumary>
        /// EPS o Entidad Territorial al cual se trasladó el paciente con VIH des afiliado
        /// </sumary>
        private string v105_EPS;
        /// <sumary>
        /// dia de muerte
        /// </sumary>
        private string v106_DateDeath;
        /// <sumary>
        /// Causa de Muerte
        /// </sumary>
        private long v107_DeathCause;
        /// <sumary>
        /// v104_DisenrollmentDateEPS
        /// </sumary>
        private string VC_v104_DisenrollmentDateEPS;
        /// <sumary>
        /// v106_DateDeath
        /// </sumary>
        private string VC_v106_DateDeath;
        /// <sumary>
        /// v105_EPS
        /// </sumary>
        private string VC_v105_EPS;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_AdministrativePreviousContribution() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        /// <param name="v103_Noveltypatient">Campo Valida Novedad del paciente respecto al reporte anterior Res 4725</param>
        /// <param name="v104_DisenrollmentDateEPS">Fecha de desafiliación de la EPS</param>
        /// <param name="v105_EPS">EPS o Entidad Territorial al cual se trasladó el paciente con VIH des afiliado</param>
        /// <param name="v106_DateDeath">dia de muerte</param>
        /// <param name="v107_DeathCause">Causa de Muerte</param>
        public RuntimeResult<string> Execute(long v103_Noveltypatient, string v104_DisenrollmentDateEPS, string v105_EPS, string v106_DateDeath, long v107_DeathCause)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.v103_Noveltypatient = v103_Noveltypatient;
                this.v104_DisenrollmentDateEPS = v104_DisenrollmentDateEPS;
                this.v105_EPS = v105_EPS;
                this.v106_DateDeath = v106_DateDeath;
                this.v107_DeathCause = v107_DeathCause;
                this.VC_v104_DisenrollmentDateEPS = FUNC_VC_v104_DisenrollmentDateEPS();
                this.VC_v106_DateDeath = FUNC_VC_v106_DateDeath();
                this.VC_v105_EPS = FUNC_VC_v105_EPS();
                #endregion

                // Validación de valores
                ValidateValues();

                return EvaluateCombinations();
            }
            catch (Exception ex)
            {
                return RuntimeResult<string>.SetError(ex.Message);
            }
        }
        #endregion

        #region Calculated variables Method
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v104_DisenrollmentDateEPS()
        {
            return Helper.USR_ValidateDate(v104_DisenrollmentDateEPS, "Debe introducir una fecha correcta,debe verificar que la fecha sea registrada de la siguiente manera: Año-Mes-Dia.Validar la variable 104 Fecha de desafiliacion de la EPS", "Debe introducir un valor correcto con respecto a la fecha de la desafiliacion de la EPS.Validar la variable 104 Fecha de la desafiliacion de la EPS");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v106_DateDeath()
        {
            return Helper.USR_ValidateDate(v106_DateDeath, "Debe introducir una fecha correctadebe verificar que la fecha sea registrada de la siguiente manera: Año-Mes-Dia.Validar la variable 106 Fecha de muerte", "Debe introducir un valor correcto validando las opciones correctas sobre la fecha de muerte.Validar la variable 106 Fecha de muerte");
        }
        /// <sumary>
        ///	
        /// </sumary>
        private string FUNC_VC_v105_EPS()
        {
            return Helper.USR_ValidateMunicipality_v105(v105_EPS);
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_v104_DisenrollmentDateEPS == "OK")) NonValidMessages.Add($"{this.VC_v104_DisenrollmentDateEPS }");
            if (!(VC_v106_DateDeath == "OK")) NonValidMessages.Add($"{this.VC_v106_DateDeath}");
            if (!((new long[] { 0, 1, 2, 3 }).Contains(v107_DeathCause))) NonValidMessages.Add($"#A005_AdministrativePreviousContribution4725");
            if (!(VC_v105_EPS == "OK")) NonValidMessages.Add($"{this.VC_v105_EPS}");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return ""; }, $"");
        }
        #endregion
    }

    /// <sumary>
    /// Clase para definición de funciones
    /// </sumary>
    public static class Helper
    {
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
                string url = "http://davincilb.ophelia.co:8080/api/api/Upload/GetDocument";
                url = url + "?company=" + company + "&libraryId=" + libraryId + "&fileId=" + fileId;
                result = SYS_WSGET(url, null);
                return result;
            }
            catch (Exception ex)
            {
                return new ENT_ActionResult() { IsError = true, ErrorMessage = ex.Message + ex.StackTrace };
            }
        }
       
        public static int USR_ValidateDocumentNumber(int adapterId, List<ENT_StructureRes4725> _entity, List<string> listErrors, int index, List<dynamic> listDocumentTypesDB)
        {
            index = 1;
            //Funcion valida si el usuario existe
            List<dynamic> personList = new List<dynamic>();
            var listErrorsDocumentNumber = new List<string>();
            Dictionary<string, ENT_StructureRes4725> _dictionary4725 = new Dictionary<string, ENT_StructureRes4725>();
            Dictionary<string, string> _dictionaryDocumentType = new Dictionary<string, string>();
            _dictionaryDocumentType.Add("TI", "3");
            _dictionaryDocumentType.Add("CC", "1");
            _dictionaryDocumentType.Add("CE", "6");
            _dictionaryDocumentType.Add("PA", "7");
            _dictionaryDocumentType.Add("RC", "2");
            _dictionaryDocumentType.Add("SC", "4");
            _dictionaryDocumentType.Add("CD", "9");
            _dictionaryDocumentType.Add("RNV", "1423");
            _dictionaryDocumentType.Add("MS", "1424");
            _dictionaryDocumentType.Add("AS", "2825");
            _dictionaryDocumentType.Add("UN", "3271");


            foreach (ENT_StructureRes4725 file in _entity)
            {
                if (!_dictionary4725.ContainsKey($"{file.TipoIdentificacion}_{file.NumeroDocumento}"))
                    _dictionary4725.Add($"{file.TipoIdentificacion}_{file.NumeroDocumento}", file);

                if (_dictionaryDocumentType.ContainsKey(file.TipoIdentificacion))
                {
                    if (!(file.Regimen.Equals("2") && (file.TipoIdentificacion.Equals("MS") || file.TipoIdentificacion.Equals("AS"))))
                    {
                        var documentTypeDictionary = _dictionaryDocumentType[file.TipoIdentificacion];

                        var documentTypeBD = listDocumentTypesDB.Where(d => d.Id == documentTypeDictionary).FirstOrDefault();
                        // Agrega personas para consultarlas posteriormente
                        personList.Add(new { DocumentType = documentTypeBD.Id.ToString(), Identification = file.NumeroDocumento, Index = index, typeId = file.TipoIdentificacion });
                    }
                }
                index++;

            }

            USR_ValidateAffiliatePersonResolutions(personList, adapterId, listErrorsDocumentNumber);
            var newlist = listErrorsDocumentNumber.Select(x => x.Replace("item", "Linea")).ToList();

            listErrors.AddRange(newlist);
            return index;
        }

        public static dynamic USR_FileToEntitiesWithValidation(string textFile, string lineSeparator, string columnSeparator, object type, List<string> ErrorList)
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
                foreach (string line in lines)
                {
                    var file = 0;
                    var instance = Activator.CreateInstance(typeEntity);
                    var properties = typeEntity.GetProperties().OrderBy(p => ((OrderAttribute)p.GetCustomAttributes(typeof(OrderAttribute), false)[0]).Order);
                    string[] columns = line.Split(columnSeparator[0]);
                    if (columns.Length == properties.Count())
                    {
                        int index = 0;
                        foreach (var property in properties)
                        {
                            property.SetValue(instance, columns[index]);
                            index++;
                        }

                        if (lstEntities.Cast<dynamic>().Any(x => x.DocumentNumber == ((dynamic)instance).DocumentNumber && x.IdentificationType == ((dynamic)instance).IdentificationType))
                        {
                            ErrorList.Add($"La persona {((dynamic)instance).IdentificationType} {((dynamic)instance).DocumentNumber} se encuentra repetida en el archivo, fila {file + 1}");
                        }
                        lstEntities.Add(instance);
                    }
                }

                return lstEntities;
            }
            catch
            {
                throw;
            }
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
        /// Fecha inicio de TAR
        /// </sumary> 
        /// <param name="date">Fecha inicio de TAR</param>
        public static string USR_ValidateTAR(string date)
        {

            if (date.Length != 7)
            {
                return "Error en la fecha,debe verificar que la fecha sea asi: Año-mes.Validar la variable 30 ,Fecha inicio de TAR";
            }

            char[] caracteres = date.ToCharArray();
            if (caracteres[4].ToString() != "-")
                return "Error en la fecha,debe verificar que la fecha sea asi: Año-mes.Validar la variable 30 ,Fecha inicio de TAR";


            string mes = "";
            mes = caracteres[5].ToString() + caracteres[6].ToString();
            int month = Int32.Parse(mes);

            if (date == "0000-00" || date == "9999-00" || month == 06)
                return "OK";


            if (month > 12 && month != 06)
                return "Debe registrar una fecha valida, si solo conoce el año en el mes debe registrar 06.Validar la variable 30 Fecha inicio de TAR";




            return "OK";
        }
        /// <sumary>
        /// validación de reglas 4725 extracción de archivos
        /// </sumary> 
        /// <param name="lineSeparator">Separador de linea</param>
        /// <param name="columnSeparator">Separador de columna</param>
        /// <param name="company">compañia</param>
        /// <param name="libraryId">id de la libreria </param>
        /// <param name="fileId">id Del archivo Rs 4725</param>
        /// <param name="columnLength">Cantidad de Columnas </param>
        /// <param name="entity">tipo de la entidad</param>
        /// <param name="listErrors">Lista de errores </param>
        public static dynamic USR_ValidateRule4725(string lineSeparator, string columnSeparator, long company, long libraryId, string fileId, long columnLength, dynamic entity, List<string> listErrors)
        {
            try
            {
                var result = USR_WSGetFile(company, libraryId, fileId);

                Type typeEntity = (Type)entity;
                Type genericListType = typeof(List<>).MakeGenericType(typeEntity);
                IList lstEntities = (IList)Activator.CreateInstance(genericListType);

                if (!result.IsError && result.IsSuccessful)
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(result.Result.ToString());
                    byte[] fileBody = data.FileBody;

                    if (fileBody != null && fileBody.Length > 0)
                    {
                        if (!USR_ValidateFileName(6, "VIH", listErrors, (string)((JValue)((dynamic)result.Result).FileName).Value))
                        {
                            listErrors.Add($"El Nombre del archivo no es valido, no cumple con la estructura especificada {result.FileName}");
                        }

                        using (Stream stream = new MemoryStream(fileBody))
                        {
                            using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                            {
                                //leemos el archivo 
                                var text = streamReader.ReadToEnd();
                                // separamos por lineas 
                                string[] lines = text.Split(new string[] { lineSeparator }, StringSplitOptions.RemoveEmptyEntries);

                                for (var i = 0; i < lines.Length; i++)
                                {
                                    string[] columns = lines[i].Split(columnSeparator[0]);

                                    if (columns.Length != columnLength)
                                    {
                                        listErrors.Add($"La estructura del archivo no corresponde con la longitud o tiene campos vacios asegurese de que todos los campos esten llenos, el error se encontro en el la Linea {i + 1}");
                                    }

                                }
                                if (listErrors.Count == 0)
                                {
                                    lstEntities = SYS_FileToEntities(text, lineSeparator, columnSeparator, entity);
                                    //lstEntities = Helper.USR_FileToEntitiesWithValidation(text, lineSeparator, columnSeparator, entity, listErrors);

                                    int index = 0;
                                    PropertyInfo[] properties = typeEntity.GetProperties();
                                    // Convertimos archivo a lista de entidades
                                    foreach (dynamic ent in lstEntities)
                                    {
                                        if (ent.ValidationErrorsList?.Count > 0)
                                        {
                                            string mensajeItem = $" Línea {index + 1}";
                                            foreach (string msg in ent.ValidationErrorsList)
                                            {
                                                //Asignamos el item de error a los mensajes de validacion de los atributos
                                                var p = properties.Select((Value, Index) => new { Value, Index })
                                                            .Single(pro => Regex.IsMatch(msg, string.Format(@"\b{0}\b", Regex.Escape(pro.Value.Name))));
                                                string msgError = ((RegexAttribute)p.Value.GetCustomAttribute(typeof(RegexAttribute)))?.Message;

                                                listErrors.Add(string.Concat(mensajeItem, ",", msgError));
                                            }
                                        }
                                        ++index;
                                    }
                                }
                                else
                                {
                                    return lstEntities;
                                }
                            }
                        }
                    }
                    else
                    {
                        listErrors.Add($"La estructura del archivo {4725} no corresponde a un formato válido 4725");
                    }
                }
                else
                {
                    listErrors.Add($"No se encontró el archivo {4725} o su estructura no corresponde al formato 4725");
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
        /// <param name="listErrors">Variable de parámetro de función vacía</param>
        /// <param name="index">index</param>
        /// <param name="listaQualification">listaQualification</param>
        /// <param name="listaCodeMunicipaly">listaCodeMunicipaly</param>
        /// <param name="listaCum">listaCum</param>
        /// <param name="ent">ent</param>
        public static dynamic USR_ValidateQualificationCodeAndCodeMunicipaly(List<string> listErrors, long index, List<dynamic> listaQualification, List<dynamic> listaCodeMunicipaly, List<dynamic> listaCum, ENT_StructureRes4725 ent)
        {
            //Validar Regla RUL_IdentityDemography4725 campo 1 CodigoEPS
            if (listaQualification.FirstOrDefault(x => x.QualificationCode == ent.CodigoEPS) == null) listErrors.Add(string.Concat("No existe el codigo EPS o es invalido (Campo 1)", $" Línea {index + 1}"));

            //Valida Codigo Municipio Residencia campo 15
            if (listaCodeMunicipaly.FirstOrDefault(x => x.Code == ent.CodigoMunicipioRes) == null) listErrors.Add(string.Concat("No existe el codigo Municipio Residencia o es invalido (Campo 15)", $" Línea {index + 1}"));

            //Valida el codigo del Municipio dane
            if (ent.MunicipioIPS != "9")
                if (listaCodeMunicipaly.FirstOrDefault(x => x.Code == ent.MunicipioIPS) == null) listErrors.Add(string.Concat("No existe el codigo DANE o es invalido (Campo 56)", $" Línea {index + 1}"));

            //Validar Regla RUL_Past_ campo 25 EntidadTerritorialAnterior
            if (ent.EntidadTerritorialAnterior != "0" && ent.EntidadTerritorialAnterior != "8" && ent.EntidadTerritorialAnterior != "9")
            {
                if (listaQualification.FirstOrDefault(x => x.QualificationCode == ent.EntidadTerritorialAnterior) == null)
                {
                    if (listaCodeMunicipaly.FirstOrDefault(x => x.Code == ent.EntidadTerritorialAnterior) == null) listErrors.Add(string.Concat("No existe el codigo EntidadTerritorial o es invalido (Campo 25) ", $" Línea {index + 1}"));
                }
            }
            //Valida si el codigo cum existe campo (Campo 44.17)
            string inicioMedicamMedicamento3NoposTAR = ent.InicioMedicamento3NoposTAR;
            string CampoMedicamento3NoposTAR = "Campo 44.17";
            USR_ValidateCumTar(listErrors, index, listaCum, inicioMedicamMedicamento3NoposTAR, CampoMedicamento3NoposTAR);

            //Valida si el codigo cum existe campo (Campo 44.18)
            string inicioMedicamMedicamento4NoposTAR = ent.InicioMedicamento4NoposTAR;
            string CampoMedicamento4NoposTAR = "Campo 44.18";
            USR_ValidateCumTar(listErrors, index, listaCum, inicioMedicamMedicamento4NoposTAR, CampoMedicamento4NoposTAR);

            //Valida si el codigo cum existe campo (Campo 44.19)
            string inicioMedicamMedicamento5NoposTAR = ent.InicioMedicamento5NoposTAR;
            string CampoMedicamento5NoposTAR = "Campo 44.19";
            USR_ValidateCumTar(listErrors, index, listaCum, inicioMedicamMedicamento5NoposTAR, CampoMedicamento5NoposTAR);

            //Valida si el codigo cum existe campo (Campo 44.19)
            string inicioMedicamMedicamento6NoposTAR = ent.InicioMedicamento6NoposTAR;
            string CampoMedicamento6NoposTAR = "Campo 44.19";
            USR_ValidateCumTar(listErrors, index, listaCum, inicioMedicamMedicamento6NoposTAR, CampoMedicamento6NoposTAR);

            return true;
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="Sex">Campo Sexo Validacion 17 - Res 4725</param>
        /// <param name="PregnatMother">Variable persona gestante Res 4725</param>
        public static string USR_ValidatePregnantMother4725(string Sex, string PregnatMother)
        {


            if (Sex == "M")
            {
                if (Convert.ToInt32(PregnatMother) != 3)
                {
                    return " El campo sexo es masculino y debe escoger la opcion 3 en el campo campo 17 (PersonaGestante)";
                }
            }

            if (Sex == "F")
            {
                if (Convert.ToInt32(PregnatMother) > 2)
                {
                    return "El campo sexo es femenino y debe escoger la opcion 0,1,2 en el campo 17 (PersonaGestante)";
                }
            }

            return "";

        }
        /// <sumary>
        /// Validacion de Numero de Fallas
        /// </sumary> 
        /// <param name="NumberFails">Variable de validacion de numero de fallas</param>
        public static string USR_ValidateNumberFail(long NumberFails)
        {
            if (NumberFails == 99)
            {
                return "OK";

            }


            if (NumberFails >= 0 && NumberFails <= 40)
            {
                return "OK";
            }

            if (NumberFails < 0 || NumberFails > 40)
            {
                return "Debe introducir un valor que este dentro del rango del numero de fallas desde que inicio la TAR.Validar la variable 51 Número de fallas desde el inicio de la TAR hasta el reporte actual";
            }

            return "Debe introducir un valor correcto,el unico valor valido es 99.Validar la variable 51 Número de fallas desde el inicio de la TAR hasta el reporte actual";




        }
        /// <sumary>
        /// Funcion que valida el numero de cambios hasta el reporte actual
        /// </sumary> 
        /// <param name="NumberChange">Variable de Numero de cambios</param>
        public static string USR_ValidateNumberChange(long NumberChange)
        {
            if (NumberChange == 99)
            {
                return "OK";

            }


            if (NumberChange >= 0 && NumberChange <= 40)
            {
                return "OK";
            }

            if (NumberChange < 0 || NumberChange > 40)
            {
                return "Debe introducir un valor que este dentro del rango establecido de acuerdo al numero de cambios de medicamentos de TAR por todas las posibles causas.Validar la variable 52 Número de cambios de medicamentos de TAR por todas las causas hasta el reporte actual";
            }

            return "Debe introducir un valor correcto,El unico valir correcto es el 99.Validar la variable 52 Número de cambios de medicamentos de TAR por todas las causas hasta el reporte actual";




        }
        /// <sumary>
        /// codigo municipio para regla v105
        /// </sumary> 
        /// <param name="Code">Variable codigo</param>
        public static string USR_ValidateMunicipality_v105(string Code)
        {
            if (Code == "0" || Code == "8" || Code == "9")
                return "OK";

            if (Code == "EPS" || Code == "EOC" || Code == "EPSI" || Code == "ESS" || Code == "CCF" || Code == "EAS")
                return "OK";

            if (Code.Length != 5)
                return "Debe ingresar un dato valido,asegurece que el tamaño del codigo de la empresa aseguradora sea correcto.Validar la variable 105 EPS o entidad territorial al cual se traslado el paciente con VIH desafiliado";


            long adapterId = 1;
            var sql = new StringBuilder();
            sql.Append("SELECT TOP 1 Id  ");
            sql.Append("FROM TypeDetail WITH (NOLOCK)");
            sql.Append("WHERE IdTypeHead=17 AND Code = '" + Code + "'");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (resultExecute.IsSuccessful)
            {
                var listTypeDetails = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());
                if (listTypeDetails.Any())
                {
                    return "OK";
                }
                else { return "Debe ingresar un dato valido, verifique la el dato registrado sea una opcion correcta.Validar la variable 105 EPS o entidad territorial al cual se traslado el paciente con VIH desafiliado"; }


            }
            else { return resultExecute.ErrorMessage.ToString(); }
        }
        /// <sumary>
        /// Valida Codigo Municipio
        /// </sumary> 
        /// <param name="Code">Codigo del municipio</param>
        public static string USR_ValidateMunicipality(string Code)
        {
            long adapterId = 1;
            var sql = new StringBuilder();
            sql.Append("SELECT TOP 1 Id  ");
            sql.Append("FROM TypeDetail WITH (NOLOCK)");
            sql.Append("WHERE IdTypeHead=17 AND Code = '" + Code + "'");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (resultExecute.IsSuccessful)
            {
                var listTypeDetails = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());
                if (listTypeDetails.Any())
                {
                    return "OK";
                }
                else { return "Debe introducir un valor correcto,el unico valor correcto es 9.Validar la variable 56 Municipio de la IPS"; ; }


            }
            else { return resultExecute.ErrorMessage.ToString(); }

        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="Months_Dispensed">Funcion de Meses Dispensados</param>
        public static string USR_ValidateMonthDispensed(long Months_Dispensed)
        {
            if ((Months_Dispensed == 98 || Months_Dispensed == 99))
            {
                return "OK";

            }


            if (Months_Dispensed >= 0 && Months_Dispensed <= 12)
            {
                return "OK";
            }

            if (Months_Dispensed < 0 || Months_Dispensed > 12)
            {
                return "Debe ingresar un dato que se encuentre dentro del rango de meses.Validar la variable 46 Número de meses que se dispensó la fórmula completa de TAR durante los primeros 12 meses luego de iniciar TAR";
            }

            return "Debe ingresar una opcion valida, debe verficar que el numero registrado sea 98 o 99.Validar la variable 46 Número de meses que se dispensó la fórmula completa de TAR durante los primeros 12 meses luego de iniciar TAR";




        }
        /// <sumary>
        /// Función que valida el código de habilitacion de la sede de la IPS
        /// </sumary> 
        /// <param name="CodeHabilitation">Variable de ingreso del codigo de habilitacion</param>
        public static string USR_ValidateIPS(string CodeHabilitation)
        {
            if (CodeHabilitation == "9")
                return "OK";

            if (CodeHabilitation.Length != 12)
                return "Debe introducir un valor correcto teniendo en cuenta el tamaño del codigo.Validar la variable 54";

            long adapterId = 1;
            var sql = new StringBuilder();
            sql.Append("SELECT TOP 1 QualificationCode  ");
            sql.Append("FROM Operator WITH (NOLOCK)");
            sql.Append("WHERE IdOperatorType=70 AND QualificationCode = '" + CodeHabilitation + "'");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (resultExecute.IsSuccessful)
            {
                var listTypeDetails = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());
                if (listTypeDetails.Any())
                {
                    return "OK";
                }
                else { return $"Error en el codigo,Debe verificar si el codigo de Habilitacion sea el correcto.Validar la variable 54"; }


            }
            else { return resultExecute.ErrorMessage.ToString(); }
        }
        /// <sumary>
        /// Valida si el nombre de una resolución es valido
        /// </sumary> 
        /// <param name="codeEPSlength">longitud del codigo de la EPS</param>
        /// <param name="resolutionCode">Codigo de la Resolucion</param>
        /// <param name="listError">Lista de errores</param>
        /// <param name="fileName">Nombre del archivo a validar</param>
        public static bool USR_ValidateFileName(long codeEPSlength, string resolutionCode, List<string> listError, string fileName)
        {
            if (listError == null)
            {
                listError = new List<string>();
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                listError.Add("El nombre del Archivo no es Valido");
                return listError.Count == 0;
            }

            if (!System.IO.Path.GetExtension(fileName).Equals(".txt"))
            {
                listError.Add("La extension del archivo no es valida");
                return listError.Count == 0;
            }


            string[] fileNameArreay = fileName.Split('_');

            if (fileNameArreay.Length != 3)
            {
                listError.Add("El nombre del archivo no es Valido");
                return listError.Count == 0;
            }

            var yyyymmdd = new Regex(@"^(?:(?:(?:(?:(?:[13579][26]|[2468][048])00)|(?:[0-9]{2}(?:(?:[13579][26])|(?:[2468][048]|0[48]))))(?:(?:(?:09|04|06|11)(?:0[1-9]|1[0-9]|2[0-9]|30))|(?:(?:01|03|05|07|08|10|12)(?:0[1-9]|1[0-9]|2[0-9]|3[01]))|(?:02(?:0[1-9]|1[0-9]|2[0-9]))))|(?:[0-9]{4}(?:(?:(?:09|04|06|11)(?:0[1-9]|1[0-9]|2[0-9]|30))|(?:(?:01|03|05|07|08|10|12)(?:0[1-9]|1[0-9]|2[0-9]|3[01]))|(?:02(?:[01][0-9]|2[0-8])))))$");

            if (!yyyymmdd.IsMatch(fileNameArreay[0]))
            {
                listError.Add("La fecha en el nombre del Archivo no es valida (verifique el formato)");
                return listError.Count == 0;
            }

            bool fecha = DateTime.TryParseExact(fileNameArreay[0], "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime dateTime);

            if (!fecha)
            {
                listError.Add("La fecha en el nombre del Archivo no es valida");
            }

            if (fileNameArreay[1].Length != codeEPSlength)
            {
                listError.Add($"Codigo de EPS {fileNameArreay[1].Length} | {codeEPSlength}");
            }

            if (!fileNameArreay[2].Replace(".txt", "").Trim().Equals(resolutionCode))
            {
                listError.Add("El nombre del archivo no es valido");
            }

            return listError.Count == 0;
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="NumDocu">Campo numero de documento Res 4725 Validacion 9</param>
        /// <param name="ValRegimen">Campo Regimen Res 4725 Validacion campo 9</param>
        public static bool USR_ValidateDocumentRes4725(string NumDocu, string ValRegimen)
        {
            if (NumDocu != "")
            {
                if (ValRegimen != "2")
                    return true;
            }

            return false;
        }
        /// <sumary>
        /// valida el formato año-mes
        /// </sumary> 
        /// <param name="Date">fecha a validar</param>
        /// <param name="ValidateDate">Valida Dato</param>
        /// <param name="ValidateStruct">Valida estructura</param>
        public static string USR_ValidateDateYYYYmm(string Date, string ValidateDate, string ValidateStruct)
        {
            if (Date == "9999-99")
                return "OK";

            int tm = Date.Length;
            if (tm != 7)
            {
                return ValidateStruct;
            }

            char[] caracteres = Date.ToCharArray();
            if (caracteres[4].ToString() != "-")
                return ValidateStruct;

            string mes = "";
            mes = caracteres[5].ToString() + caracteres[6].ToString();
            int month = Int32.Parse(mes);
            if (month < 1 || month > 12)
                return ValidateDate;

            return "OK";


        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="listErrors">listErrors</param>
        /// <param name="index">index</param>
        /// <param name="date">date</param>
        /// <param name="fielDate">fielDate</param>
        public static dynamic USR_ValidateDateTar(List<string> listErrors, long index, DateTime date, string fielDate)
        {
            if (date > DateTime.Now)
            {
                listErrors.Add(string.Concat($"{fielDate}", $" Línea {index + 1}"));
                return false;
            }
            return true;
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="listErrors">listErrors</param>
        /// <param name="index">index</param>
        /// <param name="ent">ent</param>
        public static dynamic USR_ValidateDatesTar(List<string> listErrors, long index, ENT_StructureRes4725 ent)
        {
            //Valida fecha de nacimiento 
            string fieldDateField10 = "La fecha de nacimiento no puede ser mayor a la fecha actual (campo 10)";
            DateTime fechaNacimiento;
            if (DateTime.TryParse(ent.FechaNacimiento, out fechaNacimiento))

            {
                USR_ValidateDateTar(listErrors, index, fechaNacimiento, fieldDateField10);

                decimal edadMeses = Math.Abs((Convert.ToDateTime(ent.FechaNacimiento).Month - DateTime.Now.Month) + 12 * (Convert.ToDateTime(ent.FechaNacimiento).Year - DateTime.Now.Year));
                //Valida campo 85 Estudio Carga Viral Menores 18 Meses
                if (edadMeses <= 18)
                {
                    if (ent.EstudioCargarViralMenores18Meses.Equals("9"))
                    {
                        listErrors.Add(string.Concat("El campo estudio carga viral menores de 18 meses (campo 85) los valores permitidos son 0,1,2,3,4 ya que no es menor de 18 meses porque habra adultos, el valor designado debe ser 9 (No aplica) ver la ", $" Línea {index + 1}"));
                    }
                    if (ent.EstudioSegundaCargaViralMenores18.Equals("9"))
                    {
                        listErrors.Add(string.Concat("El campo estudio segunda carga viral menores de 18 meses (campo 86) los valores permitidos son 0,1,2,3,4 ya que es un menor de 18 meses el valor designado debe ser (No aplica) ver la ", $" Línea {index + 1}"));
                    }
                }
                else
                {
                    if (!ent.EstudioCargarViralMenores18Meses.Equals("9"))
                    {
                        listErrors.Add(string.Concat("El campo estudio carga viral menores de 18 meses (campo 85) los valores permitidos son 9 ya que el menor es mayor de 18 meses ver la ", $" Línea {index + 1}"));
                    }
                    if (!ent.EstudioSegundaCargaViralMenores18.Equals("9"))
                    {
                        listErrors.Add(string.Concat("El campo estudio segunda carga viral menores de 18 meses (campo 86) los valores permitidos son 9 ya que el menor es mayor de 18 meses ver la ", $" Línea {index + 1}"));
                    }
                }
            }

            //Valida fecha de Afiliacion
            string fieldDateField16 = "La fecha de Afiliacion no puede ser mayor a la fecha actual (campo 16)";
            DateTime fechaAfiliacionEPS = Convert.ToDateTime(ent.FechaAfiliacionEPS);
            bool resField16 = USR_ValidateDateTar(listErrors, index, fechaAfiliacionEPS, fieldDateField16);

            //Valida que la fecha de afiliacion no sea menor que la de nacimiento
            if (resField16.Equals(true))
            {
                if (fechaAfiliacionEPS < fechaNacimiento)
                {
                    listErrors.Add(string.Concat("La fecha de Afiliacion (campo 16) no puede ser menor a la de Nacimiento (campo 10)", $" Línea {index + 1}"));
                }
            }

            //Valida fecha de Presuntiva
            string fieldDateField21 = "La fecha prueba presuntiva no puede ser mayor a la fecha actual (campo 21)";
            DateTime fechaPruebaPresuntiva;
            if (DateTime.TryParse(ent.FechaPruebaPresuntivaElisa, out fechaPruebaPresuntiva))
            {
                bool resField21 = USR_ValidateDateTar(listErrors, index, fechaPruebaPresuntiva, fieldDateField21);
                if (!ent.FechaPruebaPresuntivaElisa.Equals("1111-11"))
                {
                    if (resField21.Equals(true))
                    {
                        if (fechaPruebaPresuntiva < fechaAfiliacionEPS)
                        {
                            listErrors.Add(string.Concat("La fecha presuntiva (campo 21) no puede ser menor a la fecha de Afiliacion (campo 16)", $" Línea {index + 1}"));
                        }
                    }
                }
            }
            else
            {
                if (!(ent.FechaPruebaPresuntivaElisa.Equals("0000-00") || ent.FechaPruebaPresuntivaElisa.Equals("2222-22") || ent.FechaPruebaPresuntivaElisa.Equals("9999-99")))
                {
                    int year = Convert.ToInt32(ent.FechaPruebaPresuntivaElisa.Substring(0, 4));
                    int yearNow = DateTime.Now.Year;

                    if (!((year == yearNow - 1 || year == yearNow) && ent.FechaPruebaPresuntivaElisa.Substring(5, 2).Equals("99")))
                    {
                        listErrors.Add(string.Concat("La fecha presuntiva con la fecha ", $"{ent.FechaPruebaPresuntivaElisa}", " en (campo 21) es invalida recuerde que si envia yyyy-99 se valida contra el año actual ejemplo 2019-99 valide la ", $" Línea {index + 1}"));
                    }
                }
            }

            //Valida fecha de Diagnostivo Infeccion
            string fieldDateField22 = "La fecha diagnostico de infeccion no puede ser mayor a la fecha actual (campo 22)";
            DateTime fechaDiagnosticoInfeccionVIH;
            bool ValidateFechaFechaDiagnosticField22 = true;
            if (DateTime.TryParse(ent.FechaDiagnosticoInfeccionVIH, out fechaDiagnosticoInfeccionVIH))
            {
                bool resField22 = USR_ValidateDateTar(listErrors, index, fechaDiagnosticoInfeccionVIH, fieldDateField22);

                if (resField22.Equals(true))
                {
                    if (fechaDiagnosticoInfeccionVIH < fechaPruebaPresuntiva)
                    {
                        listErrors.Add(string.Concat("La fecha diagnostico de infeccion (campo 22) no puede ser menor a la fecha presuntiva (campo 21)", $" Línea {index + 1}"));
                    }
                    //Valida que el mecanismo de trasmision sea valido para fechas validas
                    if (Convert.ToInt32(ent.MecanismoTransmision) > 6)
                    {
                        listErrors.Add(string.Concat("Cuando la fecha es diferente a 9999-99 o 0000-00 en el (campo 22), no debe permitir que el (campo 26) tenga el valor 9", $" Línea {index + 1}"));
                    }
                }
            }
            else
            {
                //Valida fecha y mecanismo de transmision campo 22 y campo 26
                if (ent.FechaDiagnosticoInfeccionVIH.Equals("9999-99") || ent.FechaDiagnosticoInfeccionVIH.Equals("0000-00"))
                {
                    //Validacion campo 26
                    if (!ent.MecanismoTransmision.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, mecanismo de trasmision el (campo 26) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 27
                    if (!ent.EstadioClinicoMomentoDelDiagnostico.Equals("10"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Estadio Clinico Momento Del Diagnostico el (campo 27) debe aceptar solamente el valor 10, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 28
                    if (!ent.ConteoLinfocitosTCD4Diagnostico.Equals("99005"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Conteo Linfocitos TCD4 Diagnostico el (campo 28) debe aceptar solamente el valor 99005, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 29
                    if (!ent.ConteoLinfocitosTotalesMomentoDiagnostico.Equals("99005"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Conteo Linfocitos Totales Momento Diagnostico el (campo 29) debe aceptar solamente el valor 99005, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 30
                    if (!(ent.FechaInicioTAR.Equals("0000-00") || ent.FechaInicioTAR.Equals("9999-00")))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Fecha Inicio TAR el (campo 30) debe aceptar solamente el valor 0000-00 - 9999-99, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 31
                    if (!ent.ConteoLinfocitosTCD4MomentoInicioTAR.Equals("99005"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Conteo Linfocitos TCD4 Momento Inicio TAR el (campo 31) debe aceptar solamente el valor 99005, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 32
                    if (!ent.ConteoLinfocitosTotalesInicioTar.Equals("99005"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Conteo Linfocitos Totales Inicio Tar el (campo 32) debe aceptar solamente el valor 99005, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 33
                    if (!ent.CargaViralInicioTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Carga Viral Inicio TAR el (campo 33) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 34
                    if (!ent.MotivoInicioTAR.Equals("10"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Motivo Inicio TAR el (campo 34) debe aceptar solamente el valor 10, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 35
                    if (!ent.TeniaAnemiaAlIniciar.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Tenia Anemia Al Iniciar el (campo 35) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 36
                    if (!ent.EnfermedadRenalCronicaInicioTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Enfermedad Renal Cronica InicioTAR el (campo 36) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 37
                    if (!ent.CoinfeccionVHBIniciarTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Coinfeccion VHB Iniciar TAR el (campo 37) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 38
                    if (!ent.CoinfeccionVHCIniciarTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, CoinfeccionVHC IniciarTAR el (campo 38) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 39
                    if (!ent.TuberculosisInicioTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Tuberculosis Inicio TAR el (campo 39) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 40
                    if (!ent.CirugiaCardiovascularInicioTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Cirugia Cardiovascular Inicio TAR el (campo 40) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 41
                    if (!ent.SarcomaKaposiInicioTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Sarcoma Kaposi Inicio TAR el (campo 41) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 42
                    if (!ent.EmbarazoInicioTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Embarazo Inicio TAR el (campo 42) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 43
                    if (!ent.EnfermedadPsiquiatricaInicioTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Enfermedad Psiquiatrica Inicio TAR el (campo 43) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.1
                    if (!ent.InicioAbacavirTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Abacavir TAR el (campo 44.1) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.2
                    if (!ent.InicioAtazanavirTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Atazanavir TAR el (campo 44.2) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.3
                    if (!ent.InicioDidanosinaTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Didanosina TAR el (campo 44.3) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.4
                    if (!ent.InicioEfavirenzTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Efavirenz TAR el (campo 44.4) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.5
                    if (!ent.InicioEstavudinaTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Estavudina TAR el (campo 44.5) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.6
                    if (!ent.InicioFosamprenavirTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Fosamprenavir TAR el (campo 44.6) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.7
                    if (!ent.InicioIndinavirTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Indinavir TAR el (campo 44.7) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.8
                    if (!ent.InicioLamivudinaTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Lamivudina TAR el (campo 44.8) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.9
                    if (!ent.InicioLopinavirTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Lopinavir TAR el (campo 44.9) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.10
                    if (!ent.InicioNevirapinaTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Nevirapina TAR el (campo 44.10) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.11
                    if (!ent.InicioNelfinavirTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Nelfinavir TAR el (campo 44.11) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.12
                    if (!ent.InicioRitonavirTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Ritonavir TAR el (campo 44.12) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.13
                    if (!ent.InicioSaquinavirTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Saquinavir TAR el (campo 44.13) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.14
                    if (!ent.InicioZidovudinaTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Zidovudina TAR el (campo 44.14) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.15
                    if (!ent.InicioTenofovirTARMed1.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Tenofovir TARMed1 el (campo 44.15) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.16
                    if (!ent.InicioEmtricitabinaTARMed2.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Emtricitabina TARMed2 el (campo 44.16) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.17
                    if (!ent.InicioMedicamento3NoposTAR.Equals("0"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Medicamento 3Nopos TAR el (campo 44.17) debe aceptar solamente el valor 0, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.18
                    if (!ent.InicioMedicamento4NoposTAR.Equals("0"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Medicamento 4Nopos TAR el (campo 44.18) debe aceptar solamente el valor 0, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.19
                    if (!ent.InicioMedicamento5NoposTAR.Equals("0"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Medicamento 5Nopos TAR el (campo 44.19) debe aceptar solamente el valor 0, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 44.20
                    if (!ent.InicioMedicamento6NoposTAR.Equals("0"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Inicio Medicamento 6Nopos TAR el (campo 44.20) debe aceptar solamente el valor 0, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 45
                    if (!ent.RecibioAsesoriaInicioTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Recibio Asesoria InicioTAR el (campo 45) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 47
                    if (!ent.NumeroCitasMedicasPrimeros12Meses.Equals("97"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Numero Citas Medicas Primeros 12 Meses el (campo 47) debe aceptar solamente el valor 97, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 48
                    if (!ent.MedicamentosCambiadosIncioTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Medicamentos Cambiados Inicio TAR (campo 48) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 49
                    if (!ent.FechaPrimerCambioMedInicioTAR.Equals("9999-99"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Fecha Primer Cambio Med InicioTAR (campo 49) debe aceptar solamente el valor 9999-99, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 50
                    if (!ent.CausaCambioMedicamentoInicioTAR.Equals("9"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Causa Cambio Medicamento InicioTAR (campo 50) debe aceptar solamente el valor 9, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 51
                    if (!ent.NumerosFallasInicioTAR.Equals("99"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Numeros Fallas InicioTAR (campo 51) debe aceptar solamente el valor 99, no aplica", $" Línea {index + 1}"));

                    //Validacion campo 52
                    if (!ent.NumeroCambiosMedCausaActual.Equals("99"))
                        listErrors.Add(string.Concat("Si la fecha diagnostico Infeccion (campo 22) es 9999-99 o 0000-00 que no aplica para VIH o esta en estudio, Numero Cambios Med CausaActual (campo 52) debe aceptar solamente el valor 99, no aplica", $" Línea {index + 1}"));
                }
                else
                {
                    if (ent.MecanismoTransmision.Equals("9"))
                    {
                        listErrors.Add(string.Concat("La fecha de diagnostico (campo 22) debe ser (9999-99) o (0000-00) ya que esta ingresando en mecanismo de transmision (campo 26) el codigo 9 (no aplica)", $" Línea {index + 1}"));
                    }
                }
                ValidateFechaFechaDiagnosticField22 = false;
            }
            //Valida fecha de terapia antirretroviral
            string fieldDateField30 = "La fecha Terapia Antirretroviral no puede ser mayor a la fecha actual (campo 30)";
            DateTime fechaInicioTAR;
            if (DateTime.TryParse(ent.FechaInicioTAR, out fechaInicioTAR))
            {
                bool resField30 = USR_ValidateDateTar(listErrors, index, fechaInicioTAR, fieldDateField30);

                if (resField30.Equals(true))
                {
                    if (ValidateFechaFechaDiagnosticField22 == true)
                    {
                        if (fechaInicioTAR < fechaDiagnosticoInfeccionVIH)
                        {
                            listErrors.Add(string.Concat("La fecha Terapia Antirretroviral (campo 30) no puede ser menor a la Fecha de diagnostico (campo 22)", $" Línea {index + 1}"));
                        }
                    }
                }
            }
            else
            {
                if (!(ent.FechaInicioTAR.Equals("9999-00") || ent.FechaInicioTAR.Equals("0000-00")))
                {
                    listErrors.Add(string.Concat("La fecha Terapia Antirretroviral (campo 30) es invalida solo acepta 9999-00 - 0000-00 cuando no aplica", $" Línea {index + 1}"));
                }
            }

            //Valida fecha primer cambio medicamento del esquema inicial => VALIDAR
            string fieldDateField49 = "La fecha Terapia Antirretroviral no puede ser mayor a la fecha actual (campo 49)";
            DateTime fechaPrimerCambioMedInicioTAR;
            if (DateTime.TryParse(ent.FechaPrimerCambioMedInicioTAR, out fechaPrimerCambioMedInicioTAR))
            {
                bool resField49 = USR_ValidateDateTar(listErrors, index, fechaPrimerCambioMedInicioTAR, fieldDateField49);
                if (resField49.Equals(true))
                {
                    if (fechaPrimerCambioMedInicioTAR < fechaInicioTAR)
                    {
                        listErrors.Add(string.Concat("La fecha primer cambio medicamento (campo 49) no puede ser menor a la fecha inicio Tar(campo 30)", $" Línea {index + 1}"));
                    }
                }
            }
            else
            {
                if (!(ent.FechaPrimerCambioMedInicioTAR.Equals("0000-00") || ent.FechaPrimerCambioMedInicioTAR.Equals("7777-77") || ent.FechaPrimerCambioMedInicioTAR.Equals("8888-88") || ent.FechaPrimerCambioMedInicioTAR.Equals("9999-99")))
                {
                    int year = Convert.ToInt32(ent.FechaPrimerCambioMedInicioTAR.Substring(0, 4));
                    int yearNow = DateTime.Now.Year - 10;

                    if (!((year > yearNow) && ent.FechaPrimerCambioMedInicioTAR.Substring(5, 2).Equals("99")))
                    {
                        listErrors.Add(string.Concat("La fecha  Primer Cambio Med InicioTAR con la fecha ", $"{ent.FechaPrimerCambioMedInicioTAR}", " en (campo 49) es invalida recuerde que si envia yyyy-99 se valida contra el año actual ejemplo 2018-99- 2019-99 valide la ", $" Línea {index + 1}"));
                    }
                }
            }

            //Valida fecha Ingreso IPS actual seguimiento
            string fieldDateField55 = "La fecha ingreso IPS no puede ser mayor a la fecha actual (campo 55)";
            DateTime fechaIngresoIPS;
            if (DateTime.TryParse(ent.FechaIngresoIPS, out fechaIngresoIPS))
            {
                bool resField55 = USR_ValidateDateTar(listErrors, index, fechaIngresoIPS, fieldDateField55);
                if (resField55.Equals(true))
                {
                    if (ValidateFechaFechaDiagnosticField22 == true)
                    {
                        if (fechaIngresoIPS < fechaDiagnosticoInfeccionVIH)
                        {
                            listErrors.Add(string.Concat("La fecha de ingreso IPS (campo 55) no puede ser menor a la fecha diagnostico (campo 22)", $" Línea {index + 1}"));
                        }
                    }
                }
            }
            else
            {
                listErrors.Add(string.Concat("La fecha IngresoIPS (campo 55) es invalida", $" Línea {index + 1}"));
            }


            //Valida fecha ultimo conteo linfocitos TCD4
            string fieldDateField75 = "La fecha ultimo conteo linfocitos TCD4 no puede ser mayor a la fecha actual (campo 75)";
            DateTime fechaUltimoConteoLinfocitosCD4 = Convert.ToDateTime(ent.FechaUltimoConteoLinfocitosCD4);
            bool resField75 = USR_ValidateDateTar(listErrors, index, fechaUltimoConteoLinfocitosCD4, fieldDateField75);

            if (resField75.Equals(true))
            {
                if (ValidateFechaFechaDiagnosticField22 == true)
                {
                    if (fechaUltimoConteoLinfocitosCD4 < fechaDiagnosticoInfeccionVIH)
                    {
                        listErrors.Add(string.Concat("La fecha ultimo conteo linfocitos TCD4 (campo 75) no puede ser menor a la fecha de diagnostico (campo 22)", $" Línea {index + 1}"));
                    }
                }
            }

            //Valida fecha ultimo conteo linfocitos totales 
            string fieldDateField77 = "La fecha ultimo conteo linfocitos totales no puede ser mayor a la fecha actual (campo 77)";
            DateTime fechaUlitmoConteoLinfocitos;
            if (DateTime.TryParse(ent.FechaUlitmoConteoLinfocitos, out fechaUlitmoConteoLinfocitos))
            {
                bool resField77 = USR_ValidateDateTar(listErrors, index, fechaUlitmoConteoLinfocitos, fieldDateField77);

                if (resField77.Equals(true))
                {
                    if (ValidateFechaFechaDiagnosticField22 == true)
                    {
                        if (fechaUlitmoConteoLinfocitos < fechaDiagnosticoInfeccionVIH)
                        {
                            listErrors.Add(string.Concat("La fecha ultimo conteo linfocitos totales (campo 77) no puede ser menor a la fecha de diagnostico (campo 22)", $" Línea {index + 1}"));
                        }
                    }
                }
            }

            //Valida fecha ultima carga viral
            string fieldDateField79 = "La fecha ultima carga viral no puede ser mayor a la fecha actual (campo 79)";
            DateTime fechaUltimaCargaViralReportada;
            if (DateTime.TryParse(ent.FechaUltimaCargaViralReportada, out fechaUltimaCargaViralReportada))
            {
                bool resField79 = USR_ValidateDateTar(listErrors, index, fechaUltimaCargaViralReportada, fieldDateField79);

                if (resField79.Equals(true))
                {
                    if (ValidateFechaFechaDiagnosticField22 == true)
                    {
                        if (fechaUltimaCargaViralReportada < fechaDiagnosticoInfeccionVIH)
                        {
                            listErrors.Add(string.Concat("La fecha carga viral (campo 79) no puede ser menor a la fecha de diagnostico (campo 22)", $" Línea {index + 1}"));
                        }
                    }
                }
            }

            //Valida fecha inicio de los medicamentos
            string fieldDateField90 = "La fecha inicio de los medicamentos no puede ser mayor a la fecha actual (campo 90)";
            DateTime fechaInicioMedimamentosTAR;
            if (DateTime.TryParse(ent.FechaInicioMedimamentosTAR, out fechaInicioMedimamentosTAR))
            {
                bool resField90 = USR_ValidateDateTar(listErrors, index, fechaInicioMedimamentosTAR, fieldDateField90);

                if (resField90.Equals(true) && ent.FechaInicioMedimamentosTAR != "1111-11")
                {
                    if (fechaInicioMedimamentosTAR < fechaInicioTAR)
                    {
                        listErrors.Add(string.Concat("La fecha inicio de los medicamentos (campo 90) no puede ser menor a la fecha inicio Tar(campo 30)", $" Línea {index + 1}"));
                    }
                }
            }

            //Valida fecha inicio tratamiento antituberculoso  
            string fieldDateField94 = "La fecha inicio  tratamiento antituberculoso no puede ser mayor a la fecha actual (campo 94)";
            DateTime fechaInicioTratamientoAntituberculoso;
            if (DateTime.TryParse(ent.FechaInicioTratamientoAntituberculoso, out fechaInicioTratamientoAntituberculoso))
            {
                bool resField94 = USR_ValidateDateTar(listErrors, index, fechaInicioTratamientoAntituberculoso, fieldDateField94);

                if (resField94.Equals(true))
                {
                    if (fechaInicioTratamientoAntituberculoso < fechaAfiliacionEPS)
                    {
                        listErrors.Add(string.Concat("La fecha inicio tratamiento antituberculoso (campo 94) no puede ser menor a la fecha afiliacion Tar(campo 16)", $" Línea {index + 1}"));
                    }
                }
            }

            //Valida fecha primer tramtamiento de sifilis
            string fieldDateField100 = "La fecha primer tramtamiento de sifilis no puede ser mayor a la fecha actual (campo 100)";
            DateTime fechaPrimerTratamientoSifilis;
            if (DateTime.TryParse(ent.FechaPrimerTratamientoSifilis, out fechaPrimerTratamientoSifilis))
            {
                bool resField100 = USR_ValidateDateTar(listErrors, index, fechaPrimerTratamientoSifilis, fieldDateField100);

                if (resField100.Equals(true))
                {
                    if (fechaPrimerTratamientoSifilis < fechaAfiliacionEPS)
                    {
                        listErrors.Add(string.Concat("La fecha primer tramtamiento de sifilis (campo 100) no puede ser menor a la fecha afiliacion Tar(campo 16)", $" Línea {index + 1}"));
                    }
                }
            }

            //Valida fecha segundo tramtamiento de sifilis
            string fieldDateField101 = "La fecha segundo tramtamiento de sifilis no puede ser mayor a la fecha actual (campo 101)";
            DateTime fechaSegundoTratamientoSifilis;
            if (DateTime.TryParse(ent.FechaSegundoTratamientoSifilis, out fechaSegundoTratamientoSifilis))
            {
                bool resField101 = USR_ValidateDateTar(listErrors, index, fechaSegundoTratamientoSifilis, fieldDateField101);

                if (resField101.Equals(true))
                {
                    if (fechaSegundoTratamientoSifilis < fechaPrimerTratamientoSifilis)
                    {
                        listErrors.Add(string.Concat("La fecha segundo tramtamiento de sifilis (campo 101) no puede ser menor a la fecha primer tramtamiento de sifilis(campo 100)", $" Línea {index + 1}"));
                    }
                }
            }

            //Valida fecha tercer tramtamiento de sifilis
            string fieldDateField102 = "La fecha tercer tramtamiento de sifilis no puede ser mayor a la fecha actual (campo 102)";
            DateTime fechaTercerTratamientoSifilis;
            if (DateTime.TryParse(ent.FechaTercerTratamientoSifilis, out fechaTercerTratamientoSifilis))
            {
                bool resField102 = USR_ValidateDateTar(listErrors, index, fechaTercerTratamientoSifilis, fieldDateField102);

                if (resField102.Equals(true))
                {
                    if (fechaTercerTratamientoSifilis < fechaSegundoTratamientoSifilis)
                    {
                        listErrors.Add(string.Concat("La fecha tercer tramtamiento de sifilis (campo 102) no puede ser menor a la fecha segundo tramtamiento de sifilis(campo 101)", $" Línea {index + 1}"));
                    }
                }
            }
            //Valida fecha desafiliacion  
            string fieldDateField104 = "La fecha desafiliacion no puede ser mayor a la fecha actual (campo 104)";
            DateTime fechaDesafiliacionEPS;
            if (DateTime.TryParse(ent.FechaDesafiliacionEPS, out fechaDesafiliacionEPS))
            {
                bool resField104 = USR_ValidateDateTar(listErrors, index, fechaDesafiliacionEPS, fieldDateField104);

                if (resField104.Equals(true))
                {
                    if (fechaDesafiliacionEPS < fechaAfiliacionEPS)
                    {
                        listErrors.Add(string.Concat("La fecha desafiliacion (campo 104) no puede ser menor a la fecha Afiliacion (campo 16)", $" Línea {index + 1}"));
                    }
                }
            }

            //Valida fecha Muerte
            string fieldDateField106 = "La fecha de muerte no puede ser mayor a la fecha actual (campo 106)";
            DateTime fechaMuerte;
            if (DateTime.TryParse(ent.FechaMuerte, out fechaMuerte))
            {
                bool resField106 = USR_ValidateDateTar(listErrors, index, fechaMuerte, fieldDateField106);

                if (resField106.Equals(true))
                {
                    if (fechaMuerte < fechaNacimiento)
                    {
                        listErrors.Add(string.Concat("La fecha muerte (campo 106) no puede ser menor a la fecha Nacimiento (campo 10)", $" Línea {index + 1}"));
                    }

                    if (fechaMuerte < fechaDesafiliacionEPS)
                    {
                        listErrors.Add(string.Concat("La fecha muerte (campo 106) no puede ser menor a la fecha Desafiliacion (campo 104)", $" Línea {index + 1}"));
                    }
                    if (ent.CausaMuerte.Equals("0"))
                    {
                        listErrors.Add(string.Concat("La causa de muerte en el (campo 107) debe ser 1,2,3 ya que el paciente fallecio, verificar la ", $" Línea {index + 1}"));
                    }
                }
            }
            else
            {
                if (ent.FechaMuerte.Equals("9999-99-99"))
                {
                    if (!ent.CausaMuerte.Equals("0"))
                    {
                        listErrors.Add(string.Concat("Si la fecha de muerte es 9999-99-99 la causa de muerte debe ser igual a 0", $" Línea {index + 1}"));
                        return false;
                    }
                }
            }
            return true;
        }
        /// <sumary>
        /// Valida Fecha
        /// </sumary> 
        /// <param name="Message">Mensaje</param>
        /// <param name="Date">Fecha</param>
        public static string USR_ValidateDateRes(string Message, string Date)
        {
            int tm = Date.Length;
            if (tm != 10)
            {
                return Message;
            }

            char[] caracteres = Date.ToCharArray();
            if (caracteres[4].ToString() != "-" || caracteres[7].ToString() != "-")
                return Message;


            try
            {

                DateTime d = new DateTime();
                d = DateTime.Parse(Date);
                return "OK";
            }
            catch (Exception ex)
            {
                return Message;

            }
        }
        /// <sumary>
        /// valida la fecha de cambio de medicamento
        /// </sumary> 
        /// <param name="date">Variable de parámetro de función vacía</param>
        public static string USR_ValidateDateChangeMedicament(string date)
        {
            char[] caracteres = date.ToCharArray();
            if (date.Length != 7 || caracteres[4].ToString() != "-")
            {
                return "Error en la fecha,debe ser de la siguiente manera: Año-mes.Validar la variable 49 Fecha del primer cambio de cualquier medicamento del esquema inicial de TAR";
            }

            string mes = "";
            mes = caracteres[5].ToString() + caracteres[6].ToString();
            int month = Int32.Parse(mes);

            if (date == "0000-00" || date == "7777-77" || date == "8888-88" || date == "9999-99" || month == 99)
                return "OK";


            if (month > 12 && month != 99)
                return "Debe introducir un valor correcto.Validar la variable 49,Fecha del primer cambio de cualquier medicamento del esquema inicial de TAR";




            return "OK";
        }
        /// <sumary>
        /// valida estructura de la fecha
        /// </sumary> 
        /// <param name="Date">Valida Fecha </param>
        /// <param name="ValidateStruct">Valida estructura</param>
        /// <param name="ValidateData">Valida dato</param>
        public static string USR_ValidateDate(string Date, string ValidateStruct, string ValidateData)
        {

            if (Date == "9999-99-99")
                return "OK";

            int tm = Date.Length;
            if (tm != 10)
            {
                return ValidateStruct;
            }

            char[] caracteres = Date.ToCharArray();
            if (caracteres[4].ToString() != "-" || caracteres[7].ToString() != "-")
                return ValidateStruct;


            try
            {

                DateTime d = new DateTime();
                d = DateTime.Parse(Date);
                return "OK";
            }
            catch (Exception ex)
            {
                return ValidateData;

            }
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="listErrors">listErrors</param>
        /// <param name="index">index</param>
        /// <param name="listaCum">listaCum</param>
        /// <param name="cumTar">cumTar</param>
        /// <param name="fieldTar">fieldTar</param>
        public static dynamic USR_ValidateCumTar(List<string> listErrors, long index, List<dynamic> listaCum, string cumTar, string fieldTar)
        {
            if (cumTar != "")
            {
                string code = cumTar.Substring(cumTar.Length - 1, 1);
                if (code.Equals("1"))
                {
                    string[] cum = cumTar.Split(' ');
                    if (cum.Length == 2)
                    {
                        foreach (var item in cum)
                        {
                            if (!item.Equals("1"))
                            {
                                if (listaCum.FirstOrDefault(find => find.Code == item) == null) listErrors.Add(string.Concat("El codigo cum es invalido ", $"{fieldTar}", $" Línea {index + 1}"));
                            }
                        }
                    }
                    else
                    {
                        listErrors.Add(string.Concat("Debe enviar codigo cum + el digito 1 separado por espacio ", $"{fieldTar}", $" Línea {index + 1}"));
                    }
                }
                else
                {
                    if (!code.Equals("0"))
                    {
                        listErrors.Add(string.Concat("Debe enviar un codigo valido cum valido, Ej: (CodigoCum + 1) ó (0) si el paciente no reicbio medicamento no POS al inicio ", $"{fieldTar}", $" Línea {index + 1}"));
                    }

                }
            }
            return true;
        }
        /// <sumary>
        /// Funcion para variable 95
        /// </sumary> 
        /// <param name="PositionVariable">PositionVariable</param>
        /// <param name="CodCUM">CodCUM</param>
        /// <param name="NameVariable">Nombre Variable</param>
        public static string USR_ValidateCUM_v95(string PositionVariable, string CodCUM, string NameVariable)
        {
            if (CodCUM == "0")
                return "OK";
            if (CodCUM == "1")
                return "OK";


            long adapterId = 1;
            var sql = new StringBuilder();
            sql.Append("SELECT TOP 1 Code  ");
            sql.Append("FROM Cum WITH (NOLOCK)");
            sql.Append("WHERE Code = '" + CodCUM + "'");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (resultExecute.IsSuccessful)
            {
                var listTypeDetails = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());
                if (listTypeDetails.Any())
                {
                    return "OK";
                }
                else { return $"Error en el cum registrado.Validar la variable {PositionVariable} Actualmente recibe medicamento NO POS({NameVariable})"; }


            }
            else
            {

                return resultExecute.Result.ToString();

            }











        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="listErrors">Variable de listErrorsde función vacía</param>
        /// <param name="index">index</param>
        /// <param name="ent">ent</param>
        public static dynamic USR_ValidateCountLinfocitos(List<string> listErrors, long index, ENT_StructureRes4725 ent)
        {
            //Valida linfocitos TCD4 0 al 99005 campo 28
            if (Convert.ToInt32(ent.ConteoLinfocitosTCD4Diagnostico) > 99005)
            {
                listErrors.Add(string.Concat("El conteo linfocitos TCD4 (campo 28) solo permite entre 1 y 99005", $" Línea {index + 1}"));
            }

            //Valida linfocitos totales 1 al 99005 campo 29
            if (Convert.ToInt32(ent.ConteoLinfocitosTotalesMomentoDiagnostico) < 1 || Convert.ToInt32(ent.ConteoLinfocitosTotalesMomentoDiagnostico) > 99005)
            {
                listErrors.Add(string.Concat("El conteo linfocitos totales (campo 29) solo permite entre 1 y 99005", $" Línea {index + 1}"));
            }

            //Valida ConteoLinfocitosTCD4MomentoInicioTAR 0 al 99005 campo 31
            if (Convert.ToInt32(ent.ConteoLinfocitosTCD4MomentoInicioTAR) > 99005)
            {
                listErrors.Add(string.Concat("El conteo linfocitos totales (campo 31) solo permite entre 0 y 99005", $" Línea {index + 1}"));
            }

            //Valida ConteoLinfocitosTotalesInicioTar 1 al 99005 campo 32
            if (Convert.ToInt32(ent.ConteoLinfocitosTotalesInicioTar) < 1 || Convert.ToInt32(ent.ConteoLinfocitosTotalesInicioTar) > 99005)
            {
                listErrors.Add(string.Concat("El conteo linfocitos totales inicio tar (campo 32) solo permite entre 1 y 99005", $" Línea {index + 1}"));
            }
            //Valida UltimoConteoLinfocitos
            if (Convert.ToInt32(ent.UltimoConteoLinfocitos) > 9000)
            {
                if (!Convert.ToInt32(ent.UltimoConteoLinfocitos).Equals(9999))
                {
                    listErrors.Add(string.Concat("El conteo ultimoConteolinfocitos (campo 78) solo permite entre 0 y 9000 o 9999", $" Línea {index + 1}"));
                }
            }
            //Valida ultimo conteo de linfocitos totales
            if (Convert.ToInt32(ent.ValorUltimoConteoLinfocitos) > 5000)
            {
                if (!Convert.ToInt32(ent.ValorUltimoConteoLinfocitos).Equals(9999))
                {
                    listErrors.Add(string.Concat("El conteo ValorUltimoConteoLinfocitos (campo 76) solo permite entre 0 y 5000 o 9999", $" Línea {index + 1}"));
                    return false;
                }
            }
            return true;
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
        /// ValidateAffiliatePerson
        /// </sumary> 
        /// <param name="personList">personList</param>
        /// <param name="adapterId">adapterId</param>
        /// <param name="listErrors">listErrors</param>
        public static List<string> USR_ValidateAffiliatePersonResolutions(List<dynamic> personList, long adapterId, List<string> listErrors)
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
                    listErrors.Add($"La persona identificada con tipo de documento {item.typeId} Número {item.Identification} registrada el item {item.Index} no existe");
                }
            }

            return listErrors;
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="date">Variable de parámetro de función vacía</param>
        public static string USR_Validate21(string date)
        {
            if (date.Length != 7)
            {
                return "Debe ingresar una fecha valida, verifique que el dato tenga los siguientes datos: año-mes.Verificar variable 21,Fecha de la prueba presuntiva(Elisa) para infeccion por VIH";
            }

            char[] caracteres = date.ToCharArray();
            if (caracteres[4].ToString() != "-")
                return "Debe ingresar una fecha valida, verifique que el dato tenga los siguientes datos: año-mes.Verificar variable 21,Fecha de la prueba presuntiva(Elisa) para infeccion por VIH";


            string mes = "";
            mes = caracteres[5].ToString() + caracteres[6].ToString();
            int month = Int32.Parse(mes);

            if (date == "0000-00" || date == "1111-11" || date == "2222-22" || date == "9999-99" || month == 99)
                return "OK";


            if (month > 12 && month != 99)
                return "Debe ingresar una fecha valida,verifique que si no conoce el mes registre 99.Verificar variable 21 Fecha de la prueba presuntiva(Elisa) para infeccion por VIH";




            return "OK";
        }
        /// <sumary>
        /// Validacion variable 55
        /// </sumary> 
        /// <param name="code">code municipio variable 55</param>
        public static string USR_Validate_56(string code)
        {
            if (code == "9")
                return "OK";

            int longitud = code.Length;
            if (longitud != 5)
                return "Debe introducir un codigo correcto,verifique el tamaño del codigo y si no posee codigo registre 9.Validar la variable 56 Municipio de la IPS";

            return USR_ValidateMunicipality(code);

        }
        /// <sumary>
        /// Valida los códigos del régimen permitidos para la regla V2 Res.4725
        /// </sumary> 
        /// <param name="CodeReg">Código de régimen permitido Res 4725</param>
        public static bool USR_V2REGIMEN(double CodeReg)
        {
            if (CodeReg < 1 || CodeReg > 4)
                return false;
            return true;
        }
        /// <sumary>
        /// Se valida la existencia del código de la EPS en la tabla operador
        /// </sumary> 
        /// <param name="CodeEPS">Es el código de la EPS a la que se relaciona el diagnostico</param>
        public static string USR_V1COD_EPS(string CodeEPS)
        {
            int adapterId = 1;
            var sql = new StringBuilder();

            sql.Append(" SELECT TOP 1 * ");
            sql.Append(" FROM Operator WITH(NOLOCK)");
            sql.AppendFormat(" WHERE QualificationCode = '{0}'", CodeEPS);

            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (resultExecute.IsError)
            {
                return "Hubo un error en la ejecucion";
            }
            var Operators = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());

            var functions = Operators.Any(f => f.QualificationCode == CodeEPS);

            if (functions)
            {
                return "ok";
            }
            return "Codigo no se encuentra parametrizado.Validar la variable 1 'EPS'";
        }
        /// <sumary>
        /// Guarda la cabecera y el detalle de los archivos de la resolucion
        /// </sumary> 
        /// <param name="cutOffDate">Fecha de corte de la información reportada, corresponde al último día calendario del periodo </param>
        /// <param name="code">Código de la EPS o de la Dirección Territorial de Salud</param>
        /// <param name="idOperator">id del operador de la ips</param>
        /// <param name="caseNumber">numero del caso del proceso</param>
        /// <param name="InitialDate">fecha inicial</param>
        /// <param name="endDate">fecha final del periodo</param>
        /// <param name="idPopulation">tipo de poblacion</param>
        /// <param name="listFileEntity">registro del archivo a guardar</param>
        public static ENT_ActionResult USR_Save4725File(string cutOffDate, string code, long idOperator, string caseNumber, string InitialDate, string endDate, long idPopulation, dynamic listFileEntity)
        {
            //Typedetail Tipo de documento
            Dictionary<string, string> _dictionaryDocumentType = new Dictionary<string, string>();
            _dictionaryDocumentType.Add("TI", "3");
            _dictionaryDocumentType.Add("CC", "1");
            _dictionaryDocumentType.Add("CE", "6");
            _dictionaryDocumentType.Add("PA", "7");
            _dictionaryDocumentType.Add("RC", "2");
            _dictionaryDocumentType.Add("SC", "4");
            _dictionaryDocumentType.Add("CD", "9");
            _dictionaryDocumentType.Add("RNV", "1423");
            _dictionaryDocumentType.Add("MS", "1424");
            _dictionaryDocumentType.Add("AS", "2825");
            _dictionaryDocumentType.Add("UN", "3271");

            //Typedetail Id Sexo
            Dictionary<string, string> _dictionarySexType = new Dictionary<string, string>();
            _dictionarySexType.Add("F", "71");
            _dictionarySexType.Add("M", "72");
            _dictionarySexType.Add("O", "3251");


            List<ENT_StructureRes4725En> loadAddMasssives = new List<ENT_StructureRes4725En>();

            foreach (var item in listFileEntity)
            {
                ENT_StructureRes4725En Lista = new ENT_StructureRes4725En
                {
                    CodeEps = item.CodigoEPS,
                    RegimeType = item.Regimen,
                    PopulationGroup = item.GrupoPoblacional,
                    FirstName = item.PrimerNombre,
                    SecondName = item.SegundoNombre,
                    FirstLastName = item.PrimerApellido,
                    SecondLastName = item.SegundoApellido,
                    IdentificationType = _dictionaryDocumentType[item.TipoIdentificacion],
                    DocumentNumber = item.NumeroDocumento,
                    BirthDate = item.FechaNacimiento,
                    IdSex = _dictionarySexType[item.Sexo],
                    CodeEthnic = item.PertenenciaEtnica,
                    ResidenceAddress = item.DireccionResidencia,
                    TelephoneContact = item.TelefonoContacto,
                    MunicipalityResidence = item.CodigoMunicipioRes,
                    AffiliationDate = item.FechaAfiliacionEPS,
                    PregnantPerson = item.PersonaGestante,
                    TuberculosisPerson = item.PersonaConTuberculosis,
                    PersonUnder18MonthMotherhiv = item.PersonaMenor18MesesVIH,
                    ConditionDiagnosishivInfection = item.CondicionConRespectoDiagnosticoVIH,
                    DateElisaTest = item.FechaPruebaPresuntivaElisa,
                    DateDiagnosishiv = item.FechaDiagnosticoInfeccionVIH,
                    HowCamePresumtiveTest = item.PruebaPresuntiva,
                    AssuranceTimeDiagnosis = item.AseguramientoMomentoDelDiagnostico,
                    LastCodeEps = item.EntidadTerritorialAnterior,
                    TransmissionMechanism = item.MecanismoTransmision,
                    ClinicalStageDiagnosiSolder13 = item.EstadioClinicoMomentoDelDiagnostico,
                    ClinicalStageDiagnosisUnder13 = item.EstadioClinicoMomentoDelDiagnosticoAdolecentes,
                    DiagnosisTcd4lymphocyteCount = item.ConteoLinfocitosTCD4Diagnostico,
                    DiagnosisTotallymphocyteCount = item.ConteoLinfocitosTotalesMomentoDiagnostico,
                    StartDateTar = item.FechaInicioTAR,
                    Tcd4LymphocyteCountBeginningTar = item.ConteoLinfocitosTCD4MomentoInicioTAR,
                    TotalLymphocyteCountBeginningTar = item.ConteoLinfocitosTotalesInicioTar,
                    ViralLoadBeginningTar = item.CargaViralInicioTAR,
                    ReasonBeginningTar = item.MotivoInicioTAR,
                    HadanemiaBeginningTar = item.TeniaAnemiaAlIniciar,
                    ChronickIdNeydiseaseBeginningTar = item.EnfermedadRenalCronicaInicioTAR,
                    CoInfectionvhbBeginningTar = item.CoinfeccionVHBIniciarTAR,
                    CoInfectionvhcBeginningTar = item.CoinfeccionVHCIniciarTAR,
                    TuberculosisBeginningTar = item.TuberculosisInicioTAR,
                    CardiovascularSurgeryBeginningTar = item.CirugiaCardiovascularInicioTAR,
                    KaposisarcomaBeginningTar = item.SarcomaKaposiInicioTAR,
                    PregnantBeginningTar = item.EmbarazoInicioTAR,
                    PsychiatricillnessBeginningTar = item.EnfermedadPsiquiatricaInicioTAR,
                    AbacavirBeginningTar = item.InicioAbacavirTAR,
                    AtazanavirBeginningTar = item.InicioAtazanavirTAR,
                    DidanosinaBeginningTar = item.InicioDidanosinaTAR,
                    EfavirenzBeginningTar = item.InicioEfavirenzTAR,
                    EstavudinaBeginningTar = item.InicioEstavudinaTAR,
                    FosamprenavirBeginningTar = item.InicioFosamprenavirTAR,
                    IndinavirBeginningTar = item.InicioIndinavirTAR,
                    LamivudinaBeginningTar = item.InicioLamivudinaTAR,
                    LopinavirBeginningTar = item.InicioLopinavirTAR,
                    NevirapinaBeginningTar = item.InicioNevirapinaTAR,
                    NelfinavirBeginningTar = item.InicioNelfinavirTAR,
                    RitonavirBeginningTar = item.InicioRitonavirTAR,
                    SaquinavirBeginningTar = item.InicioSaquinavirTAR,
                    ZidovudinaBeginningTar = item.InicioZidovudinaTAR,
                    TenofovirBeginningTar = item.InicioTenofovirTARMed1,
                    EmtricitabinaBeginningTar = item.InicioEmtricitabinaTARMed2,
                    NonposMedication3BeginningTar = item.InicioMedicamento3NoposTAR,
                    NonposMedication4BeginningTar = item.InicioMedicamento4NoposTAR,
                    NonposMedication5BeginningTar = item.InicioMedicamento5NoposTAR,
                    NonposMedication6BeginningTar = item.InicioMedicamento5NoposTAR,
                    ReceiveDadviceBeginningTar = item.RecibioAsesoriaInicioTAR,
                    MonthTarFormulaDispensed = item.NumeroMesesFormulaTAR,
                    MedicalapPointMentSattended = item.NumeroCitasMedicasPrimeros12Meses,
                    InitialTarMedicationsChanged = item.MedicamentosCambiadosIncioTAR,
                    DateFirstChangeMedicationInitialTar = item.FechaPrimerCambioMedInicioTAR,
                    CauseChangeMedicationInitialTar = item.CausaCambioMedicamentoInicioTAR,
                    NumberFailuressTartTar = item.NumerosFallasInicioTAR,
                    NumberTarMedicationChangesAllCauses = item.NumeroCambiosMedCausaActual,
                    Candiesotraqbronqpul = item.CandidiasisPulmonar,
                    PulmonaryTuberculosis = item.TuberculosisExtraPulmonar,
                    InvasiveCervicalCancer = item.CancerCervixInvasivo,
                    DementiaAssociatedvih = item.DemensiaAsociadaVIH,
                    ExtrapulmonaryCoccidioidomycosis = item.CoccidioidomIcosisExtraPulmonar,
                    CmvInfection = item.InfeccionCitomegalovirus,
                    SimpleHerpes = item.HerpesSimple,
                    DiarrheaIsospora = item.DiarreaIsospora,
                    ExtrapulmonaryHistoplasmosis = item.HistoplasmosisExtrapulmonar,
                    Burkittlymphoma = item.BurkittPrimario,
                    PneumoniapNeumocystiscarinii = item.NeumoniaPneumocystiscarinii,
                    RecurrentBacterialpNeumonia = item.NeumoniaRecurrenteBacteriana,
                    SalmonellaSepticemia = item.SalmonelaSepticemia,
                    DisseminatedInfectionmacmai = item.InfeccionDiseminadaMicobacte,
                    CriptococosiSexTrapulmonar = item.CriptococosiSexTrapulmonar,
                    Kaposisarcoma = item.Kaposisarcoma,
                    Wearsyndrome = item.SindromeCaquexiaVIH,
                    Leukoencefelopatia = item.Leucoencefelopatia,
                    LymphoidinterstitialpNeumonia = item.NeumoniaIntesticialLinfoidea,
                    CerebralToxoplasmosis = item.ToxoplasmosisCerebral,
                    HabilitationCodePresent = item.CodigoHabilitacion,
                    DateEntryCurrentIps = item.FechaIngresoIPS,
                    MunicipalityResidenceIPS = item.MunicipioIPS,
                    ClinicalCareandFormulation = item.AtencionClinicaPacienteVIH,
                    EvaluationInfectologistLast6Months = item.ValoracionInfectologo6Meses,
                    GenotypingResult = item.ResultadoGenotipificacion,
                    GenotypingMoment = item.MomentoGenotipificacion,
                    CurrentClinicalSituation = item.SituacionClinicaActual,
                    CurrentClinicalStage = item.EstadioClinicoActual,
                    Dyslipidemia = item.Dislipidemia,
                    PeripheralNeuropathy = item.NeuropatiaPeriferica,
                    Lipoatrophy = item.Lipoafrofia,
                    CoInfectionvhb = item.CoInfectionvhb,
                    CoInfectionvhc = item.CoInfectionvhc,
                    Anemia = item.Anemia,
                    Livercirrhosis = item.CirrosisHepatica,
                    ChronickIdnEydisease = item.EnfermedadRenalCronica,
                    CoronaryDisease = item.EnfermedadCoronaria,
                    SexuallyTransmittedInfections = item.InfeccionTransmisionSexual12Meses,
                    NeoplasiaNotrelatedSida = item.NeoplastiaSida,
                    FunctionalDisability = item.DiscapacidadFuncional,
                    Datelasttcd4lymphocyteCount = item.FechaUltimoConteoLinfocitosCD4,
                    Lasttcd4lymphocyteCount = item.ValorUltimoConteoLinfocitos,
                    DatelastTotallymphocyteCount = item.FechaUlitmoConteoLinfocitos,
                    LastTotallymphocyteCount = item.UltimoConteoLinfocitos,
                    LastDateViralLoad = item.FechaUltimaCargaViralReportada,
                    ResultLastViralLoad = item.ResultadoUltimaCargaViral,
                    SupplyCondomsLast3Months = item.SuministroCondones3Meses,
                    NcondomsSuppliedLast3Months = item.NumeroCondones3Meses,
                    FamilyPlanningMethod = item.MetodoPlanificacionFamiliar,
                    Hepatitisbvaccine = item.VacunaHepatitisB,
                    Ppdlast12Month = item.PPDUltimos12Meses,
                    ViralloadChildrenunder18Months = item.EstudioCargarViralMenores18Meses,
                    SecondViralLoadChildrenUnder18Months = item.EstudioSegundaCargaViralMenores18,
                    NumberViralLoadChildrenUnder18Months = item.NumeroCargasViralesMenor18Meses,
                    SupplymilkFormula = item.SuministroFormulaLactea,
                    ReceiveTar = item.RecibeTar,
                    StartDateCurrentTar = item.FechaInicioMedimamentosTAR,
                    CurrentAbacavir = item.RecibeAbacavir,
                    CurrentAtazanavir = item.RecibeAtazanavir,
                    CurrentDidanosina = item.RecibeDidanosina,
                    CurrentEfavirenz = item.RecibeEfavirenz,
                    CurrentEstavudina = item.RecibeEstavudina,
                    CurrentFosamprenavir = item.RecibeFosamprenavir,
                    CurrentIndinavir = item.RecibeIndinavir,
                    CurrentLamivudina = item.RecibeLamivudina,
                    CurrentLopinavir = item.RecibeLopinavir,
                    CurrentNevirapina = item.RecibeNevirapina,
                    CurrentNelfinavir = item.RecibeNelfinavir,
                    CurrentRitonavir = item.RecibeRitonavir,
                    CurrentSaquinavir = item.RecibeSaquinavir,
                    CurrentZidovudina = item.RecibeZidovudina,
                    CurrentTenofovir = item.RecibeTenofovir,
                    CurrentEmtricita81na = item.RecibeEmtricita81na,
                    CurrentNonposMedication3 = item.RecibeNoposMedication3,
                    CurrentNonposMedication4 = item.RecibeNoposMedication4,
                    CurrentNonposMedication5 = item.RecibeNoposMedication5,
                    CurrentNonposMedication6 = item.RecibeNoposMedication6,
                    Newbornarvprophylaxis = item.ProfilaxisRecienNacidoExpuestoVIH,
                    ProphyLaxismac = item.ProfilaxisMAC,
                    ProphyLaxisfluconazol = item.ProfilaxisFluconazol,
                    ProphyLaxistrimethoprimsulfa = item.ProfilaxisTrimetoprimSulfa,
                    ProphyLaxisimmunoglobuliniv = item.ProfilaxisInmunoglobinaIV,
                    ProphyLaxisisonic = item.ProfilaxisIsoniacida,
                    AntituberculosisTreatment = item.TratamientoAntituberculosis,
                    StartDateCuurentTuberculosisTreatment = item.FechaInicioTratamientoAntituberculoso,
                    CurrentAmikacina = item.RecibeAmikacina,
                    CurrentCiprofloxacina = item.RecibeCiprofloxacina,
                    CurrentEstreptomicina = item.RecibeEstreptomicina,
                    CurrentEthambutol = item.RecibeEthambutol,
                    CurrentEthionamida = item.RecibeEthionamida,
                    CurrentIsoniacida = item.RecibeIsoniacida,
                    CurrentPirazinamida = item.RecibePirazinamida,
                    CurrentRifampicina = item.RecibeRifampicina,
                    CurrentRifabutina = item.RecibeRifabutina,
                    CurrentNonposMedication1tuber = item.RecibeMedicamentoAntibuberculoso1,
                    CurrentNonposMedication2tuber = item.RecibeMedicamentoAntibuberculoso1,
                    CurrentNonposMedication3tuber = item.RecibeMedicamentoAntibuberculoso3,
                    CurrentNonposMedication4tuber = item.RecibeMedicamentoAntibuberculoso4,
                    CurrentNonposMedication5tuber = item.RecibeMedicamentoAntibuberculoso4,
                    SerologysyphilisFirsttrimester = item.ResultadoSerologiaSifilisPrimerTrimestre,
                    SerologysyphilisSecondtrimester = item.ResultadoSerologiaSifilisSegundoTrimestre,
                    SerologysyphilisThirdtrimester = item.ResultadoSerologiaSifilisTercerTrimestre,
                    SerologyBirthTimeAbortion = item.ResultadoSerologiaPartoAborto,
                    DateFirstTreatmentSyphilis = item.FechaPrimerTratamientoSifilis,
                    DateSecondTreatmentSyphilis = item.FechaSegundoTratamientoSifilis,
                    DateThirdTreatmentSyphilis = item.FechaTercerTratamientoSifilis,
                    NoveltyPreviousReport = item.NovedadPacienteReporte,
                    Disapfiliationdateeps = item.FechaDesafiliacionEPS,
                    Habilitationcode = item.EntidadTrasladoPacienteVIH,
                    DeathDate = item.FechaMuerte,
                    DeathCause = item.CausaMuerte
                };

                loadAddMasssives.Add(Lista);
            };

            dynamic head = new
            {
                CutOffDate = cutOffDate,
                Code = code,
                ProcessDate = DateTime.Now,
                IdOperator = idOperator,
                CaseNumber = caseNumber,
                InitialDate,
                EndDate = endDate,
                IdTypePopulation = idPopulation,
                ListResolutionDetail = loadAddMasssives
            };

            string url = "http://davincilb.ophelia.co:8085/ASSURANCE/api/Resolution4725/SaveFileHeadDetail";
            ENT_ActionResult result = SYS_WSPOST(url, head, null, null);
            return result;

        }
        /// <sumary>
        /// funcion de validacion de citas medicas
        /// </sumary> 
        /// <param name="Medicals">cantidad de citas medicas</param>
        public static string USR_Medical_Appoinment(long Medicals)
        {
            if ((Medicals == 98 || Medicals == 99))
            {
                return "OK";

            }


            if (Medicals >= 0 && Medicals <= 50)
            {
                return "OK";
            }

            if (Medicals < 0 || Medicals > 50)
            {
                return "Debe ingresar un dato que se encuentre dentro del rango de acuerdo al numero de citas medicas asistidas durante los primeros 12 meses.Validar la variable 47 Número de citas médicas a las que asistió durante los primeros 12 meses luego de iniciar TAR";
            }

            return "Debe ingresar un dato valido,debe verificar que el numero registrado sea 98 o 99.Validar la variable 47 Número de citas médicas a las que asistió durante los primeros 12 meses luego de iniciar TAR";


        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="Parameters4725">parámetros enviados desde el motor para validar resolución </param>
        public static dynamic USR_Main4725(ENT_parameters4725 Parameters4725)
        {
            #region Validate          
            if (Parameters4725 == null) throw new ArgumentException($"La entidad no puede ser vacía");

            if (Parameters4725.LibraryId == 0) throw new ArgumentException($"El Id de la Libreria no puede estar vacío");

            if (Parameters4725.CompanyId == 0) throw new ArgumentException($"El Id de la Compañia no puede estar vacío");

            if (string.IsNullOrWhiteSpace(Parameters4725.CaseNumber)) throw new ArgumentException($"El numero de Caso no puede estar vacío");

            if (string.IsNullOrWhiteSpace(Parameters4725.UserCode)) throw new ArgumentException($"El Codigo del usuario no puede estar vacío");

            if (string.IsNullOrWhiteSpace(Parameters4725.FileId)) throw new ArgumentException($"El Id del Archivo no puede estar vacío");


            #endregion

            #region Constante
            const string lineSeparator = "\r\n";
            const string columnSeparator = "\t";
            const int ColumnLength = 184;
            const string folder = "Resolucion4725ResultEstructura";

            #endregion

            #region Validate struct

            var listErrors = new List<string>();

            var resultValidation = USR_ValidateRule4725(lineSeparator, columnSeparator, Parameters4725.CompanyId, Parameters4725.LibraryId, Parameters4725.FileId, ColumnLength, typeof(ENT_StructureRes4725), listErrors);

            if (listErrors.Count > 0)
            {
                string pathFile = USR_GenericSaveLog(new Dictionary<string, List<string>>() { ["4725"] = listErrors }, folder);
                var attach = USR_WSAttachFileToProcess(pathFile, Parameters4725.UserCode, Parameters4725.CompanyId.ToString(), Parameters4725.CaseNumber, "4725");
                if (attach.IsError)
                {
                    attach.ErrorMessage = "No se pudo asociar el archivo al proceso, ya que el archivo TXT no fue encontrado con los datos suministrados, favor verificar si se cargo en la plantilla de manera correcta.";
                    return attach;
                }
                return new ENT_ActionResult() { FileName = attach.FileName, IsError = true, ErrorMessage = "Hubo errores en la validación " };
            }

            #endregion
            return new ENT_ActionResult() { IsSuccessful = true, Result = resultValidation };




        }
        /// <sumary>
        /// Diagnostico de Infeccion
        /// </sumary> 
        /// <param name="date">fecha diagnostico de infeccion</param>
        public static string USR_InfectionDiagnost(string date)
        {
            if (date.Length != 7)
            {
                return "Error en la fecha,debe validar que la fecha venga de la siguiente forma: año-mes.Validar la variable 22 Fecha de diagnostico de infeccion por VIH(prueba confirmatoria)";
            }

            char[] caracteres = date.ToCharArray();
            if (caracteres[4].ToString() != "-")
                return "Error en la fecha,debe validar que la fecha venga de la siguiente forma: año-mes.Validar la variable 22 Fecha de diagnostico de infeccion por VIH(prueba confirmatoria)";


            string mes = "";
            mes = caracteres[5].ToString() + caracteres[6].ToString();
            int month = Int32.Parse(mes);

            if (date == "0000-00" || date == "9999-99" || month == 99)
                return "OK";


            if (month > 12 && month != 99)
                return "Debe ingresar una fecha valida,si solo sabe el año en el mes debe digitar 99.Validar la variable 22 Fecha de diagnostico de infeccion por VIH(prueba confirmatoria) ";




            return "OK";
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
        /// Obtiene persona por tipo de documento
        /// </sumary> 
        /// <param name="adapterId">adapterId</param>
        /// <param name="documentsNumbers">documentsNumbers</param>
        public static ENT_ActionResult USR_GetPersonByDocumentNumber(long adapterId, dynamic documentsNumbers)
        {
            StringBuilder sbPersonByNumber = new StringBuilder();
            sbPersonByNumber.Append(" DECLARE @x xml; ");
            sbPersonByNumber.Append(" SET @x = '<root><ids> ");
            foreach (var documentNumber in documentsNumbers)
                sbPersonByNumber.Append($"<id>{documentNumber.Identification}</id>");
            sbPersonByNumber.Append(" </ids></root> ';");
            sbPersonByNumber.Append(" SELECT  DISTINCT IdDocumentType, DocumentNumber FROM Person WHERE DocumentNumber IN(select T.X.value('(text())[1]', 'varchar(15)') as id from @X.nodes('/root/ids/id') as T(X)); ");

            var resultExecute = SYS_WSExecuteQuery(adapterId, sbPersonByNumber.ToString());
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
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="period">periodo en el que se desea hacer el cargue </param>
        /// <param name="year">año </param>
        public static dynamic USR_GetDatePeriod(long period, long year)
        {
            if ((period == 1 && DateTime.Now.Year - 1 != year) || (period == 2 && DateTime.Now.Year != year))
                return new ENT_ActionResult() { IsSuccessful = false, ErrorMessage = "El periodo a reportar no es valido con respecto al año " };

            DateTime InitPeriod = new DateTime(), EndPeriod = new DateTime();
            int initMonth = 0;
            int endMonth = 0;
            int endDay = 0;

            switch (period)
            {
                case 1:
                    initMonth = 1;
                    endMonth = 7;
                    endDay = 30;
                    break;
                case 2:
                    initMonth = 4;
                    endMonth = 6;
                    endDay = 30;
                    break;
            }
            DateTime date = new DateTime();
            if (DateTime.TryParse($"{1}-{initMonth}-{year}", CultureInfo.CreateSpecificCulture("es-CO"), DateTimeStyles.None, out date))
            {
                InitPeriod = date;
            }
            else
            {
                return new ENT_ActionResult() { IsSuccessful = false, ErrorMessage = "para el periodo reportado El formato de la fecha  no es valido " };
            }

            date = new DateTime();
            if (DateTime.TryParse($"{endDay}-{endMonth}-{year}", CultureInfo.CreateSpecificCulture("es-CO"), DateTimeStyles.None, out date))
            {
                EndPeriod = date;
            }
            else
            {
                return new ENT_ActionResult() { IsSuccessful = false, ErrorMessage = "para el periodo reportado El formato de la fecha  no es valido " };
            }

            dynamic dates = new
            {
                InitPeriod,
                EndPeriod
            };
            return new ENT_ActionResult() { IsSuccessful = true, Result = dates };
        }
        /// <sumary>
        /// Guarda log de un archivo 
        /// </sumary> 
        /// <param name="dictionaryResult">Dictionary con entidades a escribir en el log</param>
        /// <param name="folder">carpeta donde se va a guardar el archivo</param>
        public static string USR_GenericSaveLog(dynamic dictionaryResult, string folder)
        {
            try
            {

                string pathName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), folder);

                if (!Directory.Exists(pathName))
                    Directory.CreateDirectory(pathName);

                pathName = Path.Combine(pathName, $"{folder}{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv");

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
        /// función que valida un código existente en la tabla de CUM
        /// </sumary> 
        /// <param name="CodCUM">CodigoCUM</param>
        /// <param name="PositionVariable">Posicion de la variable en la resolución</param>
        /// <param name="NameVariable">Nombre variable</param>
        public static string USR_GenerateCUM(string CodCUM, string PositionVariable, string NameVariable)
        {

            if (CodCUM == "0")
                return "OK";
            if (CodCUM == "1")
                return "OK";


            long adapterId = 1;
            var sql = new StringBuilder();
            sql.Append("SELECT TOP 1 Code  ");
            sql.Append("FROM Cum WITH (NOLOCK)");
            sql.Append("WHERE Code = '" + CodCUM + "'");
            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (resultExecute.IsSuccessful)
            {
                var listTypeDetails = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());
                if (listTypeDetails.Any())
                {
                    return "OK";
                }
                else { return $"Error en el codigo,Debe verificar si el codigo CUM sea el correcto.Validar la variable {PositionVariable} En la TAR inicial recibió Medicamento NO POS({NameVariable})"; }


            }
            else
            {

                return resultExecute.Result.ToString();

            }











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
                string ftp = "davincilb.ophelia.co";
                string userName = "OpheliaDcom";
                string password = "iCNw7vyq6O";

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
        /// LinfocitosConteo
        /// </sumary> 
        /// <param name="Number">Conteo linfocitos</param>
        public static string USR_F_v78_LastValueLymphocyte(long Number)
        {
            if (Number == 9999 && Number == 0000 && Number == 99999)
                return "OK";

            if (Number >= 0 && Number <= 9000)
                return "OK";

            if (Number != 9999 || Number != 000)
                return "Debe introducir un valor correcto, debe validar que el dato ingresado sea 9999,000.Validar la variable 78 Valor del último conteo de linfocitos totales";

            if (Number != 99999)
                return "Debe introducir un valor correcto, debe validar que el dato ingresado sea 99999.Validar la variable 78 Valor del último conteo de linfocitos totales";


            return "Debe introducir un valor con respecto al rango teniendo en cuenta el numero de linfocitos totales del ultimo conteo.Validar la variable 78 Valor del último conteo de linfocitos totales";
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="date">0000-00</param>
        public static string USR_DateValidate(string date)
        {
            if (date.Length != 7)
                return "Debe ingresar una fecha valida, verifique que el dato tenga los siguientes datos: año-mes.Verificar variable 94,Fecha de inicio del tratamiento antituberculoso que recibe actualmente";

            char[] caracteres = date.ToCharArray();
            if (caracteres[4].ToString() != "-")
                return "Debe ingresar una fecha valida, verifique que el dato tenga los siguientes datos: año-mes.Verificar variable 94,Fecha de inicio del tratamiento antituberculoso que recibe actualmente";

            string mes = "";
            mes = caracteres[5].ToString() + caracteres[6].ToString();
            int month = Int32.Parse(mes);

            if (date == "0000-00")
                return "OK";

            if (month < 1 || month > 12)
                return "Debe ingresar una fecha valida,verifique que si no conoce el mes registre 99.Verificar variable 94 Fecha de inicio del tratamiento antituberculoso que recibe actualmente";

            return "OK";
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
                string url = "http://davincilb.ophelia.co:8080/api/api/Adapter/ExecuteQuery";
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
        /// Prototipo de verificación
        /// </sumary> 
        /// <param name="rules">Reglas a evaluar</param>
        public static object SYS_VerificationPrototype(object rules)
        {
            IEnumerable<Func<object>> funcs = ((IEnumerable<Func<object>>)rules);

            string messages = string.Empty;
            bool result = true;
            foreach (Func<object> rul in funcs)
            {
                var r = rul();
                if (!r.GetPropertyValue<bool>("IsValid"))
                {
                    result = false;
                    messages += "* " + r.GetPropertyValue<string>("Message") + "\n\r";
                }
            }

            return new { Result = result, Messages = messages };
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

    /// <summary>
    /// Encapsula el resultado de la petición
    /// </summary>
    /// <typeparam name="T">Tipo del resultado</typeparam>
    public sealed class RuntimeResult<T>
    {
        #region Properties
        /// <summary>
        /// Resultado válido
        /// </summary>
        public bool IsValid { get; private set; }
        /// <summary>
        /// Mensaje de resultado
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Nombre de archivo
        /// </summary>
        public string FileName { get; private set; }
        /// <summary>
        /// Obtiene el objeto resultado de la petición
        /// </summary>
        public T Result { get; private set; }
        #endregion

        #region Builders
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// </summary>
        /// <param name="isSuccessful">Valor que indica si el resultado de la petición fue satisfactorio</param>
        /// <param name="isError">Valor que indica si ocurrió algún error en la ejecución</param>
        /// <param name="errorMessage">Mensaje del error ocurrido</param>
        /// <param name="isValid">Define si el resultado de ejecución es válido</param>
        /// <param name="message">Enumeración de mensajes de validación que no permitieron que el resultado fuera satisfactorio</param>
        /// <param name="result">Objeto resultado de la petición</param>
        /// <param name="fileName">Archivo resultado</param>
        internal RuntimeResult(bool isValid, string message, T result, string fileName = null)
        {
            IsValid = isValid;
            Message = message;
            FileName = fileName;
            Result = result;
        }
        #endregion

        #region Factories
        /// <summary>
        /// Crea un resultado de ejecución existoso, con resultado válido
        /// </summary>
        /// <param name="result">Objeto resultado</param>
        /// <param name="message">Mensaje resultado</param>
        /// <returns>Resultado de la petición</returns>
        public static RuntimeResult<T> SetValid(T result, string message) => new RuntimeResult<T>(true, message, result);

        /// <summary>
        /// Crea un resultado de ejecución existoso, con resultado inválido
        /// </summary>
        /// <param name="result">Objeto resultado</param>
        /// /// <param name="message">Mensaje resultado</param>
        /// <returns>Resultado de la petición</returns>
        public static RuntimeResult<T> SetValid(Func<T> expression, string message) => SetValid(expression(), message);

        /// <summary>
        /// Crea un resultado de ejecución existoso, con resultado inválido
        /// </summary>
        /// <param name="result">Objeto resultado</param>
        /// <param name="message">Mensaje resultado</param>
        /// <param name="fileName">Archivo resultado</param>
        /// <returns>Resultado de la petición</returns>
        public static RuntimeResult<T> SetInvalid(T result, string message, string fileName = null) => new RuntimeResult<T>(false, message, result, fileName);

        /// <summary>
        /// Crea un resultado de ejecución existoso, con resultado inválido
        /// </summary>
        /// <param name="result">Objeto resultado</param>
        /// <param name="message">Mensaje resultado</param>
        /// <param name="fileName">Archivo resultado</param>
        /// <returns>Resultado de la petición</returns>
        public static RuntimeResult<T> SetInvalid(Func<T> expression, string message, string fileName = null) => SetInvalid(expression(), message, fileName);

        /// <summary>
        /// Crea un resultado de petición con error
        /// </summary>
        /// <param name="errorMessage">Mensaje del error ocurrido</param>
        /// <returns>Resultado de la petición</returns>
        public static RuntimeResult<T> SetError(string errorMessage) => new RuntimeResult<T>(false, errorMessage, default(T));
        #endregion
    }

    #region Entities
    /// <sumary>
    /// Entidad que recibe el servicio 4725
    /// </sumary>
    public class ENT_StructureRes4725En : EntityBase
    {
        #region Properties
        /// <sumary>
        /// CodeEps
        /// </sumary>
        private string _CodeEps;
        [Order]
        public string CodeEps { get { return _CodeEps; } set { _CodeEps = ValidateValue<string>(value, nameof(CodeEps)); } }
        /// <sumary>
        /// RegimeType
        /// </sumary>
        private string _RegimeType;
        [Order]
        public string RegimeType { get { return _RegimeType; } set { _RegimeType = ValidateValue<string>(value, nameof(RegimeType)); } }
        /// <sumary>
        /// PopulationGroup
        /// </sumary>
        private string _PopulationGroup;
        [Order]
        public string PopulationGroup { get { return _PopulationGroup; } set { _PopulationGroup = ValidateValue<string>(value, nameof(PopulationGroup)); } }
        /// <sumary>
        /// FirstName
        /// </sumary>
        private string _FirstName;
        [Order]
        public string FirstName { get { return _FirstName; } set { _FirstName = ValidateValue<string>(value, nameof(FirstName)); } }
        /// <sumary>
        /// SecondName
        /// </sumary>
        private string _SecondName;
        [Order]
        public string SecondName { get { return _SecondName; } set { _SecondName = ValidateValue<string>(value, nameof(SecondName)); } }
        /// <sumary>
        /// FirstLastName
        /// </sumary>
        private string _FirstLastName;
        [Order]
        public string FirstLastName { get { return _FirstLastName; } set { _FirstLastName = ValidateValue<string>(value, nameof(FirstLastName)); } }
        /// <sumary>
        /// SecondLastName
        /// </sumary>
        private string _SecondLastName;
        [Order]
        public string SecondLastName { get { return _SecondLastName; } set { _SecondLastName = ValidateValue<string>(value, nameof(SecondLastName)); } }
        /// <sumary>
        /// IdentificationType
        /// </sumary>
        private string _IdentificationType;
        [Order]
        public string IdentificationType { get { return _IdentificationType; } set { _IdentificationType = ValidateValue<string>(value, nameof(IdentificationType)); } }
        /// <sumary>
        /// DocumentNumber
        /// </sumary>
        private string _DocumentNumber;
        [Order]
        public string DocumentNumber { get { return _DocumentNumber; } set { _DocumentNumber = ValidateValue<string>(value, nameof(DocumentNumber)); } }
        /// <sumary>
        /// BirthDate
        /// </sumary>
        private string _BirthDate;
        [Order]
        public string BirthDate { get { return _BirthDate; } set { _BirthDate = ValidateValue<string>(value, nameof(BirthDate)); } }
        /// <sumary>
        /// IdSex
        /// </sumary>
        private string _IdSex;
        [Order]
        public string IdSex { get { return _IdSex; } set { _IdSex = ValidateValue<string>(value, nameof(IdSex)); } }
        /// <sumary>
        /// CodeEthnic
        /// </sumary>
        private string _CodeEthnic;
        [Order]
        public string CodeEthnic { get { return _CodeEthnic; } set { _CodeEthnic = ValidateValue<string>(value, nameof(CodeEthnic)); } }
        /// <sumary>
        /// ResidenceAddress
        /// </sumary>
        private string _ResidenceAddress;
        [Order]
        public string ResidenceAddress { get { return _ResidenceAddress; } set { _ResidenceAddress = ValidateValue<string>(value, nameof(ResidenceAddress)); } }
        /// <sumary>
        /// TelephoneContact
        /// </sumary>
        private string _TelephoneContact;
        [Order]
        public string TelephoneContact { get { return _TelephoneContact; } set { _TelephoneContact = ValidateValue<string>(value, nameof(TelephoneContact)); } }
        /// <sumary>
        /// MunicipalityResidence
        /// </sumary>
        private string _MunicipalityResidence;
        [Order]
        public string MunicipalityResidence { get { return _MunicipalityResidence; } set { _MunicipalityResidence = ValidateValue<string>(value, nameof(MunicipalityResidence)); } }
        /// <sumary>
        /// AffiliationDate
        /// </sumary>
        private string _AffiliationDate;
        [Order]
        public string AffiliationDate { get { return _AffiliationDate; } set { _AffiliationDate = ValidateValue<string>(value, nameof(AffiliationDate)); } }
        /// <sumary>
        /// PregnantPerson
        /// </sumary>
        private string _PregnantPerson;
        [Order]
        public string PregnantPerson { get { return _PregnantPerson; } set { _PregnantPerson = ValidateValue<string>(value, nameof(PregnantPerson)); } }
        /// <sumary>
        /// TuberculosisPerson
        /// </sumary>
        private string _TuberculosisPerson;
        [Order]
        public string TuberculosisPerson { get { return _TuberculosisPerson; } set { _TuberculosisPerson = ValidateValue<string>(value, nameof(TuberculosisPerson)); } }
        /// <sumary>
        /// PersonUnder18MonthMotherhiv
        /// </sumary>
        private string _PersonUnder18MonthMotherhiv;
        [Order]
        public string PersonUnder18MonthMotherhiv { get { return _PersonUnder18MonthMotherhiv; } set { _PersonUnder18MonthMotherhiv = ValidateValue<string>(value, nameof(PersonUnder18MonthMotherhiv)); } }
        /// <sumary>
        /// ConditionDiagnosishivInfection
        /// </sumary>
        private string _ConditionDiagnosishivInfection;
        [Order]
        public string ConditionDiagnosishivInfection { get { return _ConditionDiagnosishivInfection; } set { _ConditionDiagnosishivInfection = ValidateValue<string>(value, nameof(ConditionDiagnosishivInfection)); } }
        /// <sumary>
        /// DateElisaTest
        /// </sumary>
        private string _DateElisaTest;
        [Order]
        public string DateElisaTest { get { return _DateElisaTest; } set { _DateElisaTest = ValidateValue<string>(value, nameof(DateElisaTest)); } }
        /// <sumary>
        /// DateDiagnosishiv
        /// </sumary>
        private string _DateDiagnosishiv;
        [Order]
        public string DateDiagnosishiv { get { return _DateDiagnosishiv; } set { _DateDiagnosishiv = ValidateValue<string>(value, nameof(DateDiagnosishiv)); } }
        /// <sumary>
        /// HowCamePresumtiveTest
        /// </sumary>
        private string _HowCamePresumtiveTest;
        [Order]
        public string HowCamePresumtiveTest { get { return _HowCamePresumtiveTest; } set { _HowCamePresumtiveTest = ValidateValue<string>(value, nameof(HowCamePresumtiveTest)); } }
        /// <sumary>
        /// AssuranceTimeDiagnosis
        /// </sumary>
        private string _AssuranceTimeDiagnosis;
        [Order]
        public string AssuranceTimeDiagnosis { get { return _AssuranceTimeDiagnosis; } set { _AssuranceTimeDiagnosis = ValidateValue<string>(value, nameof(AssuranceTimeDiagnosis)); } }
        /// <sumary>
        /// LastCodeEps
        /// </sumary>
        private string _LastCodeEps;
        [Order]
        public string LastCodeEps { get { return _LastCodeEps; } set { _LastCodeEps = ValidateValue<string>(value, nameof(LastCodeEps)); } }
        /// <sumary>
        /// TransmissionMechanism
        /// </sumary>
        private string _TransmissionMechanism;
        [Order]
        public string TransmissionMechanism { get { return _TransmissionMechanism; } set { _TransmissionMechanism = ValidateValue<string>(value, nameof(TransmissionMechanism)); } }
        /// <sumary>
        /// ClinicalStageDiagnosiSolder13
        /// </sumary>
        private string _ClinicalStageDiagnosiSolder13;
        [Order]
        public string ClinicalStageDiagnosiSolder13 { get { return _ClinicalStageDiagnosiSolder13; } set { _ClinicalStageDiagnosiSolder13 = ValidateValue<string>(value, nameof(ClinicalStageDiagnosiSolder13)); } }
        /// <sumary>
        /// ClinicalStageDiagnosisUnder13
        /// </sumary>
        private string _ClinicalStageDiagnosisUnder13;
        [Order]
        public string ClinicalStageDiagnosisUnder13 { get { return _ClinicalStageDiagnosisUnder13; } set { _ClinicalStageDiagnosisUnder13 = ValidateValue<string>(value, nameof(ClinicalStageDiagnosisUnder13)); } }
        /// <sumary>
        /// DiagnosisTcd4lymphocyteCount
        /// </sumary>
        private string _DiagnosisTcd4lymphocyteCount;
        [Order]
        public string DiagnosisTcd4lymphocyteCount { get { return _DiagnosisTcd4lymphocyteCount; } set { _DiagnosisTcd4lymphocyteCount = ValidateValue<string>(value, nameof(DiagnosisTcd4lymphocyteCount)); } }
        /// <sumary>
        /// DiagnosisTotallymphocyteCount
        /// </sumary>
        private string _DiagnosisTotallymphocyteCount;
        [Order]
        public string DiagnosisTotallymphocyteCount { get { return _DiagnosisTotallymphocyteCount; } set { _DiagnosisTotallymphocyteCount = ValidateValue<string>(value, nameof(DiagnosisTotallymphocyteCount)); } }
        /// <sumary>
        /// StartDateTar
        /// </sumary>
        private string _StartDateTar;
        [Order]
        public string StartDateTar { get { return _StartDateTar; } set { _StartDateTar = ValidateValue<string>(value, nameof(StartDateTar)); } }
        /// <sumary>
        /// Tcd4LymphocyteCountBeginningTar
        /// </sumary>
        private string _Tcd4LymphocyteCountBeginningTar;
        [Order]
        public string Tcd4LymphocyteCountBeginningTar { get { return _Tcd4LymphocyteCountBeginningTar; } set { _Tcd4LymphocyteCountBeginningTar = ValidateValue<string>(value, nameof(Tcd4LymphocyteCountBeginningTar)); } }
        /// <sumary>
        /// TotalLymphocyteCountBeginningTar
        /// </sumary>
        private string _TotalLymphocyteCountBeginningTar;
        [Order]
        public string TotalLymphocyteCountBeginningTar { get { return _TotalLymphocyteCountBeginningTar; } set { _TotalLymphocyteCountBeginningTar = ValidateValue<string>(value, nameof(TotalLymphocyteCountBeginningTar)); } }
        /// <sumary>
        /// ViralLoadBeginningTar
        /// </sumary>
        private string _ViralLoadBeginningTar;
        [Order]
        public string ViralLoadBeginningTar { get { return _ViralLoadBeginningTar; } set { _ViralLoadBeginningTar = ValidateValue<string>(value, nameof(ViralLoadBeginningTar)); } }
        /// <sumary>
        /// ReasonBeginningTar
        /// </sumary>
        private string _ReasonBeginningTar;
        [Order]
        public string ReasonBeginningTar { get { return _ReasonBeginningTar; } set { _ReasonBeginningTar = ValidateValue<string>(value, nameof(ReasonBeginningTar)); } }
        /// <sumary>
        /// HadanemiaBeginningTar
        /// </sumary>
        private string _HadanemiaBeginningTar;
        [Order]
        public string HadanemiaBeginningTar { get { return _HadanemiaBeginningTar; } set { _HadanemiaBeginningTar = ValidateValue<string>(value, nameof(HadanemiaBeginningTar)); } }
        /// <sumary>
        /// ChronickIdNeydiseaseBeginningTar
        /// </sumary>
        private string _ChronickIdNeydiseaseBeginningTar;
        [Order]
        public string ChronickIdNeydiseaseBeginningTar { get { return _ChronickIdNeydiseaseBeginningTar; } set { _ChronickIdNeydiseaseBeginningTar = ValidateValue<string>(value, nameof(ChronickIdNeydiseaseBeginningTar)); } }
        /// <sumary>
        /// CoInfectionvhbBeginningTar
        /// </sumary>
        private string _CoInfectionvhbBeginningTar;
        [Order]
        public string CoInfectionvhbBeginningTar { get { return _CoInfectionvhbBeginningTar; } set { _CoInfectionvhbBeginningTar = ValidateValue<string>(value, nameof(CoInfectionvhbBeginningTar)); } }
        /// <sumary>
        /// CoInfectionvhcBeginningTar
        /// </sumary>
        private string _CoInfectionvhcBeginningTar;
        [Order]
        public string CoInfectionvhcBeginningTar { get { return _CoInfectionvhcBeginningTar; } set { _CoInfectionvhcBeginningTar = ValidateValue<string>(value, nameof(CoInfectionvhcBeginningTar)); } }
        /// <sumary>
        /// TuberculosisBeginningTar
        /// </sumary>
        private string _TuberculosisBeginningTar;
        [Order]
        public string TuberculosisBeginningTar { get { return _TuberculosisBeginningTar; } set { _TuberculosisBeginningTar = ValidateValue<string>(value, nameof(TuberculosisBeginningTar)); } }
        /// <sumary>
        /// CardiovascularSurgeryBeginningTar
        /// </sumary>
        private string _CardiovascularSurgeryBeginningTar;
        [Order]
        public string CardiovascularSurgeryBeginningTar { get { return _CardiovascularSurgeryBeginningTar; } set { _CardiovascularSurgeryBeginningTar = ValidateValue<string>(value, nameof(CardiovascularSurgeryBeginningTar)); } }
        /// <sumary>
        /// KaposisarcomaBeginningTar
        /// </sumary>
        private string _KaposisarcomaBeginningTar;
        [Order]
        public string KaposisarcomaBeginningTar { get { return _KaposisarcomaBeginningTar; } set { _KaposisarcomaBeginningTar = ValidateValue<string>(value, nameof(KaposisarcomaBeginningTar)); } }
        /// <sumary>
        /// PregnantBeginningTar
        /// </sumary>
        private string _PregnantBeginningTar;
        [Order]
        public string PregnantBeginningTar { get { return _PregnantBeginningTar; } set { _PregnantBeginningTar = ValidateValue<string>(value, nameof(PregnantBeginningTar)); } }
        /// <sumary>
        /// PsychiatricillnessBeginningTar
        /// </sumary>
        private string _PsychiatricillnessBeginningTar;
        [Order]
        public string PsychiatricillnessBeginningTar { get { return _PsychiatricillnessBeginningTar; } set { _PsychiatricillnessBeginningTar = ValidateValue<string>(value, nameof(PsychiatricillnessBeginningTar)); } }
        /// <sumary>
        /// AbacavirBeginningTar
        /// </sumary>
        private string _AbacavirBeginningTar;
        [Order]
        public string AbacavirBeginningTar { get { return _AbacavirBeginningTar; } set { _AbacavirBeginningTar = ValidateValue<string>(value, nameof(AbacavirBeginningTar)); } }
        /// <sumary>
        /// AtazanavirBeginningTar
        /// </sumary>
        private string _AtazanavirBeginningTar;
        [Order]
        public string AtazanavirBeginningTar { get { return _AtazanavirBeginningTar; } set { _AtazanavirBeginningTar = ValidateValue<string>(value, nameof(AtazanavirBeginningTar)); } }
        /// <sumary>
        /// DidanosinaBeginningTar
        /// </sumary>
        private string _DidanosinaBeginningTar;
        [Order]
        public string DidanosinaBeginningTar { get { return _DidanosinaBeginningTar; } set { _DidanosinaBeginningTar = ValidateValue<string>(value, nameof(DidanosinaBeginningTar)); } }
        /// <sumary>
        /// EfavirenzBeginningTar
        /// </sumary>
        private string _EfavirenzBeginningTar;
        [Order]
        public string EfavirenzBeginningTar { get { return _EfavirenzBeginningTar; } set { _EfavirenzBeginningTar = ValidateValue<string>(value, nameof(EfavirenzBeginningTar)); } }
        /// <sumary>
        /// EstavudinaBeginningTar
        /// </sumary>
        private string _EstavudinaBeginningTar;
        [Order]
        public string EstavudinaBeginningTar { get { return _EstavudinaBeginningTar; } set { _EstavudinaBeginningTar = ValidateValue<string>(value, nameof(EstavudinaBeginningTar)); } }
        /// <sumary>
        /// FosamprenavirBeginningTar
        /// </sumary>
        private string _FosamprenavirBeginningTar;
        [Order]
        public string FosamprenavirBeginningTar { get { return _FosamprenavirBeginningTar; } set { _FosamprenavirBeginningTar = ValidateValue<string>(value, nameof(FosamprenavirBeginningTar)); } }
        /// <sumary>
        /// IndinavirBeginningTar
        /// </sumary>
        private string _IndinavirBeginningTar;
        [Order]
        public string IndinavirBeginningTar { get { return _IndinavirBeginningTar; } set { _IndinavirBeginningTar = ValidateValue<string>(value, nameof(IndinavirBeginningTar)); } }
        /// <sumary>
        /// LamivudinaBeginningTar
        /// </sumary>
        private string _LamivudinaBeginningTar;
        [Order]
        public string LamivudinaBeginningTar { get { return _LamivudinaBeginningTar; } set { _LamivudinaBeginningTar = ValidateValue<string>(value, nameof(LamivudinaBeginningTar)); } }
        /// <sumary>
        /// LopinavirBeginningTar
        /// </sumary>
        private string _LopinavirBeginningTar;
        [Order]
        public string LopinavirBeginningTar { get { return _LopinavirBeginningTar; } set { _LopinavirBeginningTar = ValidateValue<string>(value, nameof(LopinavirBeginningTar)); } }
        /// <sumary>
        /// NevirapinaBeginningTar
        /// </sumary>
        private string _NevirapinaBeginningTar;
        [Order]
        public string NevirapinaBeginningTar { get { return _NevirapinaBeginningTar; } set { _NevirapinaBeginningTar = ValidateValue<string>(value, nameof(NevirapinaBeginningTar)); } }
        /// <sumary>
        /// NelfinavirBeginningTar
        /// </sumary>
        private string _NelfinavirBeginningTar;
        [Order]
        public string NelfinavirBeginningTar { get { return _NelfinavirBeginningTar; } set { _NelfinavirBeginningTar = ValidateValue<string>(value, nameof(NelfinavirBeginningTar)); } }
        /// <sumary>
        /// RitonavirBeginningTar
        /// </sumary>
        private string _RitonavirBeginningTar;
        [Order]
        public string RitonavirBeginningTar { get { return _RitonavirBeginningTar; } set { _RitonavirBeginningTar = ValidateValue<string>(value, nameof(RitonavirBeginningTar)); } }
        /// <sumary>
        /// SaquinavirBeginningTar
        /// </sumary>
        private string _SaquinavirBeginningTar;
        [Order]
        public string SaquinavirBeginningTar { get { return _SaquinavirBeginningTar; } set { _SaquinavirBeginningTar = ValidateValue<string>(value, nameof(SaquinavirBeginningTar)); } }
        /// <sumary>
        /// ZidovudinaBeginningTar
        /// </sumary>
        private string _ZidovudinaBeginningTar;
        [Order]
        public string ZidovudinaBeginningTar { get { return _ZidovudinaBeginningTar; } set { _ZidovudinaBeginningTar = ValidateValue<string>(value, nameof(ZidovudinaBeginningTar)); } }
        /// <sumary>
        /// TenofovirBeginningTar
        /// </sumary>
        private string _TenofovirBeginningTar;
        [Order]
        public string TenofovirBeginningTar { get { return _TenofovirBeginningTar; } set { _TenofovirBeginningTar = ValidateValue<string>(value, nameof(TenofovirBeginningTar)); } }
        /// <sumary>
        /// EmtricitabinaBeginningTar
        /// </sumary>
        private string _EmtricitabinaBeginningTar;
        [Order]
        public string EmtricitabinaBeginningTar { get { return _EmtricitabinaBeginningTar; } set { _EmtricitabinaBeginningTar = ValidateValue<string>(value, nameof(EmtricitabinaBeginningTar)); } }
        /// <sumary>
        /// NonposMedication3BeginningTar
        /// </sumary>
        private string _NonposMedication3BeginningTar;
        [Order]
        public string NonposMedication3BeginningTar { get { return _NonposMedication3BeginningTar; } set { _NonposMedication3BeginningTar = ValidateValue<string>(value, nameof(NonposMedication3BeginningTar)); } }
        /// <sumary>
        /// NonposMedication4BeginningTar
        /// </sumary>
        private string _NonposMedication4BeginningTar;
        [Order]
        public string NonposMedication4BeginningTar { get { return _NonposMedication4BeginningTar; } set { _NonposMedication4BeginningTar = ValidateValue<string>(value, nameof(NonposMedication4BeginningTar)); } }
        /// <sumary>
        /// NonposMedication5BeginningTar
        /// </sumary>
        private string _NonposMedication5BeginningTar;
        [Order]
        public string NonposMedication5BeginningTar { get { return _NonposMedication5BeginningTar; } set { _NonposMedication5BeginningTar = ValidateValue<string>(value, nameof(NonposMedication5BeginningTar)); } }
        /// <sumary>
        /// NonposMedication6BeginningTar
        /// </sumary>
        private string _NonposMedication6BeginningTar;
        [Order]
        public string NonposMedication6BeginningTar { get { return _NonposMedication6BeginningTar; } set { _NonposMedication6BeginningTar = ValidateValue<string>(value, nameof(NonposMedication6BeginningTar)); } }
        /// <sumary>
        /// ReceiveDadviceBeginningTar
        /// </sumary>
        private string _ReceiveDadviceBeginningTar;
        [Order]
        public string ReceiveDadviceBeginningTar { get { return _ReceiveDadviceBeginningTar; } set { _ReceiveDadviceBeginningTar = ValidateValue<string>(value, nameof(ReceiveDadviceBeginningTar)); } }
        /// <sumary>
        /// MonthTarFormulaDispensed
        /// </sumary>
        private string _MonthTarFormulaDispensed;
        [Order]
        public string MonthTarFormulaDispensed { get { return _MonthTarFormulaDispensed; } set { _MonthTarFormulaDispensed = ValidateValue<string>(value, nameof(MonthTarFormulaDispensed)); } }
        /// <sumary>
        /// MedicalapPointMentSattended
        /// </sumary>
        private string _MedicalapPointMentSattended;
        [Order]
        public string MedicalapPointMentSattended { get { return _MedicalapPointMentSattended; } set { _MedicalapPointMentSattended = ValidateValue<string>(value, nameof(MedicalapPointMentSattended)); } }
        /// <sumary>
        /// InitialTarMedicationsChanged
        /// </sumary>
        private string _InitialTarMedicationsChanged;
        [Order]
        public string InitialTarMedicationsChanged { get { return _InitialTarMedicationsChanged; } set { _InitialTarMedicationsChanged = ValidateValue<string>(value, nameof(InitialTarMedicationsChanged)); } }
        /// <sumary>
        /// DateFirstChangeMedicationInitialTar
        /// </sumary>
        private string _DateFirstChangeMedicationInitialTar;
        [Order]
        public string DateFirstChangeMedicationInitialTar { get { return _DateFirstChangeMedicationInitialTar; } set { _DateFirstChangeMedicationInitialTar = ValidateValue<string>(value, nameof(DateFirstChangeMedicationInitialTar)); } }
        /// <sumary>
        /// CauseChangeMedicationInitialTar
        /// </sumary>
        private string _CauseChangeMedicationInitialTar;
        [Order]
        public string CauseChangeMedicationInitialTar { get { return _CauseChangeMedicationInitialTar; } set { _CauseChangeMedicationInitialTar = ValidateValue<string>(value, nameof(CauseChangeMedicationInitialTar)); } }
        /// <sumary>
        /// NumberFailuressTartTar
        /// </sumary>
        private string _NumberFailuressTartTar;
        [Order]
        public string NumberFailuressTartTar { get { return _NumberFailuressTartTar; } set { _NumberFailuressTartTar = ValidateValue<string>(value, nameof(NumberFailuressTartTar)); } }
        /// <sumary>
        /// NumberTarMedicationChangesAllCauses
        /// </sumary>
        private string _NumberTarMedicationChangesAllCauses;
        [Order]
        public string NumberTarMedicationChangesAllCauses { get { return _NumberTarMedicationChangesAllCauses; } set { _NumberTarMedicationChangesAllCauses = ValidateValue<string>(value, nameof(NumberTarMedicationChangesAllCauses)); } }
        /// <sumary>
        /// Candiesotraqbronqpul
        /// </sumary>
        private string _Candiesotraqbronqpul;
        [Order]
        public string Candiesotraqbronqpul { get { return _Candiesotraqbronqpul; } set { _Candiesotraqbronqpul = ValidateValue<string>(value, nameof(Candiesotraqbronqpul)); } }
        /// <sumary>
        /// PulmonaryTuberculosis
        /// </sumary>
        private string _PulmonaryTuberculosis;
        [Order]
        public string PulmonaryTuberculosis { get { return _PulmonaryTuberculosis; } set { _PulmonaryTuberculosis = ValidateValue<string>(value, nameof(PulmonaryTuberculosis)); } }
        /// <sumary>
        /// InvasiveCervicalCancer
        /// </sumary>
        private string _InvasiveCervicalCancer;
        [Order]
        public string InvasiveCervicalCancer { get { return _InvasiveCervicalCancer; } set { _InvasiveCervicalCancer = ValidateValue<string>(value, nameof(InvasiveCervicalCancer)); } }
        /// <sumary>
        /// DementiaAssociatedvih
        /// </sumary>
        private string _DementiaAssociatedvih;
        [Order]
        public string DementiaAssociatedvih { get { return _DementiaAssociatedvih; } set { _DementiaAssociatedvih = ValidateValue<string>(value, nameof(DementiaAssociatedvih)); } }
        /// <sumary>
        /// ExtrapulmonaryCoccidioidomycosis
        /// </sumary>
        private string _ExtrapulmonaryCoccidioidomycosis;
        [Order]
        public string ExtrapulmonaryCoccidioidomycosis { get { return _ExtrapulmonaryCoccidioidomycosis; } set { _ExtrapulmonaryCoccidioidomycosis = ValidateValue<string>(value, nameof(ExtrapulmonaryCoccidioidomycosis)); } }
        /// <sumary>
        /// CmvInfection
        /// </sumary>
        private string _CmvInfection;
        [Order]
        public string CmvInfection { get { return _CmvInfection; } set { _CmvInfection = ValidateValue<string>(value, nameof(CmvInfection)); } }
        /// <sumary>
        /// SimpleHerpes
        /// </sumary>
        private string _SimpleHerpes;
        [Order]
        public string SimpleHerpes { get { return _SimpleHerpes; } set { _SimpleHerpes = ValidateValue<string>(value, nameof(SimpleHerpes)); } }
        /// <sumary>
        /// DiarrheaIsospora
        /// </sumary>
        private string _DiarrheaIsospora;
        [Order]
        public string DiarrheaIsospora { get { return _DiarrheaIsospora; } set { _DiarrheaIsospora = ValidateValue<string>(value, nameof(DiarrheaIsospora)); } }
        /// <sumary>
        /// ExtrapulmonaryHistoplasmosis
        /// </sumary>
        private string _ExtrapulmonaryHistoplasmosis;
        [Order]
        public string ExtrapulmonaryHistoplasmosis { get { return _ExtrapulmonaryHistoplasmosis; } set { _ExtrapulmonaryHistoplasmosis = ValidateValue<string>(value, nameof(ExtrapulmonaryHistoplasmosis)); } }
        /// <sumary>
        /// Burkittlymphoma
        /// </sumary>
        private string _Burkittlymphoma;
        [Order]
        public string Burkittlymphoma { get { return _Burkittlymphoma; } set { _Burkittlymphoma = ValidateValue<string>(value, nameof(Burkittlymphoma)); } }
        /// <sumary>
        /// PneumoniapNeumocystiscarinii
        /// </sumary>
        private string _PneumoniapNeumocystiscarinii;
        [Order]
        public string PneumoniapNeumocystiscarinii { get { return _PneumoniapNeumocystiscarinii; } set { _PneumoniapNeumocystiscarinii = ValidateValue<string>(value, nameof(PneumoniapNeumocystiscarinii)); } }
        /// <sumary>
        /// RecurrentBacterialpNeumonia
        /// </sumary>
        private string _RecurrentBacterialpNeumonia;
        [Order]
        public string RecurrentBacterialpNeumonia { get { return _RecurrentBacterialpNeumonia; } set { _RecurrentBacterialpNeumonia = ValidateValue<string>(value, nameof(RecurrentBacterialpNeumonia)); } }
        /// <sumary>
        /// SalmonellaSepticemia
        /// </sumary>
        private string _SalmonellaSepticemia;
        [Order]
        public string SalmonellaSepticemia { get { return _SalmonellaSepticemia; } set { _SalmonellaSepticemia = ValidateValue<string>(value, nameof(SalmonellaSepticemia)); } }
        /// <sumary>
        /// DisseminatedInfectionmacmai
        /// </sumary>
        private string _DisseminatedInfectionmacmai;
        [Order]
        public string DisseminatedInfectionmacmai { get { return _DisseminatedInfectionmacmai; } set { _DisseminatedInfectionmacmai = ValidateValue<string>(value, nameof(DisseminatedInfectionmacmai)); } }
        /// <sumary>
        /// CriptococosiSexTrapulmonar
        /// </sumary>
        private string _CriptococosiSexTrapulmonar;
        [Order]
        public string CriptococosiSexTrapulmonar { get { return _CriptococosiSexTrapulmonar; } set { _CriptococosiSexTrapulmonar = ValidateValue<string>(value, nameof(CriptococosiSexTrapulmonar)); } }
        /// <sumary>
        /// Kaposisarcoma
        /// </sumary>
        private string _Kaposisarcoma;
        [Order]
        public string Kaposisarcoma { get { return _Kaposisarcoma; } set { _Kaposisarcoma = ValidateValue<string>(value, nameof(Kaposisarcoma)); } }
        /// <sumary>
        /// Wearsyndrome
        /// </sumary>
        private string _Wearsyndrome;
        [Order]
        public string Wearsyndrome { get { return _Wearsyndrome; } set { _Wearsyndrome = ValidateValue<string>(value, nameof(Wearsyndrome)); } }
        /// <sumary>
        /// Leukoencefelopatia
        /// </sumary>
        private string _Leukoencefelopatia;
        [Order]
        public string Leukoencefelopatia { get { return _Leukoencefelopatia; } set { _Leukoencefelopatia = ValidateValue<string>(value, nameof(Leukoencefelopatia)); } }
        /// <sumary>
        /// LymphoidinterstitialpNeumonia
        /// </sumary>
        private string _LymphoidinterstitialpNeumonia;
        [Order]
        public string LymphoidinterstitialpNeumonia { get { return _LymphoidinterstitialpNeumonia; } set { _LymphoidinterstitialpNeumonia = ValidateValue<string>(value, nameof(LymphoidinterstitialpNeumonia)); } }
        /// <sumary>
        /// CerebralToxoplasmosis
        /// </sumary>
        private string _CerebralToxoplasmosis;
        [Order]
        public string CerebralToxoplasmosis { get { return _CerebralToxoplasmosis; } set { _CerebralToxoplasmosis = ValidateValue<string>(value, nameof(CerebralToxoplasmosis)); } }
        /// <sumary>
        /// HabilitationCodePresent
        /// </sumary>
        private string _HabilitationCodePresent;
        [Order]
        public string HabilitationCodePresent { get { return _HabilitationCodePresent; } set { _HabilitationCodePresent = ValidateValue<string>(value, nameof(HabilitationCodePresent)); } }
        /// <sumary>
        /// DateEntryCurrentIps
        /// </sumary>
        private string _DateEntryCurrentIps;
        [Order]
        public string DateEntryCurrentIps { get { return _DateEntryCurrentIps; } set { _DateEntryCurrentIps = ValidateValue<string>(value, nameof(DateEntryCurrentIps)); } }
        /// <sumary>
        /// MunicipalityResidenceIPS
        /// </sumary>
        private string _MunicipalityResidenceIPS;
        [Order]
        public string MunicipalityResidenceIPS { get { return _MunicipalityResidenceIPS; } set { _MunicipalityResidenceIPS = ValidateValue<string>(value, nameof(MunicipalityResidenceIPS)); } }
        /// <sumary>
        /// ClinicalCareandFormulation
        /// </sumary>
        private string _ClinicalCareandFormulation;
        [Order]
        public string ClinicalCareandFormulation { get { return _ClinicalCareandFormulation; } set { _ClinicalCareandFormulation = ValidateValue<string>(value, nameof(ClinicalCareandFormulation)); } }
        /// <sumary>
        /// EvaluationInfectologistLast6Months
        /// </sumary>
        private string _EvaluationInfectologistLast6Months;
        [Order]
        public string EvaluationInfectologistLast6Months { get { return _EvaluationInfectologistLast6Months; } set { _EvaluationInfectologistLast6Months = ValidateValue<string>(value, nameof(EvaluationInfectologistLast6Months)); } }
        /// <sumary>
        /// GenotypingResult
        /// </sumary>
        private string _GenotypingResult;
        [Order]
        public string GenotypingResult { get { return _GenotypingResult; } set { _GenotypingResult = ValidateValue<string>(value, nameof(GenotypingResult)); } }
        /// <sumary>
        /// GenotypingMoment
        /// </sumary>
        private string _GenotypingMoment;
        [Order]
        public string GenotypingMoment { get { return _GenotypingMoment; } set { _GenotypingMoment = ValidateValue<string>(value, nameof(GenotypingMoment)); } }
        /// <sumary>
        /// CurrentClinicalSituation
        /// </sumary>
        private string _CurrentClinicalSituation;
        [Order]
        public string CurrentClinicalSituation { get { return _CurrentClinicalSituation; } set { _CurrentClinicalSituation = ValidateValue<string>(value, nameof(CurrentClinicalSituation)); } }
        /// <sumary>
        /// CurrentClinicalStage
        /// </sumary>
        private string _CurrentClinicalStage;
        [Order]
        public string CurrentClinicalStage { get { return _CurrentClinicalStage; } set { _CurrentClinicalStage = ValidateValue<string>(value, nameof(CurrentClinicalStage)); } }
        /// <sumary>
        /// Dyslipidemia
        /// </sumary>
        private string _Dyslipidemia;
        [Order]
        public string Dyslipidemia { get { return _Dyslipidemia; } set { _Dyslipidemia = ValidateValue<string>(value, nameof(Dyslipidemia)); } }
        /// <sumary>
        /// PeripheralNeuropathy
        /// </sumary>
        private string _PeripheralNeuropathy;
        [Order]
        public string PeripheralNeuropathy { get { return _PeripheralNeuropathy; } set { _PeripheralNeuropathy = ValidateValue<string>(value, nameof(PeripheralNeuropathy)); } }
        /// <sumary>
        /// Lipoatrophy
        /// </sumary>
        private string _Lipoatrophy;
        [Order]
        public string Lipoatrophy { get { return _Lipoatrophy; } set { _Lipoatrophy = ValidateValue<string>(value, nameof(Lipoatrophy)); } }
        /// <sumary>
        /// CoInfectionvhb
        /// </sumary>
        private string _CoInfectionvhb;
        [Order]
        public string CoInfectionvhb { get { return _CoInfectionvhb; } set { _CoInfectionvhb = ValidateValue<string>(value, nameof(CoInfectionvhb)); } }
        /// <sumary>
        /// CoInfectionvhc
        /// </sumary>
        private string _CoInfectionvhc;
        [Order]
        public string CoInfectionvhc { get { return _CoInfectionvhc; } set { _CoInfectionvhc = ValidateValue<string>(value, nameof(CoInfectionvhc)); } }
        /// <sumary>
        /// Anemia
        /// </sumary>
        private string _Anemia;
        [Order]
        public string Anemia { get { return _Anemia; } set { _Anemia = ValidateValue<string>(value, nameof(Anemia)); } }
        /// <sumary>
        /// Livercirrhosis
        /// </sumary>
        private string _Livercirrhosis;
        [Order]
        public string Livercirrhosis { get { return _Livercirrhosis; } set { _Livercirrhosis = ValidateValue<string>(value, nameof(Livercirrhosis)); } }
        /// <sumary>
        /// ChronickIdnEydisease
        /// </sumary>
        private string _ChronickIdnEydisease;
        [Order]
        public string ChronickIdnEydisease { get { return _ChronickIdnEydisease; } set { _ChronickIdnEydisease = ValidateValue<string>(value, nameof(ChronickIdnEydisease)); } }
        /// <sumary>
        /// CoronaryDisease
        /// </sumary>
        private string _CoronaryDisease;
        [Order]
        public string CoronaryDisease { get { return _CoronaryDisease; } set { _CoronaryDisease = ValidateValue<string>(value, nameof(CoronaryDisease)); } }
        /// <sumary>
        /// SexuallyTransmittedInfections
        /// </sumary>
        private string _SexuallyTransmittedInfections;
        [Order]
        public string SexuallyTransmittedInfections { get { return _SexuallyTransmittedInfections; } set { _SexuallyTransmittedInfections = ValidateValue<string>(value, nameof(SexuallyTransmittedInfections)); } }
        /// <sumary>
        /// NeoplasiaNotrelatedSida
        /// </sumary>
        private string _NeoplasiaNotrelatedSida;
        [Order]
        public string NeoplasiaNotrelatedSida { get { return _NeoplasiaNotrelatedSida; } set { _NeoplasiaNotrelatedSida = ValidateValue<string>(value, nameof(NeoplasiaNotrelatedSida)); } }
        /// <sumary>
        /// FunctionalDisability
        /// </sumary>
        private string _FunctionalDisability;
        [Order]
        public string FunctionalDisability { get { return _FunctionalDisability; } set { _FunctionalDisability = ValidateValue<string>(value, nameof(FunctionalDisability)); } }
        /// <sumary>
        /// Datelasttcd4lymphocyteCount
        /// </sumary>
        private string _Datelasttcd4lymphocyteCount;
        [Order]
        public string Datelasttcd4lymphocyteCount { get { return _Datelasttcd4lymphocyteCount; } set { _Datelasttcd4lymphocyteCount = ValidateValue<string>(value, nameof(Datelasttcd4lymphocyteCount)); } }
        /// <sumary>
        /// Lasttcd4lymphocyteCount
        /// </sumary>
        private string _Lasttcd4lymphocyteCount;
        [Order]
        public string Lasttcd4lymphocyteCount { get { return _Lasttcd4lymphocyteCount; } set { _Lasttcd4lymphocyteCount = ValidateValue<string>(value, nameof(Lasttcd4lymphocyteCount)); } }
        /// <sumary>
        /// DatelastTotallymphocyteCount
        /// </sumary>
        private string _DatelastTotallymphocyteCount;
        [Order]
        public string DatelastTotallymphocyteCount { get { return _DatelastTotallymphocyteCount; } set { _DatelastTotallymphocyteCount = ValidateValue<string>(value, nameof(DatelastTotallymphocyteCount)); } }
        /// <sumary>
        /// LastTotallymphocyteCount
        /// </sumary>
        private string _LastTotallymphocyteCount;
        [Order]
        public string LastTotallymphocyteCount { get { return _LastTotallymphocyteCount; } set { _LastTotallymphocyteCount = ValidateValue<string>(value, nameof(LastTotallymphocyteCount)); } }
        /// <sumary>
        /// LastDateViralLoad
        /// </sumary>
        private string _LastDateViralLoad;
        [Order]
        public string LastDateViralLoad { get { return _LastDateViralLoad; } set { _LastDateViralLoad = ValidateValue<string>(value, nameof(LastDateViralLoad)); } }
        /// <sumary>
        /// ResultLastViralLoad
        /// </sumary>
        private string _ResultLastViralLoad;
        [Order]
        public string ResultLastViralLoad { get { return _ResultLastViralLoad; } set { _ResultLastViralLoad = ValidateValue<string>(value, nameof(ResultLastViralLoad)); } }
        /// <sumary>
        /// SupplyCondomsLast3Months
        /// </sumary>
        private string _SupplyCondomsLast3Months;
        [Order]
        public string SupplyCondomsLast3Months { get { return _SupplyCondomsLast3Months; } set { _SupplyCondomsLast3Months = ValidateValue<string>(value, nameof(SupplyCondomsLast3Months)); } }
        /// <sumary>
        /// NcondomsSuppliedLast3Months
        /// </sumary>
        private string _NcondomsSuppliedLast3Months;
        [Order]
        public string NcondomsSuppliedLast3Months { get { return _NcondomsSuppliedLast3Months; } set { _NcondomsSuppliedLast3Months = ValidateValue<string>(value, nameof(NcondomsSuppliedLast3Months)); } }
        /// <sumary>
        /// FamilyPlanningMethod
        /// </sumary>
        private string _FamilyPlanningMethod;
        [Order]
        public string FamilyPlanningMethod { get { return _FamilyPlanningMethod; } set { _FamilyPlanningMethod = ValidateValue<string>(value, nameof(FamilyPlanningMethod)); } }
        /// <sumary>
        /// Hepatitisbvaccine
        /// </sumary>
        private string _Hepatitisbvaccine;
        [Order]
        public string Hepatitisbvaccine { get { return _Hepatitisbvaccine; } set { _Hepatitisbvaccine = ValidateValue<string>(value, nameof(Hepatitisbvaccine)); } }
        /// <sumary>
        /// Ppdlast12Month
        /// </sumary>
        private string _Ppdlast12Month;
        [Order]
        public string Ppdlast12Month { get { return _Ppdlast12Month; } set { _Ppdlast12Month = ValidateValue<string>(value, nameof(Ppdlast12Month)); } }
        /// <sumary>
        /// ViralloadChildrenunder18Months
        /// </sumary>
        private string _ViralloadChildrenunder18Months;
        [Order]
        public string ViralloadChildrenunder18Months { get { return _ViralloadChildrenunder18Months; } set { _ViralloadChildrenunder18Months = ValidateValue<string>(value, nameof(ViralloadChildrenunder18Months)); } }
        /// <sumary>
        /// SecondViralLoadChildrenUnder18Months
        /// </sumary>
        private string _SecondViralLoadChildrenUnder18Months;
        [Order]
        public string SecondViralLoadChildrenUnder18Months { get { return _SecondViralLoadChildrenUnder18Months; } set { _SecondViralLoadChildrenUnder18Months = ValidateValue<string>(value, nameof(SecondViralLoadChildrenUnder18Months)); } }
        /// <sumary>
        /// NumberViralLoadChildrenUnder18Months
        /// </sumary>
        private string _NumberViralLoadChildrenUnder18Months;
        [Order]
        public string NumberViralLoadChildrenUnder18Months { get { return _NumberViralLoadChildrenUnder18Months; } set { _NumberViralLoadChildrenUnder18Months = ValidateValue<string>(value, nameof(NumberViralLoadChildrenUnder18Months)); } }
        /// <sumary>
        /// SupplymilkFormula
        /// </sumary>
        private string _SupplymilkFormula;
        [Order]
        public string SupplymilkFormula { get { return _SupplymilkFormula; } set { _SupplymilkFormula = ValidateValue<string>(value, nameof(SupplymilkFormula)); } }
        /// <sumary>
        /// ReceiveTar
        /// </sumary>
        private string _ReceiveTar;
        [Order]
        public string ReceiveTar { get { return _ReceiveTar; } set { _ReceiveTar = ValidateValue<string>(value, nameof(ReceiveTar)); } }
        /// <sumary>
        /// StartDateCurrentTar
        /// </sumary>
        private string _StartDateCurrentTar;
        [Order]
        public string StartDateCurrentTar { get { return _StartDateCurrentTar; } set { _StartDateCurrentTar = ValidateValue<string>(value, nameof(StartDateCurrentTar)); } }
        /// <sumary>
        /// CurrentAbacavir
        /// </sumary>
        private string _CurrentAbacavir;
        [Order]
        public string CurrentAbacavir { get { return _CurrentAbacavir; } set { _CurrentAbacavir = ValidateValue<string>(value, nameof(CurrentAbacavir)); } }
        /// <sumary>
        /// CurrentAtazanavir
        /// </sumary>
        private string _CurrentAtazanavir;
        [Order]
        public string CurrentAtazanavir { get { return _CurrentAtazanavir; } set { _CurrentAtazanavir = ValidateValue<string>(value, nameof(CurrentAtazanavir)); } }
        /// <sumary>
        /// CurrentDidanosina
        /// </sumary>
        private string _CurrentDidanosina;
        [Order]
        public string CurrentDidanosina { get { return _CurrentDidanosina; } set { _CurrentDidanosina = ValidateValue<string>(value, nameof(CurrentDidanosina)); } }
        /// <sumary>
        /// CurrentEfavirenz
        /// </sumary>
        private string _CurrentEfavirenz;
        [Order]
        public string CurrentEfavirenz { get { return _CurrentEfavirenz; } set { _CurrentEfavirenz = ValidateValue<string>(value, nameof(CurrentEfavirenz)); } }
        /// <sumary>
        /// CurrentEstavudina
        /// </sumary>
        private string _CurrentEstavudina;
        [Order]
        public string CurrentEstavudina { get { return _CurrentEstavudina; } set { _CurrentEstavudina = ValidateValue<string>(value, nameof(CurrentEstavudina)); } }
        /// <sumary>
        /// CurrentFosamprenavir
        /// </sumary>
        private string _CurrentFosamprenavir;
        [Order]
        public string CurrentFosamprenavir { get { return _CurrentFosamprenavir; } set { _CurrentFosamprenavir = ValidateValue<string>(value, nameof(CurrentFosamprenavir)); } }
        /// <sumary>
        /// CurrentIndinavir
        /// </sumary>
        private string _CurrentIndinavir;
        [Order]
        public string CurrentIndinavir { get { return _CurrentIndinavir; } set { _CurrentIndinavir = ValidateValue<string>(value, nameof(CurrentIndinavir)); } }
        /// <sumary>
        /// CurrentLamivudina
        /// </sumary>
        private string _CurrentLamivudina;
        [Order]
        public string CurrentLamivudina { get { return _CurrentLamivudina; } set { _CurrentLamivudina = ValidateValue<string>(value, nameof(CurrentLamivudina)); } }
        /// <sumary>
        /// CurrentLopinavir
        /// </sumary>
        private string _CurrentLopinavir;
        [Order]
        public string CurrentLopinavir { get { return _CurrentLopinavir; } set { _CurrentLopinavir = ValidateValue<string>(value, nameof(CurrentLopinavir)); } }
        /// <sumary>
        /// CurrentNevirapina
        /// </sumary>
        private string _CurrentNevirapina;
        [Order]
        public string CurrentNevirapina { get { return _CurrentNevirapina; } set { _CurrentNevirapina = ValidateValue<string>(value, nameof(CurrentNevirapina)); } }
        /// <sumary>
        /// CurrentNelfinavir
        /// </sumary>
        private string _CurrentNelfinavir;
        [Order]
        public string CurrentNelfinavir { get { return _CurrentNelfinavir; } set { _CurrentNelfinavir = ValidateValue<string>(value, nameof(CurrentNelfinavir)); } }
        /// <sumary>
        /// CurrentRitonavir
        /// </sumary>
        private string _CurrentRitonavir;
        [Order]
        public string CurrentRitonavir { get { return _CurrentRitonavir; } set { _CurrentRitonavir = ValidateValue<string>(value, nameof(CurrentRitonavir)); } }
        /// <sumary>
        /// CurrentSaquinavir
        /// </sumary>
        private string _CurrentSaquinavir;
        [Order]
        public string CurrentSaquinavir { get { return _CurrentSaquinavir; } set { _CurrentSaquinavir = ValidateValue<string>(value, nameof(CurrentSaquinavir)); } }
        /// <sumary>
        /// CurrentZidovudina
        /// </sumary>
        private string _CurrentZidovudina;
        [Order]
        public string CurrentZidovudina { get { return _CurrentZidovudina; } set { _CurrentZidovudina = ValidateValue<string>(value, nameof(CurrentZidovudina)); } }
        /// <sumary>
        /// CurrentTenofovir
        /// </sumary>
        private string _CurrentTenofovir;
        [Order]
        public string CurrentTenofovir { get { return _CurrentTenofovir; } set { _CurrentTenofovir = ValidateValue<string>(value, nameof(CurrentTenofovir)); } }
        /// <sumary>
        /// CurrentEmtricita81na
        /// </sumary>
        private string _CurrentEmtricita81na;
        [Order]
        public string CurrentEmtricita81na { get { return _CurrentEmtricita81na; } set { _CurrentEmtricita81na = ValidateValue<string>(value, nameof(CurrentEmtricita81na)); } }
        /// <sumary>
        /// CurrentNonposMedication3
        /// </sumary>
        private string _CurrentNonposMedication3;
        [Order]
        public string CurrentNonposMedication3 { get { return _CurrentNonposMedication3; } set { _CurrentNonposMedication3 = ValidateValue<string>(value, nameof(CurrentNonposMedication3)); } }
        /// <sumary>
        /// CurrentNonposMedication4
        /// </sumary>
        private string _CurrentNonposMedication4;
        [Order]
        public string CurrentNonposMedication4 { get { return _CurrentNonposMedication4; } set { _CurrentNonposMedication4 = ValidateValue<string>(value, nameof(CurrentNonposMedication4)); } }
        /// <sumary>
        /// CurrentNonposMedication5
        /// </sumary>
        private string _CurrentNonposMedication5;
        [Order]
        public string CurrentNonposMedication5 { get { return _CurrentNonposMedication5; } set { _CurrentNonposMedication5 = ValidateValue<string>(value, nameof(CurrentNonposMedication5)); } }
        /// <sumary>
        /// CurrentNonposMedication6
        /// </sumary>
        private string _CurrentNonposMedication6;
        [Order]
        public string CurrentNonposMedication6 { get { return _CurrentNonposMedication6; } set { _CurrentNonposMedication6 = ValidateValue<string>(value, nameof(CurrentNonposMedication6)); } }
        /// <sumary>
        /// Newbornarvprophylaxis
        /// </sumary>
        private string _Newbornarvprophylaxis;
        [Order]
        public string Newbornarvprophylaxis { get { return _Newbornarvprophylaxis; } set { _Newbornarvprophylaxis = ValidateValue<string>(value, nameof(Newbornarvprophylaxis)); } }
        /// <sumary>
        /// ProphyLaxismac
        /// </sumary>
        private string _ProphyLaxismac;
        [Order]
        public string ProphyLaxismac { get { return _ProphyLaxismac; } set { _ProphyLaxismac = ValidateValue<string>(value, nameof(ProphyLaxismac)); } }
        /// <sumary>
        /// ProphyLaxisfluconazol
        /// </sumary>
        private string _ProphyLaxisfluconazol;
        [Order]
        public string ProphyLaxisfluconazol { get { return _ProphyLaxisfluconazol; } set { _ProphyLaxisfluconazol = ValidateValue<string>(value, nameof(ProphyLaxisfluconazol)); } }
        /// <sumary>
        /// ProphyLaxistrimethoprimsulfa
        /// </sumary>
        private string _ProphyLaxistrimethoprimsulfa;
        [Order]
        public string ProphyLaxistrimethoprimsulfa { get { return _ProphyLaxistrimethoprimsulfa; } set { _ProphyLaxistrimethoprimsulfa = ValidateValue<string>(value, nameof(ProphyLaxistrimethoprimsulfa)); } }
        /// <sumary>
        /// ProphyLaxisimmunoglobuliniv
        /// </sumary>
        private string _ProphyLaxisimmunoglobuliniv;
        [Order]
        public string ProphyLaxisimmunoglobuliniv { get { return _ProphyLaxisimmunoglobuliniv; } set { _ProphyLaxisimmunoglobuliniv = ValidateValue<string>(value, nameof(ProphyLaxisimmunoglobuliniv)); } }
        /// <sumary>
        /// ProphyLaxisisonic
        /// </sumary>
        private string _ProphyLaxisisonic;
        [Order]
        public string ProphyLaxisisonic { get { return _ProphyLaxisisonic; } set { _ProphyLaxisisonic = ValidateValue<string>(value, nameof(ProphyLaxisisonic)); } }
        /// <sumary>
        /// AntituberculosisTreatment
        /// </sumary>
        private string _AntituberculosisTreatment;
        [Order]
        public string AntituberculosisTreatment { get { return _AntituberculosisTreatment; } set { _AntituberculosisTreatment = ValidateValue<string>(value, nameof(AntituberculosisTreatment)); } }
        /// <sumary>
        /// StartDateCuurentTuberculosisTreatment
        /// </sumary>
        private string _StartDateCuurentTuberculosisTreatment;
        [Order]
        public string StartDateCuurentTuberculosisTreatment { get { return _StartDateCuurentTuberculosisTreatment; } set { _StartDateCuurentTuberculosisTreatment = ValidateValue<string>(value, nameof(StartDateCuurentTuberculosisTreatment)); } }
        /// <sumary>
        /// CurrentAmikacina
        /// </sumary>
        private string _CurrentAmikacina;
        [Order]
        public string CurrentAmikacina { get { return _CurrentAmikacina; } set { _CurrentAmikacina = ValidateValue<string>(value, nameof(CurrentAmikacina)); } }
        /// <sumary>
        /// CurrentCiprofloxacina
        /// </sumary>
        private string _CurrentCiprofloxacina;
        [Order]
        public string CurrentCiprofloxacina { get { return _CurrentCiprofloxacina; } set { _CurrentCiprofloxacina = ValidateValue<string>(value, nameof(CurrentCiprofloxacina)); } }
        /// <sumary>
        /// CurrentEstreptomicina
        /// </sumary>
        private string _CurrentEstreptomicina;
        [Order]
        public string CurrentEstreptomicina { get { return _CurrentEstreptomicina; } set { _CurrentEstreptomicina = ValidateValue<string>(value, nameof(CurrentEstreptomicina)); } }
        /// <sumary>
        /// CurrentEthambutol
        /// </sumary>
        private string _CurrentEthambutol;
        [Order]
        public string CurrentEthambutol { get { return _CurrentEthambutol; } set { _CurrentEthambutol = ValidateValue<string>(value, nameof(CurrentEthambutol)); } }
        /// <sumary>
        /// CurrentEthionamida
        /// </sumary>
        private string _CurrentEthionamida;
        [Order]
        public string CurrentEthionamida { get { return _CurrentEthionamida; } set { _CurrentEthionamida = ValidateValue<string>(value, nameof(CurrentEthionamida)); } }
        /// <sumary>
        /// CurrentIsoniacida
        /// </sumary>
        private string _CurrentIsoniacida;
        [Order]
        public string CurrentIsoniacida { get { return _CurrentIsoniacida; } set { _CurrentIsoniacida = ValidateValue<string>(value, nameof(CurrentIsoniacida)); } }
        /// <sumary>
        /// CurrentPirazinamida
        /// </sumary>
        private string _CurrentPirazinamida;
        [Order]
        public string CurrentPirazinamida { get { return _CurrentPirazinamida; } set { _CurrentPirazinamida = ValidateValue<string>(value, nameof(CurrentPirazinamida)); } }
        /// <sumary>
        /// CurrentRifampicina
        /// </sumary>
        private string _CurrentRifampicina;
        [Order]
        public string CurrentRifampicina { get { return _CurrentRifampicina; } set { _CurrentRifampicina = ValidateValue<string>(value, nameof(CurrentRifampicina)); } }
        /// <sumary>
        /// CurrentRifabutina
        /// </sumary>
        private string _CurrentRifabutina;
        [Order]
        public string CurrentRifabutina { get { return _CurrentRifabutina; } set { _CurrentRifabutina = ValidateValue<string>(value, nameof(CurrentRifabutina)); } }
        /// <sumary>
        /// CurrentNonposMedication1tuber
        /// </sumary>
        private string _CurrentNonposMedication1tuber;
        [Order]
        public string CurrentNonposMedication1tuber { get { return _CurrentNonposMedication1tuber; } set { _CurrentNonposMedication1tuber = ValidateValue<string>(value, nameof(CurrentNonposMedication1tuber)); } }
        /// <sumary>
        /// CurrentNonposMedication2tuber
        /// </sumary>
        private string _CurrentNonposMedication2tuber;
        [Order]
        public string CurrentNonposMedication2tuber { get { return _CurrentNonposMedication2tuber; } set { _CurrentNonposMedication2tuber = ValidateValue<string>(value, nameof(CurrentNonposMedication2tuber)); } }
        /// <sumary>
        /// CurrentNonposMedication3tuber
        /// </sumary>
        private string _CurrentNonposMedication3tuber;
        [Order]
        public string CurrentNonposMedication3tuber { get { return _CurrentNonposMedication3tuber; } set { _CurrentNonposMedication3tuber = ValidateValue<string>(value, nameof(CurrentNonposMedication3tuber)); } }
        /// <sumary>
        /// CurrentNonposMedication4tuber
        /// </sumary>
        private string _CurrentNonposMedication4tuber;
        [Order]
        public string CurrentNonposMedication4tuber { get { return _CurrentNonposMedication4tuber; } set { _CurrentNonposMedication4tuber = ValidateValue<string>(value, nameof(CurrentNonposMedication4tuber)); } }
        /// <sumary>
        /// CurrentNonposMedication5tuber
        /// </sumary>
        private string _CurrentNonposMedication5tuber;
        [Order]
        public string CurrentNonposMedication5tuber { get { return _CurrentNonposMedication5tuber; } set { _CurrentNonposMedication5tuber = ValidateValue<string>(value, nameof(CurrentNonposMedication5tuber)); } }
        /// <sumary>
        /// SerologysyphilisFirsttrimester
        /// </sumary>
        private string _SerologysyphilisFirsttrimester;
        [Order]
        public string SerologysyphilisFirsttrimester { get { return _SerologysyphilisFirsttrimester; } set { _SerologysyphilisFirsttrimester = ValidateValue<string>(value, nameof(SerologysyphilisFirsttrimester)); } }
        /// <sumary>
        /// SerologysyphilisSecondtrimester
        /// </sumary>
        private string _SerologysyphilisSecondtrimester;
        [Order]
        public string SerologysyphilisSecondtrimester { get { return _SerologysyphilisSecondtrimester; } set { _SerologysyphilisSecondtrimester = ValidateValue<string>(value, nameof(SerologysyphilisSecondtrimester)); } }
        /// <sumary>
        /// SerologysyphilisThirdtrimester
        /// </sumary>
        private string _SerologysyphilisThirdtrimester;
        [Order]
        public string SerologysyphilisThirdtrimester { get { return _SerologysyphilisThirdtrimester; } set { _SerologysyphilisThirdtrimester = ValidateValue<string>(value, nameof(SerologysyphilisThirdtrimester)); } }
        /// <sumary>
        /// SerologyBirthTimeAbortion
        /// </sumary>
        private string _SerologyBirthTimeAbortion;
        [Order]
        public string SerologyBirthTimeAbortion { get { return _SerologyBirthTimeAbortion; } set { _SerologyBirthTimeAbortion = ValidateValue<string>(value, nameof(SerologyBirthTimeAbortion)); } }
        /// <sumary>
        /// DateFirstTreatmentSyphilis
        /// </sumary>
        private string _DateFirstTreatmentSyphilis;
        [Order]
        public string DateFirstTreatmentSyphilis { get { return _DateFirstTreatmentSyphilis; } set { _DateFirstTreatmentSyphilis = ValidateValue<string>(value, nameof(DateFirstTreatmentSyphilis)); } }
        /// <sumary>
        /// DateSecondTreatmentSyphilis
        /// </sumary>
        private string _DateSecondTreatmentSyphilis;
        [Order]
        public string DateSecondTreatmentSyphilis { get { return _DateSecondTreatmentSyphilis; } set { _DateSecondTreatmentSyphilis = ValidateValue<string>(value, nameof(DateSecondTreatmentSyphilis)); } }
        /// <sumary>
        /// DateThirdTreatmentSyphilis
        /// </sumary>
        private string _DateThirdTreatmentSyphilis;
        [Order]
        public string DateThirdTreatmentSyphilis { get { return _DateThirdTreatmentSyphilis; } set { _DateThirdTreatmentSyphilis = ValidateValue<string>(value, nameof(DateThirdTreatmentSyphilis)); } }
        /// <sumary>
        /// NoveltyPreviousReport
        /// </sumary>
        private string _NoveltyPreviousReport;
        [Order]
        public string NoveltyPreviousReport { get { return _NoveltyPreviousReport; } set { _NoveltyPreviousReport = ValidateValue<string>(value, nameof(NoveltyPreviousReport)); } }
        /// <sumary>
        /// Disapfiliationdateeps
        /// </sumary>
        private string _Disapfiliationdateeps;
        [Order]
        public string Disapfiliationdateeps { get { return _Disapfiliationdateeps; } set { _Disapfiliationdateeps = ValidateValue<string>(value, nameof(Disapfiliationdateeps)); } }
        /// <sumary>
        /// Habilitationcode
        /// </sumary>
        private string _Habilitationcode;
        [Order]
        public string Habilitationcode { get { return _Habilitationcode; } set { _Habilitationcode = ValidateValue<string>(value, nameof(Habilitationcode)); } }
        /// <sumary>
        /// DeathDate
        /// </sumary>
        private string _DeathDate;
        [Order]
        public string DeathDate { get { return _DeathDate; } set { _DeathDate = ValidateValue<string>(value, nameof(DeathDate)); } }
        /// <sumary>
        /// DeathCause
        /// </sumary>
        private string _DeathCause;
        [Order]
        public string DeathCause { get { return _DeathCause; } set { _DeathCause = ValidateValue<string>(value, nameof(DeathCause)); } }
        #endregion

        #region Builders
        public ENT_StructureRes4725En() : base(null) { ExtrictValidation = false; }
        public ENT_StructureRes4725En(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// Entidad con parámetros de entrada utilizados en MainRes4725
    /// </sumary>
    public class ENT_StructureRes4725 : EntityBase
    {
        #region Properties
        /// <sumary>
        /// Codigo EPS
        /// </sumary>
        private string _CodigoEPS;
        [Order]
        [Regex(@"^[0-9]+$", "Variable 1 - CodigoEPS - Sólo se permiten números, Codigo de habilitacion de la tabla operator")] public string CodigoEPS { get { return _CodigoEPS; } set { _CodigoEPS = ValidateValue<string>(value, nameof(CodigoEPS)); } }
        /// <sumary>
        /// Regimen
        /// </sumary>
        private string _Regimen;
        [Order]
        [Regex(@"^[1-4]{1}$", "Variable 2 - Regimen - Sólo se permiten números y la longitud válida son 1 caracter")] public string Regimen { get { return _Regimen; } set { _Regimen = ValidateValue<string>(value, nameof(Regimen)); } }
        /// <sumary>
        /// GroupPopulation
        /// </sumary>
        private string _GrupoPoblacional;
        [Order]
        [Regex(@"^[1-3]{1}$", "Variable 3 - GrupoPoblacional - Sólo se permiten números y la longitud válida son 1 caracter")] public string GrupoPoblacional { get { return _GrupoPoblacional; } set { _GrupoPoblacional = ValidateValue<string>(value, nameof(GrupoPoblacional)); } }
        /// <sumary>
        /// PrimerNombre
        /// </sumary>
        private string _PrimerNombre;
        [Order]
        [Regex(@"^([A-Za-z ]{3,30})$", "Variable 4 - PrimerNombre - Permite solo letras con una longitud de 3 a 30 Caracteres")] public string PrimerNombre { get { return _PrimerNombre; } set { _PrimerNombre = ValidateValue<string>(value, nameof(PrimerNombre)); } }
        /// <sumary>
        /// SecondName
        /// </sumary>
        private string _SegundoNombre;
        [Order]
        [Regex(@"^([A-Za-z0 ]){1,30}$", "Variable 5 - SegundoNombre - Solo permite letras o si no tiene segundo nombre recibe el 0 longitud 30 caracteres")] public string SegundoNombre { get { return _SegundoNombre; } set { _SegundoNombre = ValidateValue<string>(value, nameof(SegundoNombre)); } }
        /// <sumary>
        /// FirstLastName
        /// </sumary>
        private string _PrimerApellido;
        [Order]
        [Regex(@"^([A-Za-z ]{3,30})$", "Variable 6 - PrimerApellido - Permite solo letras con una longitud de 3 a 30 Caracteres")] public string PrimerApellido { get { return _PrimerApellido; } set { _PrimerApellido = ValidateValue<string>(value, nameof(PrimerApellido)); } }
        /// <sumary>
        /// SecondLastName
        /// </sumary>
        private string _SegundoApellido;
        [Order]
        [Regex(@"^([A-Za-z0 ]){1,30}$", "Variable 7 - SegundoApellido - Permite solo letras y un cero (0) con una longitud de 30 Caracteres")] public string SegundoApellido { get { return _SegundoApellido; } set { _SegundoApellido = ValidateValue<string>(value, nameof(SegundoApellido)); } }
        /// <sumary>
        /// IdDocumentType (TI,CC,CE,PA,RC,UN,RNV,MS,AS)
        /// </sumary>
        private string _TipoIdentificacion;
        [Order]
        [Regex(@"^(TI|CC|CE|PA|RC|UN|RNV|MS|AS)$", "Variable 8 - TipoIdentificacion - El valor ingresado no es válido. Valores válidos: TI, CC, CE, PA, RC, UN, RNV, MS, AS")] public string TipoIdentificacion { get { return _TipoIdentificacion; } set { _TipoIdentificacion = ValidateValue<string>(value, nameof(TipoIdentificacion)); } }
        /// <sumary>
        /// DocumentNumber  
        /// </sumary>
        private string _NumeroDocumento;
        [Order]
        [Regex(@"^.{1,20}$", "Variable 9 - NumeroDocumento - El campo no puede estar vacío y la longitud debe estar entre 1 y 20 caracteres")] public string NumeroDocumento { get { return _NumeroDocumento; } set { _NumeroDocumento = ValidateValue<string>(value, nameof(NumeroDocumento)); } }
        /// <sumary>
        /// AAAA-MM-DD
        /// </sumary>
        private string _FechaNacimiento;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))$", "Variable 10 - FechaNacimiento - El formato de la fecha ingresada no es correcto debe ingresar en formato  (AAAA-MM-DD)")] public string FechaNacimiento { get { return _FechaNacimiento; } set { _FechaNacimiento = ValidateValue<string>(value, nameof(FechaNacimiento)); } }
        /// <sumary>
        /// IdSex (F,M)
        /// </sumary>
        private string _Sexo;
        [Order]
        [Regex(@"^(M|F)$", "Variable 11 - Sexo - El valor ingresado no es válido. Valores válidos: M,F")] public string Sexo { get { return _Sexo; } set { _Sexo = ValidateValue<string>(value, nameof(Sexo)); } }
        /// <sumary>
        /// CodeEthnic (0,1,2,3,4)
        /// </sumary>
        private string _PertenenciaEtnica;
        [Order]
        [Regex(@"^[0-4]{1}$", "Variable 12 - PertenenciaEtnica - Sólo se permiten números (0,1,2,3,4) y la longitud válida 1 carácter")] public string PertenenciaEtnica { get { return _PertenenciaEtnica; } set { _PertenenciaEtnica = ValidateValue<string>(value, nameof(PertenenciaEtnica)); } }
        /// <sumary>
        /// HomeAdress
        /// </sumary>
        private string _DireccionResidencia;
        [Order]
        [Regex(@"^.{1,50}$", "Variable 13 - DireccionResidencia - El campo no puede ir vacio y su longitud max es de 50")] public string DireccionResidencia { get { return _DireccionResidencia; } set { _DireccionResidencia = ValidateValue<string>(value, nameof(DireccionResidencia)); } }
        /// <sumary>
        /// PhoneNumber
        /// </sumary>
        private string _TelefonoContacto;
        [Order]
        [Regex(@"^[0-9 ]{1,30}$", "Variable 14 - TelefonoContacto - Solo acepta numeros sin caracteres especiales, no permite campos vacios, max 30")] public string TelefonoContacto { get { return _TelefonoContacto; } set { _TelefonoContacto = ValidateValue<string>(value, nameof(TelefonoContacto)); } }
        /// <sumary>
        /// MunicipalityResidence
        /// </sumary>
        private string _CodigoMunicipioRes;
        [Order]
        [Regex(@"^([0-9]{1,5})$", "Variable 15 - CodigoMunicipioRes - Permite solo numeros con una longitud de 5 Caracteres")] public string CodigoMunicipioRes { get { return _CodigoMunicipioRes; } set { _CodigoMunicipioRes = ValidateValue<string>(value, nameof(CodigoMunicipioRes)); } }
        /// <sumary>
        /// AffiliationDate (AAAA-MM-DD )
        /// </sumary>
        private string _FechaAfiliacionEPS;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))(\-)([0-2][0-9]|(3)[0-1])$", "Variable 16 - FechaAfiliacionEPS - El formato de la fecha ingresada no es correcto, valor permitido (AAAA-MM-DD)")] public string FechaAfiliacionEPS { get { return _FechaAfiliacionEPS; } set { _FechaAfiliacionEPS = ValidateValue<string>(value, nameof(FechaAfiliacionEPS)); } }
        /// <sumary>
        /// PregnantPerson (0,1,2,3)
        /// </sumary>
        private string _PersonaGestante;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 17 - PersonaGestante - Sólo se permiten números y la longitud válida es 1 caracter")] public string PersonaGestante { get { return _PersonaGestante; } set { _PersonaGestante = ValidateValue<string>(value, nameof(PersonaGestante)); } }
        /// <sumary>
        /// TuberculosisPerson (0, 1,2, 3) 
        /// </sumary>
        private string _PersonaConTuberculosis;
        [Order]
        [Regex(@"^[0-3]{1}$", "Variable 18 - PersonaConTuberculosis - Sólo se permiten números y la longitud válida es 1 caracter")] public string PersonaConTuberculosis { get { return _PersonaConTuberculosis; } set { _PersonaConTuberculosis = ValidateValue<string>(value, nameof(PersonaConTuberculosis)); } }
        /// <sumary>
        /// ChildUnderMomVIH (0, 1) (Persona Menor de 18 meses que es hijo de madre con VIH) 
        /// </sumary>
        private string _PersonaMenor18MesesVIH;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 19 - PersonaMenor18MesesVIH - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string PersonaMenor18MesesVIH { get { return _PersonaMenor18MesesVIH; } set { _PersonaMenor18MesesVIH = ValidateValue<string>(value, nameof(PersonaMenor18MesesVIH)); } }
        /// <sumary>
        /// ConditionDiagnosishivInfection (1, 2, 3, 4) (Condición con respecto al diagnóstico de infección por VIH)
        /// </sumary>
        private string _CondicionConRespectoDiagnosticoVIH;
        [Order]
        [Regex(@"^[0-4]{1}$", "Variable 20 - CondicionConRespectoDiagnosticoVIH - Sólo se permiten números 0,1,2,3,4")] public string CondicionConRespectoDiagnosticoVIH { get { return _CondicionConRespectoDiagnosticoVIH; } set { _CondicionConRespectoDiagnosticoVIH = ValidateValue<string>(value, nameof(CondicionConRespectoDiagnosticoVIH)); } }
        /// <sumary>
        /// DateElisaTest  Formato AAAA-MM
        /// </sumary>
        private string _FechaPruebaPresuntivaElisa;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))$|^(2222-22|9999-99|\d{4}-99)$", "Variable 21 - FechaPruebaPresuntivaElisa - Solo acepta formato fecha valido AAAA-MM o acepta 1111-11 | 2222-22 | 9999-99| AÑO-99")] public string FechaPruebaPresuntivaElisa { get { return _FechaPruebaPresuntivaElisa; } set { _FechaPruebaPresuntivaElisa = ValidateValue<string>(value, nameof(FechaPruebaPresuntivaElisa)); } }
        /// <sumary>
        /// DateDiagnosishiv Formato AAAA-MM
        /// </sumary>
        private string _FechaDiagnosticoInfeccionVIH;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))$|^(0000-00|9999-99|\d{4}-99)$", "Variable 22 - FechaDiagnosticoInfeccionVIH - Solo acepta formato fecha valido AAAA-MM o acepta 0000-00 | 9999-99 | AÑO-99")] public string FechaDiagnosticoInfeccionVIH { get { return _FechaDiagnosticoInfeccionVIH; } set { _FechaDiagnosticoInfeccionVIH = ValidateValue<string>(value, nameof(FechaDiagnosticoInfeccionVIH)); } }
        /// <sumary>
        /// HowCamePresumtiveTest (1,2,3,4,5, 6, 7, 8, 9, 10) Cómo llegó a la prueba presuntiva 
        /// </sumary>
        private string _PruebaPresuntiva;
        [Order]
        [Regex(@"^[1-9]$|^[1-9]$|^(10)$", "Variable 23 - PruebaPresuntiva - Sólo se permiten números (1,2,3,4,5, 6, 7, 8, 9, 10)")] public string PruebaPresuntiva { get { return _PruebaPresuntiva; } set { _PruebaPresuntiva = ValidateValue<string>(value, nameof(PruebaPresuntiva)); } }
        /// <sumary>
        /// AssuranceTimeDiagnosis (1, 2, 3, 4, 5, 6, 9)  Aseguramiento al momento del diagnóstico
        /// </sumary>
        private string _AseguramientoMomentoDelDiagnostico;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 24 - AseguramientoMomentoDelDiagnostico - Sólo se permiten números y la longitud válida es 1 caracter")] public string AseguramientoMomentoDelDiagnostico { get { return _AseguramientoMomentoDelDiagnostico; } set { _AseguramientoMomentoDelDiagnostico = ValidateValue<string>(value, nameof(AseguramientoMomentoDelDiagnostico)); } }
        /// <sumary>
        /// LastCodeEps EPS o Entidad Territorial anterior Campo 25
        /// </sumary>
        private string _EntidadTerritorialAnterior;
        [Order]
        [Regex(@"^[0-9a-zA-Z]+$", "Variable 25 - EntidadTerritorialAnterior - Sólo se permiten números y letras")] public string EntidadTerritorialAnterior { get { return _EntidadTerritorialAnterior; } set { _EntidadTerritorialAnterior = ValidateValue<string>(value, nameof(EntidadTerritorialAnterior)); } }
        /// <sumary>
        /// Variable 26 - MecanismoTransmision - TransmissionMechanism (0,1, 2, 3, 4, 5, 6, 9) Mecanismo de transmisión
        /// </sumary>
        private string _MecanismoTransmision;
        [Order]
        [Regex(@"^(0|1|2|3|4|5|6|9)$", "Variable 26 - MecanismoTransmision - Sólo se permiten números (0,1,2,3,4,5,6,9)")] public string MecanismoTransmision { get { return _MecanismoTransmision; } set { _MecanismoTransmision = ValidateValue<string>(value, nameof(MecanismoTransmision)); } }
        /// <sumary>
        /// ClinicalStageDiagnosiSolder13 ({1, 2, 3,4,5, 6, 7, 8, 9, 10, 11)  Estadio clínico al momento del diagnóstico
        /// </sumary>
        private string _EstadioClinicoMomentoDelDiagnostico;
        [Order]
        [Regex(@"^([1-9]|1[0-1])$", "Variable 27 - EstadioClinicoMomentoDelDiagnostico - Sólo se permiten números (1, 2, 3,4,5, 6, 7, 8, 9, 10, 11)")] public string EstadioClinicoMomentoDelDiagnostico { get { return _EstadioClinicoMomentoDelDiagnostico; } set { _EstadioClinicoMomentoDelDiagnostico = ValidateValue<string>(value, nameof(EstadioClinicoMomentoDelDiagnostico)); } }
        /// <sumary>
        /// Estadio clínico al momento del diagnóstico (Adolescentes de 13 años en adelante y adultos)
        /// </sumary>
        private string _EstadioClinicoMomentoDelDiagnosticoAdolecentes;
        [Order]
        [Regex(@"^([1-9]|1[0-4])$", "Variable 27.1 - EstadioClinicoMomentoDelDiagnosticoAdolecentes - Sólo se permiten números (1, 2, 3,4,5, 6, 7, 8, 9, 10, 11,12,13,14)")] public string EstadioClinicoMomentoDelDiagnosticoAdolecentes { get { return _EstadioClinicoMomentoDelDiagnosticoAdolecentes; } set { _EstadioClinicoMomentoDelDiagnosticoAdolecentes = ValidateValue<string>(value, nameof(EstadioClinicoMomentoDelDiagnosticoAdolecentes)); } }
        /// <sumary>
        /// ConteoLinfocitosTCD4Diagnostico Solo permite numeros de (0 a 99005)
        /// </sumary>
        private string _ConteoLinfocitosTCD4Diagnostico;
        [Order]
        [Regex(@"^[0-9]{1,5}$", "Variable 28 - ConteoLinfocitosTCD4Diagnostico - Actualizado Solo permite numeros de (0 a 99005)")] public string ConteoLinfocitosTCD4Diagnostico { get { return _ConteoLinfocitosTCD4Diagnostico; } set { _ConteoLinfocitosTCD4Diagnostico = ValidateValue<string>(value, nameof(ConteoLinfocitosTCD4Diagnostico)); } }
        /// <sumary>
        /// DiagnosisTotallymphocyteCount cifra absoluta, linfocitos totales (1 a 99005) 
        /// </sumary>
        private string _ConteoLinfocitosTotalesMomentoDiagnostico;
        [Order]
        [Regex(@"^[0-9]{1,5}$", "Variable 29 - ConteoLinfocitosTotalesMomentoDiagnostico - Solo permite numeros de (1 a 99005)")] public string ConteoLinfocitosTotalesMomentoDiagnostico { get { return _ConteoLinfocitosTotalesMomentoDiagnostico; } set { _ConteoLinfocitosTotalesMomentoDiagnostico = ValidateValue<string>(value, nameof(ConteoLinfocitosTotalesMomentoDiagnostico)); } }
        /// <sumary>
        /// StartDateTar (Fecha inicio de TAR)
        /// </sumary>
        private string _FechaInicioTAR;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))$", "Variable 30 - FechaInicioTAR - Solo acepta una fecha valida AAAA-MM o (0000-00 - 9999-00)")] public string FechaInicioTAR { get { return _FechaInicioTAR; } set { _FechaInicioTAR = ValidateValue<string>(value, nameof(FechaInicioTAR)); } }
        /// <sumary>
        /// Tcd4LymphocyteCountBeginningTar (valores posibles cifra absoluta CD4 0 a 99005 )
        /// </sumary>
        private string _ConteoLinfocitosTCD4MomentoInicioTAR;
        [Order]
        [Regex(@"^[0-9]{1,5}$", "Variable 31 - ConteoLinfocitosTCD4MomentoInicioTAR - Solo permite numeros de (0 a 99005)")] public string ConteoLinfocitosTCD4MomentoInicioTAR { get { return _ConteoLinfocitosTCD4MomentoInicioTAR; } set { _ConteoLinfocitosTCD4MomentoInicioTAR = ValidateValue<string>(value, nameof(ConteoLinfocitosTCD4MomentoInicioTAR)); } }
        /// <sumary>
        /// TotalLymphocyteCountBeginningTar (Conteo de linfocitos totales al momento de inicio de la TAR ) cifra absoluta, linfocitos totales {1 a 99005} 
        /// </sumary>
        private string _ConteoLinfocitosTotalesInicioTar;
        [Order]
        [Regex(@"^[0-9]{1,5}$", "Variable 32 - ConteoLinfocitosTotalesInicioTar - Solo permite numeros de (1 a 99005)")] public string ConteoLinfocitosTotalesInicioTar { get { return _ConteoLinfocitosTotalesInicioTar; } set { _ConteoLinfocitosTotalesInicioTar = ValidateValue<string>(value, nameof(ConteoLinfocitosTotalesInicioTar)); } }
        /// <sumary>
        /// ViralLoadBeginningTar ((0,1,2,3,4,5, 6, 9) Carga Viral al inicio de TAR )
        /// </sumary>
        private string _CargaViralInicioTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|5|6|9)$", "Variable 33 - CargaViralInicioTAR - Sólo se permiten números (0,1,2,3,4,5,6,9)")] public string CargaViralInicioTAR { get { return _CargaViralInicioTAR; } set { _CargaViralInicioTAR = ValidateValue<string>(value, nameof(CargaViralInicioTAR)); } }
        /// <sumary>
        /// ReasonBeginningTar (1,2,3,4,5,6,7,8,9,10 ) Motivo de inicio de la TAR
        /// </sumary>
        private string _MotivoInicioTAR;
        [Order]
        [Regex(@"^[0-9]$|^[1-9]$|^(10)$", "Variable 34 - MotivoInicioTAR - Sólo se permiten números (1,2,3,4,5,6,7,8,9,10)")] public string MotivoInicioTAR { get { return _MotivoInicioTAR; } set { _MotivoInicioTAR = ValidateValue<string>(value, nameof(MotivoInicioTAR)); } }
        /// <sumary>
        /// HadanemiaBeginningTar (0,1,2, 3, 4, 9) Tenía Anemia al iniciar TAR 
        /// </sumary>
        private string _TeniaAnemiaAlIniciar;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 35 - TeniaAnemiaAlIniciar - Sólo se permiten números (0,1,2,3,4,9)")] public string TeniaAnemiaAlIniciar { get { return _TeniaAnemiaAlIniciar; } set { _TeniaAnemiaAlIniciar = ValidateValue<string>(value, nameof(TeniaAnemiaAlIniciar)); } }
        /// <sumary>
        /// ChronickIdNeydiseaseBeginningTar (0,1,2, 3, 4, 9) Tenía Enfermedad renal crónica al iniciar TAR  
        /// </sumary>
        private string _EnfermedadRenalCronicaInicioTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 36 - EnfermedadRenalCronicaInicioTAR - Sólo se permiten números (0,1,2,3,4,9)")] public string EnfermedadRenalCronicaInicioTAR { get { return _EnfermedadRenalCronicaInicioTAR; } set { _EnfermedadRenalCronicaInicioTAR = ValidateValue<string>(value, nameof(EnfermedadRenalCronicaInicioTAR)); } }
        /// <sumary>
        /// CoInfectionvhbBeginningTar (0,1,2, 3, 4, 9) Tenía Coinfección con el VHB al iniciar TAR 
        /// </sumary>
        private string _CoinfeccionVHBIniciarTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 37 - CoinfeccionVHBIniciarTAR - Sólo se permiten números (0,1,2,3,4,9)")] public string CoinfeccionVHBIniciarTAR { get { return _CoinfeccionVHBIniciarTAR; } set { _CoinfeccionVHBIniciarTAR = ValidateValue<string>(value, nameof(CoinfeccionVHBIniciarTAR)); } }
        /// <sumary>
        /// CoInfectionvhcBeginningTar (0,1,2, 3, 4, 9) Tenía Coinfección con el VHC al iniciar TAR 
        /// </sumary>
        private string _CoinfeccionVHCIniciarTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 38 - CoinfeccionVHCIniciarTAR - Sólo se permiten números (0,1,2,3,4,9)")] public string CoinfeccionVHCIniciarTAR { get { return _CoinfeccionVHCIniciarTAR; } set { _CoinfeccionVHCIniciarTAR = ValidateValue<string>(value, nameof(CoinfeccionVHCIniciarTAR)); } }
        /// <sumary>
        /// TuberculosisBeginningTar (0,1,2,3, 4, 9) Tenía Tuberculosis al iniciar TAR 
        /// </sumary>
        private string _TuberculosisInicioTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 39 - TuberculosisInicioTAR - Sólo se permiten números (0,1,2,3,4,9)")] public string TuberculosisInicioTAR { get { return _TuberculosisInicioTAR; } set { _TuberculosisInicioTAR = ValidateValue<string>(value, nameof(TuberculosisInicioTAR)); } }
        /// <sumary>
        /// CardiovascularSurgeryBeginningTar (0,1,2, 3, 4, 9) Tenía Cirugía cardiovascular o infarto previo al inicio de la TAR 
        /// </sumary>
        private string _CirugiaCardiovascularInicioTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 40 - CirugiaCardiovascularInicioTAR - Sólo se permiten números (0,1,2,3,4,9)")] public string CirugiaCardiovascularInicioTAR { get { return _CirugiaCardiovascularInicioTAR; } set { _CirugiaCardiovascularInicioTAR = ValidateValue<string>(value, nameof(CirugiaCardiovascularInicioTAR)); } }
        /// <sumary>
        /// KaposisarcomaBeginningTar (0,1,2,3,4,9)  Tenía Sarcoma de Kaposi al iniciar TAR
        /// </sumary>
        private string _SarcomaKaposiInicioTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 41 - SarcomaKaposiInicioTAR - Sólo se permiten números (0,1,2,3,4,9)")] public string SarcomaKaposiInicioTAR { get { return _SarcomaKaposiInicioTAR; } set { _SarcomaKaposiInicioTAR = ValidateValue<string>(value, nameof(SarcomaKaposiInicioTAR)); } }
        /// <sumary>
        /// PregnantBeginningTar (0,1,2,3,4,5,9) 
        /// </sumary>
        private string _EmbarazoInicioTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|5|9)$", "Variable 42 - EmbarazoInicioTAR - Sólo se permiten números (0,1,2,3,4,5,9)")] public string EmbarazoInicioTAR { get { return _EmbarazoInicioTAR; } set { _EmbarazoInicioTAR = ValidateValue<string>(value, nameof(EmbarazoInicioTAR)); } }
        /// <sumary>
        /// PsychiatricillnessBeginningTar (0,1,2,3,4,9) 
        /// </sumary>
        private string _EnfermedadPsiquiatricaInicioTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 43 - EnfermedadPsiquiatricaInicioTAR - Sólo se permiten números (0,1,2,3,4,9)")] public string EnfermedadPsiquiatricaInicioTAR { get { return _EnfermedadPsiquiatricaInicioTAR; } set { _EnfermedadPsiquiatricaInicioTAR = ValidateValue<string>(value, nameof(EnfermedadPsiquiatricaInicioTAR)); } }
        /// <sumary>
        /// AbacavirBeginningTar (0,1,9)  Al inicio de la TAR recibió ABACAVIR 
        /// </sumary>
        private string _InicioAbacavirTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.1 - InicioAbacavirTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioAbacavirTAR { get { return _InicioAbacavirTAR; } set { _InicioAbacavirTAR = ValidateValue<string>(value, nameof(InicioAbacavirTAR)); } }
        /// <sumary>
        /// AtazanavirBeginningTar (0,1,9) Al inicio de la TAR recibió ATAZANAVIR 
        /// </sumary>
        private string _InicioAtazanavirTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.2 - InicioAtazanavirTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioAtazanavirTAR { get { return _InicioAtazanavirTAR; } set { _InicioAtazanavirTAR = ValidateValue<string>(value, nameof(InicioAtazanavirTAR)); } }
        /// <sumary>
        /// DidanosinaBeginningTar (0,1,9)   Al inicio de la TAR recibió DIDANOSINA 
        /// </sumary>
        private string _InicioDidanosinaTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.3 - InicioDidanosinaTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioDidanosinaTAR { get { return _InicioDidanosinaTAR; } set { _InicioDidanosinaTAR = ValidateValue<string>(value, nameof(InicioDidanosinaTAR)); } }
        /// <sumary>
        /// EfavirenzBeginningTar (0,1,9)
        /// </sumary>
        private string _InicioEfavirenzTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.4 - InicioEfavirenzTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioEfavirenzTAR { get { return _InicioEfavirenzTAR; } set { _InicioEfavirenzTAR = ValidateValue<string>(value, nameof(InicioEfavirenzTAR)); } }
        /// <sumary>
        /// EstavudinaBeginningTar (0,1,9) Al inicio de la TAR recibió ESTAVUDINA 
        /// </sumary>
        private string _InicioEstavudinaTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.5 - InicioEstavudinaTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioEstavudinaTAR { get { return _InicioEstavudinaTAR; } set { _InicioEstavudinaTAR = ValidateValue<string>(value, nameof(InicioEstavudinaTAR)); } }
        /// <sumary>
        /// FosamprenavirBeginningTar (0,1,9) Al inicio de la TAR recibió FOSAMPRENAVIR 
        /// </sumary>
        private string _InicioFosamprenavirTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.6 - InicioFosamprenavirTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioFosamprenavirTAR { get { return _InicioFosamprenavirTAR; } set { _InicioFosamprenavirTAR = ValidateValue<string>(value, nameof(InicioFosamprenavirTAR)); } }
        /// <sumary>
        /// IndinavirBeginningTar (0,1,9) Al inicio de la TAR recibió INDINAVIR 
        /// </sumary>
        private string _InicioIndinavirTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.7 - InicioIndinavirTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioIndinavirTAR { get { return _InicioIndinavirTAR; } set { _InicioIndinavirTAR = ValidateValue<string>(value, nameof(InicioIndinavirTAR)); } }
        /// <sumary>
        /// LamivudinaBeginningTar (0,1,9) Al inicio de la TAR recibió LAMIVUDINA
        /// </sumary>
        private string _InicioLamivudinaTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.8 - InicioLamivudinaTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioLamivudinaTAR { get { return _InicioLamivudinaTAR; } set { _InicioLamivudinaTAR = ValidateValue<string>(value, nameof(InicioLamivudinaTAR)); } }
        /// <sumary>
        /// LopinavirBeginningTar (0,1,9) Al inicio de la TAR recibió LOPINAVIR  
        /// </sumary>
        private string _InicioLopinavirTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.9 - InicioLopinavirTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioLopinavirTAR { get { return _InicioLopinavirTAR; } set { _InicioLopinavirTAR = ValidateValue<string>(value, nameof(InicioLopinavirTAR)); } }
        /// <sumary>
        /// NevirapinaBeginningTar (0,1,9) Al inicio de la TAR recibió NEVIRAPINA 
        /// </sumary>
        private string _InicioNevirapinaTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.10 - InicioNevirapinaTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioNevirapinaTAR { get { return _InicioNevirapinaTAR; } set { _InicioNevirapinaTAR = ValidateValue<string>(value, nameof(InicioNevirapinaTAR)); } }
        /// <sumary>
        /// NelfinavirBeginningTa (0,1,9) Al inicio de la TAR recibió NELFINAVIR  
        /// </sumary>
        private string _InicioNelfinavirTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.11 - InicioNelfinavirTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioNelfinavirTAR { get { return _InicioNelfinavirTAR; } set { _InicioNelfinavirTAR = ValidateValue<string>(value, nameof(InicioNelfinavirTAR)); } }
        /// <sumary>
        /// RitonavirBeginningTar (0,1,9)  Al inicio de la TAR recibió RITONAVIR
        /// </sumary>
        private string _InicioRitonavirTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.12 - InicioRitonavirTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioRitonavirTAR { get { return _InicioRitonavirTAR; } set { _InicioRitonavirTAR = ValidateValue<string>(value, nameof(InicioRitonavirTAR)); } }
        /// <sumary>
        /// SaquinavirBeginningTar (0,1,9)   Al inicio de la TAR recibió SAQUINAVIR
        /// </sumary>
        private string _InicioSaquinavirTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.13 - InicioSaquinavirTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioSaquinavirTAR { get { return _InicioSaquinavirTAR; } set { _InicioSaquinavirTAR = ValidateValue<string>(value, nameof(InicioSaquinavirTAR)); } }
        /// <sumary>
        /// ZidovudinaBeginningTar (0,1,9) Al inicio de la TAR recibió ZIDOVUDINA 
        /// </sumary>
        private string _InicioZidovudinaTAR;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.14 - InicioZidovudinaTAR - Sólo se permiten números (0|1|9) y la longitud válida es 1 caracter")] public string InicioZidovudinaTAR { get { return _InicioZidovudinaTAR; } set { _InicioZidovudinaTAR = ValidateValue<string>(value, nameof(InicioZidovudinaTAR)); } }
        /// <sumary>
        /// TenofovirBeginningTar En la TAR inicial recibió Medicamento NO POS (medicamento 1) 
        /// </sumary>
        private string _InicioTenofovirTARMed1;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.15 - InicioTenofovirTARMed1 - Solo permite (0,1,9) Resolucion 2012")] public string InicioTenofovirTARMed1 { get { return _InicioTenofovirTARMed1; } set { _InicioTenofovirTARMed1 = ValidateValue<string>(value, nameof(InicioTenofovirTARMed1)); } }
        /// <sumary>
        /// EmtricitabinaBeginningTar En la TAR inicial recibió Medicamento NO POS (medicamento 2) 
        /// </sumary>
        private string _InicioEmtricitabinaTARMed2;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 44.16 - InicioEmtricitabinaTARMed2 - Solo permite (0,1,9) Resolucion 2012")] public string InicioEmtricitabinaTARMed2 { get { return _InicioEmtricitabinaTARMed2; } set { _InicioEmtricitabinaTARMed2 = ValidateValue<string>(value, nameof(InicioEmtricitabinaTARMed2)); } }
        /// <sumary>
        /// NonposMedication3BeginningTar (En la TAR inicial recibió Medicamento NO POS (medicamento 3) )
        /// </sumary>
        private string _InicioMedicamento3NoposTAR;
        [Order]
        [Regex(@"^[0-9-]+$", "Variable 44.17 - InicioMedicamento3NoposTAR - Solo permite codigo cum + (0,1)")] public string InicioMedicamento3NoposTAR { get { return _InicioMedicamento3NoposTAR; } set { _InicioMedicamento3NoposTAR = ValidateValue<string>(value, nameof(InicioMedicamento3NoposTAR)); } }
        /// <sumary>
        /// NonposMedication4BeginningTar (En la TAR inicial recibió Medicamento NO POS (medicamento 4) )
        /// </sumary>
        private string _InicioMedicamento4NoposTAR;
        [Order]
        [Regex(@"^[0-9-]+$", "Variable 44.18 - InicioMedicamento4NoposTAR - Solo permite codigo cum + (0,1)")] public string InicioMedicamento4NoposTAR { get { return _InicioMedicamento4NoposTAR; } set { _InicioMedicamento4NoposTAR = ValidateValue<string>(value, nameof(InicioMedicamento4NoposTAR)); } }
        /// <sumary>
        /// NonposMedication5BeginningTar (En la TAR inicial recibió Medicamento NO POS (medicamento 5))
        /// </sumary>
        private string _InicioMedicamento5NoposTAR;
        [Order]
        [Regex(@"^[0-9-]+$", "Variable 44.19 - InicioMedicamento5NoposTAR - Solo permite codigo cum + (0,1)")] public string InicioMedicamento5NoposTAR { get { return _InicioMedicamento5NoposTAR; } set { _InicioMedicamento5NoposTAR = ValidateValue<string>(value, nameof(InicioMedicamento5NoposTAR)); } }
        /// <sumary>
        /// NonposMedication6BeginningTar (En la TAR inicial recibió Medicamento NO POS (medicamento 6) )
        /// </sumary>
        private string _InicioMedicamento6NoposTAR;
        [Order]
        [Regex(@"^[0-9a-zA-Z]+$", "Variable 44.20 - InicioMedicamento6NoposTAR - Sólo se permiten números y letras")] public string InicioMedicamento6NoposTAR { get { return _InicioMedicamento6NoposTAR; } set { _InicioMedicamento6NoposTAR = ValidateValue<string>(value, nameof(InicioMedicamento6NoposTAR)); } }
        /// <sumary>
        /// ReceiveDadviceBeginningTar (0,1,2,3,4,9)  Recibió asesoría antes de iniciar TAR 
        /// </sumary>
        private string _RecibioAsesoriaInicioTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 45 - RecibioAsesoriaInicioTAR - Sólo se permiten números (0,1,2,3,4,9)")] public string RecibioAsesoriaInicioTAR { get { return _RecibioAsesoriaInicioTAR; } set { _RecibioAsesoriaInicioTAR = ValidateValue<string>(value, nameof(RecibioAsesoriaInicioTAR)); } }
        /// <sumary>
        /// MonthTarFormulaDispensed (valores posibles 0 a 12, desde enero 2011) (Número de meses que se dispensó la fórmula completa de TAR durante los primeros 12 meses luego de iniciar TAR )
        /// </sumary>
        private string _NumeroMesesFormulaTAR;
        [Order]
        [Regex(@"^[0-9]{1,2}$", "Variable 46 - NumeroMesesFormulaTAR - Sólo se permiten números y la longitud válida es 2 caracteres")] public string NumeroMesesFormulaTAR { get { return _NumeroMesesFormulaTAR; } set { _NumeroMesesFormulaTAR = ValidateValue<string>(value, nameof(NumeroMesesFormulaTAR)); } }
        /// <sumary>
        /// MedicalapPointMentSattended (0 y 50, 97, 98, 99)  (Alguno de los medicamentos con los que inició TAR ha sido cambiado, por cualquier motivo )
        /// </sumary>
        private string _NumeroCitasMedicasPrimeros12Meses;
        [Order]
        [Regex(@"^([0-9]|1[0-9]|2[0-9]|3[0-9]|4[0-9]|5[0-0])$|^(97|98|99)$", "Variable 47 - NumeroCitasMedicasPrimeros12Meses - Solo permite numeros de 0 a 50 o 98-99")] public string NumeroCitasMedicasPrimeros12Meses { get { return _NumeroCitasMedicasPrimeros12Meses; } set { _NumeroCitasMedicasPrimeros12Meses = ValidateValue<string>(value, nameof(NumeroCitasMedicasPrimeros12Meses)); } }
        /// <sumary>
        /// Valores permitidos (0,1,2,3,4,9)  Alguno de los medicamentos con los que inició TAR ha sido cambiado, por cualquier motivo 
        /// </sumary>
        private string _MedicamentosCambiadosIncioTAR;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 48 - MedicamentosCambiadosIncioTAR - Sólo se permiten números (0,1,2,3,4,9)")] public string MedicamentosCambiadosIncioTAR { get { return _MedicamentosCambiadosIncioTAR; } set { _MedicamentosCambiadosIncioTAR = ValidateValue<string>(value, nameof(MedicamentosCambiadosIncioTAR)); } }
        /// <sumary>
        /// DateFirstChangeMedicationInitialTar (AAAA-MM ) Fecha del primer cambio de cualquier medicamento del esquema inicial de TAR
        /// </sumary>
        private string _FechaPrimerCambioMedInicioTAR;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))$|^(7777-77|8888-88|9999-99|\d{4}-99)$", "Variable 49 - FechaPrimerCambioMedInicioTAR - Solo acepta formato fecha valido AAAA-MM o acepta (0000-00 | 7777-77 | 8888-88 |9999-99)")] public string FechaPrimerCambioMedInicioTAR { get { return _FechaPrimerCambioMedInicioTAR; } set { _FechaPrimerCambioMedInicioTAR = ValidateValue<string>(value, nameof(FechaPrimerCambioMedInicioTAR)); } }
        /// <sumary>
        /// CauseChangeMedicationInitialTar (1, 2, 3, 4, 5, 6, 9) Causa de cambio de medicamento con el que inició la TAR
        /// </sumary>
        private string _CausaCambioMedicamentoInicioTAR;
        [Order]
        [Regex(@"^(1|2|3|4|5|6|9)$", "Variable 50 - CausaCambioMedicamentoInicioTAR - Sólo se permiten números (1,2,3,4,5,6,9)")] public string CausaCambioMedicamentoInicioTAR { get { return _CausaCambioMedicamentoInicioTAR; } set { _CausaCambioMedicamentoInicioTAR = ValidateValue<string>(value, nameof(CausaCambioMedicamentoInicioTAR)); } }
        /// <sumary>
        /// NumberFailuressTartTar (valores posibles (cantidad de fallas de 0 a 20, y 99) ) Número de fallas desde el inicio de la TAR hasta el reporte actual 
        /// </sumary>
        private string _NumerosFallasInicioTAR;
        [Order]
        [Regex(@"^([0-9]|1[0-9]|2[0-9])$|^(99)$|^([a-zA-Z]{1,2})", "Variable 51 - NumerosFallasInicioTAR - Solo permite numeros de 0 a 20 o 99")] public string NumerosFallasInicioTAR { get { return _NumerosFallasInicioTAR; } set { _NumerosFallasInicioTAR = ValidateValue<string>(value, nameof(NumerosFallasInicioTAR)); } }
        /// <sumary>
        /// Numérico (int) valores posibles (cantidad de cambios de 0 a 40, y 99)  Número de cambios de medicamentos de TAR por todas las causas hasta el reporte actual 
        /// </sumary>
        private string _NumeroCambiosMedCausaActual;
        [Order]
        [Regex(@"^([0-9]|1[0-9]|2[0-9]|3[0-9]|4[0-9])$|^(99)$|^([a-zA-Z]{1,2})", "Variable 52 - NumeroCambiosMedCausaActual - Solo permite numeros de 0 a 40 o 99 ")] public string NumeroCambiosMedCausaActual { get { return _NumeroCambiosMedCausaActual; } set { _NumeroCambiosMedCausaActual = ValidateValue<string>(value, nameof(NumeroCambiosMedCausaActual)); } }
        /// <sumary>
        /// Valores posibles (0, 1)  Tiene o ha tenido Candidiasis esofágica, traqueal, bronquial o pulmonar 
        /// </sumary>
        private string _CandidiasisPulmonar;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.1 - CandidiasisPulmonar - Solo permite (0,1)")] public string CandidiasisPulmonar { get { return _CandidiasisPulmonar; } set { _CandidiasisPulmonar = ValidateValue<string>(value, nameof(CandidiasisPulmonar)); } }
        /// <sumary>
        /// PulmonaryTuberculosis (valores posibles (0, 1)) Tiene o ha tenido Tuberculosis extra pulmonar o pulmonar 
        /// </sumary>
        private string _TuberculosisExtraPulmonar;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.2 - TuberculosisExtraPulmonar - Solo permite (0,1)")] public string TuberculosisExtraPulmonar { get { return _TuberculosisExtraPulmonar; } set { _TuberculosisExtraPulmonar = ValidateValue<string>(value, nameof(TuberculosisExtraPulmonar)); } }
        /// <sumary>
        /// valores posibles (0, 1)  Tiene o ha tenido Cáncer de cérvix invasivo 
        /// </sumary>
        private string _CancerCervixInvasivo;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.3 - CancerCervixInvasivo - Solo permite (0,1)")] public string CancerCervixInvasivo { get { return _CancerCervixInvasivo; } set { _CancerCervixInvasivo = ValidateValue<string>(value, nameof(CancerCervixInvasivo)); } }
        /// <sumary>
        /// DementiaAssociatedvih valores posibles (0, 1) Tiene o ha tenido demencia asociada al VIH (deterioro cognitivo o de otras funciones que interfiere con las actividades laborales o/y rutinarias)  
        /// </sumary>
        private string _DemensiaAsociadaVIH;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.4 - DemensiaAsociadaVIH - Solo permite (0,1)")] public string DemensiaAsociadaVIH { get { return _DemensiaAsociadaVIH; } set { _DemensiaAsociadaVIH = ValidateValue<string>(value, nameof(DemensiaAsociadaVIH)); } }
        /// <sumary>
        ///  (valores posibles (0, 1)) Tiene o ha tenido Coccidioidomicosisextrapulmonar
        /// </sumary>
        private string _CoccidioidomIcosisExtraPulmonar;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.5 - CoccidioidomIcosisExtraPulmonar - Solo permite (0,1)")] public string CoccidioidomIcosisExtraPulmonar { get { return _CoccidioidomIcosisExtraPulmonar; } set { _CoccidioidomIcosisExtraPulmonar = ValidateValue<string>(value, nameof(CoccidioidomIcosisExtraPulmonar)); } }
        /// <sumary>
        /// (valores posibles (0, 1) ) Tiene o ha tenido Infección por Citomegalovírus (CMV) de cualquier órgano excepto hígado, bazo, o ganglios linfáticos
        /// </sumary>
        private string _InfeccionCitomegalovirus;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.6 - InfeccionCitomegalovirus - Solo permite (0,1)")] public string InfeccionCitomegalovirus { get { return _InfeccionCitomegalovirus; } set { _InfeccionCitomegalovirus = ValidateValue<string>(value, nameof(InfeccionCitomegalovirus)); } }
        /// <sumary>
        /// SimpleHerpes
        /// </sumary>
        private string _HerpesSimple;
        [Order]
        [Regex(@"[0|1]$", "Variable 53.7 - HerpesSimple - Solo permite (0,1)")] public string HerpesSimple { get { return _HerpesSimple; } set { _HerpesSimple = ValidateValue<string>(value, nameof(HerpesSimple)); } }
        /// <sumary>
        /// Valores posibles (0, 1) Tiene o ha tenido Diarrea por Isospora belli o Cryptosporidium de más de un mes de duración
        /// </sumary>
        private string _DiarreaIsospora;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.8 - DiarreaIsospora - Solo permite (0,1)")] public string DiarreaIsospora { get { return _DiarreaIsospora; } set { _DiarreaIsospora = ValidateValue<string>(value, nameof(DiarreaIsospora)); } }
        /// <sumary>
        /// Numérico (int) valores posibles {0, 1}  Tiene o ha tenido Histoplasmosis extra pulmonar
        /// </sumary>
        private string _HistoplasmosisExtrapulmonar;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.9 - HistoplasmosisExtrapulmonar - Solo permite (0,1)")] public string HistoplasmosisExtrapulmonar { get { return _HistoplasmosisExtrapulmonar; } set { _HistoplasmosisExtrapulmonar = ValidateValue<string>(value, nameof(HistoplasmosisExtrapulmonar)); } }
        /// <sumary>
        /// (valores posibles (0, 1)) Tiene o ha tenido Linfoma de Burkitt, inmunoblástico, o primario del sistema nervioso central 
        /// </sumary>
        private string _BurkittPrimario;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.10 - BurkittPrimario - Solo permite (0,1)")] public string BurkittPrimario { get { return _BurkittPrimario; } set { _BurkittPrimario = ValidateValue<string>(value, nameof(BurkittPrimario)); } }
        /// <sumary>
        /// (valores posibles (0, 1) )  Tiene o ha tenido Neumonía por Pneumocystiscarinii (jirovecii) 
        /// </sumary>
        private string _NeumoniaPneumocystiscarinii;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.11 - NeumoniaPneumocystiscarinii - Solo permite (0,1)")] public string NeumoniaPneumocystiscarinii { get { return _NeumoniaPneumocystiscarinii; } set { _NeumoniaPneumocystiscarinii = ValidateValue<string>(value, nameof(NeumoniaPneumocystiscarinii)); } }
        /// <sumary>
        /// (Valores permitidos 0,1) Tiene o ha tenido Neumonía bacteriana recurrente (2 o más episodios en 1 año)
        /// </sumary>
        private string _NeumoniaRecurrenteBacteriana;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.12 - NeumoniaRecurrenteBacteriana - Solo permite (0,1)")] public string NeumoniaRecurrenteBacteriana { get { return _NeumoniaRecurrenteBacteriana; } set { _NeumoniaRecurrenteBacteriana = ValidateValue<string>(value, nameof(NeumoniaRecurrenteBacteriana)); } }
        /// <sumary>
        /// Valores permitidos (0,1) Tiene o ha tenido Septicemia por Salmonella (no tifoidea)
        /// </sumary>
        private string _SalmonelaSepticemia;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.13 - SalmonelaSepticemia - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string SalmonelaSepticemia { get { return _SalmonelaSepticemia; } set { _SalmonelaSepticemia = ValidateValue<string>(value, nameof(SalmonelaSepticemia)); } }
        /// <sumary>
        /// (valores posibles (0, 1) ) Tiene o ha tenido Infección diseminada por Micobacteriumavium (MAC) - Intracelular o Kansasii (MAI) 
        /// </sumary>
        private string _InfeccionDiseminadaMicobacte;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.14 - InfeccionDiseminadaMicobacte - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string InfeccionDiseminadaMicobacte { get { return _InfeccionDiseminadaMicobacte; } set { _InfeccionDiseminadaMicobacte = ValidateValue<string>(value, nameof(InfeccionDiseminadaMicobacte)); } }
        /// <sumary>
        /// CriptococosiSexTrapulmonar (valores posibles (0, 1))  Tiene o ha tenido Criptococosisextrapulmonar
        /// </sumary>
        private string _CriptococosiSexTrapulmonar;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 53.15 - CriptococosiSexTrapulmonar - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string CriptococosiSexTrapulmonar { get { return _CriptococosiSexTrapulmonar; } set { _CriptococosiSexTrapulmonar = ValidateValue<string>(value, nameof(CriptococosiSexTrapulmonar)); } }
        /// <sumary>
        /// (valores posibles (0, 1)) Tiene o ha tenido Sarcoma de Kaposi 
        /// </sumary>
        private string _Kaposisarcoma;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.16 - Kaposisarcoma - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string Kaposisarcoma { get { return _Kaposisarcoma; } set { _Kaposisarcoma = ValidateValue<string>(value, nameof(Kaposisarcoma)); } }
        /// <sumary>
        /// (valores posibles (0, 1)) Tiene o ha tenido Síndrome de desgaste o caquexia asociada a VIH
        /// </sumary>
        private string _SindromeCaquexiaVIH;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.17 - SindromeCaquexiaVIH - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string SindromeCaquexiaVIH { get { return _SindromeCaquexiaVIH; } set { _SindromeCaquexiaVIH = ValidateValue<string>(value, nameof(SindromeCaquexiaVIH)); } }
        /// <sumary>
        /// (valores posibles (0, 1) ) Tiene o ha tenido Leucoencefalopatia multifocal progresiva o encefalopatía por VIH
        /// </sumary>
        private string _Leucoencefelopatia;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.18 - Leucoencefelopatia - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string Leucoencefelopatia { get { return _Leucoencefelopatia; } set { _Leucoencefelopatia = ValidateValue<string>(value, nameof(Leucoencefelopatia)); } }
        /// <sumary>
        /// (valores posibles (0, 1)) Tiene o ha tenido Neumonía intersticial linfoidea 
        /// </sumary>
        private string _NeumoniaIntesticialLinfoidea;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.19 - NeumoniaIntesticialLinfoidea - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string NeumoniaIntesticialLinfoidea { get { return _NeumoniaIntesticialLinfoidea; } set { _NeumoniaIntesticialLinfoidea = ValidateValue<string>(value, nameof(NeumoniaIntesticialLinfoidea)); } }
        /// <sumary>
        /// (valores posibles (0, 1)) Tiene o ha tenido Toxoplasmosis cerebral 
        /// </sumary>
        private string _ToxoplasmosisCerebral;
        [Order]
        [Regex(@"^[0-1]$", "Variable 53.20 - ToxoplasmosisCerebral - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string ToxoplasmosisCerebral { get { return _ToxoplasmosisCerebral; } set { _ToxoplasmosisCerebral = ValidateValue<string>(value, nameof(ToxoplasmosisCerebral)); } }
        /// <sumary>
        /// Código de habilitación de la sede de la IPS donde se hace el seguimiento y atención de la infección por el VIH al paciente actualmente
        /// </sumary>
        private string _CodigoHabilitacion;
        [Order]
        [Regex(@"^[0-9]{12}$|^(9)", "Variable 54 - CodigoHabilitacion - Solo permite codigo de 12 caracteres o el numero 9")] public string CodigoHabilitacion { get { return _CodigoHabilitacion; } set { _CodigoHabilitacion = ValidateValue<string>(value, nameof(CodigoHabilitacion)); } }
        /// <sumary>
        /// (AAAA-MM-DD) Fecha en la que el paciente inició de atención en la IPS que le realiza seguimiento en el formato AAAA-MM-DD. 9999-99-99: No aplica. 
        /// </sumary>
        private string _FechaIngresoIPS;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))(\-)([0-2][0-9]|(3)[0-1])$|^(9999-99-99)$", "Variable 55 - FechaIngresoIPS - Solo acepta formato fecha valido AAAA-MM-DD o acepta 9999-99-99")] public string FechaIngresoIPS { get { return _FechaIngresoIPS; } set { _FechaIngresoIPS = ValidateValue<string>(value, nameof(FechaIngresoIPS)); } }
        /// <sumary>
        /// IPSMunicipality Código del municipio en 5 dígitos en donde está operando la IPS según la división político administrativa – DANE. 9: No aplica
        /// </sumary>
        private string _MunicipioIPS;
        [Order]
        [Regex(@"^[0-9]{5}$|^(9)", "Variable 56 - MunicipioIPS - Solo acepta codigo municipio 5 digitos o acepta el numero 9")] public string MunicipioIPS { get { return _MunicipioIPS; } set { _MunicipioIPS = ValidateValue<string>(value, nameof(MunicipioIPS)); } }
        /// <sumary>
        /// (valores posibles (1, 2, 3, 4, 5, 6, 9)) Quién hace la atención clínica y formulación para la infección por el VIH al paciente actualmente
        /// </sumary>
        private string _AtencionClinicaPacienteVIH;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 57 - AtencionClinicaPacienteVIH - Sólo se permiten números (1, 2, 3, 4, 5, 6, 9) y la longitud válida es 1 caracter")] public string AtencionClinicaPacienteVIH { get { return _AtencionClinicaPacienteVIH; } set { _AtencionClinicaPacienteVIH = ValidateValue<string>(value, nameof(AtencionClinicaPacienteVIH)); } }
        /// <sumary>
        /// AssessmentInfectologistSixMonths  (valores posibles (1, 2, 3, 4,9) )  Valoración por Infectólogo en los últimos 6 meses 
        /// </sumary>
        private string _ValoracionInfectologo6Meses;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 58 - ValoracionInfectologo6Meses - Sólo se permiten números 1, 2, 3, 4,9")] public string ValoracionInfectologo6Meses { get { return _ValoracionInfectologo6Meses; } set { _ValoracionInfectologo6Meses = ValidateValue<string>(value, nameof(ValoracionInfectologo6Meses)); } }
        /// <sumary>
        /// Valores posibles {0, 1, 2, 9}  (Ha tenido al menos un resultado de genotipificación )
        /// </sumary>
        private string _ResultadoGenotipificacion;
        [Order]
        [Regex(@"^(0|1|2|9)$", "Variable 59 - ResultadoGenotipificacion - Sólo se permiten números y letras (0,1,2,9)")] public string ResultadoGenotipificacion { get { return _ResultadoGenotipificacion; } set { _ResultadoGenotipificacion = ValidateValue<string>(value, nameof(ResultadoGenotipificacion)); } }
        /// <sumary>
        /// Valores posibles { 0,1,2,3,4,5,6,9}  (Momento de la genotipificacion)
        /// </sumary>
        private string _MomentoGenotipificacion;
        [Order]
        [Regex(@"^(0|1|2|3|4|5|6|9)$", "Variable 60 - MomentoGenotipificacion - Sólo se permiten números y letras (0,1,2,3,4,5,6,9)")] public string MomentoGenotipificacion { get { return _MomentoGenotipificacion; } set { _MomentoGenotipificacion = ValidateValue<string>(value, nameof(MomentoGenotipificacion)); } }
        /// <sumary>
        /// valores posibles {0, 1, 2, 9}  (Situación clínica actual )
        /// </sumary>
        private string _SituacionClinicaActual;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 61 - SituacionClinicaActual - Sólo se permiten números (0, 1, 2, 9) y la longitud válida es 1 caracter")] public string SituacionClinicaActual { get { return _SituacionClinicaActual; } set { _SituacionClinicaActual = ValidateValue<string>(value, nameof(SituacionClinicaActual)); } }
        /// <sumary>
        /// sistema CDC 2008, valores posibles {1, 2, 3, 9}  (Estadio clínico actual )
        /// </sumary>
        private string _EstadioClinicoActual;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 62 - EstadioClinicoActual - Sólo se permiten números (1, 2, 3, 9) y la longitud válida es 1 caracter")] public string EstadioClinicoActual { get { return _EstadioClinicoActual; } set { _EstadioClinicoActual = ValidateValue<string>(value, nameof(EstadioClinicoActual)); } }
        /// <sumary>
        /// Valores posibles {0, 1}  (Tiene dislipidemia )
        /// </sumary>
        private string _Dislipidemia;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 63 - Dislipidemia - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string Dislipidemia { get { return _Dislipidemia; } set { _Dislipidemia = ValidateValue<string>(value, nameof(Dislipidemia)); } }
        /// <sumary>
        /// valores posibles {0, 1} (Tiene neuropatía periférica )
        /// </sumary>
        private string _NeuropatiaPeriferica;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 64 - NeuropatiaPeriferica - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string NeuropatiaPeriferica { get { return _NeuropatiaPeriferica; } set { _NeuropatiaPeriferica = ValidateValue<string>(value, nameof(NeuropatiaPeriferica)); } }
        /// <sumary>
        /// valores posibles {0, 1}  (Tiene lipoatrofia o lipodistrofia )
        /// </sumary>
        private string _Lipoafrofia;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 65 - Lipoafrofia - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string Lipoafrofia { get { return _Lipoafrofia; } set { _Lipoafrofia = ValidateValue<string>(value, nameof(Lipoafrofia)); } }
        /// <sumary>
        /// valores posibles {0, 1}  (Tiene coinfección con el VHB )
        /// </sumary>
        private string _CoInfectionvhb;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 66 - CoInfectionvhb - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string CoInfectionvhb { get { return _CoInfectionvhb; } set { _CoInfectionvhb = ValidateValue<string>(value, nameof(CoInfectionvhb)); } }
        /// <sumary>
        /// valores posibles {0, 1}  (Tiene coinfección con el VHC )
        /// </sumary>
        private string _CoInfectionvhc;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 67 - CoInfectionvhc - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string CoInfectionvhc { get { return _CoInfectionvhc; } set { _CoInfectionvhc = ValidateValue<string>(value, nameof(CoInfectionvhc)); } }
        /// <sumary>
        /// valores posibles {0, 1}  (Tiene Anemia)
        /// </sumary>
        private string _Anemia;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 68 - Anemia - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string Anemia { get { return _Anemia; } set { _Anemia = ValidateValue<string>(value, nameof(Anemia)); } }
        /// <sumary>
        /// Valores posibles {0, 1}  Tiene Cirrosis hepática
        /// </sumary>
        private string _CirrosisHepatica;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 69 - CirrosisHepatica - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string CirrosisHepatica { get { return _CirrosisHepatica; } set { _CirrosisHepatica = ValidateValue<string>(value, nameof(CirrosisHepatica)); } }
        /// <sumary>
        /// valores posibles {0, 1}  (Tiene Enfermedad renal crónica )
        /// </sumary>
        private string _EnfermedadRenalCronica;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 70 - EnfermedadRenalCronica - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string EnfermedadRenalCronica { get { return _EnfermedadRenalCronica; } set { _EnfermedadRenalCronica = ValidateValue<string>(value, nameof(EnfermedadRenalCronica)); } }
        /// <sumary>
        /// Valores posibles {0, 1}  (Tiene Enfermedad coronaria )
        /// </sumary>
        private string _EnfermedadCoronaria;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 71 - EnfermedadCoronaria - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string EnfermedadCoronaria { get { return _EnfermedadCoronaria; } set { _EnfermedadCoronaria = ValidateValue<string>(value, nameof(EnfermedadCoronaria)); } }
        /// <sumary>
        /// Valores posibles {0, 1}  Tiene o ha tenido otras Infecciones de Transmisión Sexual en los últimos 12 meses 
        /// </sumary>
        private string _InfeccionTransmisionSexual12Meses;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 72 - InfeccionTransmisionSexual12Meses - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string InfeccionTransmisionSexual12Meses { get { return _InfeccionTransmisionSexual12Meses; } set { _InfeccionTransmisionSexual12Meses = ValidateValue<string>(value, nameof(InfeccionTransmisionSexual12Meses)); } }
        /// <sumary>
        /// Valores posibles {0, 1}   Tiene o ha tenido otra neoplasia no relacionada con SIDA 
        /// </sumary>
        private string _NeoplastiaSida;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 73 - NeoplastiaSida - Sólo se permiten números 0 | 1 y la longitud válida es 1 caracter")] public string NeoplastiaSida { get { return _NeoplastiaSida; } set { _NeoplastiaSida = ValidateValue<string>(value, nameof(NeoplastiaSida)); } }
        /// <sumary>
        /// Valores posibles {0, 1, 2} (Discapacidad funcional )
        /// </sumary>
        private string _DiscapacidadFuncional;
        [Order]
        [Regex(@"^[0-3]{1}$", "Variable 74 - DiscapacidadFuncional - Sólo se permiten números 0, 1, 2,3 y la longitud válida es 1 caracter")] public string DiscapacidadFuncional { get { return _DiscapacidadFuncional; } set { _DiscapacidadFuncional = ValidateValue<string>(value, nameof(DiscapacidadFuncional)); } }
        /// <sumary>
        /// AAAA-MM-DD  (Fecha último conteo de linfocitos T CD4+ )
        /// </sumary>
        private string _FechaUltimoConteoLinfocitosCD4;
        [Order]
        [Regex(@"^([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))$", "Variable 75 - FechaUltimoConteoLinfocitosCD4 - El formato de la fecha ingresada no es correcto, valor permitido (AAAA-MM-DD)")] public string FechaUltimoConteoLinfocitosCD4 { get { return _FechaUltimoConteoLinfocitosCD4; } set { _FechaUltimoConteoLinfocitosCD4 = ValidateValue<string>(value, nameof(FechaUltimoConteoLinfocitosCD4)); } }
        /// <sumary>
        /// Valores posibles {0 a 5000}, 9999,000  (Valor del último conteo de linfocitos T CD4+ ) 
        /// </sumary>
        private string _ValorUltimoConteoLinfocitos;
        [Order]
        [Regex(@"^[0-9]{1,4}$", "Variable 76 - ValorUltimoConteoLinfocitos - Solo permite valores de 0 a 5000 y 9999")] public string ValorUltimoConteoLinfocitos { get { return _ValorUltimoConteoLinfocitos; } set { _ValorUltimoConteoLinfocitos = ValidateValue<string>(value, nameof(ValorUltimoConteoLinfocitos)); } }
        /// <sumary>
        /// Valores Permitidos AAAA-MM-DD   (Fecha última carga viral reportada)
        /// </sumary>
        private string _FechaUlitmoConteoLinfocitos;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))(\-)([0-2][0-9]|(3)[0-1])$|^(9999-99-99)", "Variable 77 - FechaUlitmoConteoLinfocitos - Solo permite AAAA-MM-DD o 9999-99-99")] public string FechaUlitmoConteoLinfocitos { get { return _FechaUlitmoConteoLinfocitos; } set { _FechaUlitmoConteoLinfocitos = ValidateValue<string>(value, nameof(FechaUlitmoConteoLinfocitos)); } }
        /// <sumary>
        /// Valores posibles {0 a 9000}, 9999,000  (Valor del último conteo de linfocitos totales)
        /// </sumary>
        private string _UltimoConteoLinfocitos;
        [Order]
        [Regex(@"^[0-9]{1,4}$", "Variable 78 - UltimoConteoLinfocitos - Solo permite valores de 0 a 9000 y 9999")] public string UltimoConteoLinfocitos { get { return _UltimoConteoLinfocitos; } set { _UltimoConteoLinfocitos = ValidateValue<string>(value, nameof(UltimoConteoLinfocitos)); } }
        /// <sumary>
        /// Valores permiditidos  AAAA-MM-DD   Fecha última carga viral reportada 
        /// </sumary>
        private string _FechaUltimaCargaViralReportada;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))(\-)([0-2][0-9]|(3)[0-1])$|^(9999-99-99)", "Variable 79 - FechaUltimaCargaViralReportada - El formato de la fecha ingresada no es correcto, valor permitido (AAAA-MM-DD)")] public string FechaUltimaCargaViralReportada { get { return _FechaUltimaCargaViralReportada; } set { _FechaUltimaCargaViralReportada = ValidateValue<string>(value, nameof(FechaUltimaCargaViralReportada)); } }
        /// <sumary>
        /// Valores posibles {0, 1, 2, 9}  (Resultado de la última Carga Viral reportada )
        /// </sumary>
        private string _ResultadoUltimaCargaViral;
        [Order]
        [Regex(@"^(0|1|2|3|9|)$", "Variable 80 - ResultadoUltimaCargaViral - Sólo se permiten números (0,1,2,3,9) y la longitud válida es 1 carácter")] public string ResultadoUltimaCargaViral { get { return _ResultadoUltimaCargaViral; } set { _ResultadoUltimaCargaViral = ValidateValue<string>(value, nameof(ResultadoUltimaCargaViral)); } }
        /// <sumary>
        /// Valores posibles {0,1,2,3,9}   Suministro de condones en los últimos 3 meses 
        /// </sumary>
        private string _SuministroCondones3Meses;
        [Order]
        [Regex(@"^(0|1|2|3|9|)$", "Variable 81 - SuministroCondones3Meses - Sólo se permiten números (0,1,2,3,9) y la longitud válida es 1 carácter")] public string SuministroCondones3Meses { get { return _SuministroCondones3Meses; } set { _SuministroCondones3Meses = ValidateValue<string>(value, nameof(SuministroCondones3Meses)); } }
        /// <sumary>
        /// NcondomsSuppliedLast3Months Valores permitidos (0,1,3,9)
        /// </sumary>
        private string _NumeroCondones3Meses;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 81.1 - NumeroCondones3Meses - Sólo se permiten números (0,1,3,9) y la longitud válida es 1 caracter ")] public string NumeroCondones3Meses { get { return _NumeroCondones3Meses; } set { _NumeroCondones3Meses = ValidateValue<string>(value, nameof(NumeroCondones3Meses)); } }
        /// <sumary>
        /// Valores permitidos {0,1,2,3,4,5,9} Método de planificación familiar
        /// </sumary>
        private string _MetodoPlanificacionFamiliar;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 82 - MetodoPlanificacionFamiliar - Sólo se permiten números (0,1,2,3,4,9) y la longitud válida es 1 carácter")] public string MetodoPlanificacionFamiliar { get { return _MetodoPlanificacionFamiliar; } set { _MetodoPlanificacionFamiliar = ValidateValue<string>(value, nameof(MetodoPlanificacionFamiliar)); } }
        /// <sumary>
        /// Valores posibles(0,1,2,3,9}  Vacuna Hepatitis B
        /// </sumary>
        private string _VacunaHepatitisB;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 83 - VacunaHepatitisB - Sólo se permiten números (0,1,2,3,9) y la longitud válida es 1 caracter")] public string VacunaHepatitisB { get { return _VacunaHepatitisB; } set { _VacunaHepatitisB = ValidateValue<string>(value, nameof(VacunaHepatitisB)); } }
        /// <sumary>
        /// Valores posibles {0,1,2,9} Se le realizó PPD en los últimos 12 meses 
        /// </sumary>
        private string _PPDUltimos12Meses;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 84 - PPDUltimos12Meses - Sólo se permiten números (0,1,2,9) y la longitud válida es 1 caracter")] public string PPDUltimos12Meses { get { return _PPDUltimos12Meses; } set { _PPDUltimos12Meses = ValidateValue<string>(value, nameof(PPDUltimos12Meses)); } }
        /// <sumary>
        /// (valores posibles {0,1,2,3,4,9} ) Estudio con carga viral para menores de 18 meses, hijos de madre con infección por VIH  
        /// </sumary>
        private string _EstudioCargarViralMenores18Meses;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 85 - EstudioCargarViralMenores18Meses - Sólo se permiten números (0,1,2,3,4,9) y la longitud válida es 1 caracter")] public string EstudioCargarViralMenores18Meses { get { return _EstudioCargarViralMenores18Meses; } set { _EstudioCargarViralMenores18Meses = ValidateValue<string>(value, nameof(EstudioCargarViralMenores18Meses)); } }
        /// <sumary>
        /// Valores posibles {0,1,2,3,4,9}  (Estudio con segunda carga viral para menores de 18 meses, hijos de madres con infección por VIH)
        /// </sumary>
        private string _EstudioSegundaCargaViralMenores18;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 86 - EstudioSegundaCargaViralMenores18 - Sólo se permiten números (0,1,2,3,4,9) y la longitud válida es 1 caracter")] public string EstudioSegundaCargaViralMenores18 { get { return _EstudioSegundaCargaViralMenores18; } set { _EstudioSegundaCargaViralMenores18 = ValidateValue<string>(value, nameof(EstudioSegundaCargaViralMenores18)); } }
        /// <sumary>
        /// NumberViralLoadChildrenUnder18Months
        /// </sumary>
        private string _NumeroCargasViralesMenor18Meses;
        [Order]
        [Regex(@"^([0-9]|1[0-2])$|^(99)$", "Variable 87 - NumeroCargasViralesMenor18Meses - Solo acepta numeros del 0 a 12 y 99")] public string NumeroCargasViralesMenor18Meses { get { return _NumeroCargasViralesMenor18Meses; } set { _NumeroCargasViralesMenor18Meses = ValidateValue<string>(value, nameof(NumeroCargasViralesMenor18Meses)); } }
        /// <sumary>
        /// Valores posibles {0,1,9}  (Suministro de Formula láctea)
        /// </sumary>
        private string _SuministroFormulaLactea;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 88 - SuministroFormulaLactea - Sólo se permiten números (0,1,9)  y la longitud válida es 1 caracter")] public string SuministroFormulaLactea { get { return _SuministroFormulaLactea; } set { _SuministroFormulaLactea = ValidateValue<string>(value, nameof(SuministroFormulaLactea)); } }
        /// <sumary>
        /// Valores posibles {0,1, 2,3,4,9}  (Recibe TAR )
        /// </sumary>
        private string _RecibeTar;
        [Order]
        [Regex(@"^(0|1|2|3|4|9)$", "Variable 89 - RecibeTar - Sólo se permiten números (0,1,2,3,4,9) y la longitud válida es 1 carácter")] public string RecibeTar { get { return _RecibeTar; } set { _RecibeTar = ValidateValue<string>(value, nameof(RecibeTar)); } }
        /// <sumary>
        /// Valores (AAAA-MM ) Fecha de inicio de los medicamentos de la TAR que recibe actualmente 
        /// </sumary>
        private string _FechaInicioMedimamentosTAR;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))$|^(0000-00|8888-88|9999-99)$", "Variable 90 - FechaInicioMedimamentosTAR - Solo acepta formato fecha valido AAAA-MM o acepta (0000-00 |8888-88 |9999-99)")] public string FechaInicioMedimamentosTAR { get { return _FechaInicioMedimamentosTAR; } set { _FechaInicioMedimamentosTAR = ValidateValue<string>(value, nameof(FechaInicioMedimamentosTAR)); } }
        /// <sumary>
        /// Valores {0, 1, 9}  Actualmente recibe ABACAVIR 
        /// </sumary>
        private string _RecibeAbacavir;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.1 - RecibeAbacavir - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 carácter")] public string RecibeAbacavir { get { return _RecibeAbacavir; } set { _RecibeAbacavir = ValidateValue<string>(value, nameof(RecibeAbacavir)); } }
        /// <sumary>
        /// Valores {0, 1, 9}  Actualmente recibe ATAZANAVIR 
        /// </sumary>
        private string _RecibeAtazanavir;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.2 - RecibeAtazanavir - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 carácter")] public string RecibeAtazanavir { get { return _RecibeAtazanavir; } set { _RecibeAtazanavir = ValidateValue<string>(value, nameof(RecibeAtazanavir)); } }
        /// <sumary>
        /// Valores {0, 1, 9}   Actualmente recibe DIDANOSINA 
        /// </sumary>
        private string _RecibeDidanosina;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.3 - RecibeDidanosina - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeDidanosina { get { return _RecibeDidanosina; } set { _RecibeDidanosina = ValidateValue<string>(value, nameof(RecibeDidanosina)); } }
        /// <sumary>
        /// Valores  {0, 1, 9}  Actualmente recibe EFAVIRENZ 
        /// </sumary>
        private string _RecibeEfavirenz;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.4 - RecibeEfavirenz - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeEfavirenz { get { return _RecibeEfavirenz; } set { _RecibeEfavirenz = ValidateValue<string>(value, nameof(RecibeEfavirenz)); } }
        /// <sumary>
        /// Valores {0, 1, 9}  Actualmente recibe ESTAVUDINA 
        /// </sumary>
        private string _RecibeEstavudina;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.5 - RecibeEstavudina - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeEstavudina { get { return _RecibeEstavudina; } set { _RecibeEstavudina = ValidateValue<string>(value, nameof(RecibeEstavudina)); } }
        /// <sumary>
        /// Valores {0, 1, 9}  Actualmente recibe FOSAMPRENAVIR 
        /// </sumary>
        private string _RecibeFosamprenavir;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.6 - RecibeFosamprenavir - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeFosamprenavir { get { return _RecibeFosamprenavir; } set { _RecibeFosamprenavir = ValidateValue<string>(value, nameof(RecibeFosamprenavir)); } }
        /// <sumary>
        /// Valores {0, 1, 9}  Actualmente recibe INDINAVIR 
        /// </sumary>
        private string _RecibeIndinavir;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.7 - RecibeIndinavir - Sólo se permiten números (0, 1, 9)  y la longitud válida es 1 caracter")] public string RecibeIndinavir { get { return _RecibeIndinavir; } set { _RecibeIndinavir = ValidateValue<string>(value, nameof(RecibeIndinavir)); } }
        /// <sumary>
        /// Valores  {0, 1, 9} Actualmente recibe LAMIVUDINA 
        /// </sumary>
        private string _RecibeLamivudina;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.8 - RecibeLamivudina - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeLamivudina { get { return _RecibeLamivudina; } set { _RecibeLamivudina = ValidateValue<string>(value, nameof(RecibeLamivudina)); } }
        /// <sumary>
        /// Valores {0, 1, 9}  Actualmente recibe LOPINAVIR 
        /// </sumary>
        private string _RecibeLopinavir;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.9 - RecibeLopinavir - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeLopinavir { get { return _RecibeLopinavir; } set { _RecibeLopinavir = ValidateValue<string>(value, nameof(RecibeLopinavir)); } }
        /// <sumary>
        /// Valores {0, 1, 9} Actualmente recibe NEVIRAPINA 
        /// </sumary>
        private string _RecibeNevirapina;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.10 - RecibeNevirapina - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeNevirapina { get { return _RecibeNevirapina; } set { _RecibeNevirapina = ValidateValue<string>(value, nameof(RecibeNevirapina)); } }
        /// <sumary>
        /// Valores  {0, 1, 9}  Actualmente recibe NELFINAVIR
        /// </sumary>
        private string _RecibeNelfinavir;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.11 - RecibeNelfinavir - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeNelfinavir { get { return _RecibeNelfinavir; } set { _RecibeNelfinavir = ValidateValue<string>(value, nameof(RecibeNelfinavir)); } }
        /// <sumary>
        /// Valores  {0, 1, 9}  Actualmente recibe RITONAVIR
        /// </sumary>
        private string _RecibeRitonavir;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.12 - RecibeRitonavir - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeRitonavir { get { return _RecibeRitonavir; } set { _RecibeRitonavir = ValidateValue<string>(value, nameof(RecibeRitonavir)); } }
        /// <sumary>
        /// Valores {0, 1, 9}  Actualmente recibe SAQUINAVIR
        /// </sumary>
        private string _RecibeSaquinavir;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.13 - RecibeSaquinavir - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeSaquinavir { get { return _RecibeSaquinavir; } set { _RecibeSaquinavir = ValidateValue<string>(value, nameof(RecibeSaquinavir)); } }
        /// <sumary>
        /// Valores {0, 1, 9}   Actualmente recibe ZIDOVUDINA
        /// </sumary>
        private string _RecibeZidovudina;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.14 - RecibeZidovudina - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 caracter")] public string RecibeZidovudina { get { return _RecibeZidovudina; } set { _RecibeZidovudina = ValidateValue<string>(value, nameof(RecibeZidovudina)); } }
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS (medicamento 1) 
        /// </sumary>
        private string _RecibeTenofovir;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.15 - RecibeTenofovir - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 carácter")] public string RecibeTenofovir { get { return _RecibeTenofovir; } set { _RecibeTenofovir = ValidateValue<string>(value, nameof(RecibeTenofovir)); } }
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS (medicamento 2) 
        /// </sumary>
        private string _RecibeEmtricita81na;
        [Order]
        [Regex(@"^(0|1|9)$", "Variable 91.16 - RecibeEmtricitacina - Sólo se permiten números (0, 1, 9) y la longitud válida es 1 carácter")] public string RecibeEmtricita81na { get { return _RecibeEmtricita81na; } set { _RecibeEmtricita81na = ValidateValue<string>(value, nameof(RecibeEmtricita81na)); } }
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS (medicamento 3)
        /// </sumary>
        private string _RecibeNoposMedication3;
        [Order]
        [Regex(@"^[0-9a-zA-Z]+$", "Variable 91.17 - RecibeNoposMedication3 - Sólo se permiten números y letras")] public string RecibeNoposMedication3 { get { return _RecibeNoposMedication3; } set { _RecibeNoposMedication3 = ValidateValue<string>(value, nameof(RecibeNoposMedication3)); } }
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS (medicamento 4)  
        /// </sumary>
        private string _RecibeNoposMedication4;
        [Order]
        [Regex(@"^[0-9a-zA-Z]+$", "Variable 91.18 - RecibeNoposMedication4 - Sólo se permiten números y letras")] public string RecibeNoposMedication4 { get { return _RecibeNoposMedication4; } set { _RecibeNoposMedication4 = ValidateValue<string>(value, nameof(RecibeNoposMedication4)); } }
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS(medicamento 5) 
        /// </sumary>
        private string _RecibeNoposMedication5;
        [Order]
        [Regex(@"^[0-9a-zA-Z]+$", "Variable 91.19 - RecibeNoposMedication5 - Sólo se permiten números y letras")] public string RecibeNoposMedication5 { get { return _RecibeNoposMedication5; } set { _RecibeNoposMedication5 = ValidateValue<string>(value, nameof(RecibeNoposMedication5)); } }
        /// <sumary>
        /// Actualmente en la TAR recibe Medicamento NO POS (medicamento 6) 
        /// </sumary>
        private string _RecibeNoposMedication6;
        [Order]
        [Regex(@"^[0-9a-zA-Z]+$", "Variable 91.20 - RecibeNoposMedication6 - Sólo se permiten números y letras")] public string RecibeNoposMedication6 { get { return _RecibeNoposMedication6; } set { _RecibeNoposMedication6 = ValidateValue<string>(value, nameof(RecibeNoposMedication6)); } }
        /// <sumary>
        /// Valores {0, 1, 2} Profilaxis ARV para el recién nacido expuesto al VIH (hijo de madre con infección por VIH) 
        /// </sumary>
        private string _ProfilaxisRecienNacidoExpuestoVIH;
        [Order]
        [Regex(@"^(0|1|2)$", "Variable 92 - ProfilaxisRecienNacidoExpuestoVIH - Solo permite numeros (0,1,2) resolucion 783")] public string ProfilaxisRecienNacidoExpuestoVIH { get { return _ProfilaxisRecienNacidoExpuestoVIH; } set { _ProfilaxisRecienNacidoExpuestoVIH = ValidateValue<string>(value, nameof(ProfilaxisRecienNacidoExpuestoVIH)); } }
        /// <sumary>
        /// valores posibles {0, 1, 2}  Profilaxispara MAC (Mycobacterium Avium Complex) 
        /// </sumary>
        private string _ProfilaxisMAC;
        [Order]
        [Regex(@"^[0-2]{1}$", "Variable 92.1 - ProfilaxisMAC - Sólo se permiten números (0, 1, 2) y la longitud válida es 1 caracter")] public string ProfilaxisMAC { get { return _ProfilaxisMAC; } set { _ProfilaxisMAC = ValidateValue<string>(value, nameof(ProfilaxisMAC)); } }
        /// <sumary>
        /// Valores posibles {0, 1, 2}  (Profilaxis con Fluconazol )
        /// </sumary>
        private string _ProfilaxisFluconazol;
        [Order]
        [Regex(@"^[0-2]{1}$", "Variable 92.2 - ProfilaxisFluconazol - Sólo se permiten números (0, 1, 2)y la longitud válida es 1 caracter")] public string ProfilaxisFluconazol { get { return _ProfilaxisFluconazol; } set { _ProfilaxisFluconazol = ValidateValue<string>(value, nameof(ProfilaxisFluconazol)); } }
        /// <sumary>
        /// valores posibles {0, 1, 2}  Profilaxis con Trimetoprim Sulfa 
        /// </sumary>
        private string _ProfilaxisTrimetoprimSulfa;
        [Order]
        [Regex(@"^[0-2]{1}$", "Variable 92.3 - ProfilaxisTrimetoprimSulfa - Sólo se permiten números (0, 1, 2) y la longitud válida es 1 caracter")] public string ProfilaxisTrimetoprimSulfa { get { return _ProfilaxisTrimetoprimSulfa; } set { _ProfilaxisTrimetoprimSulfa = ValidateValue<string>(value, nameof(ProfilaxisTrimetoprimSulfa)); } }
        /// <sumary>
        ///  Profilaxis con inmunoglobulina IV  valores posibles {0, 1, 2}
        /// </sumary>
        private string _ProfilaxisInmunoglobinaIV;
        [Order]
        [Regex(@"^[0-2]{1}$", "Variable 92.4 - ProfilaxisInmunoglobinaIV - Solo permite numeros (0,1,2) y la longitud válida es 1 caracter")] public string ProfilaxisInmunoglobinaIV { get { return _ProfilaxisInmunoglobinaIV; } set { _ProfilaxisInmunoglobinaIV = ValidateValue<string>(value, nameof(ProfilaxisInmunoglobinaIV)); } }
        /// <sumary>
        /// Valores posibles {0, 1, 2}  Profilaxis con Isoniacida
        /// </sumary>
        private string _ProfilaxisIsoniacida;
        [Order]
        [Regex(@"^[0-2]{1}$", "Variable 92.5 - ProfilaxisIsoniacida - Solo permite numeros (0,1,2) y la longitud válida es 1 caracter")] public string ProfilaxisIsoniacida { get { return _ProfilaxisIsoniacida; } set { _ProfilaxisIsoniacida = ValidateValue<string>(value, nameof(ProfilaxisIsoniacida)); } }
        /// <sumary>
        /// Valores posibles {0, 1, 2, 3, 9}  Recibe tratamiento antituberculoso
        /// </sumary>
        private string _TratamientoAntituberculosis;
        [Order]
        [Regex(@"^[0-9]{1}$", "Variable 93 - TratamientoAntituberculosis - Sólo se permiten números (0, 1, 2, 3, 9) y la longitud válida es 1 caracter")] public string TratamientoAntituberculosis { get { return _TratamientoAntituberculosis; } set { _TratamientoAntituberculosis = ValidateValue<string>(value, nameof(TratamientoAntituberculosis)); } }
        /// <sumary>
        /// Valores Permitidos AAAA-MM (Fecha de inicio del tratamiento antituberculoso que recibe actualmente )
        /// </sumary>
        private string _FechaInicioTratamientoAntituberculoso;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))$", "Variable 94 - FechaInicioTratamientoAntituberculoso - Solo acepta formato fecha valido AAAA-MM o acepta (0000-00)")] public string FechaInicioTratamientoAntituberculoso { get { return _FechaInicioTratamientoAntituberculoso; } set { _FechaInicioTratamientoAntituberculoso = ValidateValue<string>(value, nameof(FechaInicioTratamientoAntituberculoso)); } }
        /// <sumary>
        /// Numérico (int) {0, 1}  (Actualmente recibe AMIKACINA)
        /// </sumary>
        private string _RecibeAmikacina;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 95.1 - RecibeAmikacina - Solo permite numeros (0,1) y la longitud válida es 1 caracter")] public string RecibeAmikacina { get { return _RecibeAmikacina; } set { _RecibeAmikacina = ValidateValue<string>(value, nameof(RecibeAmikacina)); } }
        /// <sumary>
        /// Numérico (int) {0, 1} (Actualmente recibe CIPROFLOXACINA )
        /// </sumary>
        private string _RecibeCiprofloxacina;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 95.2 - RecibeCiprofloxacina - Solo permite numeros (0,1) y la longitud válida es 1 caracter")] public string RecibeCiprofloxacina { get { return _RecibeCiprofloxacina; } set { _RecibeCiprofloxacina = ValidateValue<string>(value, nameof(RecibeCiprofloxacina)); } }
        /// <sumary>
        /// Variable 95.3 - RecibeEstreptomicina - Numérico (int) {0, 1} Actualmente recibe ESTREPTOMICINA 
        /// </sumary>
        private string _RecibeEstreptomicina;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 95.3 - RecibeEstreptomicina - Solo permite numeros (0,1) y la longitud válida es 1 caracter")] public string RecibeEstreptomicina { get { return _RecibeEstreptomicina; } set { _RecibeEstreptomicina = ValidateValue<string>(value, nameof(RecibeEstreptomicina)); } }
        /// <sumary>
        /// Numérico (int) {0, 1} (Actualmente recibe ETHAMBUTOL )
        /// </sumary>
        private string _RecibeEthambutol;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 95.4 - RecibeEthambutol - Solo permite numeros (0,1) y la longitud válida es 1 caracter")] public string RecibeEthambutol { get { return _RecibeEthambutol; } set { _RecibeEthambutol = ValidateValue<string>(value, nameof(RecibeEthambutol)); } }
        /// <sumary>
        /// Numérico (int) {0, 1}  Actualmente recibe ETHIONAMIDA 
        /// </sumary>
        private string _RecibeEthionamida;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 95.5 - RecibeEthionamida - Solo permite numeros (0,1) y la longitud válida es 1 caracter")] public string RecibeEthionamida { get { return _RecibeEthionamida; } set { _RecibeEthionamida = ValidateValue<string>(value, nameof(RecibeEthionamida)); } }
        /// <sumary>
        /// Numérico (int) {0, 1}  Actualmente recibe ISONIACIDA 
        /// </sumary>
        private string _RecibeIsoniacida;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 95.6 - RecibeIsoniacida - Solo permite numeros (0,1) y la longitud válida es 1 caracter")] public string RecibeIsoniacida { get { return _RecibeIsoniacida; } set { _RecibeIsoniacida = ValidateValue<string>(value, nameof(RecibeIsoniacida)); } }
        /// <sumary>
        /// Numérico (int) {0, 1} (Actualmente recibe PIRAZINAMIDA)
        /// </sumary>
        private string _RecibePirazinamida;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 95.7 - RecibePirazinamida - Solo permite numeros (0,1) y la longitud válida es 1 caracter")] public string RecibePirazinamida { get { return _RecibePirazinamida; } set { _RecibePirazinamida = ValidateValue<string>(value, nameof(RecibePirazinamida)); } }
        /// <sumary>
        /// Numérico (int) {0, 1} (Actualmente recibe RIFAMPICINA )
        /// </sumary>
        private string _RecibeRifampicina;
        [Order]
        [Regex(@"^[0-1]{1}$", "Variable 95.8 - RecibeRifampicina - Solo permite numeros (0,1) y la longitud válida es 1 caracter")] public string RecibeRifampicina { get { return _RecibeRifampicina; } set { _RecibeRifampicina = ValidateValue<string>(value, nameof(RecibeRifampicina)); } }
        /// <sumary>
        /// Actualmente recibe RIFABUTINA
        /// </sumary>
        private string _RecibeRifabutina;
        [Order]
        [Regex(@"^[0-9-]+$", "Variable 95.9 - RecibeRifabutina - Recibe codigo cum o (0,1)")] public string RecibeRifabutina { get { return _RecibeRifabutina; } set { _RecibeRifabutina = ValidateValue<string>(value, nameof(RecibeRifabutina)); } }
        /// <sumary>
        /// Actualmente recibe Medicamento NO POS (medicamento antituberculoso 1) 
        /// </sumary>
        private string _RecibeMedicamentoAntibuberculoso1;
        [Order]
        [Regex(@"^[0-9-]+$", "Variable 95.10 - RecibeMedicamentoAntibuberculoso1 - Recibe codigo cum o (0,1)")] public string RecibeMedicamentoAntibuberculoso1 { get { return _RecibeMedicamentoAntibuberculoso1; } set { _RecibeMedicamentoAntibuberculoso1 = ValidateValue<string>(value, nameof(RecibeMedicamentoAntibuberculoso1)); } }
        /// <sumary>
        /// Actualmente recibe Medicamento NO POS (medicamento antituberculoso 2) 
        /// </sumary>
        private string _RecibeMedicamentoAntibuberculoso2;
        [Order]
        [Regex(@"^[0-9-]+$", "Variable 95.11 - RecibeMedicamentoAntibuberculoso2 - Recibe codigo cum o (0,1)")] public string RecibeMedicamentoAntibuberculoso2 { get { return _RecibeMedicamentoAntibuberculoso2; } set { _RecibeMedicamentoAntibuberculoso2 = ValidateValue<string>(value, nameof(RecibeMedicamentoAntibuberculoso2)); } }
        /// <sumary>
        /// Actualmente recibe Medicamento NO POS (medicamento antituberculoso 3)
        /// </sumary>
        private string _RecibeMedicamentoAntibuberculoso3;
        [Order]
        [Regex(@"^[0-9a-zA-Z]+$", "Variable 95.12 - RecibeMedicamentoAntibuberculoso3 - Recibe codigo cum o (0,1)")] public string RecibeMedicamentoAntibuberculoso3 { get { return _RecibeMedicamentoAntibuberculoso3; } set { _RecibeMedicamentoAntibuberculoso3 = ValidateValue<string>(value, nameof(RecibeMedicamentoAntibuberculoso3)); } }
        /// <sumary>
        /// Actualmente recibe Medicamento NO POS (medicamento antituberculoso 4) 
        /// </sumary>
        private string _RecibeMedicamentoAntibuberculoso4;
        [Order]
        [Regex(@"^[0-9a-zA-Z]+$", "Variable 95.13 - RecibeMedicamentoAntibuberculoso4 - Recibe codigo cum o (0,1)")] public string RecibeMedicamentoAntibuberculoso4 { get { return _RecibeMedicamentoAntibuberculoso4; } set { _RecibeMedicamentoAntibuberculoso4 = ValidateValue<string>(value, nameof(RecibeMedicamentoAntibuberculoso4)); } }
        /// <sumary>
        /// Actualmente recibe Medicamento NO POS (medicamento antituberculoso 5) 
        /// </sumary>
        private string _RecibeMedicamentoAntibuberculoso5;
        [Order]
        [Regex(@"^[0-9-]+$", "Variable 95.14 - RecibeMedicamentoAntibuberculoso5 - Recibe codigo cum o (0,1)")] public string RecibeMedicamentoAntibuberculoso5 { get { return _RecibeMedicamentoAntibuberculoso5; } set { _RecibeMedicamentoAntibuberculoso5 = ValidateValue<string>(value, nameof(RecibeMedicamentoAntibuberculoso5)); } }
        /// <sumary>
        /// Resultado de Serología para Sífilis en primer trimestre de gestación 
        /// </sumary>
        private string _ResultadoSerologiaSifilisPrimerTrimestre;
        [Order]
        [Regex(@"^(1|2|3|9)$", "Variable 96 - ResultadoSerologiaSifilisPrimerTrimestre - Solo permite numeros (1,2,3,9)")] public string ResultadoSerologiaSifilisPrimerTrimestre { get { return _ResultadoSerologiaSifilisPrimerTrimestre; } set { _ResultadoSerologiaSifilisPrimerTrimestre = ValidateValue<string>(value, nameof(ResultadoSerologiaSifilisPrimerTrimestre)); } }
        /// <sumary>
        /// Resultado de Serología para Sífilis en segundo trimestre de gestación 
        /// </sumary>
        private string _ResultadoSerologiaSifilisSegundoTrimestre;
        [Order]
        [Regex(@"^(1|2|3|9)$", "Variable 97 - ResultadoSerologiaSifilisSegundoTrimestre - Solo permite numeros (1,2,3,9)")] public string ResultadoSerologiaSifilisSegundoTrimestre { get { return _ResultadoSerologiaSifilisSegundoTrimestre; } set { _ResultadoSerologiaSifilisSegundoTrimestre = ValidateValue<string>(value, nameof(ResultadoSerologiaSifilisSegundoTrimestre)); } }
        /// <sumary>
        /// Resultado de Serología para Sífilis en tercer trimestre de gestación 
        /// </sumary>
        private string _ResultadoSerologiaSifilisTercerTrimestre;
        [Order]
        [Regex(@"^(1|2|3|9)$", "Variable 98 - ResultadoSerologiaSifilisTercerTrimestre - Solo permite numeros (1,2,3,9)")] public string ResultadoSerologiaSifilisTercerTrimestre { get { return _ResultadoSerologiaSifilisTercerTrimestre; } set { _ResultadoSerologiaSifilisTercerTrimestre = ValidateValue<string>(value, nameof(ResultadoSerologiaSifilisTercerTrimestre)); } }
        /// <sumary>
        /// Resultado de Serología al momento del parto o aborto (para gestantes sin serología del tercer trimestre)
        /// </sumary>
        private string _ResultadoSerologiaPartoAborto;
        [Order]
        [Regex(@"^(1|2|3|9)$", "Variable 99 - ResultadoSerologiaPartoAborto - Solo permite numeros (1,2,3,9)")] public string ResultadoSerologiaPartoAborto { get { return _ResultadoSerologiaPartoAborto; } set { _ResultadoSerologiaPartoAborto = ValidateValue<string>(value, nameof(ResultadoSerologiaPartoAborto)); } }
        /// <sumary>
        /// Valores AAAA-MM Fecha de primer tratamiento de la Sífilis 
        /// </sumary>
        private string _FechaPrimerTratamientoSifilis;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))$|^(9999-99)$", "Variable 100 - FechaPrimerTratamientoSifilis - Solo acepta formato fecha valido AAAA-MM o acepta (0000-00|9999-99)")] public string FechaPrimerTratamientoSifilis { get { return _FechaPrimerTratamientoSifilis; } set { _FechaPrimerTratamientoSifilis = ValidateValue<string>(value, nameof(FechaPrimerTratamientoSifilis)); } }
        /// <sumary>
        /// Valores AAAA-MM  (Fecha de segundo tratamiento de la Sífilis )
        /// </sumary>
        private string _FechaSegundoTratamientoSifilis;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))$|^(9999-99)$", "Variable 101 - FechaSegundoTratamientoSifilis - Solo acepta formato fecha valido AAAA-MM o acepta (0000-00|9999-99)")] public string FechaSegundoTratamientoSifilis { get { return _FechaSegundoTratamientoSifilis; } set { _FechaSegundoTratamientoSifilis = ValidateValue<string>(value, nameof(FechaSegundoTratamientoSifilis)); } }
        /// <sumary>
        /// Valores Permiditos (AAAA-MM) Fecha de tercer tratamiento de la Sífilis
        /// </sumary>
        private string _FechaTercerTratamientoSifilis;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))$|^(9999-99)$", "Variable 102 - FechaTercerTratamientoSifilis - Solo acepta formato fecha valido AAAA-MM o acepta (0000-00|9999-99)")] public string FechaTercerTratamientoSifilis { get { return _FechaTercerTratamientoSifilis; } set { _FechaTercerTratamientoSifilis = ValidateValue<string>(value, nameof(FechaTercerTratamientoSifilis)); } }
        /// <sumary>
        /// valores posibles {0 a 16}  Novedad del paciente respecto al reporte anterior 
        /// </sumary>
        private string _NovedadPacienteReporte;
        [Order]
        [Regex(@"^([0-9]|1[0-6])$", "Variable 103 - NovedadPacienteReporte - Solo permite numeros del 0 al 16")] public string NovedadPacienteReporte { get { return _NovedadPacienteReporte; } set { _NovedadPacienteReporte = ValidateValue<string>(value, nameof(NovedadPacienteReporte)); } }
        /// <sumary>
        /// AAAA-MM-DD  (Fecha de desafiliación de la EPS)
        /// </sumary>
        private string _FechaDesafiliacionEPS;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))(\-)([0-2][0-9]|(3)[0-1])$|^(9999-99-99)", "Variable 104 - FechaDesafiliacionEPS - Solo acepta formato fecha valido AAAA-MM-DD o acepta (9999-99-99)")] public string FechaDesafiliacionEPS { get { return _FechaDesafiliacionEPS; } set { _FechaDesafiliacionEPS = ValidateValue<string>(value, nameof(FechaDesafiliacionEPS)); } }
        /// <sumary>
        /// EPS o Entidad Territorial al cual se trasladó el paciente con VIH des afiliado
        /// </sumary>
        private string _EntidadTrasladoPacienteVIH;
        [Order]
        [Regex(@"^([0-9]{1,12})$", "Variable 105 - EntidadTrasladoPacienteVIH - Permite solo numeros con una longitud de 5 Caracteres")] public string EntidadTrasladoPacienteVIH { get { return _EntidadTrasladoPacienteVIH; } set { _EntidadTrasladoPacienteVIH = ValidateValue<string>(value, nameof(EntidadTrasladoPacienteVIH)); } }
        /// <sumary>
        /// AAAA-MM-DD (Fecha de Muerte )
        /// </sumary>
        private string _FechaMuerte;
        [Order]
        [Regex(@"^\d{4}(\-)(((0)[0-9])|((1)[0-2]))(\-)([0-2][0-9]|(3)[0-1])$|^(9999-99-99)", "Variable 106 - FechaMuerte - Solo acepta formato fecha valido AAAA-MM-DD o acepta (9999-99-99)")] public string FechaMuerte { get { return _FechaMuerte; } set { _FechaMuerte = ValidateValue<string>(value, nameof(FechaMuerte)); } }
        /// <sumary>
        /// valores posibles {0,1,2,3} Causa de Muerte
        /// </sumary>
        private string _CausaMuerte;
        [Order]
        [Regex(@"^[0-3]{1}$", "Variable 107 - CausaMuerte - Solo permite numeros (0,1,2,3) y la longitud válida es 1 caracter")] public string CausaMuerte { get { return _CausaMuerte; } set { _CausaMuerte = ValidateValue<string>(value, nameof(CausaMuerte)); } }
        #endregion

        #region Builders
        public ENT_StructureRes4725() : base(null) { ExtrictValidation = false; }
        public ENT_StructureRes4725(object obj) : base(obj) { ExtrictValidation = false; }
        #endregion

        #region Body

        #endregion
    }
    /// <sumary>
    /// entidad para recibir los parametros de 4725
    /// </sumary>
    public class ENT_parameters4725 : EntityBase
    {
        #region Properties
        /// <sumary>
        /// id de la libreria ftp
        /// </sumary>
        private long _LibraryId;
        [Order]
        public long LibraryId { get { return _LibraryId; } set { _LibraryId = ValidateValue<long>(value, nameof(LibraryId)); } }
        /// <sumary>
        /// compañia id
        /// </sumary>
        private long _CompanyId;
        [Order]
        public long CompanyId { get { return _CompanyId; } set { _CompanyId = ValidateValue<long>(value, nameof(CompanyId)); } }
        /// <sumary>
        /// Numero del caso
        /// </sumary>
        private string _CaseNumber;
        [Order]
        public string CaseNumber { get { return _CaseNumber; } set { _CaseNumber = ValidateValue<string>(value, nameof(CaseNumber)); } }
        /// <sumary>
        /// Codigo del Usuario
        /// </sumary>
        private string _UserCode;
        [Order]
        public string UserCode { get { return _UserCode; } set { _UserCode = ValidateValue<string>(value, nameof(UserCode)); } }
        /// <sumary>
        /// id del archivo
        /// </sumary>
        private string _FileId;
        [Order]
        public string FileId { get { return _FileId; } set { _FileId = ValidateValue<string>(value, nameof(FileId)); } }
        /// <sumary>
        /// Codigo del Archivo
        /// </sumary>
        private string _CodeFile;
        [Order]
        public string CodeFile { get { return _CodeFile; } set { _CodeFile = ValidateValue<string>(value, nameof(CodeFile)); } }
        /// <sumary>
        /// año del periodo
        /// </sumary>
        private long _year;
        [Order]
        public long year { get { return _year; } set { _year = ValidateValue<long>(value, nameof(year)); } }
        /// <sumary>
        /// id del operador
        /// </sumary>
        private long _operatorId;
        [Order]
        public long operatorId { get { return _operatorId; } set { _operatorId = ValidateValue<long>(value, nameof(operatorId)); } }
        /// <sumary>
        /// id del tipo de población
        /// </sumary>
        private long _IdTypePopulation;
        [Order]
        public long IdTypePopulation { get { return _IdTypePopulation; } set { _IdTypePopulation = ValidateValue<long>(value, nameof(IdTypePopulation)); } }
        #endregion

        #region Builders
        public ENT_parameters4725() : base(null) { ExtrictValidation = false; }
        public ENT_parameters4725(object obj) : base(obj) { ExtrictValidation = false; }
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