
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

namespace OpheliaSuiteV2.BRMRuntime
{
    /// <sumary>
    /// VC_Execute1393File_Expression
    /// </sumary> 
    public sealed class VC_Execute1393File_Expression
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Id de la libreria
        /// </sumary>
        private long LibraryIdIn;
        /// <sumary>
        /// id de la compañia
        /// </sumary>
        private long CompanyIdIn;
        /// <sumary>
        /// numero del caso
        /// </sumary>
        private string CaseNumberIn;
        /// <sumary>
        /// Codigo del Usuario
        /// </sumary>
        private string UserCodeIn;
        /// <sumary>
        /// identificador del archivo
        /// </sumary>
        private string FileIdIn;
        /// <sumary>
        /// codigo del archivo
        /// </sumary>
        private string CodeFileIn;
        /// <sumary>
        /// periodo del cargue
        /// </sumary>
        private long periodln;
        /// <sumary>
        /// Año
        /// </sumary>
        private long yearln;
        /// <sumary>
        /// Id del Operador
        /// </sumary>
        private long operatorIdln;
        /// <sumary>
        /// IdTypePopulationln
        /// </sumary>
        private long IdTypePopulationln;
        /// <sumary>
        /// Guarda mensaje de error 
        /// </sumary>
        private string ResultMessage;
        /// <sumary>
        /// Valida los archivos y ejecuta las reglas de 1393
        /// </sumary>
        private bool VC_Execute1393File;
        #endregion

        #region Members
        /// <sumary>
        /// Valida sección de medicamentos de la resolución
        /// </sumary>
        private readonly RUL_ValidateMedicine RUL_ValidateMedicine = new RUL_ValidateMedicine();
        /// <sumary>
        /// Valida segunda parte de exámenes médicos 
        /// </sumary>
        private readonly RUL_ValidateDiagnosis RUL_ValidateDiagnosis = new RUL_ValidateDiagnosis();
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        private readonly RUL_Validate1393Treatment RUL_Validate1393Treatment = new RUL_Validate1393Treatment();
        /// <sumary>
        /// Valida los datos de exámenes del afiliado
        /// </sumary>
        private readonly RUL_Validate1393TestData RUL_Validate1393TestData = new RUL_Validate1393TestData();
        /// <sumary>
        /// Valida segunda parte de periodos de la r 1393
        /// </sumary>
        private readonly RUL_Validate1393periodTwo RUL_Validate1393periodTwo = new RUL_Validate1393periodTwo();
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        private readonly RUL_Validate1393period RUL_Validate1393period = new RUL_Validate1393period();
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        private readonly RUL_Validate1393Consultation RUL_Validate1393Consultation = new RUL_Validate1393Consultation();
        /// <sumary>
        /// Valida los campos de 1393
        /// </sumary>
        private readonly RUL_Validate1393AfiliateData RUL_Validate1393AfiliateData = new RUL_Validate1393AfiliateData();
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public VC_Execute1393File_Expression() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// VC_Execute1393File_Expression
        /// </sumary>
        /// <param name="LibraryIdIn">Id de la libreria</param>
        /// <param name="CompanyIdIn">id de la compañia</param>
        /// <param name="CaseNumberIn">numero del caso</param>
        /// <param name="UserCodeIn">Codigo del Usuario</param>
        /// <param name="FileIdIn">identificador del archivo</param>
        /// <param name="CodeFileIn">codigo del archivo</param>
        /// <param name="periodln">periodo del cargue</param>
        /// <param name="yearln">Año</param>
        /// <param name="operatorIdln">Id del Operador</param>
        /// <param name="IdTypePopulationln">IdTypePopulationln</param>
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
                this.VC_Execute1393File = FUNC_VC_Execute1393File();
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
        private bool FUNC_VC_Execute1393File()
        {

            #region Validate Parameters


            if (periodln < 1 || periodln > 2) throw new ArgumentException($"El periodo de Cargue no es valido  debe ser (1 o 2) {periodln}");
            if (operatorIdln <= 0) throw new ArgumentException($"El Id del operador debe ser Mayor a 0 {operatorIdln}");
            if (CompanyIdIn <= 0) throw new ArgumentException($"El CompanyId  debe ser Mayor a 0 {CompanyIdIn}");
            if (string.IsNullOrWhiteSpace(CaseNumberIn)) throw new ArgumentException($"El Numero del Caso no puede ser Vacio {CaseNumberIn}");
            if (string.IsNullOrWhiteSpace(UserCodeIn)) throw new ArgumentException($"El codigo del usuario no puede ser Vacio {UserCodeIn}");
            if (string.IsNullOrWhiteSpace(FileIdIn)) throw new ArgumentException($"El Id del archivo no puede ser Vacio {FileIdIn}");
            if (IdTypePopulationln <= 0) throw new ArgumentException($" {nameof(IdTypePopulationln)} Valor no valido debe ser mayor a 0 {IdTypePopulationln}");
            if (LibraryIdIn <= 0) throw new ArgumentException($" {nameof(LibraryIdIn)} Valor no valido debe ser mayor a 0 {LibraryIdIn}");



            #endregion

            #region Valida Tipo de poblacion
            int adapterId = 1;
            var sql = new StringBuilder();

            sql.Append(" SELECT Id ");
            sql.Append(" FROM TypeDetail WITH(NOLOCK)");
            sql.Append(" WHERE IdTypeHead = 72");
            sql.AppendFormat(" AND  Code = {0} ", IdTypePopulationln);

            var resultTypepopulation = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (!resultTypepopulation.IsSuccessful)
            {
                this.ResultMessage = resultTypepopulation.ErrorMessage;
                return false;
            }
            dynamic idtypepopulation = JsonConvert.DeserializeObject<dynamic>(resultTypepopulation.Result.ToString());

            if (idtypepopulation == null || idtypepopulation.Count <= 0)
            {
                this.ResultMessage = "No se encontro el tipo de poblacion";
                return false;
            }

            int info = (int)((JProperty)((JContainer)((JContainer)idtypepopulation).First).First).Value;

            #endregion

            #region ValidaTipoOperador
            sql = new StringBuilder();

            sql.Append(" SELECT Id ");
            sql.Append(" FROM Operator WITH(NOLOCK)");
            sql.AppendFormat(" WHERE  Id = {0} ", operatorIdln);

            var resultIdOperator = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());
            if (!resultIdOperator.IsSuccessful)
            {
                this.ResultMessage = resultIdOperator.ErrorMessage;
                return false;
            }

            var _idOperator = JsonConvert.DeserializeObject<List<dynamic>>(resultIdOperator.Result.ToString());

            if (_idOperator == null || _idOperator.Count() == 0)
            {
                this.ResultMessage = "Id operador invalido";
                return false;
            }

            #endregion

            #region Validate Dates
            var resulDate = Helper.USR_GetDatePeriod(periodln, yearln);
            if (!resulDate.IsSuccessful)
            {
                this.ResultMessage = resulDate.ErrorMessage;
                return false;
            }

            var IsValidPeriod = Helper.USR_ValidatePeriodReported(resulDate.Result.InitPeriod, resulDate.Result.EndPeriod, operatorIdln, info);

            if (!IsValidPeriod.IsSucessfull)
            {
                this.ResultMessage = IsValidPeriod.ErrorMessage;
                return false;
            }
            #endregion

            var resultValidationS = Helper.USR_MainStructure1393(CompanyIdIn, LibraryIdIn, FileIdIn, UserCodeIn, CaseNumberIn);

            if (!resultValidationS.IsSucessfull)
            {
                this.FileName = resultValidationS.FileName;
                this.ResultMessage = resultValidationS.ErrorMessage;
                return false;
            }

            var ListEntidad = (List<ENT_Resolution1393>)resultValidationS.Result;

            #region optiene Operadore
            sql = new StringBuilder();

            // consulta todo los operadores  para validar en memoria
            sql.Append(" SELECT DISTINCT  QualificationCode");
            sql.Append(" FROM Operator WITH(NOLOCK)");
            sql.Append(" where IdOperatorType = 70");

            var operatorResul = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (operatorResul.IsError)
            {
                this.FileName = operatorResul.FileName;
                this.ResultMessage = operatorResul.ErrorMessage;
                return false;
            }
            var listaQualification = JsonConvert.DeserializeObject<List<dynamic>>(operatorResul.Result.ToString());
            #endregion

            #region  Consulta Codigo CUM

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

            #endregion

            #region carga la configuracion de los campos para validar

            sql = new StringBuilder();
            sql.Append("SELECT CodeFunction AS [Key], [Status] AS [Value] ");
            sql.Append(" FROM ConfigurationDetail WHERE IdConfigurationHead = '1393' ");

            var configurationFields = Helper.SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (configurationFields.IsError)
            {
                this.FileName = configurationFields.FileName;
                this.ResultMessage = configurationFields.ErrorMessage;
                return false;
            }



            Dictionary<string, bool> pairs = new Dictionary<string, bool>();

            var s = JsonConvert.DeserializeObject<List<dynamic>>(configurationFields.Result.ToString());

            #endregion


            #region codigo municipio

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

            #endregion


            var listErrors = new List<string>();
            pairs = s.ToDictionary(x => (string)x.Key, x => (bool)x.Value);
            int index = 0;

            var documentTypes = Helper.USR_GetDocumentTypes(adapterId);
            var listDocumentTypesDB = JsonConvert.DeserializeObject<List<dynamic>>(documentTypes.Result.ToString());
            Helper.USR_ValidateDocumentNumber1393(ListEntidad, listErrors, index, listDocumentTypesDB);

            index = 0;

            string cod = "ValidateField";

            #region optiene Valida reglas
            foreach (var item in ListEntidad)
            {
                #region Ejecucion de reglas
                var res = Helper.SYS_VerificationPrototype(new Func<object>[] {
        () => RUL_Validate1393AfiliateData.Execute(
            pairs[$"{cod}{nameof(item.Regime)}"] ? item.Regime : "TRUE",
            pairs[$"{cod}{nameof(item.PopulationGroup)}"] ? Convert.ToInt32(item.PopulationGroup) : int.MinValue,
            item.BirthDate,
            pairs[$"{cod}{nameof(item.Sex)}"] ? item.Sex : "TRUE",
            pairs[$"{cod}{nameof(item.Ethnicity)}"] ? Convert.ToInt32(item.Ethnicity) : int.MinValue,
            1,
            Convert.ToDateTime(item.AffiliationDate)
            ),

        #region 1393period
        () => RUL_Validate1393period.Execute(
            pairs[$"{cod}{nameof(item.WeightLastSemester)}"] ? Convert.ToInt32(item.WeightLastSemester) : int.MinValue,
            pairs[$"{cod}{nameof(item.LastHandXRay)}"] ? Convert.ToInt32(item.LastHandXRay) : int.MinValue,
            pairs[$"{cod}{nameof(item.LastFootXRay)}"] ? Convert.ToInt32(item.LastFootXRay) : int.MinValue,
            pairs[$"{cod}{nameof(item.PCRLastSemester)}"] ? Convert.ToInt32(item.PCRLastSemester) : int.MinValue,
            pairs[$"{cod}{nameof(item.VSGLastSemester)}"] ? Convert.ToInt32(item.VSGLastSemester) : int.MinValue,
            pairs[$"{cod}{nameof(item.HemoglobinLastSemester)}"] ? Convert.ToInt32(item.HemoglobinLastSemester) : int.MinValue,
            pairs[$"{cod}{nameof(item.LeukocytesLastSemester)}"] ? Convert.ToInt32(item.LeukocytesLastSemester) : int.MinValue,
            pairs[$"{cod}{nameof(item.CreatinineLastSemester)}"] ? Convert.ToInt32(item.CreatinineLastSemester) : int.MinValue,
            pairs[$"{cod}{nameof(item.TFGLastSemester)}"] ? Convert.ToInt32(item.TFGLastSemester) : int.MinValue,
            pairs[$"{cod}{nameof(item.PartialUrineLastSemester)}"] ? Convert.ToInt32(item.PartialUrineLastSemester) : int.MinValue,
            pairs[$"{cod}{nameof(item.ALTLastSemester)}"] ? Convert.ToInt32(item.ALTLastSemester) : int.MinValue,
            pairs[$"{cod}{nameof(item.CurrentHTA)}"] ? Convert.ToInt32(item.CurrentHTA) : int.MinValue,
            pairs[$"{cod}{nameof(item.CurrentDM)}"] ? Convert.ToInt32(item.CurrentDM) : int.MinValue,
            pairs[$"{cod}{nameof(item.CurrentECV)}"] ? Convert.ToInt32(item.CurrentECV) : int.MinValue,
            pairs[$"{cod}{nameof(item.CurrentERC)}"] ? Convert.ToInt32(item.CurrentERC) : int.MinValue,
            pairs[$"{cod}{nameof(item.CurrentOsteoporosis)}"] ? Convert.ToInt32(item.CurrentOsteoporosis) : int.MinValue,
            pairs[$"{cod}{nameof(item.CurrentSyndromeSjogren)}"] ? Convert.ToInt32(item.CurrentSyndromeSjogren) : int.MinValue
            ), 
        #endregion

        #region 1393periodTwo
        () => RUL_Validate1393periodTwo.Execute(
            item.DateLastDAS28,
            item.DateFirstDAS28,
            pairs[$"{cod}{nameof(item.ProfessionalLastDAS28)}"] ? Convert.ToInt32(item.ProfessionalLastDAS28) : int.MinValue,
            pairs[$"{cod}{nameof(item.ResultLastDAS28)}"] ? Convert.ToInt32(item.ResultLastDAS28) : int.MinValue,
            pairs[$"{cod}{nameof(item.StatusCurrentActivityDAS28)}"] ? Convert.ToInt32(item.StatusCurrentActivityDAS28) : int.MinValue,
            item.DateLastHAQ,
            item.DateFirstHAQ,
            pairs[$"{cod}{nameof(item.HAQLastSemester)}"] ? Convert.ToInt32(item.HAQLastSemester) : int.MinValue,
            pairs[$"{cod}{nameof(item.AnalgesicNotOpioids)}"] ? Convert.ToInt32(item.AnalgesicNotOpioids) : int.MinValue,
            pairs[$"{cod}{nameof(item.AnalgesicOpioids)}"] ? Convert.ToInt32(item.AnalgesicOpioids) : int.MinValue,
            pairs[$"{cod}{nameof(item.AINES)}"] ? Convert.ToInt32(item.AINES) : int.MinValue,
            pairs[$"{cod}{nameof(item.Corticosteroids)}"] ? Convert.ToInt32(item.Corticosteroids) : int.MinValue,
            pairs[$"{cod}{nameof(item.MonthsUseGlucocorticoids)}"] ? Convert.ToInt32(item.MonthsUseGlucocorticoids) : int.MinValue,
            pairs[$"{cod}{nameof(item.Calcium)}"] ? Convert.ToInt32(item.Calcium) : int.MinValue,
            pairs[$"{cod}{nameof(item.VitaminD)}"] ? Convert.ToInt32(item.VitaminD) : int.MinValue
            ),

        #endregion

        #region 1393Consultation
        () => RUL_Validate1393Consultation.Execute(
            pairs[$"{cod}{nameof(item.RheumatologistConsultNumberLastYear)}"] ? Convert.ToInt32(item.RheumatologistConsultNumberLastYear) : int.MinValue,
            pairs[$"{cod}{nameof(item.InternistConsultNumberLastYear)}"] ? Convert.ToInt32(item.InternistConsultNumberLastYear) : int.MinValue,
            pairs[$"{cod}{nameof(item.FamilyDoctorConsultNumberLastYear)}"] ? Convert.ToInt32(item.FamilyDoctorConsultNumberLastYear) : int.MinValue,
            pairs[$"{cod}{nameof(item.JointReplacementOne)}"] ? Convert.ToInt32(item.JointReplacementOne) : int.MinValue,
            pairs[$"{cod}{nameof(item.JointReplacementTwo)}"] ? Convert.ToInt32(item.JointReplacementTwo) : int.MinValue,
            pairs[$"{cod}{nameof(item.JointReplacementThree)}"] ? Convert.ToInt32(item.JointReplacementThree) : int.MinValue,
            pairs[$"{cod}{nameof(item.JointReplacementFour)}"] ? Convert.ToInt32(item.JointReplacementFour) : int.MinValue,
            pairs[$"{cod}{nameof(item.NumberHospitalizationsLastYear)}"] ? Convert.ToInt32(item.NumberHospitalizationsLastYear) : int.MinValue,
            item.DateDiagnosis,
            pairs[$"{cod}{nameof(item.CurrentDoctorAR)}"] ? Convert.ToInt32(item.CurrentDoctorAR) : int.MinValue,
            pairs[$"{cod}{nameof(item.NoveltyPatient)}"] ? Convert.ToInt32(item.NoveltyPatient) : int.MinValue,
            item.AffiliationDate,
            item.DisenrollmentDate,
            item.DeathDate,
            item.BirthDate,
            pairs[$"{cod}{nameof(item.DeathCause)}"] ? Convert.ToInt32(item.DeathCause) : int.MinValue
            ),

        #endregion

        #region 1393Treatment
        () => RUL_Validate1393Treatment.Execute(
            item.InitialDateTreatmentDMARD,
            item.InitialDateTreatmentDMARDTwo,
            pairs[$"{cod}{nameof(item.Azathioprine)}"] ? Convert.ToInt32(item.Azathioprine) : int.MinValue,
            pairs[$"{cod}{nameof(item.Ciclosporina)}"] ? Convert.ToInt32(item.Ciclosporina) : int.MinValue,
            pairs[$"{cod}{nameof(item.Cyclophosphamide)}"] ? Convert.ToInt32(item.Cyclophosphamide) : int.MinValue,
            pairs[$"{cod}{nameof(item.Chloroquine)}"] ? Convert.ToInt32(item.Chloroquine) : int.MinValue,
            pairs[$"{cod}{nameof(item.Dpenicillamine)}"] ? Convert.ToInt32(item.Dpenicillamine) : int.MinValue,
            pairs[$"{cod}{nameof(item.Etanercept)}"] ? Convert.ToInt32(item.Etanercept) : int.MinValue,
            pairs[$"{cod}{nameof(item.Leflunomide)}"] ? Convert.ToInt32(item.Leflunomide) : int.MinValue,
            pairs[$"{cod}{nameof(item.Methotrexate)}"] ? Convert.ToInt32(item.Methotrexate) : int.MinValue,
            pairs[$"{cod}{nameof(item.Rituximab)}"] ? Convert.ToInt32(item.Rituximab) : int.MinValue,
            pairs[$"{cod}{nameof(item.Sulfasalazine)}"] ? Convert.ToInt32(item.Sulfasalazine) : int.MinValue,
            pairs[$"{cod}{nameof(item.Abatacept)}"] ? Convert.ToInt32(item.Abatacept) : int.MinValue,
            pairs[$"{cod}{nameof(item.Adalimumab)}"] ? Convert.ToInt32(item.Adalimumab) : int.MinValue,
            pairs[$"{cod}{nameof(item.Certolizumab)}"] ? Convert.ToInt32(item.Certolizumab) : int.MinValue,
            pairs[$"{cod}{nameof(item.Golimumab)}"] ? Convert.ToInt32(item.Golimumab) : int.MinValue,
            pairs[$"{cod}{nameof(item.Hydroxychloroquine)}"] ? Convert.ToInt32(item.Hydroxychloroquine) : int.MinValue,
            pairs[$"{cod}{nameof(item.Infliximab)}"] ? Convert.ToInt32(item.Infliximab) : int.MinValue,
            pairs[$"{cod}{nameof(item.GoldSaltsOne)}"] ? Convert.ToInt32(item.GoldSaltsOne) : int.MinValue,
            pairs[$"{cod}{nameof(item.Tocilizumab)}"] ? Convert.ToInt32(item.Tocilizumab) : int.MinValue,
            pairs[$"{cod}{nameof(item.Tofacitinib)}"] ? Convert.ToInt32(item.Tofacitinib) : int.MinValue,
            pairs[$"{cod}{nameof(item.Anakinra)}"] ? Convert.ToInt32(item.Anakinra) : int.MinValue
        ),

        #endregion

        #region RUL_ValidateMedicine
        () => RUL_ValidateMedicine.Execute(
            pairs[$"{cod}{nameof(item.Azatioprina)}"] ? Convert.ToInt32(item.Azatioprina) : int.MinValue,
            pairs[$"{cod}{nameof(item.Ciclosporina)}"] ? Convert.ToInt32(item.Ciclosporina) : int.MinValue,
            pairs[$"{cod}{nameof(item.Ciclofosfamida)}"] ? Convert.ToInt32(item.Ciclofosfamida) : int.MinValue,
            pairs[$"{cod}{nameof(item.Cloroquina)}"] ? Convert.ToInt32(item.Cloroquina) : int.MinValue,
            pairs[$"{cod}{nameof(item.DPenicilamina)}"] ? Convert.ToInt32(item.DPenicilamina) : int.MinValue,
            pairs[$"{cod}{nameof(item.Etanercept)}"] ? Convert.ToInt32(item.Etanercept) : int.MinValue,
            pairs[$"{cod}{nameof(item.Leflunomida)}"] ? Convert.ToInt32(item.Leflunomida) : int.MinValue,
            pairs[$"{cod}{nameof(item.Metotrexate)}"] ? Convert.ToInt32(item.Metotrexate) : int.MinValue,
            pairs[$"{cod}{nameof(item.Rituximab)}"] ? Convert.ToInt32(item.Rituximab) : int.MinValue,
            pairs[$"{cod}{nameof(item.Sulfasalazina)}"] ? Convert.ToInt32(item.Sulfasalazina) : int.MinValue,
            pairs[$"{cod}{nameof(item.Abatacept)}"] ? Convert.ToInt32(item.Abatacept) : int.MinValue,
            pairs[$"{cod}{nameof(item.Adalimumab)}"] ? Convert.ToInt32(item.Adalimumab) : int.MinValue,
            pairs[$"{cod}{nameof(item.Certolizumab)}"] ? Convert.ToInt32(item.Certolizumab) : int.MinValue,
            pairs[$"{cod}{nameof(item.Golimumab)}"] ? Convert.ToInt32(item.Golimumab) : int.MinValue,
            pairs[$"{cod}{nameof(item.Hidroxicloroquina)}"] ? Convert.ToInt32(item.Hidroxicloroquina) : int.MinValue,
            pairs[$"{cod}{nameof(item.Infliximab)}"] ? Convert.ToInt32(item.Infliximab) : int.MinValue,
            pairs[$"{cod}{nameof(item.GoldSalts)}"] ? Convert.ToInt32(item.GoldSalts) : int.MinValue,
            pairs[$"{cod}{nameof(item.Tocilizumab)}"] ? Convert.ToInt32(item.Tocilizumab) : int.MinValue,
            pairs[$"{cod}{nameof(item.Tofacitinib)}"] ? Convert.ToInt32(item.Tofacitinib) : int.MinValue,
            pairs[$"{cod}{nameof(item.Anakinra)}"] ? Convert.ToInt32(item.Anakinra) : int.MinValue
            ), 
#endregion

        #region Diagnosis
                () => RUL_ValidateDiagnosis.Execute(
            item.DateLastDAS28,
            pairs[$"{cod}{nameof(item.ProfessionalFirstDAS28)}"] ? Convert.ToInt32(item.ProfessionalFirstDAS28) : int.MinValue,
            pairs[$"{cod}{nameof(item.ResultFirstDAS28)}"] ? Convert.ToInt32(item.ResultFirstDAS28) : int.MinValue,
            item.DateFirstHAQ,
            item.DateDiagnosis,
            pairs[$"{cod}{nameof(item.InitialHAQ)}"] ? Convert.ToInt32(item.InitialHAQ) : int.MinValue,
            item.InitialDateTreatmentDMARD,
            pairs[$"{cod}{nameof(item.InitialAnalgesicNotOpioids)}"] ? Convert.ToInt32(item.InitialAnalgesicNotOpioids) : int.MinValue,
            pairs[$"{cod}{nameof(item.InitialAnalgesicOpioids)}"] ? Convert.ToInt32(item.InitialAnalgesicOpioids) : int.MinValue,
            pairs[$"{cod}{nameof(item.StartAINES)}"] ? Convert.ToInt32(item.StartAINES) : int.MinValue,
            pairs[$"{cod}{nameof(item.StartCorticosteroids)}"] ? Convert.ToInt32(item.StartCorticosteroids) : int.MinValue,
            item.InitialDateTreatmentDMARDTwo ,
            pairs[$"{cod}{nameof(item.InitialScreeningDMARD)}"] ? Convert.ToInt32(item.InitialScreeningDMARD) : int.MinValue,
            pairs[$"{cod}{nameof(item.LymphomaHistoryDMARD)}"] ? Convert.ToInt32(item.LymphomaHistoryDMARD) : int.MinValue
            ),

#endregion

        #region ruleTestData
                () => RUL_Validate1393TestData.Execute(
            item.InitDateSymptom,
            item.FirstVisitDateSpecialist,
            item.DateDiagnosis,
            pairs[$"{cod}{nameof(item.Height)}"] ? Convert.ToInt32(item.Height) : int.MinValue,
            pairs[$"{cod}{nameof(item.Weight)}"] ? Convert.ToInt32(item.Weight) : int.MinValue,
            pairs[$"{cod}{nameof(item.HandXRay)}"] ? Convert.ToInt32(item.HandXRay) : int.MinValue,
            pairs[$"{cod}{nameof(item.FootXRay)}"] ? Convert.ToInt32(item.FootXRay) : int.MinValue,
            pairs[$"{cod}{nameof(item.InitialVSG)}"] ? Convert.ToInt32(item.InitialVSG) : int.MinValue,
            pairs[$"{cod}{nameof(item.InitialPCR)}"] ? Convert.ToInt32(item.InitialPCR) : int.MinValue,
            pairs[$"{cod}{nameof(item.InitialRheumatoidFactor)}"] ? Convert.ToInt32(item.InitialRheumatoidFactor) : int.MinValue,
            pairs[$"{cod}{nameof(item.InitialHemoglobin)}"] ? Convert.ToInt32(item.InitialHemoglobin) : int.MinValue,
            pairs[$"{cod}{nameof(item.InitialLeukocytes)}"] ? Convert.ToInt32(item.InitialLeukocytes) : int.MinValue,
            pairs[$"{cod}{nameof(item.InitialCeatrinin)}"] ? Convert.ToInt32(item.InitialCeatrinin) : int.MinValue,
            pairs[$"{cod}{nameof(item.InitialTFG)}"] ? Convert.ToInt32(item.InitialTFG) : int.MinValue,
            pairs[$"{cod}{nameof(item.InitialUrinePartial)}"] ? Convert.ToInt32(item.InitialUrinePartial) : int.MinValue,
            pairs[$"{cod}{nameof(item.InicialALT)}"] ? Convert.ToInt32(item.InicialALT) : int.MinValue,
            pairs[$"{cod}{nameof(item.AntiCCPDiagnosis)}"] ? Convert.ToInt32(item.AntiCCPDiagnosis) : int.MinValue,
            pairs[$"{cod}{nameof(item.HTADiagnosis)}"] ? Convert.ToInt32(item.HTADiagnosis) : int.MinValue,
            pairs[$"{cod}{nameof(item.DMDiagnosis)}"] ? Convert.ToInt32(item.DMDiagnosis) : int.MinValue,
            pairs[$"{cod}{nameof(item.ECVDiagnosis)}"] ? Convert.ToInt32(item.ECVDiagnosis) : int.MinValue,
            pairs[$"{cod}{nameof(item.ERCDiagnosis)}"] ? Convert.ToInt32(item.ERCDiagnosis) : int.MinValue,
            pairs[$"{cod}{nameof(item.OsteoporosisDiagnosis)}"] ? Convert.ToInt32(item.OsteoporosisDiagnosis) : int.MinValue,
            pairs[$"{cod}{nameof(item.SyndromeSjogren)}"] ? Convert.ToInt32(item.SyndromeSjogren) : int.MinValue)
                        });
                #endregion

                #endregion

                Helper.USR_ValidateQualificationCodeAndCodeCumMunicipaly(listErrors, index, listaQualification, listaCum, listaCodeMunicipaly, item);

                Helper.USR_ValidateCumTar(listErrors, index, listaCum, item.OtherMedicineNoPOSOne, "Codigo no se encuentra parametrizado.Validar la variable 73 Otro medicamento NO POS");
                Helper.USR_ValidateCumTar(listErrors, index, listaCum, item.OtherMedicineNoPOSTwo, "Codigo no se encuentra parametrizado.Validar la variable 74 Otro medicamento NO POS 2");
                Helper.USR_ValidateCumTar(listErrors, index, listaCum, item.OtherMedicineNoPOSThree, "Si requiere otro medicamento NO POS digitar codigo CUM, de lo contrario digitar 0. Validar la variable 75 Otro medicamento NO POS 3");
                Helper.USR_ValidateCumTar(listErrors, index, listaCum, item.OtherMedicineNoPOSFour, "Si requiere otro medicamento NO POS digitar codigo CUM, de lo contrario digitar 0. Validar la variable 76 Otro medicamento NO POS 4");

                Helper.USR_ValidateCumTar(listErrors, index, listaCum, item.OtherMedicineNotPosOne, "Codigo no se encuentra parametrizado.Validar la variable 128 Otro medicamento NO POS 1");
                Helper.USR_ValidateCumTar(listErrors, index, listaCum, item.OtherMedicineNotPosTwo, "Codigo no se encuentra parametrizado.Validar la variable 129 Otro medicamento NO POS 2");
                Helper.USR_ValidateCumTar(listErrors, index, listaCum, item.OtherMedicineNotPosThree, "Codigo no se encuentra parametrizado.Validar la variable 130 Otro medicamento NO POS 3");
                Helper.USR_ValidateCumTar(listErrors, index, listaCum, item.OtherMedicineNotPosFour, "Codigo no se encuentra parametrizado.Validar la variable 131 Otro medicamento NO POS 4");

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
            #endregion

            #region Save log 
            const string folder = "Resolution1393";
            if (listErrors.Count > 0)
            {
                string pathFile = Helper.USR_GenericSaveLog(new Dictionary<string, List<string>>() { ["1393"] = listErrors }, folder);
                var attach = Helper.USR_WSAttachFileToProcess(pathFile, UserCodeIn, CompanyIdIn.ToString(), CaseNumberIn, "1393");
                if (attach.IsError)
                {
                    this.ResultMessage = "No se pudo asociar el archivo al proceso. " + attach.ErrorMessage;
                    return false;
                }
                this.FileName = attach.FileName;
                this.ResultMessage = "Hubo errores en la validación ";
                return false;

            }

            #endregion


            #region save file data

            string cutOffDate = resulDate.Result.EndPeriod.ToString("MM/dd/yyyy");
            string code = "1393";
            string InitialDate = resulDate.Result.InitPeriod.ToString("MM/dd/yyyy");
            string endDate = resulDate.Result.EndPeriod.ToString("MM/dd/yyyy");

            #endregion

            var resultSave = Helper.USR_SaveData1393(cutOffDate, code, operatorIdln, CaseNumberIn, InitialDate, endDate, info, ListEntidad);

            if (resultSave.IsError)
            {
                this.ResultMessage = "Ocurrio un error al guardar el archivo " + resultSave.ErrorMessage;
                return false;
            }


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
    /// Valida sección de medicamentos de la resolución
    /// </sumary> 
    public sealed class RUL_ValidateMedicine
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Azatioprina
        /// </sumary>
        private long Azatioprina;
        /// <sumary>
        /// Ciclosporina
        /// </sumary>
        private long Ciclosporina;
        /// <sumary>
        /// Ciclofosfamida
        /// </sumary>
        private long Ciclofosfamida;
        /// <sumary>
        /// Cloroquina
        /// </sumary>
        private long Cloroquina;
        /// <sumary>
        /// DPenicilamina
        /// </sumary>
        private long DPenicilamina;
        /// <sumary>
        /// Etanercept
        /// </sumary>
        private long Etanercept;
        /// <sumary>
        /// Leflunomida
        /// </sumary>
        private long Leflunomida;
        /// <sumary>
        /// Metotrexate
        /// </sumary>
        private long Metotrexate;
        /// <sumary>
        /// valida Rituximab
        /// </sumary>
        private long Rituximab;
        /// <sumary>
        /// Sulfasalazina
        /// </sumary>
        private long Sulfasalazina;
        /// <sumary>
        /// Abatacept
        /// </sumary>
        private long Abatacept;
        /// <sumary>
        /// Adalimumab
        /// </sumary>
        private long Adalimumab;
        /// <sumary>
        /// Certolizumab
        /// </sumary>
        private long Certolizumab;
        /// <sumary>
        /// Golimumab
        /// </sumary>
        private long Golimumab;
        /// <sumary>
        /// Hidroxicloroquina
        /// </sumary>
        private long Hidroxicloroquina;
        /// <sumary>
        /// Infliximab
        /// </sumary>
        private long Infliximab;
        /// <sumary>
        /// GoldSalts
        /// </sumary>
        private long GoldSalts;
        /// <sumary>
        /// valida Tocilizumab
        /// </sumary>
        private long Tocilizumab;
        /// <sumary>
        /// validate Tofacitinib
        /// </sumary>
        private long Tofacitinib;
        /// <sumary>
        /// validate Anakinra
        /// </sumary>
        private long Anakinra;
        /// <sumary>
        /// Valida Azatioprina
        /// </sumary>
        private bool VC_ValidateAzatioprina;
        /// <sumary>
        /// Ciclosporina
        /// </sumary>
        private bool VC_ValidateCiclosporina;
        /// <sumary>
        /// valida Ciclofosfamida
        /// </sumary>
        private bool VC_ValidateCiclofosfamida;
        /// <sumary>
        /// Valida Cloroquina
        /// </sumary>
        private bool VC_Cloroquina;
        /// <sumary>
        /// valida DPenicilamina
        /// </sumary>
        private bool VC_ValidateDPenicilamina;
        /// <sumary>
        /// valida Etanercept
        /// </sumary>
        private bool VC_ValidateEtanercept;
        /// <sumary>
        /// valida Leflunomida
        /// </sumary>
        private bool VC_ValidateLeflunomida;
        /// <sumary>
        /// valida Metotrexate
        /// </sumary>
        private bool VC_ValidateMetotrexate;
        /// <sumary>
        /// valida Rituximab
        /// </sumary>
        private bool VC_ValidateRituximab;
        /// <sumary>
        /// valida Sulfasalazina
        /// </sumary>
        private bool VC_ValidateSulfasalazina;
        /// <sumary>
        /// valida Abatacept
        /// </sumary>
        private bool VC_ValidateAbatacept;
        /// <sumary>
        /// valida Adalimumab
        /// </sumary>
        private bool VC_ValidateAdalimumab;
        /// <sumary>
        /// valida Certolizumab
        /// </sumary>
        private bool VC_VlidateCertolizumab;
        /// <sumary>
        /// Valida Golimumab
        /// </sumary>
        private bool VC_ValidateGolimumab;
        /// <sumary>
        /// valida Hidroxicloroquina
        /// </sumary>
        private bool VC_ValidateHidroxicloroquina;
        /// <sumary>
        /// valida Infliximab
        /// </sumary>
        private bool VC_ValidateInfliximab;
        /// <sumary>
        /// valida GoldSalts
        /// </sumary>
        private bool VC_ValidateGoldSalts;
        /// <sumary>
        /// valida Tocilizumab
        /// </sumary>
        private bool VC_ValidateTocilizumab;
        /// <sumary>
        /// valida Tofacitinib
        /// </sumary>
        private bool VC_ValidateTofacitinib;
        /// <sumary>
        /// valida Anakinra
        /// </sumary>
        private bool VC_ValidateAnakinra;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_ValidateMedicine() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Valida sección de medicamentos de la resolución
        /// </sumary>
        /// <param name="Azatioprina">Azatioprina</param>
        /// <param name="Ciclosporina">Ciclosporina</param>
        /// <param name="Ciclofosfamida">Ciclofosfamida</param>
        /// <param name="Cloroquina">Cloroquina</param>
        /// <param name="DPenicilamina">DPenicilamina</param>
        /// <param name="Etanercept">Etanercept</param>
        /// <param name="Leflunomida">Leflunomida</param>
        /// <param name="Metotrexate">Metotrexate</param>
        /// <param name="Rituximab">valida Rituximab</param>
        /// <param name="Sulfasalazina">Sulfasalazina</param>
        /// <param name="Abatacept">Abatacept</param>
        /// <param name="Adalimumab">Adalimumab</param>
        /// <param name="Certolizumab">Certolizumab</param>
        /// <param name="Golimumab">Golimumab</param>
        /// <param name="Hidroxicloroquina">Hidroxicloroquina</param>
        /// <param name="Infliximab">Infliximab</param>
        /// <param name="GoldSalts">GoldSalts</param>
        /// <param name="Tocilizumab">valida Tocilizumab</param>
        /// <param name="Tofacitinib">validate Tofacitinib</param>
        /// <param name="Anakinra">validate Anakinra</param>
        public RuntimeResult<string> Execute(long Azatioprina, long Ciclosporina, long Ciclofosfamida, long Cloroquina, long DPenicilamina, long Etanercept, long Leflunomida, long Metotrexate, long Rituximab, long Sulfasalazina, long Abatacept, long Adalimumab, long Certolizumab, long Golimumab, long Hidroxicloroquina, long Infliximab, long GoldSalts, long Tocilizumab, long Tofacitinib, long Anakinra)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.Azatioprina = Azatioprina;
                this.Ciclosporina = Ciclosporina;
                this.Ciclofosfamida = Ciclofosfamida;
                this.Cloroquina = Cloroquina;
                this.DPenicilamina = DPenicilamina;
                this.Etanercept = Etanercept;
                this.Leflunomida = Leflunomida;
                this.Metotrexate = Metotrexate;
                this.Rituximab = Rituximab;
                this.Sulfasalazina = Sulfasalazina;
                this.Abatacept = Abatacept;
                this.Adalimumab = Adalimumab;
                this.Certolizumab = Certolizumab;
                this.Golimumab = Golimumab;
                this.Hidroxicloroquina = Hidroxicloroquina;
                this.Infliximab = Infliximab;
                this.GoldSalts = GoldSalts;
                this.Tocilizumab = Tocilizumab;
                this.Tofacitinib = Tofacitinib;
                this.Anakinra = Anakinra;
                this.VC_ValidateAzatioprina = FUNC_VC_ValidateAzatioprina();
                this.VC_ValidateCiclosporina = FUNC_VC_ValidateCiclosporina();
                this.VC_ValidateCiclofosfamida = FUNC_VC_ValidateCiclofosfamida();
                this.VC_Cloroquina = FUNC_VC_Cloroquina();
                this.VC_ValidateDPenicilamina = FUNC_VC_ValidateDPenicilamina();
                this.VC_ValidateEtanercept = FUNC_VC_ValidateEtanercept();
                this.VC_ValidateLeflunomida = FUNC_VC_ValidateLeflunomida();
                this.VC_ValidateMetotrexate = FUNC_VC_ValidateMetotrexate();
                this.VC_ValidateRituximab = FUNC_VC_ValidateRituximab();
                this.VC_ValidateSulfasalazina = FUNC_VC_ValidateSulfasalazina();
                this.VC_ValidateAbatacept = FUNC_VC_ValidateAbatacept();
                this.VC_ValidateAdalimumab = FUNC_VC_ValidateAdalimumab();
                this.VC_VlidateCertolizumab = FUNC_VC_VlidateCertolizumab();
                this.VC_ValidateGolimumab = FUNC_VC_ValidateGolimumab();
                this.VC_ValidateHidroxicloroquina = FUNC_VC_ValidateHidroxicloroquina();
                this.VC_ValidateInfliximab = FUNC_VC_ValidateInfliximab();
                this.VC_ValidateGoldSalts = FUNC_VC_ValidateGoldSalts();
                this.VC_ValidateTocilizumab = FUNC_VC_ValidateTocilizumab();
                this.VC_ValidateTofacitinib = FUNC_VC_ValidateTofacitinib();
                this.VC_ValidateAnakinra = FUNC_VC_ValidateAnakinra();
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
        private bool FUNC_VC_ValidateAzatioprina()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Azatioprina, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCiclosporina()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Ciclosporina, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCiclofosfamida()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Ciclofosfamida, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_Cloroquina()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Cloroquina, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateDPenicilamina()
        {
            return Helper.USR_ValidateGenericRange(0, 1, DPenicilamina, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateEtanercept()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Etanercept, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateLeflunomida()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Leflunomida, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateMetotrexate()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Metotrexate, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateRituximab()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Rituximab, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateSulfasalazina()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Sulfasalazina, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAbatacept()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Abatacept, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAdalimumab()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Adalimumab, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_VlidateCertolizumab()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Certolizumab, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateGolimumab()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Golimumab, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateHidroxicloroquina()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Hidroxicloroquina, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInfliximab()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Infliximab, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateGoldSalts()
        {
            return Helper.USR_ValidateGenericRange(0, 1, GoldSalts, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateTocilizumab()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Tocilizumab, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateTofacitinib()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Tofacitinib, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAnakinra()
        {
            return Helper.USR_ValidateGenericRange(0, 1, Anakinra, new List<long> { int.MinValue });
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_ValidateAzatioprina == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Azatioprina. Validar la variable 53 Azatioprina");
            if (!(VC_ValidateCiclosporina == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Ciclosporina. Validar la variable 54 Ciclosporina");
            if (!(VC_ValidateCiclofosfamida == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Ciclofosfamida. Validar la variable 55 Ciclofosfamida");
            if (!(VC_Cloroquina == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Cloroquina. Validar la variable 56 Cloroquina");
            if (!(VC_ValidateDPenicilamina == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a D-penicilamina. Validar la variable 57 D-penicilamina");
            if (!(VC_ValidateEtanercept == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Etanercept. Validar la variable 58 Etanercept");
            if (!(VC_ValidateLeflunomida == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Leflunomida. Validar la variable 59 Leflunomida");
            if (!(VC_ValidateMetotrexate == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Metotrexate. Validar la variable 60 Metotrexate");
            if (!(VC_ValidateRituximab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Rituximab. Validar la variable 61 Rituximab");
            if (!(VC_ValidateSulfasalazina == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Sulfasalazina. Validar la variable 62 Sulfasalazina");
            if (!(VC_ValidateAbatacept == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Abatacept. Validar la variable 63 Abatacept");
            if (!(VC_ValidateAdalimumab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Adalimumab. Validar la variable 64 Adalimumab");
            if (!(VC_VlidateCertolizumab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Certolizumab. Validar la variable 65 Certolizumab");
            if (!(VC_ValidateGolimumab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Golimumab. Validar la variable 66 Golimumab");
            if (!(VC_ValidateHidroxicloroquina == true)) NonValidMessages.Add($"La longitud del campo excede el valor permitido. Validar la variable 67 Hidroxicloroquina");
            if (!(VC_ValidateInfliximab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Infliximab. Validar la variable 68 Infliximab");
            if (!(VC_ValidateGoldSalts == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Sales de oro. Validar la variable 69 Sales de oro");
            if (!(VC_ValidateTocilizumab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Tocilizumab. Validar la variable 70 Tocilizumab");
            if (!(VC_ValidateTofacitinib == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Tofacitinib. Validar la variable 71 Tofacitinib");
            if (!(VC_ValidateAnakinra == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Anakinra. Validar la variable 72 Anakinra");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return ""; }, $"OK");
        }
        #endregion
    }
    /// <sumary>
    /// Valida segunda parte de exámenes médicos 
    /// </sumary> 
    public sealed class RUL_ValidateDiagnosis
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Fecha del primer DAS 28 realizado
        /// </sumary>
        private string DateFirstDAS28;
        /// <sumary>
        /// Profesional que realizo el primer DAS 28
        /// </sumary>
        private long ProfessionalFirstDAS28;
        /// <sumary>
        /// Resultado del primer DAS 28
        /// </sumary>
        private long ResultFirstDAS28;
        /// <sumary>
        /// Fecha del primer HAQ realizado
        /// </sumary>
        private string DateFirstHAQ;
        /// <sumary>
        /// DateDiagnosis
        /// </sumary>
        private string DateDiagnosis;
        /// <sumary>
        /// HAQ inicial
        /// </sumary>
        private long InitialHAQ;
        /// <sumary>
        /// Fecha inicio tratamiento con DMARD
        /// </sumary>
        private string InitialDateTreatmentNotDMARD;
        /// <sumary>
        /// Analgésicos No Opioides al inicio
        /// </sumary>
        private long InitialAnalgesicNotOpioids;
        /// <sumary>
        /// Analgésicos Opioides (Codeina, Tramadol) al inicio
        /// </sumary>
        private long InitialAnalgesicOpioids;
        /// <sumary>
        /// AINES al inicio
        /// </sumary>
        private long StartAINES;
        /// <sumary>
        /// Corticoides al inicio
        /// </sumary>
        private long StartCorticosteroids;
        /// <sumary>
        /// Fecha inicio tratamiento con DMARD
        /// </sumary>
        private string InitialDateTreatmentWhitDMARD;
        /// <sumary>
        /// Tamizaje para TB antes del inicio de DMARD
        /// </sumary>
        private long InitialScreeningDMARD;
        /// <sumary>
        /// Antecedente de linfoma antes del inicio de DMARD
        /// </sumary>
        private long LymphomaHistoryDMARD;
        /// <sumary>
        /// Fecha del primer DAS 28 realizado
        /// </sumary>
        private bool VC_ValidateDateFirstDAS28;
        /// <sumary>
        /// Profesional que realizo el primer DAS 28
        /// </sumary>
        private bool VC_ValidateProfessionalFirstDAS28;
        /// <sumary>
        /// Resultado del primer DAS 28
        /// </sumary>
        private bool VC_ValidateResultFirstDAS28;
        /// <sumary>
        /// Fecha del primer HAQ realizado
        /// </sumary>
        private bool VC_ValidateDateFirstHAQ;
        /// <sumary>
        /// HAQ inicial
        /// </sumary>
        private bool VC_ValidateInitialHAQ;
        /// <sumary>
        /// Fecha inicio tratamiento con DMARD
        /// </sumary>
        private bool VC_ValidateInitialDateTreatmentNotDMARD;
        /// <sumary>
        /// Analgésicos No Opioides al inicio
        /// </sumary>
        private bool VC_ValidateInitialAnalgesicNotOpioids;
        /// <sumary>
        /// Analgésicos Opioides (Codeina, Tramadol) al inicio
        /// </sumary>
        private bool VC_ValidateInitialAnalgesicOpioids;
        /// <sumary>
        /// AINES al inicio
        /// </sumary>
        private bool VC_ValidateStartAINES;
        /// <sumary>
        /// Corticoides al inicio
        /// </sumary>
        private bool VC_ValidateStartCorticosteroids;
        /// <sumary>
        /// Fecha inicio tratamiento con DMARD
        /// </sumary>
        private bool VC_ValidateInitialDateTreatmentWhitDMARD;
        /// <sumary>
        /// Tamizaje para TB antes del inicio de DMARD
        /// </sumary>
        private bool VC_ValidateInitialScreeningDMARD;
        /// <sumary>
        /// Antecedente de linfoma antes del inicio de DMARD
        /// </sumary>
        private bool VC_ValidateLymphomaHistoryDMARD;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_ValidateDiagnosis() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Valida segunda parte de exámenes médicos 
        /// </sumary>
        /// <param name="DateFirstDAS28">Fecha del primer DAS 28 realizado</param>
        /// <param name="ProfessionalFirstDAS28">Profesional que realizo el primer DAS 28</param>
        /// <param name="ResultFirstDAS28">Resultado del primer DAS 28</param>
        /// <param name="DateFirstHAQ">Fecha del primer HAQ realizado</param>
        /// <param name="DateDiagnosis">DateDiagnosis</param>
        /// <param name="InitialHAQ">HAQ inicial</param>
        /// <param name="InitialDateTreatmentNotDMARD">Fecha inicio tratamiento con DMARD</param>
        /// <param name="InitialAnalgesicNotOpioids">Analgésicos No Opioides al inicio</param>
        /// <param name="InitialAnalgesicOpioids">Analgésicos Opioides (Codeina, Tramadol) al inicio</param>
        /// <param name="StartAINES">AINES al inicio</param>
        /// <param name="StartCorticosteroids">Corticoides al inicio</param>
        /// <param name="InitialDateTreatmentWhitDMARD">Fecha inicio tratamiento con DMARD</param>
        /// <param name="InitialScreeningDMARD">Tamizaje para TB antes del inicio de DMARD</param>
        /// <param name="LymphomaHistoryDMARD">Antecedente de linfoma antes del inicio de DMARD</param>
        public RuntimeResult<string> Execute(string DateFirstDAS28, long ProfessionalFirstDAS28, long ResultFirstDAS28, string DateFirstHAQ, string DateDiagnosis, long InitialHAQ, string InitialDateTreatmentNotDMARD, long InitialAnalgesicNotOpioids, long InitialAnalgesicOpioids, long StartAINES, long StartCorticosteroids, string InitialDateTreatmentWhitDMARD, long InitialScreeningDMARD, long LymphomaHistoryDMARD)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.DateFirstDAS28 = DateFirstDAS28;
                this.ProfessionalFirstDAS28 = ProfessionalFirstDAS28;
                this.ResultFirstDAS28 = ResultFirstDAS28;
                this.DateFirstHAQ = DateFirstHAQ;
                this.DateDiagnosis = DateDiagnosis;
                this.InitialHAQ = InitialHAQ;
                this.InitialDateTreatmentNotDMARD = InitialDateTreatmentNotDMARD;
                this.InitialAnalgesicNotOpioids = InitialAnalgesicNotOpioids;
                this.InitialAnalgesicOpioids = InitialAnalgesicOpioids;
                this.StartAINES = StartAINES;
                this.StartCorticosteroids = StartCorticosteroids;
                this.InitialDateTreatmentWhitDMARD = InitialDateTreatmentWhitDMARD;
                this.InitialScreeningDMARD = InitialScreeningDMARD;
                this.LymphomaHistoryDMARD = LymphomaHistoryDMARD;
                this.VC_ValidateDateFirstDAS28 = FUNC_VC_ValidateDateFirstDAS28();
                this.VC_ValidateProfessionalFirstDAS28 = FUNC_VC_ValidateProfessionalFirstDAS28();
                this.VC_ValidateResultFirstDAS28 = FUNC_VC_ValidateResultFirstDAS28();
                this.VC_ValidateDateFirstHAQ = FUNC_VC_ValidateDateFirstHAQ();
                this.VC_ValidateInitialHAQ = FUNC_VC_ValidateInitialHAQ();
                this.VC_ValidateInitialDateTreatmentNotDMARD = FUNC_VC_ValidateInitialDateTreatmentNotDMARD();
                this.VC_ValidateInitialAnalgesicNotOpioids = FUNC_VC_ValidateInitialAnalgesicNotOpioids();
                this.VC_ValidateInitialAnalgesicOpioids = FUNC_VC_ValidateInitialAnalgesicOpioids();
                this.VC_ValidateStartAINES = FUNC_VC_ValidateStartAINES();
                this.VC_ValidateStartCorticosteroids = FUNC_VC_ValidateStartCorticosteroids();
                this.VC_ValidateInitialDateTreatmentWhitDMARD = FUNC_VC_ValidateInitialDateTreatmentWhitDMARD();
                this.VC_ValidateInitialScreeningDMARD = FUNC_VC_ValidateInitialScreeningDMARD();
                this.VC_ValidateLymphomaHistoryDMARD = FUNC_VC_ValidateLymphomaHistoryDMARD();
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
        private bool FUNC_VC_ValidateDateFirstDAS28()
        {
            if (!DateTime.TryParse(DateFirstDAS28, out DateTime resdd))
                return false;

            var fech = Convert.ToDateTime(DateFirstDAS28);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateProfessionalFirstDAS28()
        {
            return Helper.USR_ValidateGenericRange(1, 9, ProfessionalFirstDAS28, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateResultFirstDAS28()
        {
            return Helper.USR_ValidateGenericRange(0, 10, ResultFirstDAS28, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateDateFirstHAQ()
        {
            if (!DateTime.TryParse(DateFirstHAQ, out DateTime resdd))
                return false;

            if (!DateTime.TryParse(DateDiagnosis, out DateTime salid))
                return false;

            var fech = Convert.ToDateTime(DateFirstHAQ);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;
            if (fech < Convert.ToDateTime(DateDiagnosis))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialHAQ()
        {
            return Helper.USR_ValidateGenericRange(0, 3, InitialHAQ, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialDateTreatmentNotDMARD()
        {
            if (!DateTime.TryParse(InitialDateTreatmentNotDMARD, out DateTime resdd))
                return false;

            if (!DateTime.TryParse(DateDiagnosis, out DateTime salid))
                return false;

            var fech = Convert.ToDateTime(InitialDateTreatmentNotDMARD);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;
            if (fech < Convert.ToDateTime(DateDiagnosis))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialAnalgesicNotOpioids()
        {
            return Helper.USR_ValidateGenericRange(0, 1, InitialAnalgesicNotOpioids, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialAnalgesicOpioids()
        {
            return Helper.USR_ValidateGenericRange(0, 1, InitialAnalgesicOpioids, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateStartAINES()
        {
            return Helper.USR_ValidateGenericRange(0, 1, StartAINES, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateStartCorticosteroids()
        {
            return Helper.USR_ValidateGenericRange(0, 1, StartCorticosteroids, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialDateTreatmentWhitDMARD()
        {
            if (!DateTime.TryParse(InitialDateTreatmentWhitDMARD, out DateTime resdd))
                return false;

            if (!DateTime.TryParse(DateDiagnosis, out DateTime salid))
                return false;

            var fech = Convert.ToDateTime(InitialDateTreatmentWhitDMARD);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;
            if (fech < Convert.ToDateTime(DateDiagnosis))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialScreeningDMARD()
        {
            return Helper.USR_ValidateGenericRange(0, 1, InitialScreeningDMARD, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateLymphomaHistoryDMARD()
        {
            return Helper.USR_ValidateGenericRange(0, 1, LymphomaHistoryDMARD, new List<long> { 300, int.MinValue });
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_ValidateDateFirstDAS28 == true)) NonValidMessages.Add($"Debe introducir una fecha valida del primer DAS 28 realizado. Validar la variable 40 Fecha del primer DAS 28 realizado");
            if (!(VC_ValidateProfessionalFirstDAS28 == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al Tipo de profesional. Validar la variable 41 Profesional que realizo el primer DAS 28");
            if (!(VC_ValidateResultFirstDAS28 == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al Resultado del primer DAS 28. Validar la variable 42 Resultado del primer DAS 28");
            if (!(VC_ValidateDateFirstHAQ == true)) NonValidMessages.Add($"Validar la variable 43 Fecha primer HAQ realizado Y Verificar fecha de diagnostico. Validar la variable 19 Fecha diagnostico AR");
            if (!(VC_ValidateInitialHAQ == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al HAQ inicial. Validar la variable 44 HAQ inicial");
            if (!(VC_ValidateInitialDateTreatmentNotDMARD == true)) NonValidMessages.Add($"Validar la variable 45 Fecha inicio tratamiento sin DMARD y Verificar fecha de diagnostico. Validar la variable 19 Fecha diagnostico AR");
            if (!(VC_ValidateInitialAnalgesicNotOpioids == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Analgésicos No Opioides al inicio. Validar la variable 46 Analgésicos No Opioides al inicio");
            if (!(VC_ValidateInitialAnalgesicOpioids == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Analgésicos Opioides al inicio. Validar la variable 47 Analgésicos Opioides (Codeina, Tramadol) al inicio");
            if (!(VC_ValidateStartAINES == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a AINES al inicio. Validar la variable 48 AINES al inicio");
            if (!(VC_ValidateStartCorticosteroids == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Corticoides al inicio. Validar la variable 49 Corticoides al inicio");
            if (!(VC_ValidateInitialDateTreatmentWhitDMARD == true)) NonValidMessages.Add($"Validar la variable 50 Fecha inicio tratamiento con DMARD y Verificar fecha de diagnostico. Validar la variable 19 Fecha diagnostico AR");
            if (!(VC_ValidateInitialScreeningDMARD == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Tamizaje para TB antes del inicio de DMARD. Validar la variable 51 Tamizaje para TB antes del inicio de DMARD");
            if (!(VC_ValidateLymphomaHistoryDMARD == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al Antecedente de linfoma antes del inicio de DMARD. Validar la variable 52 Antecedente de linfoma antes del inicio de DMARD");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return ""; }, $"OK");
        }
        #endregion
    }
    /// <sumary>
    /// Plantilla de reglas
    /// </sumary> 
    public sealed class RUL_Validate1393Treatment
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Fecha inicio tratamiento actual con DMARD
        /// </sumary>
        private string InitialDateCurrentTreatmentDMARD;
        /// <sumary>
        /// InitialDateTreatmentWhitDMARD
        /// </sumary>
        private string InitialDateTreatmentWhitDMARD;
        /// <sumary>
        /// Azathioprine
        /// </sumary>
        private long Azathioprine;
        /// <sumary>
        /// Cyclosporine
        /// </sumary>
        private long Cyclosporine;
        /// <sumary>
        /// Cyclophosphamide
        /// </sumary>
        private long Cyclophosphamide;
        /// <sumary>
        /// Chloroquine
        /// </sumary>
        private long Chloroquine;
        /// <sumary>
        /// Dpenicillamine
        /// </sumary>
        private long Dpenicillamine;
        /// <sumary>
        /// Etanercept
        /// </sumary>
        private long Etanercept;
        /// <sumary>
        /// Leflunomide
        /// </sumary>
        private long Leflunomide;
        /// <sumary>
        /// Methotrexate
        /// </sumary>
        private long Methotrexate;
        /// <sumary>
        /// Rituximab
        /// </sumary>
        private long Rituximab;
        /// <sumary>
        /// Sulfasalazine
        /// </sumary>
        private long Sulfasalazine;
        /// <sumary>
        /// Abatacept
        /// </sumary>
        private long Abatacept;
        /// <sumary>
        /// Adalimumab
        /// </sumary>
        private long Adalimumab;
        /// <sumary>
        /// Certolizumab
        /// </sumary>
        private long Certolizumab;
        /// <sumary>
        /// Golimumab
        /// </sumary>
        private long Golimumab;
        /// <sumary>
        /// Hydroxychloroquine
        /// </sumary>
        private long Hydroxychloroquine;
        /// <sumary>
        /// Infliximab
        /// </sumary>
        private long Infliximab;
        /// <sumary>
        /// GoldSaltsOne
        /// </sumary>
        private long GoldSaltsOne;
        /// <sumary>
        /// Tocilizumab
        /// </sumary>
        private long Tocilizumab;
        /// <sumary>
        /// Tofacitinib
        /// </sumary>
        private long Tofacitinib;
        /// <sumary>
        /// valida Anakinra
        /// </sumary>
        private long Anakinra;
        /// <sumary>
        /// Variable de plantilla
        /// </sumary>
        private bool VC_ValidateInitialDateCurrentTreatmentDMARD;
        /// <sumary>
        /// Valida Azathioprine
        /// </sumary>
        private bool VC_ValidateAzathioprine;
        /// <sumary>
        /// valida Cyclosporine
        /// </sumary>
        private bool VC_ValidateCyclosporine;
        /// <sumary>
        /// Cyclophosphamide
        /// </sumary>
        private bool VC_ValidateCyclophosphamide;
        /// <sumary>
        /// Valida Chloroquine
        /// </sumary>
        private bool VC_ValidateChloroquine;
        /// <sumary>
        /// Dpenicillamine
        /// </sumary>
        private bool VC_ValidateDpenicillamine;
        /// <sumary>
        /// valida Etanercept
        /// </sumary>
        private bool VC_ValidateEtanercept;
        /// <sumary>
        /// valida Leflunomide
        /// </sumary>
        private bool VC_ValidateLeflunomide;
        /// <sumary>
        /// valida Methotrexate
        /// </sumary>
        private bool VC_ValidateMethotrexate;
        /// <sumary>
        /// valida Rituximab
        /// </sumary>
        private bool VC_ValidateRituximab;
        /// <sumary>
        /// valida Sulfasalazine
        /// </sumary>
        private bool VC_ValidateSulfasalazine;
        /// <sumary>
        /// Abatacept
        /// </sumary>
        private bool VC_ValidateAbatacept;
        /// <sumary>
        /// valida Adalimumab
        /// </sumary>
        private bool VC_ValidateAdalimumab;
        /// <sumary>
        /// Certolizumab
        /// </sumary>
        private bool VC_ValidateCertolizumab;
        /// <sumary>
        /// valida Golimumab
        /// </sumary>
        private bool VC_ValidateGolimumab;
        /// <sumary>
        /// valida Hydroxychloroquine
        /// </sumary>
        private bool VC_ValidateHydroxychloroquine;
        /// <sumary>
        /// valida Infliximab
        /// </sumary>
        private bool VC_ValidateInfliximab;
        /// <sumary>
        /// GoldSaltsOne
        /// </sumary>
        private bool VC_ValidateGoldSaltsOne;
        /// <sumary>
        /// valida Tocilizumab
        /// </sumary>
        private bool VC_ValidateTocilizumab;
        /// <sumary>
        /// valida Tofacitinib
        /// </sumary>
        private bool VC_ValidateTofacitinib;
        /// <sumary>
        /// valida Anakinra
        /// </sumary>
        private bool VC_ValidateAnakinra;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_Validate1393Treatment() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        /// <param name="InitialDateCurrentTreatmentDMARD">Fecha inicio tratamiento actual con DMARD</param>
        /// <param name="InitialDateTreatmentWhitDMARD">InitialDateTreatmentWhitDMARD</param>
        /// <param name="Azathioprine">Azathioprine</param>
        /// <param name="Cyclosporine">Cyclosporine</param>
        /// <param name="Cyclophosphamide">Cyclophosphamide</param>
        /// <param name="Chloroquine">Chloroquine</param>
        /// <param name="Dpenicillamine">Dpenicillamine</param>
        /// <param name="Etanercept">Etanercept</param>
        /// <param name="Leflunomide">Leflunomide</param>
        /// <param name="Methotrexate">Methotrexate</param>
        /// <param name="Rituximab">Rituximab</param>
        /// <param name="Sulfasalazine">Sulfasalazine</param>
        /// <param name="Abatacept">Abatacept</param>
        /// <param name="Adalimumab">Adalimumab</param>
        /// <param name="Certolizumab">Certolizumab</param>
        /// <param name="Golimumab">Golimumab</param>
        /// <param name="Hydroxychloroquine">Hydroxychloroquine</param>
        /// <param name="Infliximab">Infliximab</param>
        /// <param name="GoldSaltsOne">GoldSaltsOne</param>
        /// <param name="Tocilizumab">Tocilizumab</param>
        /// <param name="Tofacitinib">Tofacitinib</param>
        /// <param name="Anakinra">valida Anakinra</param>
        public RuntimeResult<string> Execute(string InitialDateCurrentTreatmentDMARD, string InitialDateTreatmentWhitDMARD, long Azathioprine, long Cyclosporine, long Cyclophosphamide, long Chloroquine, long Dpenicillamine, long Etanercept, long Leflunomide, long Methotrexate, long Rituximab, long Sulfasalazine, long Abatacept, long Adalimumab, long Certolizumab, long Golimumab, long Hydroxychloroquine, long Infliximab, long GoldSaltsOne, long Tocilizumab, long Tofacitinib, long Anakinra)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.InitialDateCurrentTreatmentDMARD = InitialDateCurrentTreatmentDMARD;
                this.InitialDateTreatmentWhitDMARD = InitialDateTreatmentWhitDMARD;
                this.Azathioprine = Azathioprine;
                this.Cyclosporine = Cyclosporine;
                this.Cyclophosphamide = Cyclophosphamide;
                this.Chloroquine = Chloroquine;
                this.Dpenicillamine = Dpenicillamine;
                this.Etanercept = Etanercept;
                this.Leflunomide = Leflunomide;
                this.Methotrexate = Methotrexate;
                this.Rituximab = Rituximab;
                this.Sulfasalazine = Sulfasalazine;
                this.Abatacept = Abatacept;
                this.Adalimumab = Adalimumab;
                this.Certolizumab = Certolizumab;
                this.Golimumab = Golimumab;
                this.Hydroxychloroquine = Hydroxychloroquine;
                this.Infliximab = Infliximab;
                this.GoldSaltsOne = GoldSaltsOne;
                this.Tocilizumab = Tocilizumab;
                this.Tofacitinib = Tofacitinib;
                this.Anakinra = Anakinra;
                this.VC_ValidateInitialDateCurrentTreatmentDMARD = FUNC_VC_ValidateInitialDateCurrentTreatmentDMARD();
                this.VC_ValidateAzathioprine = FUNC_VC_ValidateAzathioprine();
                this.VC_ValidateCyclosporine = FUNC_VC_ValidateCyclosporine();
                this.VC_ValidateCyclophosphamide = FUNC_VC_ValidateCyclophosphamide();
                this.VC_ValidateChloroquine = FUNC_VC_ValidateChloroquine();
                this.VC_ValidateDpenicillamine = FUNC_VC_ValidateDpenicillamine();
                this.VC_ValidateEtanercept = FUNC_VC_ValidateEtanercept();
                this.VC_ValidateLeflunomide = FUNC_VC_ValidateLeflunomide();
                this.VC_ValidateMethotrexate = FUNC_VC_ValidateMethotrexate();
                this.VC_ValidateRituximab = FUNC_VC_ValidateRituximab();
                this.VC_ValidateSulfasalazine = FUNC_VC_ValidateSulfasalazine();
                this.VC_ValidateAbatacept = FUNC_VC_ValidateAbatacept();
                this.VC_ValidateAdalimumab = FUNC_VC_ValidateAdalimumab();
                this.VC_ValidateCertolizumab = FUNC_VC_ValidateCertolizumab();
                this.VC_ValidateGolimumab = FUNC_VC_ValidateGolimumab();
                this.VC_ValidateHydroxychloroquine = FUNC_VC_ValidateHydroxychloroquine();
                this.VC_ValidateInfliximab = FUNC_VC_ValidateInfliximab();
                this.VC_ValidateGoldSaltsOne = FUNC_VC_ValidateGoldSaltsOne();
                this.VC_ValidateTocilizumab = FUNC_VC_ValidateTocilizumab();
                this.VC_ValidateTofacitinib = FUNC_VC_ValidateTofacitinib();
                this.VC_ValidateAnakinra = FUNC_VC_ValidateAnakinra();
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
        private bool FUNC_VC_ValidateInitialDateCurrentTreatmentDMARD()
        {
            if (!DateTime.TryParse(InitialDateCurrentTreatmentDMARD, out DateTime resdd))
                return false;

            if (!DateTime.TryParse(InitialDateTreatmentWhitDMARD, out DateTime salid))
                return false;

            var fech = Convert.ToDateTime(InitialDateCurrentTreatmentDMARD);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;
            if (fech < Convert.ToDateTime(InitialDateTreatmentWhitDMARD))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAzathioprine()
        {
            if (Azathioprine == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Azathioprine, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCyclosporine()
        {
            if (Cyclosporine == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Cyclosporine, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCyclophosphamide()
        {
            if (Cyclophosphamide == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Cyclophosphamide, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateChloroquine()
        {
            if (Chloroquine == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 1, Chloroquine, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateDpenicillamine()
        {
            if (Dpenicillamine == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Dpenicillamine, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateEtanercept()
        {
            if (Etanercept == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Etanercept, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateLeflunomide()
        {
            if (Leflunomide == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Leflunomide, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateMethotrexate()
        {
            if (Methotrexate == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Methotrexate, new List<long> { 300 });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateRituximab()
        {
            if (Rituximab == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Rituximab, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateSulfasalazine()
        {
            if (Sulfasalazine == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Sulfasalazine, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAbatacept()
        {
            if (Abatacept == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Abatacept, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAdalimumab()
        {
            if (Adalimumab == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Adalimumab, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCertolizumab()
        {
            if (Certolizumab == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Certolizumab, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateGolimumab()
        {
            if (Golimumab == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Golimumab, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateHydroxychloroquine()
        {
            if (Hydroxychloroquine == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Hydroxychloroquine, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInfliximab()
        {
            if (Infliximab == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Infliximab, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateGoldSaltsOne()
        {
            if (GoldSaltsOne == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, GoldSaltsOne, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateTocilizumab()
        {
            if (Tocilizumab == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Tocilizumab, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateTofacitinib()
        {
            if (Tofacitinib == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Tofacitinib, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAnakinra()
        {
            if (Anakinra == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, Anakinra, new List<long> { });
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_ValidateInitialDateCurrentTreatmentDMARD == true)) NonValidMessages.Add($"Vaidar fecha de inicio del tratamiento con DMARD.Validar variable 45.6.  Y Validar la variable 107 Fecha inicio tratamiento actual con DMARD");
            if (!(VC_ValidateAzathioprine == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Azatioprina.Validar la variable 108 Azatioprina");
            if (!(VC_ValidateCyclosporine == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Ciclosporina.Validar la variable 109 Ciclosporina");
            if (!(VC_ValidateCyclophosphamide == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Ciclofosfamida.Validar la variable 110 Ciclofosfamida");
            if (!(VC_ValidateChloroquine == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Cloroquina.Validar la variable 111 Cloroquina");
            if (!(VC_ValidateDpenicillamine == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a D-penicilamina.Validar la variable 112 D-penicilamina");
            if (!(VC_ValidateEtanercept == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Etanercept.Validar la variable 113 Etanercept");
            if (!(VC_ValidateLeflunomide == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Leflunomida.Validar la variable 114 Leflunomida");
            if (!(VC_ValidateMethotrexate == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Metotrexate.Validar la variable 115 Metotrexate");
            if (!(VC_ValidateRituximab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Rituximab. Validar la variable 61 Rituximab");
            if (!(VC_ValidateSulfasalazine == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Sulfasalazina.Validar la variable 117 Sulfasalazina");
            if (!(VC_ValidateAbatacept == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Abatacept. Validar la variable 63 Abatacept");
            if (!(VC_ValidateAdalimumab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Adalimumab.Validar la variable 119 Adalimumab");
            if (!(VC_ValidateCertolizumab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Certolizumab.Validar la variable 120 Certolizumab");
            if (!(VC_ValidateGolimumab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Golimumab.Validar la variable 121 Golimumab");
            if (!(VC_ValidateHydroxychloroquine == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Hidroxicloroquina.Validar la variable 122 Hidroxicloroquina");
            if (!(VC_ValidateInfliximab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Infliximab.Validar la variable 123 Infliximab");
            if (!(VC_ValidateGoldSaltsOne == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Sales de oro.Validar la variable 124 Sales de oro");
            if (!(VC_ValidateTocilizumab == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Tocilizumab.Validar la variable 125 Tocilizumab");
            if (!(VC_ValidateTofacitinib == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Tofacitinib.Validar la variable 126 Tofacitinib");
            if (!(VC_ValidateAnakinra == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Anakinra.Validar la variable 127 Anakinra");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return ""; }, $"OK");
        }
        #endregion
    }
    /// <sumary>
    /// Valida los datos de exámenes del afiliado
    /// </sumary> 
    public sealed class RUL_Validate1393TestData
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// fecha primera visita
        /// </sumary>
        private string InitDateSymptom;
        /// <sumary>
        /// Fecha primera visita especialista por AR
        /// </sumary>
        private string FirstVisitDateSpecialist;
        /// <sumary>
        /// Fecha diagnostico
        /// </sumary>
        private string DateDiagnosis;
        /// <sumary>
        /// Height
        /// </sumary>
        private long Height;
        /// <sumary>
        /// Peso
        /// </sumary>
        private long Weight;
        /// <sumary>
        /// Radiografia de manos al diagnostico
        /// </sumary>
        private long HandXRay;
        /// <sumary>
        /// Radiografia de pies al diagnostico
        /// </sumary>
        private long FootXRay;
        /// <sumary>
        /// VSG inicial
        /// </sumary>
        private long InitialVSG;
        /// <sumary>
        /// InitialPCR
        /// </sumary>
        private long InitialPCR;
        /// <sumary>
        /// Factor reumatoideo inicial
        /// </sumary>
        private long InitialRheumatoidFactor;
        /// <sumary>
        /// Hemoglobina inicial
        /// </sumary>
        private long InitialHemoglobin;
        /// <sumary>
        /// Leucocitos inicial
        /// </sumary>
        private long InitialLeukocytes;
        /// <sumary>
        /// Creatinina inicial
        /// </sumary>
        private long InitialCeatrinin;
        /// <sumary>
        /// TFG inicial
        /// </sumary>
        private long InitialTFG;
        /// <sumary>
        /// Parcial de orina inicial
        /// </sumary>
        private long InitialUrinePartial;
        /// <sumary>
        /// ALT inicial
        /// </sumary>
        private long InicialALT;
        /// <sumary>
        /// Debe introducir un valor correcto con respecto a Anti-CCP al diagnóstico. Validar la variable 33 Anti-CCP al diagnóstico
        /// </sumary>
        private long AntiCCPDiagnosis;
        /// <sumary>
        /// HTA al diagnóstico
        /// </sumary>
        private long HTADiagnosis;
        /// <sumary>
        /// ECV al diagnóstico
        /// </sumary>
        private long DMDiagnosis;
        /// <sumary>
        /// ECV al diagnóstico
        /// </sumary>
        private long ECVDiagnosis;
        /// <sumary>
        /// ERC al diagnóstico
        /// </sumary>
        private long ERCDiagnosis;
        /// <sumary>
        /// Osteoporosis al diagnóstico
        /// </sumary>
        private long OsteoporosisDiagnosis;
        /// <sumary>
        /// Sindrome de Sjogren al diagnóstico
        /// </sumary>
        private long SyndromeSjogren;
        /// <sumary>
        /// Variable de plantilla
        /// </sumary>
        private bool VC_InitDateSymptom;
        /// <sumary>
        /// valida Fecha primera visita 
        /// </sumary>
        private bool VC_ValidateFirstVisitDateSpecialist;
        /// <sumary>
        /// valida fecha primer diagnostico
        /// </sumary>
        private bool VC_ValidateDateDiagnosis;
        /// <sumary>
        /// valida talla
        /// </sumary>
        private bool VC_ValidateHeight;
        /// <sumary>
        /// valida peso
        /// </sumary>
        private bool VC_ValidateWeight;
        /// <sumary>
        /// VALIDA FootXRay
        /// </sumary>
        private bool VC_ValidateFootXRay;
        /// <sumary>
        /// VALIDA HandXRay
        /// </sumary>
        private bool VC_ValidateHandXRay;
        /// <sumary>
        /// validat InitialVSG
        /// </sumary>
        private bool VC_ValidateInitialVSG;
        /// <sumary>
        /// valida InitialPCR
        /// </sumary>
        private bool VC_ValidateInitialPCR;
        /// <sumary>
        /// Factor reumatoideo inicial
        /// </sumary>
        private bool VC_InitialRheumatoidFactor;
        /// <sumary>
        /// Valida InitialHemoglobin
        /// </sumary>
        private bool VC_ValidateInitialHemoglobin;
        /// <sumary>
        /// Leucocitos inicial
        /// </sumary>
        private bool VC_ValidateInitialLeukocytes;
        /// <sumary>
        /// Creatinina inicial
        /// </sumary>
        private bool VC_ValidateInitialCeatrinin;
        /// <sumary>
        /// TFG inicial
        /// </sumary>
        private bool VC_ValidateInitialTFG;
        /// <sumary>
        /// Parcial de orina inicial
        /// </sumary>
        private bool VC_ValidateInitialUrinePartial;
        /// <sumary>
        /// ALT inicial
        /// </sumary>
        private bool VC_ValidateInicialALT;
        /// <sumary>
        /// Anti-CCP al diagnóstico
        /// </sumary>
        private bool VC_ValidateAntiCCPDiagnosis;
        /// <sumary>
        /// HTA al diagnóstico
        /// </sumary>
        private bool VC_ValidateHTADiagnosis;
        /// <sumary>
        /// ECV al diagnóstico
        /// </sumary>
        private bool VC_ValidateDMDiagnosis;
        /// <sumary>
        /// ECV al diagnóstico
        /// </sumary>
        private bool VC_ValidateECVDiagnosis;
        /// <sumary>
        /// ERC al diagnóstico
        /// </sumary>
        private bool VC_ValidateERCDiagnosis;
        /// <sumary>
        /// Osteoporosis al diagnóstico
        /// </sumary>
        private bool VC_ValidateOsteoporosisDiagnosis;
        /// <sumary>
        /// Sindrome de Sjogren al diagnóstico
        /// </sumary>
        private bool VC_ValidateSyndromeSjogren;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_Validate1393TestData() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Valida los datos de exámenes del afiliado
        /// </sumary>
        /// <param name="InitDateSymptom">fecha primera visita</param>
        /// <param name="FirstVisitDateSpecialist">Fecha primera visita especialista por AR</param>
        /// <param name="DateDiagnosis">Fecha diagnostico</param>
        /// <param name="Height">Height</param>
        /// <param name="Weight">Peso</param>
        /// <param name="HandXRay">Radiografia de manos al diagnostico</param>
        /// <param name="FootXRay">Radiografia de pies al diagnostico</param>
        /// <param name="InitialVSG">VSG inicial</param>
        /// <param name="InitialPCR">InitialPCR</param>
        /// <param name="InitialRheumatoidFactor">Factor reumatoideo inicial</param>
        /// <param name="InitialHemoglobin">Hemoglobina inicial</param>
        /// <param name="InitialLeukocytes">Leucocitos inicial</param>
        /// <param name="InitialCeatrinin">Creatinina inicial</param>
        /// <param name="InitialTFG">TFG inicial</param>
        /// <param name="InitialUrinePartial">Parcial de orina inicial</param>
        /// <param name="InicialALT">ALT inicial</param>
        /// <param name="AntiCCPDiagnosis">Debe introducir un valor correcto con respecto a Anti-CCP al diagnóstico. Validar la variable 33 Anti-CCP al diagnóstico</param>
        /// <param name="HTADiagnosis">HTA al diagnóstico</param>
        /// <param name="DMDiagnosis">ECV al diagnóstico</param>
        /// <param name="ECVDiagnosis">ECV al diagnóstico</param>
        /// <param name="ERCDiagnosis">ERC al diagnóstico</param>
        /// <param name="OsteoporosisDiagnosis">Osteoporosis al diagnóstico</param>
        /// <param name="SyndromeSjogren">Sindrome de Sjogren al diagnóstico</param>
        public RuntimeResult<string> Execute(string InitDateSymptom, string FirstVisitDateSpecialist, string DateDiagnosis, long Height, long Weight, long HandXRay, long FootXRay, long InitialVSG, long InitialPCR, long InitialRheumatoidFactor, long InitialHemoglobin, long InitialLeukocytes, long InitialCeatrinin, long InitialTFG, long InitialUrinePartial, long InicialALT, long AntiCCPDiagnosis, long HTADiagnosis, long DMDiagnosis, long ECVDiagnosis, long ERCDiagnosis, long OsteoporosisDiagnosis, long SyndromeSjogren)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.InitDateSymptom = InitDateSymptom;
                this.FirstVisitDateSpecialist = FirstVisitDateSpecialist;
                this.DateDiagnosis = DateDiagnosis;
                this.Height = Height;
                this.Weight = Weight;
                this.HandXRay = HandXRay;
                this.FootXRay = FootXRay;
                this.InitialVSG = InitialVSG;
                this.InitialPCR = InitialPCR;
                this.InitialRheumatoidFactor = InitialRheumatoidFactor;
                this.InitialHemoglobin = InitialHemoglobin;
                this.InitialLeukocytes = InitialLeukocytes;
                this.InitialCeatrinin = InitialCeatrinin;
                this.InitialTFG = InitialTFG;
                this.InitialUrinePartial = InitialUrinePartial;
                this.InicialALT = InicialALT;
                this.AntiCCPDiagnosis = AntiCCPDiagnosis;
                this.HTADiagnosis = HTADiagnosis;
                this.DMDiagnosis = DMDiagnosis;
                this.ECVDiagnosis = ECVDiagnosis;
                this.ERCDiagnosis = ERCDiagnosis;
                this.OsteoporosisDiagnosis = OsteoporosisDiagnosis;
                this.SyndromeSjogren = SyndromeSjogren;
                this.VC_InitDateSymptom = FUNC_VC_InitDateSymptom();
                this.VC_ValidateFirstVisitDateSpecialist = FUNC_VC_ValidateFirstVisitDateSpecialist();
                this.VC_ValidateDateDiagnosis = FUNC_VC_ValidateDateDiagnosis();
                this.VC_ValidateHeight = FUNC_VC_ValidateHeight();
                this.VC_ValidateWeight = FUNC_VC_ValidateWeight();
                this.VC_ValidateFootXRay = FUNC_VC_ValidateFootXRay();
                this.VC_ValidateHandXRay = FUNC_VC_ValidateHandXRay();
                this.VC_ValidateInitialVSG = FUNC_VC_ValidateInitialVSG();
                this.VC_ValidateInitialPCR = FUNC_VC_ValidateInitialPCR();
                this.VC_InitialRheumatoidFactor = FUNC_VC_InitialRheumatoidFactor();
                this.VC_ValidateInitialHemoglobin = FUNC_VC_ValidateInitialHemoglobin();
                this.VC_ValidateInitialLeukocytes = FUNC_VC_ValidateInitialLeukocytes();
                this.VC_ValidateInitialCeatrinin = FUNC_VC_ValidateInitialCeatrinin();
                this.VC_ValidateInitialTFG = FUNC_VC_ValidateInitialTFG();
                this.VC_ValidateInitialUrinePartial = FUNC_VC_ValidateInitialUrinePartial();
                this.VC_ValidateInicialALT = FUNC_VC_ValidateInicialALT();
                this.VC_ValidateAntiCCPDiagnosis = FUNC_VC_ValidateAntiCCPDiagnosis();
                this.VC_ValidateHTADiagnosis = FUNC_VC_ValidateHTADiagnosis();
                this.VC_ValidateDMDiagnosis = FUNC_VC_ValidateDMDiagnosis();
                this.VC_ValidateECVDiagnosis = FUNC_VC_ValidateECVDiagnosis();
                this.VC_ValidateERCDiagnosis = FUNC_VC_ValidateERCDiagnosis();
                this.VC_ValidateOsteoporosisDiagnosis = FUNC_VC_ValidateOsteoporosisDiagnosis();
                this.VC_ValidateSyndromeSjogren = FUNC_VC_ValidateSyndromeSjogren();
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
        private bool FUNC_VC_InitDateSymptom()
        {
            if (!DateTime.TryParse(InitDateSymptom, out DateTime resdd))
                return false;

            if (!DateTime.TryParse(FirstVisitDateSpecialist, out DateTime salid))
                return false;

            var fech = Convert.ToDateTime(InitDateSymptom);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;
            if (fech < Convert.ToDateTime(FirstVisitDateSpecialist))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateFirstVisitDateSpecialist()
        {
            if (!DateTime.TryParse(FirstVisitDateSpecialist, out DateTime resdd))
                return false;

            if (!DateTime.TryParse(DateDiagnosis, out DateTime salid))
                return false;

            var fech = Convert.ToDateTime(FirstVisitDateSpecialist);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1822-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;
            if (fech < Convert.ToDateTime(DateDiagnosis))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateDateDiagnosis()
        {
            if (!DateTime.TryParse(DateDiagnosis, out DateTime resdd))
                return false;

            var fech = Convert.ToDateTime(DateDiagnosis);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateHeight()
        {
            return Helper.USR_ValidateGenericRange(50, 250, Height, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateWeight()
        {
            return Helper.USR_ValidateGenericRange(3, 300, Weight, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateFootXRay()
        {
            return Helper.USR_ValidateGenericRange(0, 1, FootXRay, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateHandXRay()
        {
            return Helper.USR_ValidateGenericRange(0, 1, HandXRay, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialVSG()
        {
            return Helper.USR_ValidateGenericRange(0, 250, InitialVSG, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialPCR()
        {
            return Helper.USR_ValidateGenericRange(0, 250, InitialPCR, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_InitialRheumatoidFactor()
        {
            return Helper.USR_ValidateGenericRange(0, 1, InitialRheumatoidFactor, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialHemoglobin()
        {
            return Helper.USR_ValidateGenericRange(3, 50, InitialHemoglobin, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialLeukocytes()
        {
            return Helper.USR_ValidateGenericRange(0, 20000, InitialLeukocytes, new List<long> { 22222, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialCeatrinin()
        {
            return Helper.USR_ValidateGenericRange(0, 20, InitialCeatrinin, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialTFG()
        {
            return Helper.USR_ValidateGenericRange(0, 250, InitialTFG, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInitialUrinePartial()
        {
            return Helper.USR_ValidateGenericRange(0, 1, InitialUrinePartial, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInicialALT()
        {
            return Helper.USR_ValidateGenericRange(0, 1, InicialALT, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAntiCCPDiagnosis()
        {
            return Helper.USR_ValidateGenericRange(0, 1, AntiCCPDiagnosis, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateHTADiagnosis()
        {
            return Helper.USR_ValidateGenericRange(0, 1, HTADiagnosis, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateDMDiagnosis()
        {
            return Helper.USR_ValidateGenericRange(0, 1, DMDiagnosis, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateECVDiagnosis()
        {
            return Helper.USR_ValidateGenericRange(0, 1, ECVDiagnosis, new List<long> { 300 });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateERCDiagnosis()
        {
            return Helper.USR_ValidateGenericRange(0, 1, ERCDiagnosis, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateOsteoporosisDiagnosis()
        {
            return Helper.USR_ValidateGenericRange(0, 1, OsteoporosisDiagnosis, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateSyndromeSjogren()
        {
            return Helper.USR_ValidateGenericRange(0, 1, SyndromeSjogren, new List<long> { 300, int.MinValue });
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_InitDateSymptom == true)) NonValidMessages.Add($"Debe ingresar una fecha valida Validar la variable 17 Fecha inicio sintomas de AR y Validar la variable 18 Fecha primera visita	");
            if (!(VC_ValidateFirstVisitDateSpecialist == true)) NonValidMessages.Add($"Debe ingresar una fecha valida Validar la variable 18 Fecha primera visita especialista por AR y Verificar fecha de diagnostico. Validar la variable 19 Fecha diagnostico AR");
            if (!(VC_ValidateDateDiagnosis == true)) NonValidMessages.Add($"Debe ingresar una fecha valida. Validar la variable 19 Fecha diagnostico AR");
            if (!(VC_ValidateHeight == true)) NonValidMessages.Add($"Revisar talla, debe estar entre 50 a 250 cm. Validar la variable 20 Talla");
            if (!(VC_ValidateWeight == true)) NonValidMessages.Add($"Revisar Peso, debe estar entre 3 a 300 Kg. Validar la variable 21 Peso inicial");
            if (!(VC_ValidateFootXRay == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al resultado de la radiografia. Validar la variable 22 Radiografia de manos al diagnostico");
            if (!(VC_ValidateHandXRay == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al resultado de la radiografia. Validar la variable 23 Radiografia de pies al diagnostico");
            if (!(VC_ValidateInitialVSG == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a VSG inicial. Validar la variable 24 VSG inicial");
            if (!(VC_ValidateInitialPCR == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a PCR inicial. Validar la variable 25 PCR inicial");
            if (!(VC_InitialRheumatoidFactor == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al Factor reumatoideo inicial. Validar la variable 26 Factor reumatoideo inicial");
            if (!(VC_ValidateInitialHemoglobin == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Hemoglobina inicial. Validar la variable 27 Hemoglobina inicial");
            if (!(VC_ValidateInitialLeukocytes == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Leucocitos inicial. Validar la variable 28 Leucocitos inicial");
            if (!(VC_ValidateInitialCeatrinin == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Creatinina inicial. Validar la variable 29 Creatinina inicial");
            if (!(VC_ValidateInitialTFG == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a TFG inicial. Validar la variable 30 TFG inicial");
            if (!(VC_ValidateInitialUrinePartial == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Parcial de orina inicial. Validar la variable 31 Parcial de orina inicial");
            if (!(VC_ValidateInicialALT == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a ALT inicial. Validar la variable 32 ALT inicial");
            if (!(VC_ValidateAntiCCPDiagnosis == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Anti-CCP al diagnóstico. Validar la variable 33 Anti-CCP al diagnóstico");
            if (!(VC_ValidateHTADiagnosis == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a HTA al diagnóstico. Validar la variable 34 HTA al diagnóstico");
            if (!(VC_ValidateDMDiagnosis == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a DM al diagnóstico. Validar la variable 35 DM al diagnóstico");
            if (!(VC_ValidateECVDiagnosis == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a ECV al diagnóstico. Validar la variable 36 ECV al diagnóstico");
            if (!(VC_ValidateERCDiagnosis == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a ERC al diagnóstico. Validar la variable 37 ERC al diagnóstico");
            if (!(VC_ValidateOsteoporosisDiagnosis == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Osteoporosis al diagnóstico. Validar la variable 38 Osteoporosis al diagnóstico");
            if (!(VC_ValidateSyndromeSjogren == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Sindrome de Sjogren al diagnóstico. Validar la variable 39 Sindrome de Sjogren al diagnóstico");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return ""; }, $"OK");
        }
        #endregion
    }
    /// <sumary>
    /// Valida segunda parte de periodos de la r 1393
    /// </sumary> 
    public sealed class RUL_Validate1393periodTwo
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Fecha del último DAS 28 realizado
        /// </sumary>
        private string DateLastDAS28;
        /// <sumary>
        /// fecha primer das solo para compara con last DAS
        /// </sumary>
        private string DateFirstDAS28;
        /// <sumary>
        /// Profesional que realizó el último DAS 28
        /// </sumary>
        private long R1393ProfessionalLastDAS28;
        /// <sumary>
        /// Resultado del último DAS 28
        /// </sumary>
        private long ResultLastDAS28;
        /// <sumary>
        /// Estado de actividad actual de la AR según DAS 28
        /// </sumary>
        private long StatusCurrentActivityDAS28;
        /// <sumary>
        /// Estado de actividad actual de la AR según DAS 28
        /// </sumary>
        private string DateLastHAQ;
        /// <sumary>
        /// DateFirstHAQ Solo comparacion
        /// </sumary>
        private string DateFirstHAQ;
        /// <sumary>
        /// HAQ último semestre
        /// </sumary>
        private long HAQLastSemester;
        /// <sumary>
        /// Analgésicos No Opioides (Acetaminofén, Dipirona)
        /// </sumary>
        private long AnalgesicNotOpioids;
        /// <sumary>
        /// Analgésicos Opioides (Codeina, Tramadol)
        /// </sumary>
        private long AnalgesicOpioids;
        /// <sumary>
        /// AINES
        /// </sumary>
        private long AINES;
        /// <sumary>
        /// Corticosteroids
        /// </sumary>
        private long Corticosteroids;
        /// <sumary>
        /// Meses de uso de Glucocorticoides
        /// </sumary>
        private long MonthsUseGlucocorticoids;
        /// <sumary>
        /// Calcium
        /// </sumary>
        private long Calcium;
        /// <sumary>
        /// VitaminD
        /// </sumary>
        private long VitaminD;
        /// <sumary>
        /// Fecha del último DAS 28 realizado
        /// </sumary>
        private bool VC_ValidateDateLastDAS28;
        /// <sumary>
        /// Profesional que realizó el último DAS 28
        /// </sumary>
        private bool VC_ValidateR1393ProfessionalLastDAS28;
        /// <sumary>
        /// Resultado del último DAS 28
        /// </sumary>
        private bool VC_ValidateResultLastDAS28;
        /// <sumary>
        /// Estado de actividad actual de la AR según DAS 28
        /// </sumary>
        private bool VC_ValidateStatusCurrentActivityDAS28;
        /// <sumary>
        /// Fecha del último HAQ realizado
        /// </sumary>
        private bool VC_ValidateDateLastHAQ;
        /// <sumary>
        /// HAQ último semestre
        /// </sumary>
        private bool VC_ValidateHAQLastSemester;
        /// <sumary>
        /// Analgésicos No Opioides (Acetaminofén, Dipirona)
        /// </sumary>
        private bool VC_ValidateAnalgesicNotOpioids;
        /// <sumary>
        /// Analgésicos Opioides (Codeina, Tramadol)
        /// </sumary>
        private bool VC_ValidateAnalgesicOpioids;
        /// <sumary>
        /// valida AINES
        /// </sumary>
        private bool VC_ValidateAINES;
        /// <sumary>
        /// valida Corticosteroids
        /// </sumary>
        private bool VC_ValidateCorticosteroids;
        /// <sumary>
        /// Meses de uso de Glucocorticoides
        /// </sumary>
        private bool VC_ValidateMonthsUseGlucocorticoids;
        /// <sumary>
        /// Valida Calcium
        /// </sumary>
        private bool VC_ValidateCalcium;
        /// <sumary>
        /// Vitamina D
        /// </sumary>
        private bool VC_ValidateVitaminD;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_Validate1393periodTwo() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Valida segunda parte de periodos de la r 1393
        /// </sumary>
        /// <param name="DateLastDAS28">Fecha del último DAS 28 realizado</param>
        /// <param name="DateFirstDAS28">fecha primer das solo para compara con last DAS</param>
        /// <param name="R1393ProfessionalLastDAS28">Profesional que realizó el último DAS 28</param>
        /// <param name="ResultLastDAS28">Resultado del último DAS 28</param>
        /// <param name="StatusCurrentActivityDAS28">Estado de actividad actual de la AR según DAS 28</param>
        /// <param name="DateLastHAQ">Estado de actividad actual de la AR según DAS 28</param>
        /// <param name="DateFirstHAQ">DateFirstHAQ Solo comparacion</param>
        /// <param name="HAQLastSemester">HAQ último semestre</param>
        /// <param name="AnalgesicNotOpioids">Analgésicos No Opioides (Acetaminofén, Dipirona)</param>
        /// <param name="AnalgesicOpioids">Analgésicos Opioides (Codeina, Tramadol)</param>
        /// <param name="AINES">AINES</param>
        /// <param name="Corticosteroids">Corticosteroids</param>
        /// <param name="MonthsUseGlucocorticoids">Meses de uso de Glucocorticoides</param>
        /// <param name="Calcium">Calcium</param>
        /// <param name="VitaminD">VitaminD</param>
        public RuntimeResult<string> Execute(string DateLastDAS28, string DateFirstDAS28, long R1393ProfessionalLastDAS28, long ResultLastDAS28, long StatusCurrentActivityDAS28, string DateLastHAQ, string DateFirstHAQ, long HAQLastSemester, long AnalgesicNotOpioids, long AnalgesicOpioids, long AINES, long Corticosteroids, long MonthsUseGlucocorticoids, long Calcium, long VitaminD)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.DateLastDAS28 = DateLastDAS28;
                this.DateFirstDAS28 = DateFirstDAS28;
                this.R1393ProfessionalLastDAS28 = R1393ProfessionalLastDAS28;
                this.ResultLastDAS28 = ResultLastDAS28;
                this.StatusCurrentActivityDAS28 = StatusCurrentActivityDAS28;
                this.DateLastHAQ = DateLastHAQ;
                this.DateFirstHAQ = DateFirstHAQ;
                this.HAQLastSemester = HAQLastSemester;
                this.AnalgesicNotOpioids = AnalgesicNotOpioids;
                this.AnalgesicOpioids = AnalgesicOpioids;
                this.AINES = AINES;
                this.Corticosteroids = Corticosteroids;
                this.MonthsUseGlucocorticoids = MonthsUseGlucocorticoids;
                this.Calcium = Calcium;
                this.VitaminD = VitaminD;
                this.VC_ValidateDateLastDAS28 = FUNC_VC_ValidateDateLastDAS28();
                this.VC_ValidateR1393ProfessionalLastDAS28 = FUNC_VC_ValidateR1393ProfessionalLastDAS28();
                this.VC_ValidateResultLastDAS28 = FUNC_VC_ValidateResultLastDAS28();
                this.VC_ValidateStatusCurrentActivityDAS28 = FUNC_VC_ValidateStatusCurrentActivityDAS28();
                this.VC_ValidateDateLastHAQ = FUNC_VC_ValidateDateLastHAQ();
                this.VC_ValidateHAQLastSemester = FUNC_VC_ValidateHAQLastSemester();
                this.VC_ValidateAnalgesicNotOpioids = FUNC_VC_ValidateAnalgesicNotOpioids();
                this.VC_ValidateAnalgesicOpioids = FUNC_VC_ValidateAnalgesicOpioids();
                this.VC_ValidateAINES = FUNC_VC_ValidateAINES();
                this.VC_ValidateCorticosteroids = FUNC_VC_ValidateCorticosteroids();
                this.VC_ValidateMonthsUseGlucocorticoids = FUNC_VC_ValidateMonthsUseGlucocorticoids();
                this.VC_ValidateCalcium = FUNC_VC_ValidateCalcium();
                this.VC_ValidateVitaminD = FUNC_VC_ValidateVitaminD();
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
        private bool FUNC_VC_ValidateDateLastDAS28()
        {
            if (!DateTime.TryParse(DateLastDAS28, out DateTime resdd))
                return false;

            if (!DateTime.TryParse(DateFirstDAS28, out DateTime salid))
                return false;

            var fech = Convert.ToDateTime(DateLastDAS28);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;
            if (fech < Convert.ToDateTime(DateFirstDAS28))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateR1393ProfessionalLastDAS28()
        {
            if (R1393ProfessionalLastDAS28 == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(1, 9, R1393ProfessionalLastDAS28, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateResultLastDAS28()
        {
            if (ResultLastDAS28 == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 10, ResultLastDAS28, new List<long> { 300 });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateStatusCurrentActivityDAS28()
        {
            if (StatusCurrentActivityDAS28 == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 2, StatusCurrentActivityDAS28, new List<long> { 300 });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateDateLastHAQ()
        {
            if (!DateTime.TryParse(DateFirstHAQ, out DateTime resdd))
                return false;

            if (!DateTime.TryParse(DateLastHAQ, out DateTime salid))
                return false;

            var fech = Convert.ToDateTime(DateLastHAQ);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;
            if (fech < Convert.ToDateTime(DateLastHAQ))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateHAQLastSemester()
        {
            return Helper.USR_ValidateGenericRange(0, 3, HAQLastSemester, new List<long> { 300 });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAnalgesicNotOpioids()
        {
            if (AnalgesicNotOpioids == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 1, AnalgesicNotOpioids, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAnalgesicOpioids()
        {
            if (AnalgesicOpioids == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 1, AnalgesicOpioids, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateAINES()
        {
            if (AINES == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 1, AINES, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCorticosteroids()
        {
            if (Corticosteroids == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 1, Corticosteroids, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateMonthsUseGlucocorticoids()
        {
            if (MonthsUseGlucocorticoids == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 12, MonthsUseGlucocorticoids, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCalcium()
        {
            if (Calcium == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 1, Calcium, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateVitaminD()
        {
            if (VitaminD == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 1, VitaminD, new List<long> { });
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_ValidateDateLastDAS28 == true)) NonValidMessages.Add($"Verificar Fecha del primer DAS 28 realizado. Validar variable 40 Fecha del primer DAS 28 realizado Y Validar la variable 94 Fecha del último DAS 28 realizado");
            if (!(VC_ValidateR1393ProfessionalLastDAS28 == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al Tipo de profesional. Validar la variable 95 Profesional que realizó el último DAS 28");
            if (!(VC_ValidateResultLastDAS28 == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Resultado del último DAS 28. Validar la variable 96 Resultado del último DAS 28");
            if (!(VC_ValidateStatusCurrentActivityDAS28 == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al Estado de actividad actual de la AR según DAS 28. Validar la variable 97 Estado de actividad actual de la AR según DAS 28");
            if (!(VC_ValidateDateLastHAQ == true)) NonValidMessages.Add($"Verificar Fecha del primer HAQ realizado. Validar variable 43. Y Validar  la variable 98 Fecha del último HAQ realizado");
            if (!(VC_ValidateHAQLastSemester == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a HAQ último semestre. Validar la variable 99 HAQ último semestre");
            if (!(VC_ValidateAnalgesicNotOpioids == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Analgésicos No Opioides (Acetaminofén, Dipirona). Validar la variable 100 Analgésicos No Opioides (Acetaminofén, Dipirona)");
            if (!(VC_ValidateAnalgesicOpioids == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Analgésicos Opioides (Codeina, Tramadol). Validar la variable 101 Analgésicos Opioides (Codeina, Tramadol)");
            if (!(VC_ValidateAINES == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a AINES. Validar la variable 102 AINES");
            if (!(VC_ValidateCorticosteroids == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Corticoides. Validar la variable 103 Corticoides");
            if (!(VC_ValidateMonthsUseGlucocorticoids == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Meses de uso de Glucocorticoides. Validar la variable 104 Meses de uso de Glucocorticoides");
            if (!(VC_ValidateCalcium == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Calcio. Validar la variable 105 Calcio");
            if (!(VC_ValidateVitaminD == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Vitamina D. Validar la variable 106 Vitamina D");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return ""; }, $"OK");
        }
        #endregion
    }
    /// <sumary>
    /// Plantilla de reglas
    /// </sumary> 
    public sealed class RUL_Validate1393period
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// WeightLastSemester
        /// </sumary>
        private long WeightLastSemester;
        /// <sumary>
        /// LastHandXRay
        /// </sumary>
        private long LastHandXRay;
        /// <sumary>
        /// LastFootXRay
        /// </sumary>
        private long LastFootXRay;
        /// <sumary>
        /// PCRLastSemester
        /// </sumary>
        private long PCRLastSemester;
        /// <sumary>
        /// VSG último semestre
        /// </sumary>
        private long VSGLastSemester;
        /// <sumary>
        /// Hemoglobina último semestre
        /// </sumary>
        private long HemoglobinLastSemester;
        /// <sumary>
        /// Leucocitos último semestre
        /// </sumary>
        private long LeukocytesLastSemester;
        /// <sumary>
        /// Creatinina último semestre
        /// </sumary>
        private long CreatinineLastSemester;
        /// <sumary>
        /// TFG último semestre
        /// </sumary>
        private long TFGLastSemester;
        /// <sumary>
        /// Parcial de Orina último semestre
        /// </sumary>
        private long PartialUrineLastSemester;
        /// <sumary>
        /// ALT último semestre
        /// </sumary>
        private long ALTLastSemester;
        /// <sumary>
        /// HTA actual
        /// </sumary>
        private long CurrentHTA;
        /// <sumary>
        /// DM actual
        /// </sumary>
        private long CurrentDM;
        /// <sumary>
        /// ECV actual
        /// </sumary>
        private long CurrentECV;
        /// <sumary>
        /// ERC actual
        /// </sumary>
        private long CurrentERC;
        /// <sumary>
        /// Osteoporosis actual
        /// </sumary>
        private long CurrentOsteoporosis;
        /// <sumary>
        /// Sindrome de Sjogren actual
        /// </sumary>
        private long CurrentSyndromeSjogren;
        /// <sumary>
        /// valida WeightLastSemester
        /// </sumary>
        private bool VC_ValidateWeightLastSemester;
        /// <sumary>
        /// valida LastHandXRay
        /// </sumary>
        private bool VC_ValidateLastHandXRay;
        /// <sumary>
        /// valida LastFootXRay
        /// </sumary>
        private bool VC_ValidateLastFootXRay;
        /// <sumary>
        /// valida PCR último semestre
        /// </sumary>
        private bool VC_ValidatePCRLastSemester;
        /// <sumary>
        /// valida VSG último semestre
        /// </sumary>
        private bool VC_ValidateVSGLastSemester;
        /// <sumary>
        /// Hemoglobina último semestre
        /// </sumary>
        private bool VC_ValidateHemoglobinLastSemester;
        /// <sumary>
        /// Leucocitos último semestre
        /// </sumary>
        private bool VC_ValidateLeukocytesLastSemester;
        /// <sumary>
        /// Creatinina último semestre
        /// </sumary>
        private bool VC_ValidateCreatinineLastSemester;
        /// <sumary>
        /// TFG último semestre
        /// </sumary>
        private bool VC_ValidateTFGLastSemester;
        /// <sumary>
        /// Parcial de Orina último semestre
        /// </sumary>
        private bool VC_ValidatePartialUrineLastSemester;
        /// <sumary>
        /// ALT último semestre
        /// </sumary>
        private bool VC_ValidateALTLastSemester;
        /// <sumary>
        /// HTA actual
        /// </sumary>
        private bool VC_ValidateCurrentHTA;
        /// <sumary>
        /// DM actual
        /// </sumary>
        private bool VC_ValidateCurrentDM;
        /// <sumary>
        /// ECV actual
        /// </sumary>
        private bool VC_ValidateCurrentECV;
        /// <sumary>
        /// ERC actual
        /// </sumary>
        private bool VC_ValidateCurrentERC;
        /// <sumary>
        /// Osteoporosis actual
        /// </sumary>
        private bool VC_ValidateCurrentOsteoporosis;
        /// <sumary>
        /// Sindrome de Sjogren actual
        /// </sumary>
        private bool VC_ValidateCurrentSyndromeSjogren;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_Validate1393period() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        /// <param name="WeightLastSemester">WeightLastSemester</param>
        /// <param name="LastHandXRay">LastHandXRay</param>
        /// <param name="LastFootXRay">LastFootXRay</param>
        /// <param name="PCRLastSemester">PCRLastSemester</param>
        /// <param name="VSGLastSemester">VSG último semestre</param>
        /// <param name="HemoglobinLastSemester">Hemoglobina último semestre</param>
        /// <param name="LeukocytesLastSemester">Leucocitos último semestre</param>
        /// <param name="CreatinineLastSemester">Creatinina último semestre</param>
        /// <param name="TFGLastSemester">TFG último semestre</param>
        /// <param name="PartialUrineLastSemester">Parcial de Orina último semestre</param>
        /// <param name="ALTLastSemester">ALT último semestre</param>
        /// <param name="CurrentHTA">HTA actual</param>
        /// <param name="CurrentDM">DM actual</param>
        /// <param name="CurrentECV">ECV actual</param>
        /// <param name="CurrentERC">ERC actual</param>
        /// <param name="CurrentOsteoporosis">Osteoporosis actual</param>
        /// <param name="CurrentSyndromeSjogren">Sindrome de Sjogren actual</param>
        public RuntimeResult<string> Execute(long WeightLastSemester, long LastHandXRay, long LastFootXRay, long PCRLastSemester, long VSGLastSemester, long HemoglobinLastSemester, long LeukocytesLastSemester, long CreatinineLastSemester, long TFGLastSemester, long PartialUrineLastSemester, long ALTLastSemester, long CurrentHTA, long CurrentDM, long CurrentECV, long CurrentERC, long CurrentOsteoporosis, long CurrentSyndromeSjogren)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.WeightLastSemester = WeightLastSemester;
                this.LastHandXRay = LastHandXRay;
                this.LastFootXRay = LastFootXRay;
                this.PCRLastSemester = PCRLastSemester;
                this.VSGLastSemester = VSGLastSemester;
                this.HemoglobinLastSemester = HemoglobinLastSemester;
                this.LeukocytesLastSemester = LeukocytesLastSemester;
                this.CreatinineLastSemester = CreatinineLastSemester;
                this.TFGLastSemester = TFGLastSemester;
                this.PartialUrineLastSemester = PartialUrineLastSemester;
                this.ALTLastSemester = ALTLastSemester;
                this.CurrentHTA = CurrentHTA;
                this.CurrentDM = CurrentDM;
                this.CurrentECV = CurrentECV;
                this.CurrentERC = CurrentERC;
                this.CurrentOsteoporosis = CurrentOsteoporosis;
                this.CurrentSyndromeSjogren = CurrentSyndromeSjogren;
                this.VC_ValidateWeightLastSemester = FUNC_VC_ValidateWeightLastSemester();
                this.VC_ValidateLastHandXRay = FUNC_VC_ValidateLastHandXRay();
                this.VC_ValidateLastFootXRay = FUNC_VC_ValidateLastFootXRay();
                this.VC_ValidatePCRLastSemester = FUNC_VC_ValidatePCRLastSemester();
                this.VC_ValidateVSGLastSemester = FUNC_VC_ValidateVSGLastSemester();
                this.VC_ValidateHemoglobinLastSemester = FUNC_VC_ValidateHemoglobinLastSemester();
                this.VC_ValidateLeukocytesLastSemester = FUNC_VC_ValidateLeukocytesLastSemester();
                this.VC_ValidateCreatinineLastSemester = FUNC_VC_ValidateCreatinineLastSemester();
                this.VC_ValidateTFGLastSemester = FUNC_VC_ValidateTFGLastSemester();
                this.VC_ValidatePartialUrineLastSemester = FUNC_VC_ValidatePartialUrineLastSemester();
                this.VC_ValidateALTLastSemester = FUNC_VC_ValidateALTLastSemester();
                this.VC_ValidateCurrentHTA = FUNC_VC_ValidateCurrentHTA();
                this.VC_ValidateCurrentDM = FUNC_VC_ValidateCurrentDM();
                this.VC_ValidateCurrentECV = FUNC_VC_ValidateCurrentECV();
                this.VC_ValidateCurrentERC = FUNC_VC_ValidateCurrentERC();
                this.VC_ValidateCurrentOsteoporosis = FUNC_VC_ValidateCurrentOsteoporosis();
                this.VC_ValidateCurrentSyndromeSjogren = FUNC_VC_ValidateCurrentSyndromeSjogren();
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
        private bool FUNC_VC_ValidateWeightLastSemester()
        {
            return Helper.USR_ValidateGenericRange(3, 300, WeightLastSemester, new List<long> { int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateLastHandXRay()
        {
            return Helper.USR_ValidateGenericRange(0, 1, LastHandXRay, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateLastFootXRay()
        {
            return Helper.USR_ValidateGenericRange(0, 1, LastFootXRay, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidatePCRLastSemester()
        {
            if (PCRLastSemester == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 250, PCRLastSemester, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateVSGLastSemester()
        {
            return Helper.USR_ValidateGenericRange(0, 250, VSGLastSemester, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateHemoglobinLastSemester()
        {
            return Helper.USR_ValidateGenericRange(3, 50, HemoglobinLastSemester, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateLeukocytesLastSemester()
        {
            return Helper.USR_ValidateGenericRange(0, 20000, LeukocytesLastSemester, new List<long> { 22222, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCreatinineLastSemester()
        {
            return Helper.USR_ValidateGenericRange(0, 20, CreatinineLastSemester, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateTFGLastSemester()
        {
            return Helper.USR_ValidateGenericRange(0, 250, TFGLastSemester, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidatePartialUrineLastSemester()
        {
            return Helper.USR_ValidateGenericRange(0, 1, PartialUrineLastSemester, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateALTLastSemester()
        {
            return Helper.USR_ValidateGenericRange(0, 1, ALTLastSemester, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCurrentHTA()
        {
            return Helper.USR_ValidateGenericRange(0, 1, CurrentHTA, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCurrentDM()
        {
            return Helper.USR_ValidateGenericRange(0, 1, CurrentDM, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCurrentECV()
        {
            return Helper.USR_ValidateGenericRange(0, 1, CurrentECV, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCurrentERC()
        {
            return Helper.USR_ValidateGenericRange(0, 1, CurrentERC, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCurrentOsteoporosis()
        {
            return Helper.USR_ValidateGenericRange(0, 1, CurrentOsteoporosis, new List<long> { 300, int.MinValue });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCurrentSyndromeSjogren()
        {
            return Helper.USR_ValidateGenericRange(0, 1, CurrentSyndromeSjogren, new List<long> { 300, int.MinValue });
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_ValidateWeightLastSemester == true)) NonValidMessages.Add($"Revisar Peso, debe estar entre 3 a 300 Kg. Validar la variable 77 Peso último semestre");
            if (!(VC_ValidateLastHandXRay == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a la ultima radiografía de manos. Validar la variable 78 Ultima radiografía de manos");
            if (!(VC_ValidateLastFootXRay == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a la ultima radiografía de pies. Validar la variable 79 Ultima radiografía de pies");
            if (!(VC_ValidatePCRLastSemester == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a PCR último semestre. Validar la variable 80 PCR último semestre");
            if (!(VC_ValidateVSGLastSemester == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a VSG último semestre. Validar la variable 81 VSG último semestre");
            if (!(VC_ValidateHemoglobinLastSemester == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Hemoglobina último semestre. Validar la variable 82 Hemoglobina último semestre");
            if (!(VC_ValidateLeukocytesLastSemester == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Leucocitos último semestre. Validar la variable 83 Leucocitos último semestre");
            if (!(VC_ValidateCreatinineLastSemester == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Creatinina último semestre. Validar la variable 84 Creatinina último semestre");
            if (!(VC_ValidateTFGLastSemester == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a TFG último semestre. Validar la variable 85 TFG último semestre");
            if (!(VC_ValidatePartialUrineLastSemester == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Parcial de Orina último semestre. Validar la variable 86 Parcial de Orina último semestre");
            if (!(VC_ValidateALTLastSemester == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a ALT último semestre. Validar la variable 87 ALT último semestre");
            if (!(VC_ValidateCurrentHTA == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a HTA actual. Validar la variable 88 HTA actual");
            if (!(VC_ValidateCurrentDM == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a DM actual. Validar la variable 89 DM actual");
            if (!(VC_ValidateCurrentECV == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a ECV actual. Validar la variable 90 ECV actual");
            if (!(VC_ValidateCurrentERC == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a ERC actual. Validar la variable 91 ERC actual");
            if (!(VC_ValidateCurrentOsteoporosis == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Osteoporosis actual. Validar la variable 92 Osteoporosis actual");
            if (!(VC_ValidateCurrentSyndromeSjogren == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Síndrome de Sjogren actual. Validar la variable 93 Síndrome de Sjogren actual");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return ""; }, $"OK");
        }
        #endregion
    }
    /// <sumary>
    /// Plantilla de reglas
    /// </sumary> 
    public sealed class RUL_Validate1393Consultation
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// RheumatologistConsultNumberLastYear
        /// </sumary>
        private long RheumatologistConsultNumberLastYear;
        /// <sumary>
        /// Número de consultas con Internista por AR en el último año
        /// </sumary>
        private long InternistConsultNumberLastYear;
        /// <sumary>
        /// Número de consultas con Médico Familiar por AR en el último año
        /// </sumary>
        private long FamilyDoctorConsultNumberLastYear;
        /// <sumary>
        /// Reemplazo articular 1 por AR
        /// </sumary>
        private long JointReplacementOne;
        /// <sumary>
        /// Reemplazo articular 2 por AR
        /// </sumary>
        private long JointReplacementTwo;
        /// <sumary>
        /// Reemplazo articular 3 por AR
        /// </sumary>
        private long JointReplacementThree;
        /// <sumary>
        /// JointReplacementFour
        /// </sumary>
        private long JointReplacementFour;
        /// <sumary>
        /// No de hospitalizaciones por AR en último año
        /// </sumary>
        private long NumberHospitalizationsLastYear;
        /// <sumary>
        /// fecha diagnostico AR
        /// </sumary>
        private string DateDiagnosis;
        /// <sumary>
        /// Quién hace la atención clínica para AR al paciente actualmente
        /// </sumary>
        private long CurrentDoctorAR;
        /// <sumary>
        /// Novedad del paciente respecto al reporte anterior
        /// </sumary>
        private long NoveltyPatient;
        /// <sumary>
        /// AffiliationDate
        /// </sumary>
        private string AffiliationDate;
        /// <sumary>
        /// DisenrollmentDate
        /// </sumary>
        private string DisenrollmentDate;
        /// <sumary>
        /// DeathDate
        /// </sumary>
        private string DeathDate;
        /// <sumary>
        /// BirthDate
        /// </sumary>
        private string BirthDate;
        /// <sumary>
        /// DeathCause
        /// </sumary>
        private long DeathCause;
        /// <sumary>
        /// valida Número de consultas con reumatólogo en el último año
        /// </sumary>
        private bool VC_ValidateRheumatologistConsultNumberLastYear;
        /// <sumary>
        /// Número de consultas con Internista por AR en el último año
        /// </sumary>
        private bool VC_ValidateInternistConsultNumberLastYear;
        /// <sumary>
        /// Número de consultas con Médico Familiar por AR en el último año
        /// </sumary>
        private bool VC_ValidateFamilyDoctorConsultNumberLastYear;
        /// <sumary>
        /// Reemplazo articular 1 por AR
        /// </sumary>
        private bool VC_ValidateJointReplacementOne;
        /// <sumary>
        /// Reemplazo articular 2 por AR
        /// </sumary>
        private bool VC_ValidateJointReplacementTwo;
        /// <sumary>
        /// Reemplazo articular 3 por AR
        /// </sumary>
        private bool VC_ValidateJointReplacementThree;
        /// <sumary>
        /// JointReplacementFour
        /// </sumary>
        private bool VC_ValidateJointReplacementFour;
        /// <sumary>
        /// valida No de hospitalizaciones por AR en último año
        /// </sumary>
        private bool VC_ValidateNumberHospitalizationsLastYear;
        /// <sumary>
        /// Quién hace la atención clínica para AR al paciente actualmente
        /// </sumary>
        private bool VC_ValidateCurrentDoctorAR;
        /// <sumary>
        /// Novedad del paciente respecto al reporte anterior
        /// </sumary>
        private bool VC_ValidateNoveltyPatient;
        /// <sumary>
        /// Fecha de desafiliación de la EAPB
        /// </sumary>
        private bool VC_ValidateDisenrollmentDate;
        /// <sumary>
        /// valida DeathDate
        /// </sumary>
        private bool VC_ValidateDeathDate;
        /// <sumary>
        /// DeathCause
        /// </sumary>
        private bool VC_ValidateDeathCause;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_Validate1393Consultation() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Plantilla de reglas
        /// </sumary>
        /// <param name="RheumatologistConsultNumberLastYear">RheumatologistConsultNumberLastYear</param>
        /// <param name="InternistConsultNumberLastYear">Número de consultas con Internista por AR en el último año</param>
        /// <param name="FamilyDoctorConsultNumberLastYear">Número de consultas con Médico Familiar por AR en el último año</param>
        /// <param name="JointReplacementOne">Reemplazo articular 1 por AR</param>
        /// <param name="JointReplacementTwo">Reemplazo articular 2 por AR</param>
        /// <param name="JointReplacementThree">Reemplazo articular 3 por AR</param>
        /// <param name="JointReplacementFour">JointReplacementFour</param>
        /// <param name="NumberHospitalizationsLastYear">No de hospitalizaciones por AR en último año</param>
        /// <param name="DateDiagnosis">fecha diagnostico AR</param>
        /// <param name="CurrentDoctorAR">Quién hace la atención clínica para AR al paciente actualmente</param>
        /// <param name="NoveltyPatient">Novedad del paciente respecto al reporte anterior</param>
        /// <param name="AffiliationDate">AffiliationDate</param>
        /// <param name="DisenrollmentDate">DisenrollmentDate</param>
        /// <param name="DeathDate">DeathDate</param>
        /// <param name="BirthDate">BirthDate</param>
        /// <param name="DeathCause">DeathCause</param>
        public RuntimeResult<string> Execute(long RheumatologistConsultNumberLastYear, long InternistConsultNumberLastYear, long FamilyDoctorConsultNumberLastYear, long JointReplacementOne, long JointReplacementTwo, long JointReplacementThree, long JointReplacementFour, long NumberHospitalizationsLastYear, string DateDiagnosis, long CurrentDoctorAR, long NoveltyPatient, string AffiliationDate, string DisenrollmentDate, string DeathDate, string BirthDate, long DeathCause)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.RheumatologistConsultNumberLastYear = RheumatologistConsultNumberLastYear;
                this.InternistConsultNumberLastYear = InternistConsultNumberLastYear;
                this.FamilyDoctorConsultNumberLastYear = FamilyDoctorConsultNumberLastYear;
                this.JointReplacementOne = JointReplacementOne;
                this.JointReplacementTwo = JointReplacementTwo;
                this.JointReplacementThree = JointReplacementThree;
                this.JointReplacementFour = JointReplacementFour;
                this.NumberHospitalizationsLastYear = NumberHospitalizationsLastYear;
                this.DateDiagnosis = DateDiagnosis;
                this.CurrentDoctorAR = CurrentDoctorAR;
                this.NoveltyPatient = NoveltyPatient;
                this.AffiliationDate = AffiliationDate;
                this.DisenrollmentDate = DisenrollmentDate;
                this.DeathDate = DeathDate;
                this.BirthDate = BirthDate;
                this.DeathCause = DeathCause;
                this.VC_ValidateRheumatologistConsultNumberLastYear = FUNC_VC_ValidateRheumatologistConsultNumberLastYear();
                this.VC_ValidateInternistConsultNumberLastYear = FUNC_VC_ValidateInternistConsultNumberLastYear();
                this.VC_ValidateFamilyDoctorConsultNumberLastYear = FUNC_VC_ValidateFamilyDoctorConsultNumberLastYear();
                this.VC_ValidateJointReplacementOne = FUNC_VC_ValidateJointReplacementOne();
                this.VC_ValidateJointReplacementTwo = FUNC_VC_ValidateJointReplacementTwo();
                this.VC_ValidateJointReplacementThree = FUNC_VC_ValidateJointReplacementThree();
                this.VC_ValidateJointReplacementFour = FUNC_VC_ValidateJointReplacementFour();
                this.VC_ValidateNumberHospitalizationsLastYear = FUNC_VC_ValidateNumberHospitalizationsLastYear();
                this.VC_ValidateCurrentDoctorAR = FUNC_VC_ValidateCurrentDoctorAR();
                this.VC_ValidateNoveltyPatient = FUNC_VC_ValidateNoveltyPatient();
                this.VC_ValidateDisenrollmentDate = FUNC_VC_ValidateDisenrollmentDate();
                this.VC_ValidateDeathDate = FUNC_VC_ValidateDeathDate();
                this.VC_ValidateDeathCause = FUNC_VC_ValidateDeathCause();
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
        private bool FUNC_VC_ValidateRheumatologistConsultNumberLastYear()
        {
            if (RheumatologistConsultNumberLastYear == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 12, RheumatologistConsultNumberLastYear, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateInternistConsultNumberLastYear()
        {
            if (InternistConsultNumberLastYear == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 12, InternistConsultNumberLastYear, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateFamilyDoctorConsultNumberLastYear()
        {
            if (FamilyDoctorConsultNumberLastYear == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 12, FamilyDoctorConsultNumberLastYear, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateJointReplacementOne()
        {
            if (JointReplacementOne == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, JointReplacementOne, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateJointReplacementTwo()
        {
            if (JointReplacementTwo == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, JointReplacementTwo, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateJointReplacementThree()
        {
            if (JointReplacementThree == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, JointReplacementThree, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateJointReplacementFour()
        {
            if (JointReplacementFour == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 5, JointReplacementFour, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateNumberHospitalizationsLastYear()
        {
            if (NumberHospitalizationsLastYear == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 12, NumberHospitalizationsLastYear, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateCurrentDoctorAR()
        {
            if (CurrentDoctorAR == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(1, 7, CurrentDoctorAR, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateNoveltyPatient()
        {
            if (NoveltyPatient == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(1, 12, NoveltyPatient, new List<long> { });
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateDisenrollmentDate()
        {
            if (!DateTime.TryParse(DisenrollmentDate, out DateTime resdd))
                return false;

            if (!DateTime.TryParse(AffiliationDate, out DateTime salid))
                return false;

            var fech = Convert.ToDateTime(DisenrollmentDate);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1800-01-01"))
                return true;
            if (fech == Convert.ToDateTime("1811-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;
            if (fech < Convert.ToDateTime(AffiliationDate))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateDeathDate()
        {
            if (!DateTime.TryParse(DeathDate, out DateTime resdd))
                return false;

            if (!DateTime.TryParse(BirthDate, out DateTime salid))
                return false;

            var fech = Convert.ToDateTime(DeathDate);

            if (fech > DateTime.Now)
                return false;

            if (fech == Convert.ToDateTime("1799-01-01"))
                return true;

            if (fech <= Convert.ToDateTime("1900-01-01"))
                return false;
            if (fech < Convert.ToDateTime(BirthDate))
                return false;

            return true;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateDeathCause()
        {
            if (DeathCause == int.MinValue) return true;
            return Helper.USR_ValidateGenericRange(0, 3, DeathCause, new List<long> { });
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!(VC_ValidateRheumatologistConsultNumberLastYear == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Número de consultas con reumatólogo en el último año. Validar la variable 132 Número de consultas con reumatólogo en el último año");
            if (!(VC_ValidateInternistConsultNumberLastYear == true)) NonValidMessages.Add($"La longitud del campo excede el valor permitido. Validar la variable 133 Número de consultas con Internista por AR en el último año");
            if (!(VC_ValidateFamilyDoctorConsultNumberLastYear == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Número de consultas con Médico Familiar por AR en el último año. Validar la variable 134 Número de consultas con Médico Familiar por AR en el último año");
            if (!(VC_ValidateJointReplacementOne == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Reemplazo articular 1 por AR.Validar la variable 135 Reemplazo articular 1 por AR");
            if (!(VC_ValidateJointReplacementTwo == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Reemplazo articular 2 por AR.Validar la variable 136 Reemplazo articular 2 por AR");
            if (!(VC_ValidateJointReplacementThree == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Reemplazo articular 3 por AR.Validar la variable 137 Reemplazo articular 3 por AR");
            if (!(VC_ValidateJointReplacementFour == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Reemplazo articular 4 por AR.Validar la variable 138 Reemplazo articular 4 por AR");
            if (!(VC_ValidateNumberHospitalizationsLastYear == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a No de hospitalizaciones por AR en último año. Validar la variable 139 No de hospitalizaciones por AR en último año");
            if (!(VC_ValidateCurrentDoctorAR == true)) NonValidMessages.Add($"R1393CurrentDoctorAR");
            if (!(VC_ValidateNoveltyPatient == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al tipo de Novedad del paciente respecto al reporte anterior. Validar la variable 144 Novedad del paciente respecto al reporte anterior");
            if (!(VC_ValidateDisenrollmentDate == true)) NonValidMessages.Add($"Verificar Fecha de afiliación a la EAPB.Validar variable 16. Y Validar longitud de fecha. Validar la variable 145 Fecha de desafiliación de la EAPB");
            if (!(VC_ValidateDeathDate == true)) NonValidMessages.Add($"Verificar  fecha desafiliacion de la EAPB. Validar variable 82. Y Verificar fecha de nacimiento. Validar variable 10.");
            if (!(VC_ValidateDeathCause == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a Causa de muerte. Validar la variable 148 Causa de muerte");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return ""; }, $"OK");
        }
        #endregion
    }
    /// <sumary>
    /// Valida los campos de 1393
    /// </sumary> 
    public sealed class RUL_Validate1393AfiliateData
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// Tipo de Regimen
        /// </sumary>
        private string RegimenType;
        /// <sumary>
        /// Grupo poblacional
        /// </sumary>
        private long PoblationalGroup;
        /// <sumary>
        /// Fecha de nacimiento 
        /// </sumary>
        private string BirthDate;
        /// <sumary>
        /// SEXO
        /// </sumary>
        private string Sex;
        /// <sumary>
        /// Grupo étnico
        /// </sumary>
        private long Ethnicity;
        /// <sumary>
        /// index del registro
        /// </sumary>
        private long Index;
        /// <sumary>
        /// Fecha Afiliación
        /// </sumary>
        private DateTime AffiliationDate;
        /// <sumary>
        /// verifica que este activo para validar Poblational Group dentro de un rango
        /// </sumary>
        private bool VC_PoblationalGroup;
        /// <sumary>
        /// Valida fecha nacimiente
        /// </sumary>
        private bool VC_ValidateBirthDate;
        /// <sumary>
        /// Valida la fecha de afiliacion
        /// </sumary>
        private bool VC_ValidAfiliationDate;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public RUL_Validate1393AfiliateData() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// Valida los campos de 1393
        /// </sumary>
        /// <param name="RegimenType">Tipo de Regimen</param>
        /// <param name="PoblationalGroup">Grupo poblacional</param>
        /// <param name="BirthDate">Fecha de nacimiento </param>
        /// <param name="Sex">SEXO</param>
        /// <param name="Ethnicity">Grupo étnico</param>
        /// <param name="Index">index del registro</param>
        /// <param name="AffiliationDate">Fecha Afiliación</param>
        public RuntimeResult<string> Execute(string RegimenType, long PoblationalGroup, string BirthDate, string Sex, long Ethnicity, long Index, DateTime AffiliationDate)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.RegimenType = RegimenType;
                this.PoblationalGroup = PoblationalGroup;
                this.BirthDate = BirthDate;
                this.Sex = Sex;
                this.Ethnicity = Ethnicity;
                this.Index = Index;
                this.AffiliationDate = AffiliationDate;
                this.VC_PoblationalGroup = FUNC_VC_PoblationalGroup();
                this.VC_ValidateBirthDate = FUNC_VC_ValidateBirthDate();
                this.VC_ValidAfiliationDate = FUNC_VC_ValidAfiliationDate();
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
        private bool FUNC_VC_PoblationalGroup()
        {
            // si es igual a int.MinValue esta inactiva y envia true por defecto
            if (PoblationalGroup == int.MinValue) return true;

            if ((PoblationalGroup >= 1 && PoblationalGroup <= 16) || (PoblationalGroup >= 31 && PoblationalGroup <= 39) || (PoblationalGroup >= 50 && PoblationalGroup <= 60) || (PoblationalGroup == 99))
            {
                return true;
            }
            return false;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidateBirthDate()
        {
            if (DateTime.TryParse(BirthDate, out DateTime resdd))
                return true;
            return false;
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_ValidAfiliationDate()
        {
            return AffiliationDate >= Convert.ToDateTime(BirthDate);
        }
        #endregion

        #region Private Methods
        /// <sumary>
        /// Valida que el valor ingresado en la variable sea válido
        /// </sumary>
        private void ValidateValues()
        {
            List<string> NonValidMessages = new List<string>();
            if (!((new string[] { "C", "S", "P", "E", "N", "TRUE" }).Contains(RegimenType))) NonValidMessages.Add($"Registrar un valor correcto con respecto al tipo de regimen. Validar la variable 2 Tipo de regimen en Linea {Index}");
            if (!(VC_PoblationalGroup == true)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto al Grupo poblacional.Validar la variable 3 Grupo poblacional");
            if (!(VC_ValidateBirthDate == true)) NonValidMessages.Add($"Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar variable 10 Fecha de nacimiento");
            if (!((new string[] { "M", "F", "TRUE" }).Contains(Sex))) NonValidMessages.Add($"Debe ingresar el sexo valido. Validar la variable 11 Sexo");
            if (!(Ethnicity >= 1 && Ethnicity <= 6)) NonValidMessages.Add($"Debe introducir un valor correcto con respecto a la pertenencia étnica.Validar la variable 12 Grupo étnico");
            if (!(VC_ValidAfiliationDate == true)) NonValidMessages.Add($"La fecha debe ser mayor a la fecha de nacimiento. Validar la variable 10 Fecha de nacimiento y variable 16 Fecha de afiliación");

            if (NonValidMessages.Count > 0)
                throw new ArgumentException(string.Join(Environment.NewLine, NonValidMessages));
        }

        /// <sumary>
        /// Evalua las combinaciones
        /// </sumary>
        private RuntimeResult<string> EvaluateCombinations()
        {
            return RuntimeResult<string>.SetValid(() => { return ""; }, $"OK");
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
        /// se valida estructura y que no se dupliquen registros en el archivo
        /// </sumary> 
        /// <param name="lineSeparator">Separador de linea</param>
        /// <param name="columnSeparator">Separador de columna</param>
        /// <param name="company">compañia</param>
        /// <param name="libraryId">id de la libreria</param>
        /// <param name="fileId">id Del archivo Rs 4725</param>
        /// <param name="columnLength">Cantidad de Columnas</param>
        /// <param name="entity">tipo de la entidad</param>
        /// <param name="listErrors">Lista donde se almacenan los errores</param>
        public static dynamic USR_ValidateStructure1393(string lineSeparator, string columnSeparator, long company, long libraryId, string fileId, long columnLength, dynamic entity, List<string> listErrors)
        {
            if (string.IsNullOrEmpty(lineSeparator))
                throw new ArgumentNullException(nameof(lineSeparator));

            if (string.IsNullOrEmpty(columnSeparator))
                throw new ArgumentNullException(nameof(columnSeparator));

            if (company == 0)
                throw new ArgumentNullException(nameof(company));

            if (libraryId == 0)
                throw new ArgumentNullException(nameof(libraryId));

            if (string.IsNullOrEmpty(fileId))
                throw new ArgumentNullException(nameof(fileId));

            if (columnLength == 0)
                throw new ArgumentNullException(nameof(columnLength));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (listErrors == null) listErrors = new List<string>();

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
                    if (!USR_ValidateFileName(7, "ARTRITIS", listErrors, (string)((JValue)((dynamic)result.Result).FileName).Value))
                    {
                        listErrors.Add($"El Nombre del archivo no es valido, no cumple con la estructura espesificada");
                    }
                    using (Stream stream = new MemoryStream(fileBody))
                    {
                        using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                        {
                            var text = streamReader.ReadToEnd();
                            string[] lines = text.Split(new string[] { lineSeparator }, StringSplitOptions.RemoveEmptyEntries);

                            for (var i = 0; i < lines.Length; i++)
                            {
                                string[] columns = lines[i].Split(columnSeparator[0]);

                                if (columns.Length != columnLength)
                                {
                                    listErrors.Add($"La estructura del archivo no corresponde con la longitud, item {i + 1}");
                                }
                            }

                            lstEntities = SYS_FileToEntities(text, lineSeparator, columnSeparator, entity);
                        }
                    }

                    //Asignamos el item de error a los mensajes de validacion de los atributos
                    int index = 0;
                    PropertyInfo[] properties = typeEntity.GetProperties();

                    Dictionary<string, string> dic = new Dictionary<string, string>();

                    foreach (dynamic ent in lstEntities)
                    {
                        //Valida que el usuario no este repetido
                        if (dic.ContainsKey($"{ent.DocumentType}_{ent.DocumentNumber}"))
                        {
                            listErrors.Add($"Fila {index + 1} La personsa identificada con {ent.IdentificationType} {ent.DocumentNumber} se encuentra repetida");
                        }
                        else
                        {
                            dic.Add($"{ent.DocumentType}_{ent.DocumentNumber}", ent.DocumentNumber);
                        }

                        if (ent.ValidationErrorsList?.Count > 0)
                        {
                            string mensajeItem = $" Línea {index + 1}";
                            foreach (string msg in ent.ValidationErrorsList)
                            {
                                var p = properties.Select((Value, Index) => new { Value, Index })
                                .FirstOrDefault(pro => msg.Contains(pro.Value.Name));

                                listErrors.Add(string.Concat(msg, ",", mensajeItem, ",", $" Columna {p.Index + 1}"));

                            }
                        }
                        ++index;
                    }

                }
                else
                {
                    listErrors.Add($"La estructura del archivo {1393} no corresponde a un formato válido ");
                }

            }
            else
            {
                listErrors.Add($"No se encontro el Archivo");
            }
            return lstEntities;
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="listError">lista para guardar errores </param>
        /// <param name="index">indice de la iteracion </param>
        /// <param name="qualificationList">qualificationList</param>
        /// <param name="listCum">listado de CUM</param>
        /// <param name="listaCodeMunicipaly">lista Code Municipaly</param>
        /// <param name="entity">entidad de la resolución 1393</param>
        public static long USR_ValidateQualificationCodeAndCodeCumMunicipaly(List<string> listError, long index, List<dynamic> qualificationList, List<dynamic> listCum, List<dynamic> listaCodeMunicipaly, ENT_Resolution1393 entity)
        {
            if (qualificationList.FirstOrDefault(x => x.QualificationCode == entity.CodeEPS) == null) listError.Add(string.Concat("Codigo no se encuentra parametrizado.Validar la variable 1 Codigo de la EAPB"));

            if (listaCodeMunicipaly.FirstOrDefault(x => x.Code == entity.Municipaly) == null) listError.Add(string.Concat(" Codigo no se encuentra parametrizado. Validar la variable 15 Codigo municipio de residencia.", $" Línea {index + 1}"));

            if (listaCodeMunicipaly.FirstOrDefault(x => x.Code == entity.MunicipalyIPS) == null) listError.Add(string.Concat("Codigo no se encuentra parametrizado. Validar la variable 141 Codigo municipio de la IPS.", $" Línea {index + 1}"));

            return 1;
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="InitPeriod">Fecha periodo inicial</param>
        /// <param name="EndPeriod">fecha final del periodo</param>
        /// <param name="idOperador">id del operador</param>
        /// <param name="idTypePopulation">id population</param>
        public static dynamic USR_ValidatePeriodReported(DateTime InitPeriod, DateTime EndPeriod, long idOperador, long idTypePopulation)
        {
            int adapterId = 1;
            var sql = new StringBuilder();

            sql.Append(" SELECT IdOperator ");
            sql.Append("FROM FileHead1393 WITH(NOLOCK)");
            sql.AppendFormat(" WHERE InitialDate = '{0}'", InitPeriod.ToString("MM/dd/yyyy"));
            sql.AppendFormat(" AND EndDate = '{0}'", EndPeriod.ToString("MM/dd/yyyy"));
            sql.AppendFormat(" AND  IdOperator = {0} ", idOperador);
            sql.AppendFormat(" AND IdTypePopulation = {0}", idTypePopulation);

            var resultExecute = SYS_WSExecuteQuery(adapterId, sql.ToString());

            if (resultExecute.IsError)
            {
                return resultExecute;
            }

            var _head = JsonConvert.DeserializeObject<List<dynamic>>(resultExecute.Result.ToString());

            if (_head != null && _head.Count() > 0)
                return new ENT_ActionResult() { IsSucessfull = false, IsError = true, ErrorMessage = "Ya existe un Cargue para este periodo" };

            return new ENT_ActionResult() { IsSucessfull = true };
        }
        /// <sumary>
        /// Valida que un numero este dentro de un rango o sea igual a otro numero
        /// </sumary> 
        /// <param name="minValue">Valor minimo aceptado</param>
        /// <param name="maxValue">Maximo Valor Aceptado</param>
        /// <param name="valueToEvaluate">Valor a evaluar </param>
        /// <param name="otherValues">Valor opcional a evaluar</param>
        public static bool USR_ValidateGenericRange(long minValue, long maxValue, long valueToEvaluate, List<long> otherValues)
        {
            var range = (valueToEvaluate >= minValue && valueToEvaluate <= maxValue);

            var valide = (otherValues != null && otherValues.Count > 0) ? otherValues.Contains(valueToEvaluate) : range;

            if (range || valide)
            {
                return true;
            }
            else
            {
                return false;
            }

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
        /// Valida documento de afiliado y novedad 
        /// </sumary> 
        /// <param name="_entity">entidad de la resolucion 1393</param>
        /// <param name="listErrors">se agregan los errores que encuentra la funcion</param>
        /// <param name="index">index de la iteracion</param>
        /// <param name="listDocumentTypesDB">listDocumentTypesDB</param>
        public static long USR_ValidateDocumentNumber1393(List<ENT_Resolution1393> _entity, List<string> listErrors, long index, List<dynamic> listDocumentTypesDB)
        {
            List<dynamic> personList = new List<dynamic>();
            Dictionary<string, ENT_Resolution1393> _dictionary1393 = new Dictionary<string, ENT_Resolution1393>();
            var listErrorsDocumentNumber = new List<string>();
            Dictionary<string, string> _dictionaryDocumentType = new Dictionary<string, string>();
            long adapterId = 1;

            var sql = new StringBuilder();
            sql.Append(" SELECT T.Code AS[KEY], T.Id AS[Value]");
            sql.Append(" FROM TypeDetail T");
            sql.Append(" WHERE IdTypeHead = 55");


            var res = SYS_WSExecuteQuery(adapterId, sql.ToString());

            _dictionaryDocumentType = JsonConvert.DeserializeObject<List<dynamic>>(res.Result.ToString()).ToDictionary(x => (string)x.KEY, x => (string)x.Value);

            var l = _entity.Select((obj, i) => (dynamic)new { DocumentType = _dictionaryDocumentType[obj.DocumentType], Identification = obj.DocumentNumber, Index = i + 1, typeId = obj.DocumentType }).ToList();

            USR_ValidateAffiliatePerson(l, adapterId, listErrorsDocumentNumber);
            var newlist = listErrorsDocumentNumber.Select(x => x.Replace("item", "Linea")).ToList();

            listErrors.AddRange(newlist);

            return index;
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
        /// divide una lista en subitems
        /// </sumary> 
        /// <param name="sourceList">Lista a tratar </param>
        /// <param name="maxSubItems">cantidad de items a dividir</param>
        public static dynamic USR_SplitList1393(dynamic sourceList, long maxSubItems)
        {
            return ((IList)sourceList).Cast<dynamic>()
    .Select((x, i) => new { Index = i, Value = x })
    .GroupBy(x => x.Index / maxSubItems)
    .Select(x => x.Select(v => v.Value).ToList())
    .ToList();
        }
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="cutOffDate">Fecha</param>
        /// <param name="code">codigo de resolucion</param>
        /// <param name="idOperator">id del operador</param>
        /// <param name="caseNumber">Numero del Caso</param>
        /// <param name="InitialDate">fecha inicial</param>
        /// <param name="endDate">fecha final</param>
        /// <param name="idPopulation">idPopulation</param>
        /// <param name="listFileEntity">entidad a guardar lista </param>
        public static ENT_ActionResult USR_SaveData1393(string cutOffDate, string code, long idOperator, string caseNumber, string InitialDate, string endDate, long idPopulation, dynamic listFileEntity)
        {
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

            };

            List<dynamic> loaddData = new List<dynamic>();

            string url = "http://davincilb.ophelia.co:8085/ASSURANCE/api/Resolution1393/SaveFile1393Head";
            ENT_ActionResult result = Helper.SYS_WSPOST(url, head, null, null);

            if (result.IsError)
            {
                return result;
            }

            foreach (var item in (List<ENT_Resolution1393>)listFileEntity)
            {
                dynamic detail = new
                {
                    IdFileHead1393 = result.Result.ToString(),
                    item.CodeEPS,
                    item.Regime,
                    item.PopulationGroup,
                    item.FirstName,
                    item.SecondName,
                    item.FirstLastName,
                    item.SecondLastName,
                    item.DocumentType,
                    item.DocumentNumber,
                    item.BirthDate,
                    item.Sex,
                    item.Ethnicity,
                    item.Address,
                    item.Phone,
                    item.Municipaly,
                    item.AffiliationDate,
                    item.InitDateSymptom,
                    item.FirstVisitDateSpecialist,
                    item.DateDiagnosis,
                    item.Height,
                    item.Weight,
                    item.HandXRay,
                    item.FootXRay,
                    item.InitialVSG,
                    item.InitialPCR,
                    item.InitialRheumatoidFactor,
                    item.InitialHemoglobin,
                    item.InitialLeukocytes,
                    item.InitialCeatrinin,
                    item.InitialTFG,
                    item.InitialUrinePartial,
                    item.InicialALT,
                    item.AntiCCPDiagnosis,
                    item.HTADiagnosis,
                    item.DMDiagnosis,
                    item.ECVDiagnosis,
                    item.ERCDiagnosis,
                    item.OsteoporosisDiagnosis,
                    item.SyndromeSjogren,
                    item.DateFirstDAS28,
                    item.ProfessionalFirstDAS28,
                    item.ResultFirstDAS28,
                    item.DateFirstHAQ,
                    item.InitialHAQ,
                    item.InitialDateTreatmentDMARD,
                    item.InitialAnalgesicNotOpioids,
                    item.InitialAnalgesicOpioids,
                    item.StartAINES,
                    item.StartCorticosteroids,
                    item.InitialDateTreatmentDMARDTwo,
                    item.InitialScreeningDMARD,
                    item.LymphomaHistoryDMARD,
                    item.Azatioprina,
                    item.Ciclosporina,
                    item.Ciclofosfamida,
                    item.Cloroquina,
                    item.DPenicilamina,
                    item.Etanercept,
                    item.Leflunomida,
                    item.Metotrexate,
                    item.Rituximab,
                    item.Sulfasalazina,
                    item.Abatacept,
                    item.Adalimumab,
                    item.Certolizumab,
                    item.Golimumab,
                    item.Hidroxicloroquina,
                    item.Infliximab,
                    item.GoldSalts,
                    item.Tocilizumab,
                    item.Tofacitinib,
                    item.Anakinra,
                    item.OtherMedicineNoPOSOne,
                    item.OtherMedicineNoPOSTwo,
                    item.OtherMedicineNoPOSThree,
                    item.OtherMedicineNoPOSFour,
                    item.WeightLastSemester,
                    item.LastHandXRay,
                    item.LastFootXRay,
                    item.PCRLastSemester,
                    item.VSGLastSemester,
                    item.HemoglobinLastSemester,
                    item.LeukocytesLastSemester,
                    item.CreatinineLastSemester,
                    item.TFGLastSemester,
                    item.PartialUrineLastSemester,
                    item.ALTLastSemester,
                    item.CurrentHTA,
                    item.CurrentDM,
                    item.CurrentECV,
                    item.CurrentERC,
                    item.CurrentOsteoporosis,
                    item.CurrentSyndromeSjogren,
                    item.DateLastDAS28,
                    item.ProfessionalLastDAS28,
                    item.ResultLastDAS28,
                    item.StatusCurrentActivityDAS28,
                    item.DateLastHAQ,
                    item.HAQLastSemester,
                    item.AnalgesicNotOpioids,
                    item.AnalgesicOpioids,
                    item.AINES,
                    item.Corticosteroids,
                    item.MonthsUseGlucocorticoids,
                    item.Calcium,
                    item.VitaminD,
                    item.InitialDateCurrentTreatmentDMARD,
                    item.Azathioprine,
                    item.Cyclosporine,
                    item.Cyclophosphamide,
                    item.Chloroquine,
                    item.Dpenicillamine,
                    item.EtanerceptTwo,
                    item.Leflunomide,
                    item.Methotrexate,
                    item.RituximabTwo,
                    item.Sulfasalazine,
                    item.AbataceptTwo,
                    item.AdalimumabTwo,
                    item.CertolizumabTwo,
                    item.GolimumabTwo,
                    item.Hydroxychloroquine,
                    item.InfliximabTwo,
                    item.GoldSaltsOne,
                    item.TocilizumabTwo,
                    item.TofacitinibTwo,
                    item.AnakinraTwo,
                    item.OtherMedicineNotPosOne,
                    item.OtherMedicineNotPosTwo,
                    item.OtherMedicineNotPosThree,
                    item.OtherMedicineNotPosFour,
                    item.RheumatologistConsultNumberLastYear,
                    item.InternistConsultNumberLastYear,
                    item.FamilyDoctorConsultNumberLastYear,
                    item.JointReplacementOne,
                    item.JointReplacementTwo,
                    item.JointReplacementThree,
                    item.JointReplacementFour,
                    item.NumberHospitalizationsLastYear,
                    item.HabilitationCode,
                    item.MunicipalyIPS,
                    item.InitialDateCurrentIPSAR,
                    item.CurrentDoctorAR,
                    item.NoveltyPatient,
                    item.DisenrollmentDate,
                    item.EAPB,
                    item.DeathDate,
                    item.DeathCause,
                    item.AnnualCostDMARDPOS,
                    item.AnnualCostDMARDNOPOS,
                    item.TotalAnnualCostAR,
                    item.TotalAnnualCostInabilities,
                };
                loaddData.Add(detail);
            }

            var lotes = USR_SplitList1393(loaddData, 4000);
            url = "http://davincilb.ophelia.co:8085/ASSURANCE/api/Resolution1393/SaveFile1393Detail";
            foreach (var item in lotes)
            {
                SYS_WSPOST(url, item, null, null);
            }

            return new ENT_ActionResult() { IsSuccessful = true };
        }
        /// <sumary>
        /// Funcion principal para validar estructura y guardar en el log
        /// </sumary> 
        /// <param name="company">Compañia</param>
        /// <param name="libraryId">Id de la libreria</param>
        /// <param name="fileId">id del archivo</param>
        /// <param name="UserCode">Codigo del Usuario</param>
        /// <param name="CaseNumber">Numero del caso </param>
        public static dynamic USR_MainStructure1393(long company, long libraryId, string fileId, string UserCode, string CaseNumber)
        {
            #region Validate
            if (company == 0)
                throw new ArgumentNullException(nameof(company));

            if (libraryId == 0)
                throw new ArgumentNullException(nameof(libraryId));

            if (string.IsNullOrEmpty(fileId))
                throw new ArgumentNullException(nameof(fileId));
            #endregion

            #region Constante
            const string lineSeparator = "\r\n";
            const string columnSeparator = "|";
            const int ColumnLength = 152;
            const string folder = "Resolucion1393";
            #endregion

            var listErrors = new List<string>();

            var result = USR_ValidateStructure1393(lineSeparator, columnSeparator, company, libraryId, fileId, ColumnLength, typeof(ENT_Resolution1393), listErrors);

            if (listErrors.Count > 0)
            {
                string pathFile = USR_GenericSaveLog(new Dictionary<string, List<string>>() { ["1393"] = listErrors }, folder);
                var attach = USR_WSAttachFileToProcess(pathFile, UserCode, company.ToString(), CaseNumber, "1393");

                if (attach.IsError)
                {
                    attach.ErrorMessage = "No se pudo asociar el archivo al proceso, ya que el archivo TXT no fue encontrado con los datos suministrados, favor verificar si se cargo en la plantilla de manera correcta.";
                    return attach;
                }
                return new ENT_ActionResult() { IsSucessfull = false, FileName = attach.FileName, IsError = true, ErrorMessage = "Hubo errores en la validación " };
            }

            return new ENT_ActionResult() { IsSucessfull = true, IsError = false, Result = result };
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
            sbPersonByNumber.Append(" DECLARE @XmlDocumentHandle int; ");
            sbPersonByNumber.Append(" DECLARE @XmlDocument xml; ");
            sbPersonByNumber.Append(" SET @XmlDocument = '<root><ids> ");
            foreach (var documentNumber in documentsNumbers)
                sbPersonByNumber.Append($"<id>{documentNumber.Identification}</id>");
            sbPersonByNumber.Append(" </ids></root> ';");
            sbPersonByNumber.Append("  EXEC sp_xml_preparedocument @XmlDocumentHandle OUTPUT, @XmlDocument; ");
            sbPersonByNumber.Append(" SELECT  DISTINCT Id,IdDocumentType, DocumentNumber FROM Person WITH (NOLOCK) WHERE DocumentNumber IN( ");
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
    /// Entidad representa resolucion 1393
    /// </sumary>
    public class ENT_Resolution1393 : EntityBase
    {
        #region Properties
        /// <sumary>
        /// Codigo de la EPS
        /// </sumary>
        private string _CodeEPS;
        [Order]
        [Regex(@"^.{1,6}$", "Validar longitud de codigo que no supere 6 digitos si es EAPB y 5 si es entidad territorial. Validar la variable 1 Codigo de la EAPB")] public string CodeEPS { get { return _CodeEPS; } set { _CodeEPS = ValidateValue<string>(value, nameof(CodeEPS)); } }
        /// <sumary>
        /// regime
        /// </sumary>
        private string _Regime;
        [Order]
        [Regex(@"^(C|S|P|E|N)$", "La longitud del campo no contiene el valor permitido. Validar la variable 2 Tipo de regimen")] public string Regime { get { return _Regime; } set { _Regime = ValidateValue<string>(value, nameof(Regime)); } }
        /// <sumary>
        /// grupo poblacional
        /// </sumary>
        private string _PopulationGroup;
        [Order]
        [Regex(@"^[0-9]{1,2}$", "La longitud del campo excede el valor permitido. Validar la variable 3 Grupo poblacional")] public string PopulationGroup { get { return _PopulationGroup; } set { _PopulationGroup = ValidateValue<string>(value, nameof(PopulationGroup)); } }
        /// <sumary>
        /// primer nombre
        /// </sumary>
        private string _FirstName;
        [Order]
        [Regex(@"^([A-Za-z ]{3,30})$", "Debe ingresar un nombre valido. Validar la variable 4 Primer nombre")] public string FirstName { get { return _FirstName; } set { _FirstName = ValidateValue<string>(value, nameof(FirstName)); } }
        /// <sumary>
        /// segundo nombre
        /// </sumary>
        private string _SecondName;
        [Order]
        [Regex(@"^([A-Za-z ]{3,30})$", "Debe ingresar un nombre valido, en caso de no tener segundo nombre registre NONE. Validar la variable 5 Segundo nombre")] public string SecondName { get { return _SecondName; } set { _SecondName = ValidateValue<string>(value, nameof(SecondName)); } }
        /// <sumary>
        /// apellido 
        /// </sumary>
        private string _FirstLastName;
        [Order]
        [Regex(@"^([A-Za-z ]{3,30})$", "Debe ingresar un apellido valido. Validar la variable 6 Primer apellido")] public string FirstLastName { get { return _FirstLastName; } set { _FirstLastName = ValidateValue<string>(value, nameof(FirstLastName)); } }
        /// <sumary>
        /// segundo apellido
        /// </sumary>
        private string _SecondLastName;
        [Order]
        [Regex(@"^([A-Za-z ]{3,30})$", "Debe ingresar un apellido valido, en caso de no tener segundo apellido registre NOAP. Validar la variable 5 Segundo nombre")] public string SecondLastName { get { return _SecondLastName; } set { _SecondLastName = ValidateValue<string>(value, nameof(SecondLastName)); } }
        /// <sumary>
        /// tipo documento
        /// </sumary>
        private string _DocumentType;
        [Order]
        [Regex(@"^(TI|CC|CE|PA|RC|UN|RNV|MS|AS)$", "La longitud del campo excede el valor permitido. Validar la variable 8 Tipo identificación")] public string DocumentType { get { return _DocumentType; } set { _DocumentType = ValidateValue<string>(value, nameof(DocumentType)); } }
        /// <sumary>
        /// numero documento
        /// </sumary>
        private string _DocumentNumber;
        [Order]
        [Regex(@"^[A-Za-z0-9_-]{1,20}$", "La longitud del campo excede el valor permitido. Validar la variable 9 Numero de identificación")] public string DocumentNumber { get { return _DocumentNumber; } set { _DocumentNumber = ValidateValue<string>(value, nameof(DocumentNumber)); } }
        /// <sumary>
        /// Fecha de nacimiento
        /// </sumary>
        private string _BirthDate;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "La longitud del campo no es correcta. Validar variable 10 Fecha de nacimiento")] public string BirthDate { get { return _BirthDate; } set { _BirthDate = ValidateValue<string>(value, nameof(BirthDate)); } }
        /// <sumary>
        /// sexo
        /// </sumary>
        private string _Sex;
        [Order]
        [Regex(@"^(M|F)$", "La longitud del campo excede el valor permitido. Validar la variable 11 Sexo")] public string Sex { get { return _Sex; } set { _Sex = ValidateValue<string>(value, nameof(Sex)); } }
        /// <sumary>
        /// Grupo étnico
        /// </sumary>
        private string _Ethnicity;
        [Order]
        [Regex(@"^[1-6]$", "La longitud del campo excede el valor permitido. Validar la variable 12 Grupo étnico")] public string Ethnicity { get { return _Ethnicity; } set { _Ethnicity = ValidateValue<string>(value, nameof(Ethnicity)); } }
        /// <sumary>
        /// Dirección de residencia
        /// </sumary>
        private string _Address;
        [Order]
        [Regex(@"^.{1,50}$", "La longitud del campo excede el valor permitido. Validar la variable 13 Dirección de residencia.")] public string Address { get { return _Address; } set { _Address = ValidateValue<string>(value, nameof(Address)); } }
        /// <sumary>
        /// Teléfono de contacto
        /// </sumary>
        private string _Phone;
        [Order]
        [Regex(@"^[0-9]{2,30}$", "La longitud del campo excede el valor permitido. Validar la variable 14 Teléfono de contacto.")] public string Phone { get { return _Phone; } set { _Phone = ValidateValue<string>(value, nameof(Phone)); } }
        /// <sumary>
        /// Codigo municipio de residencia
        /// </sumary>
        private string _Municipaly;
        [Order]
        [Regex(@"^.{1,5}$", "Validar longitud de codigo. Validar la variable 15 Codigo municipio de residencia.")] public string Municipaly { get { return _Municipaly; } set { _Municipaly = ValidateValue<string>(value, nameof(Municipaly)); } }
        /// <sumary>
        /// Fecha de afiliación
        /// </sumary>
        private string _AffiliationDate;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Validar longitud de fecha. Validar la variable 16 Fecha de afiliación     |      Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la  variable 16 Fecha de afiliación.")] public string AffiliationDate { get { return _AffiliationDate; } set { _AffiliationDate = ValidateValue<string>(value, nameof(AffiliationDate)); } }
        /// <sumary>
        /// date init
        /// </sumary>
        private string _InitDateSymptom;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Validar longitud de fecha. Validar la variable 17 Fecha inicio sintomas de AR   |  Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 17 Fecha inicio sintomas de AR")] public string InitDateSymptom { get { return _InitDateSymptom; } set { _InitDateSymptom = ValidateValue<string>(value, nameof(InitDateSymptom)); } }
        /// <sumary>
        /// Fecha primera visita especialista por AR
        /// </sumary>
        private string _FirstVisitDateSpecialist;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Validar longitud de fecha. Validar la variable 18 Fecha primera visita especialista por AR   |  Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 18 Fecha primera visita especialista por AR")] public string FirstVisitDateSpecialist { get { return _FirstVisitDateSpecialist; } set { _FirstVisitDateSpecialist = ValidateValue<string>(value, nameof(FirstVisitDateSpecialist)); } }
        /// <sumary>
        /// Fecha diagnostico AR
        /// </sumary>
        private string _DateDiagnosis;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Validar longitud de fecha. Validar la variable 19 Fecha diagnostico AR.  |  Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 19 Fecha diagnostico AR")] public string DateDiagnosis { get { return _DateDiagnosis; } set { _DateDiagnosis = ValidateValue<string>(value, nameof(DateDiagnosis)); } }
        /// <sumary>
        /// Talla
        /// </sumary>
        private string _Height;
        [Order]
        [Regex(@"^[0-9]{2,3}$", "La longitud del campo excede el valor permitido. Validar la variable 20 Talla")] public string Height { get { return _Height; } set { _Height = ValidateValue<string>(value, nameof(Height)); } }
        /// <sumary>
        /// Peso inicial
        /// </sumary>
        private string _Weight;
        [Order]
        [Regex(@"^[0-9]{3}([.][0-9]{1})?$", "La longitud del campo excede el valor permitido. Validar la variable 21 Peso inicial")] public string Weight { get { return _Weight; } set { _Weight = ValidateValue<string>(value, nameof(Weight)); } }
        /// <sumary>
        /// Radiografia de manos al diagnostico
        /// </sumary>
        private string _HandXRay;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 22 Radiografia de manos al diagnostico")] public string HandXRay { get { return _HandXRay; } set { _HandXRay = ValidateValue<string>(value, nameof(HandXRay)); } }
        /// <sumary>
        /// Radiografia de pies al diagnostico
        /// </sumary>
        private string _FootXRay;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 23 Radiografia de pies al diagnostico")] public string FootXRay { get { return _FootXRay; } set { _FootXRay = ValidateValue<string>(value, nameof(FootXRay)); } }
        /// <sumary>
        /// VSG inicial
        /// </sumary>
        private string _InitialVSG;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 24 VSG inicial")] public string InitialVSG { get { return _InitialVSG; } set { _InitialVSG = ValidateValue<string>(value, nameof(InitialVSG)); } }
        /// <sumary>
        /// PCR inicial
        /// </sumary>
        private string _InitialPCR;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 25 PCR inicial")] public string InitialPCR { get { return _InitialPCR; } set { _InitialPCR = ValidateValue<string>(value, nameof(InitialPCR)); } }
        /// <sumary>
        /// Factor reumatoideo inicial
        /// </sumary>
        private string _InitialRheumatoidFactor;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 26 Factor reumatoideo inicial")] public string InitialRheumatoidFactor { get { return _InitialRheumatoidFactor; } set { _InitialRheumatoidFactor = ValidateValue<string>(value, nameof(InitialRheumatoidFactor)); } }
        /// <sumary>
        /// Hemoglobina inicial
        /// </sumary>
        private string _InitialHemoglobin;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 27 Hemoglobina inicial")] public string InitialHemoglobin { get { return _InitialHemoglobin; } set { _InitialHemoglobin = ValidateValue<string>(value, nameof(InitialHemoglobin)); } }
        /// <sumary>
        /// Leucocitos inicial
        /// </sumary>
        private string _InitialLeukocytes;
        [Order]
        [Regex(@"^[0-9]{1,5}$", "La longitud del campo excede el valor permitido. Validar la variable 28 Leucocitos inicial")] public string InitialLeukocytes { get { return _InitialLeukocytes; } set { _InitialLeukocytes = ValidateValue<string>(value, nameof(InitialLeukocytes)); } }
        /// <sumary>
        /// Creatinina inicial
        /// </sumary>
        private string _InitialCeatrinin;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 29 Creatinina inicial.")] public string InitialCeatrinin { get { return _InitialCeatrinin; } set { _InitialCeatrinin = ValidateValue<string>(value, nameof(InitialCeatrinin)); } }
        /// <sumary>
        /// TFG inicial
        /// </sumary>
        private string _InitialTFG;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 30 TFG inicial.")] public string InitialTFG { get { return _InitialTFG; } set { _InitialTFG = ValidateValue<string>(value, nameof(InitialTFG)); } }
        /// <sumary>
        /// Parcial de orina inicial
        /// </sumary>
        private string _InitialUrinePartial;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 31 Parcial de orina inicial.")] public string InitialUrinePartial { get { return _InitialUrinePartial; } set { _InitialUrinePartial = ValidateValue<string>(value, nameof(InitialUrinePartial)); } }
        /// <sumary>
        /// ALT inicial
        /// </sumary>
        private string _InicialALT;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 32 ALT inicial.")] public string InicialALT { get { return _InicialALT; } set { _InicialALT = ValidateValue<string>(value, nameof(InicialALT)); } }
        /// <sumary>
        /// Anti-CCP al diagnóstico
        /// </sumary>
        private string _AntiCCPDiagnosis;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 33 Anti-CCP al diagnóstico.")] public string AntiCCPDiagnosis { get { return _AntiCCPDiagnosis; } set { _AntiCCPDiagnosis = ValidateValue<string>(value, nameof(AntiCCPDiagnosis)); } }
        /// <sumary>
        /// HTA al diagnóstico
        /// </sumary>
        private string _HTADiagnosis;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 34 HTA al diagnóstico.")] public string HTADiagnosis { get { return _HTADiagnosis; } set { _HTADiagnosis = ValidateValue<string>(value, nameof(HTADiagnosis)); } }
        /// <sumary>
        /// DM al diagnóstico
        /// </sumary>
        private string _DMDiagnosis;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 35 DM al diagnóstico.")] public string DMDiagnosis { get { return _DMDiagnosis; } set { _DMDiagnosis = ValidateValue<string>(value, nameof(DMDiagnosis)); } }
        /// <sumary>
        /// ECV al diagnóstico
        /// </sumary>
        private string _ECVDiagnosis;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 36 ECV al diagnóstico.")] public string ECVDiagnosis { get { return _ECVDiagnosis; } set { _ECVDiagnosis = ValidateValue<string>(value, nameof(ECVDiagnosis)); } }
        /// <sumary>
        /// ERC al diagnóstico
        /// </sumary>
        private string _ERCDiagnosis;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 37 ERC al diagnóstico.")] public string ERCDiagnosis { get { return _ERCDiagnosis; } set { _ERCDiagnosis = ValidateValue<string>(value, nameof(ERCDiagnosis)); } }
        /// <sumary>
        /// Osteoporosis al diagnóstico
        /// </sumary>
        private string _OsteoporosisDiagnosis;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 38 Osteoporosis al diagnóstico.")] public string OsteoporosisDiagnosis { get { return _OsteoporosisDiagnosis; } set { _OsteoporosisDiagnosis = ValidateValue<string>(value, nameof(OsteoporosisDiagnosis)); } }
        /// <sumary>
        /// Sindrome de Sjogren al diagnóstico
        /// </sumary>
        private string _SyndromeSjogren;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 39 Sindrome de Sjogren al diagnóstico.")] public string SyndromeSjogren { get { return _SyndromeSjogren; } set { _SyndromeSjogren = ValidateValue<string>(value, nameof(SyndromeSjogren)); } }
        /// <sumary>
        /// Fecha del primer DAS 28 realizado
        /// </sumary>
        private string _DateFirstDAS28;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar variable 10 Fecha del primer DAS 28 realizado |  Debe introducir una fecha valida del primer DAS 28 realizado. Validar la variable 40 Fecha del primer DAS 28 realizado.")] public string DateFirstDAS28 { get { return _DateFirstDAS28; } set { _DateFirstDAS28 = ValidateValue<string>(value, nameof(DateFirstDAS28)); } }
        /// <sumary>
        /// Profesional que realizo el primer DAS 28
        /// </sumary>
        private string _ProfessionalFirstDAS28;
        [Order]
        [Regex(@"^.{1}$", "La longitud del campo excede el valor permitido. Validar la variable 41 Profesional que realizo el primer DAS 28.")] public string ProfessionalFirstDAS28 { get { return _ProfessionalFirstDAS28; } set { _ProfessionalFirstDAS28 = ValidateValue<string>(value, nameof(ProfessionalFirstDAS28)); } }
        /// <sumary>
        /// Resultado del primer DAS 28
        /// </sumary>
        private string _ResultFirstDAS28;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 42 Resultado del primer DAS 28.")] public string ResultFirstDAS28 { get { return _ResultFirstDAS28; } set { _ResultFirstDAS28 = ValidateValue<string>(value, nameof(ResultFirstDAS28)); } }
        /// <sumary>
        /// Fecha primer HAQ realizado
        /// </sumary>
        private string _DateFirstHAQ;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Validar longitud de fecha. Validar la variable 43 Fecha primer HAQ realizado | Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 43 Fecha primer HAQ realizado")] public string DateFirstHAQ { get { return _DateFirstHAQ; } set { _DateFirstHAQ = ValidateValue<string>(value, nameof(DateFirstHAQ)); } }
        /// <sumary>
        /// HAQ inicial
        /// </sumary>
        private string _InitialHAQ;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 44 HAQ inicial.")] public string InitialHAQ { get { return _InitialHAQ; } set { _InitialHAQ = ValidateValue<string>(value, nameof(InitialHAQ)); } }
        /// <sumary>
        /// Fecha inicio tratamiento sin DMARD
        /// </sumary>
        private string _InitialDateTreatmentDMARD;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 45 Fecha inicio tratamiento sin DMARD")] public string InitialDateTreatmentDMARD { get { return _InitialDateTreatmentDMARD; } set { _InitialDateTreatmentDMARD = ValidateValue<string>(value, nameof(InitialDateTreatmentDMARD)); } }
        /// <sumary>
        /// Analgésicos No Opioides al inicio
        /// </sumary>
        private string _InitialAnalgesicNotOpioids;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 46 Analgésicos No Opioides al inicio.")] public string InitialAnalgesicNotOpioids { get { return _InitialAnalgesicNotOpioids; } set { _InitialAnalgesicNotOpioids = ValidateValue<string>(value, nameof(InitialAnalgesicNotOpioids)); } }
        /// <sumary>
        /// Analgésicos Opioides (Codeina, Tramadol) al inicio
        /// </sumary>
        private string _InitialAnalgesicOpioids;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 47 Analgésicos Opioides (Codeina, Tramadol) al inicio.")] public string InitialAnalgesicOpioids { get { return _InitialAnalgesicOpioids; } set { _InitialAnalgesicOpioids = ValidateValue<string>(value, nameof(InitialAnalgesicOpioids)); } }
        /// <sumary>
        /// AINES al inicio
        /// </sumary>
        private string _StartAINES;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 48 AINES al inicio.")] public string StartAINES { get { return _StartAINES; } set { _StartAINES = ValidateValue<string>(value, nameof(StartAINES)); } }
        /// <sumary>
        /// Corticoides al inicio
        /// </sumary>
        private string _StartCorticosteroids;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 49 Corticoides al inicio.")] public string StartCorticosteroids { get { return _StartCorticosteroids; } set { _StartCorticosteroids = ValidateValue<string>(value, nameof(StartCorticosteroids)); } }
        /// <sumary>
        /// Fecha inicio tratamiento sin DMARD
        /// </sumary>
        private string _InitialDateTreatmentDMARDTwo;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Validar longitud de fecha. Validar la variable 45 Fecha inicio tratamiento sin DMARD  |  Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 45 Fecha inicio tratamiento sin DMARD")] public string InitialDateTreatmentDMARDTwo { get { return _InitialDateTreatmentDMARDTwo; } set { _InitialDateTreatmentDMARDTwo = ValidateValue<string>(value, nameof(InitialDateTreatmentDMARDTwo)); } }
        /// <sumary>
        /// Tamizaje para TB antes del inicio de DMARD
        /// </sumary>
        private string _InitialScreeningDMARD;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 51 Tamizaje para TB antes del inicio de DMARD.")] public string InitialScreeningDMARD { get { return _InitialScreeningDMARD; } set { _InitialScreeningDMARD = ValidateValue<string>(value, nameof(InitialScreeningDMARD)); } }
        /// <sumary>
        /// Antecedente de linfoma antes del inicio de DMARD
        /// </sumary>
        private string _LymphomaHistoryDMARD;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 52 Antecedente de linfoma antes del inicio de DMARD.")] public string LymphomaHistoryDMARD { get { return _LymphomaHistoryDMARD; } set { _LymphomaHistoryDMARD = ValidateValue<string>(value, nameof(LymphomaHistoryDMARD)); } }
        /// <sumary>
        /// Azatioprina
        /// </sumary>
        private string _Azatioprina;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 53 Azatioprina.")] public string Azatioprina { get { return _Azatioprina; } set { _Azatioprina = ValidateValue<string>(value, nameof(Azatioprina)); } }
        /// <sumary>
        /// Ciclosporina
        /// </sumary>
        private string _Ciclosporina;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 54 Ciclosporina.")] public string Ciclosporina { get { return _Ciclosporina; } set { _Ciclosporina = ValidateValue<string>(value, nameof(Ciclosporina)); } }
        /// <sumary>
        /// Ciclofosfamida
        /// </sumary>
        private string _Ciclofosfamida;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 55 Ciclofosfamida.")] public string Ciclofosfamida { get { return _Ciclofosfamida; } set { _Ciclofosfamida = ValidateValue<string>(value, nameof(Ciclofosfamida)); } }
        /// <sumary>
        /// Cloroquina
        /// </sumary>
        private string _Cloroquina;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 56 Cloroquina.")] public string Cloroquina { get { return _Cloroquina; } set { _Cloroquina = ValidateValue<string>(value, nameof(Cloroquina)); } }
        /// <sumary>
        /// D-penicilamina
        /// </sumary>
        private string _DPenicilamina;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 57 D-penicilamina.")] public string DPenicilamina { get { return _DPenicilamina; } set { _DPenicilamina = ValidateValue<string>(value, nameof(DPenicilamina)); } }
        /// <sumary>
        /// Etanercept
        /// </sumary>
        private string _Etanercept;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 113 Etanercept.")] public string Etanercept { get { return _Etanercept; } set { _Etanercept = ValidateValue<string>(value, nameof(Etanercept)); } }
        /// <sumary>
        /// Leflunomida
        /// </sumary>
        private string _Leflunomida;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 59 Leflunomida.")] public string Leflunomida { get { return _Leflunomida; } set { _Leflunomida = ValidateValue<string>(value, nameof(Leflunomida)); } }
        /// <sumary>
        /// Metotrexate
        /// </sumary>
        private string _Metotrexate;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 60 Metotrexate.")] public string Metotrexate { get { return _Metotrexate; } set { _Metotrexate = ValidateValue<string>(value, nameof(Metotrexate)); } }
        /// <sumary>
        /// Rituximab
        /// </sumary>
        private string _Rituximab;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 116 Rituximab.")] public string Rituximab { get { return _Rituximab; } set { _Rituximab = ValidateValue<string>(value, nameof(Rituximab)); } }
        /// <sumary>
        /// Sulfasalazina
        /// </sumary>
        private string _Sulfasalazina;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 62 Sulfasalazina.")] public string Sulfasalazina { get { return _Sulfasalazina; } set { _Sulfasalazina = ValidateValue<string>(value, nameof(Sulfasalazina)); } }
        /// <sumary>
        /// Abatacept
        /// </sumary>
        private string _Abatacept;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 63 Abatacept.")] public string Abatacept { get { return _Abatacept; } set { _Abatacept = ValidateValue<string>(value, nameof(Abatacept)); } }
        /// <sumary>
        /// Adalimumab
        /// </sumary>
        private string _Adalimumab;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 64 Adalimumab.")] public string Adalimumab { get { return _Adalimumab; } set { _Adalimumab = ValidateValue<string>(value, nameof(Adalimumab)); } }
        /// <sumary>
        /// Certolizumab
        /// </sumary>
        private string _Certolizumab;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 65 Certolizumab.")] public string Certolizumab { get { return _Certolizumab; } set { _Certolizumab = ValidateValue<string>(value, nameof(Certolizumab)); } }
        /// <sumary>
        /// Golimumab
        /// </sumary>
        private string _Golimumab;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 66 Golimumab.")] public string Golimumab { get { return _Golimumab; } set { _Golimumab = ValidateValue<string>(value, nameof(Golimumab)); } }
        /// <sumary>
        /// Hidroxicloroquina
        /// </sumary>
        private string _Hidroxicloroquina;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 67 Hidroxicloroquina.")] public string Hidroxicloroquina { get { return _Hidroxicloroquina; } set { _Hidroxicloroquina = ValidateValue<string>(value, nameof(Hidroxicloroquina)); } }
        /// <sumary>
        /// Infliximab
        /// </sumary>
        private string _Infliximab;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 68 Infliximab.")] public string Infliximab { get { return _Infliximab; } set { _Infliximab = ValidateValue<string>(value, nameof(Infliximab)); } }
        /// <sumary>
        /// Sales de oro
        /// </sumary>
        private string _GoldSalts;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 69 Sales de oro.")] public string GoldSalts { get { return _GoldSalts; } set { _GoldSalts = ValidateValue<string>(value, nameof(GoldSalts)); } }
        /// <sumary>
        /// Tocilizumab
        /// </sumary>
        private string _Tocilizumab;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 70 Tocilizumab.")] public string Tocilizumab { get { return _Tocilizumab; } set { _Tocilizumab = ValidateValue<string>(value, nameof(Tocilizumab)); } }
        /// <sumary>
        /// Tofacitinib
        /// </sumary>
        private string _Tofacitinib;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 126 Tofacitinib.")] public string Tofacitinib { get { return _Tofacitinib; } set { _Tofacitinib = ValidateValue<string>(value, nameof(Tofacitinib)); } }
        /// <sumary>
        /// Anakinra
        /// </sumary>
        private string _Anakinra;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 72 Anakinra.")] public string Anakinra { get { return _Anakinra; } set { _Anakinra = ValidateValue<string>(value, nameof(Anakinra)); } }
        /// <sumary>
        /// Otro medicamento NO POS
        /// </sumary>
        private string _OtherMedicineNoPOSOne;
        [Order]
        [Regex(@"^.{1,20}$", "La longitud del campo excede el valor permitido. Validar la variable 73 Otro medicamento NO POS")] public string OtherMedicineNoPOSOne { get { return _OtherMedicineNoPOSOne; } set { _OtherMedicineNoPOSOne = ValidateValue<string>(value, nameof(OtherMedicineNoPOSOne)); } }
        /// <sumary>
        /// Otro medicamento NO POS 2
        /// </sumary>
        private string _OtherMedicineNoPOSTwo;
        [Order]
        [Regex(@"^.{1,20}$", "La longitud del campo excede el valor permitido. Validar la variable 74 Otro medicamento NO POS 2")] public string OtherMedicineNoPOSTwo { get { return _OtherMedicineNoPOSTwo; } set { _OtherMedicineNoPOSTwo = ValidateValue<string>(value, nameof(OtherMedicineNoPOSTwo)); } }
        /// <sumary>
        /// Otro medicamento NO POS 3
        /// </sumary>
        private string _OtherMedicineNoPOSThree;
        [Order]
        [Regex(@"^.{1,20}$", "La longitud del campo excede el valor permitido. Validar la variable 75 Otro medicamento NO POS 3")] public string OtherMedicineNoPOSThree { get { return _OtherMedicineNoPOSThree; } set { _OtherMedicineNoPOSThree = ValidateValue<string>(value, nameof(OtherMedicineNoPOSThree)); } }
        /// <sumary>
        /// Otro medicamento NO POS 4
        /// </sumary>
        private string _OtherMedicineNoPOSFour;
        [Order]
        [Regex(@"^.{1,20}$", "La longitud del campo excede el valor permitido. Validar la variable 76 Otro medicamento NO POS 4")] public string OtherMedicineNoPOSFour { get { return _OtherMedicineNoPOSFour; } set { _OtherMedicineNoPOSFour = ValidateValue<string>(value, nameof(OtherMedicineNoPOSFour)); } }
        /// <sumary>
        /// Peso último semestre
        /// </sumary>
        private string _WeightLastSemester;
        [Order]
        [Regex(@"^[0-9]{3}([.][0-9]{1})?$", "La longitud del campo excede el valor permitido. Validar la variable 77 Peso último semestre.")] public string WeightLastSemester { get { return _WeightLastSemester; } set { _WeightLastSemester = ValidateValue<string>(value, nameof(WeightLastSemester)); } }
        /// <sumary>
        /// Ultima radiografía de manos
        /// </sumary>
        private string _LastHandXRay;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 78 Ultima radiografía de manos")] public string LastHandXRay { get { return _LastHandXRay; } set { _LastHandXRay = ValidateValue<string>(value, nameof(LastHandXRay)); } }
        /// <sumary>
        /// Ultima radiografía de pies
        /// </sumary>
        private string _LastFootXRay;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 79 Ultima radiografía de pies")] public string LastFootXRay { get { return _LastFootXRay; } set { _LastFootXRay = ValidateValue<string>(value, nameof(LastFootXRay)); } }
        /// <sumary>
        /// PCR último semestre
        /// </sumary>
        private string _PCRLastSemester;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 80 PCR último semestre")] public string PCRLastSemester { get { return _PCRLastSemester; } set { _PCRLastSemester = ValidateValue<string>(value, nameof(PCRLastSemester)); } }
        /// <sumary>
        /// VSG último semestre
        /// </sumary>
        private string _VSGLastSemester;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 81 VSG último semestre")] public string VSGLastSemester { get { return _VSGLastSemester; } set { _VSGLastSemester = ValidateValue<string>(value, nameof(VSGLastSemester)); } }
        /// <sumary>
        /// Hemoglobina último semestre
        /// </sumary>
        private string _HemoglobinLastSemester;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 82 Hemoglobina último semestre")] public string HemoglobinLastSemester { get { return _HemoglobinLastSemester; } set { _HemoglobinLastSemester = ValidateValue<string>(value, nameof(HemoglobinLastSemester)); } }
        /// <sumary>
        /// Leucocitos último semestre
        /// </sumary>
        private string _LeukocytesLastSemester;
        [Order]
        [Regex(@"^[0-9]{1,5}$", "La longitud del campo excede el valor permitido. Validar la variable 83 Leucocitos último semestre")] public string LeukocytesLastSemester { get { return _LeukocytesLastSemester; } set { _LeukocytesLastSemester = ValidateValue<string>(value, nameof(LeukocytesLastSemester)); } }
        /// <sumary>
        /// Creatinina último semestre
        /// </sumary>
        private string _CreatinineLastSemester;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 84 Creatinina último semestre")] public string CreatinineLastSemester { get { return _CreatinineLastSemester; } set { _CreatinineLastSemester = ValidateValue<string>(value, nameof(CreatinineLastSemester)); } }
        /// <sumary>
        /// TFG último semestre
        /// </sumary>
        private string _TFGLastSemester;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 85 TFG último semestre")] public string TFGLastSemester { get { return _TFGLastSemester; } set { _TFGLastSemester = ValidateValue<string>(value, nameof(TFGLastSemester)); } }
        /// <sumary>
        /// "Parcial de Orina último semestre
        /// </sumary>
        private string _PartialUrineLastSemester;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 86 Parcial de Orina último semestre")] public string PartialUrineLastSemester { get { return _PartialUrineLastSemester; } set { _PartialUrineLastSemester = ValidateValue<string>(value, nameof(PartialUrineLastSemester)); } }
        /// <sumary>
        /// ALT último semestre
        /// </sumary>
        private string _ALTLastSemester;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 87 ALT último semestre")] public string ALTLastSemester { get { return _ALTLastSemester; } set { _ALTLastSemester = ValidateValue<string>(value, nameof(ALTLastSemester)); } }
        /// <sumary>
        /// "HTA actual
        /// </sumary>
        private string _CurrentHTA;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 88 HTA actual")] public string CurrentHTA { get { return _CurrentHTA; } set { _CurrentHTA = ValidateValue<string>(value, nameof(CurrentHTA)); } }
        /// <sumary>
        /// DM actual
        /// </sumary>
        private string _CurrentDM;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 89 DM actual")] public string CurrentDM { get { return _CurrentDM; } set { _CurrentDM = ValidateValue<string>(value, nameof(CurrentDM)); } }
        /// <sumary>
        /// ECV actual
        /// </sumary>
        private string _CurrentECV;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 90 ECV actual")] public string CurrentECV { get { return _CurrentECV; } set { _CurrentECV = ValidateValue<string>(value, nameof(CurrentECV)); } }
        /// <sumary>
        /// ERC actual
        /// </sumary>
        private string _CurrentERC;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 91 ERC actual")] public string CurrentERC { get { return _CurrentERC; } set { _CurrentERC = ValidateValue<string>(value, nameof(CurrentERC)); } }
        /// <sumary>
        /// Osteoporosis actual
        /// </sumary>
        private string _CurrentOsteoporosis;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 92 Osteoporosis actual")] public string CurrentOsteoporosis { get { return _CurrentOsteoporosis; } set { _CurrentOsteoporosis = ValidateValue<string>(value, nameof(CurrentOsteoporosis)); } }
        /// <sumary>
        /// Síndrome de Sjogren actual
        /// </sumary>
        private string _CurrentSyndromeSjogren;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 93 Síndrome de Sjogren actual")] public string CurrentSyndromeSjogren { get { return _CurrentSyndromeSjogren; } set { _CurrentSyndromeSjogren = ValidateValue<string>(value, nameof(CurrentSyndromeSjogren)); } }
        /// <sumary>
        /// Fecha del último DAS 28 realizado
        /// </sumary>
        private string _DateLastDAS28;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Validar longitud de fecha. Validar la variable 94 Fecha del último DAS 28 realizado |  Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 94 Fecha del último DAS 28 realizado")] public string DateLastDAS28 { get { return _DateLastDAS28; } set { _DateLastDAS28 = ValidateValue<string>(value, nameof(DateLastDAS28)); } }
        /// <sumary>
        /// Profesional que realizó el último DAS 28
        /// </sumary>
        private string _ProfessionalLastDAS28;
        [Order]
        [Regex(@"^[0-9]$", "La longitud del campo excede el valor permitido. Validar la variable 95 Profesional que realizó el último DAS 28")] public string ProfessionalLastDAS28 { get { return _ProfessionalLastDAS28; } set { _ProfessionalLastDAS28 = ValidateValue<string>(value, nameof(ProfessionalLastDAS28)); } }
        /// <sumary>
        /// Resultado del último DAS 28
        /// </sumary>
        private string _ResultLastDAS28;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 96 Resultado del último DAS 28")] public string ResultLastDAS28 { get { return _ResultLastDAS28; } set { _ResultLastDAS28 = ValidateValue<string>(value, nameof(ResultLastDAS28)); } }
        /// <sumary>
        /// Estado de actividad actual de la AR según DAS 28
        /// </sumary>
        private string _StatusCurrentActivityDAS28;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 97 Estado de actividad actual de la AR según DAS 28")] public string StatusCurrentActivityDAS28 { get { return _StatusCurrentActivityDAS28; } set { _StatusCurrentActivityDAS28 = ValidateValue<string>(value, nameof(StatusCurrentActivityDAS28)); } }
        /// <sumary>
        /// Fecha del último HAQ realizado
        /// </sumary>
        private string _DateLastHAQ;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Validar longitud de fecha. Validar la variable 98 Fecha del último HAQ realizado  |  Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 98 Fecha del último HAQ realizado")] public string DateLastHAQ { get { return _DateLastHAQ; } set { _DateLastHAQ = ValidateValue<string>(value, nameof(DateLastHAQ)); } }
        /// <sumary>
        /// HAQ último semestre
        /// </sumary>
        private string _HAQLastSemester;
        [Order]
        [Regex(@"^[0-9]{1,3}$", "La longitud del campo excede el valor permitido. Validar la variable 99 HAQ último semestre")] public string HAQLastSemester { get { return _HAQLastSemester; } set { _HAQLastSemester = ValidateValue<string>(value, nameof(HAQLastSemester)); } }
        /// <sumary>
        /// Analgésicos No Opioides (Acetaminofén, Dipirona)
        /// </sumary>
        private string _AnalgesicNotOpioids;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 100 Analgésicos No Opioides (Acetaminofén, Dipirona)")] public string AnalgesicNotOpioids { get { return _AnalgesicNotOpioids; } set { _AnalgesicNotOpioids = ValidateValue<string>(value, nameof(AnalgesicNotOpioids)); } }
        /// <sumary>
        /// Analgésicos Opioides (Codeina, Tramadol)
        /// </sumary>
        private string _AnalgesicOpioids;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 100 Analgésicos Opioides (Codeina, Tramadol)")] public string AnalgesicOpioids { get { return _AnalgesicOpioids; } set { _AnalgesicOpioids = ValidateValue<string>(value, nameof(AnalgesicOpioids)); } }
        /// <sumary>
        /// AINES
        /// </sumary>
        private string _AINES;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 102 AINES")] public string AINES { get { return _AINES; } set { _AINES = ValidateValue<string>(value, nameof(AINES)); } }
        /// <sumary>
        /// Corticoides
        /// </sumary>
        private string _Corticosteroids;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 103 Corticoides")] public string Corticosteroids { get { return _Corticosteroids; } set { _Corticosteroids = ValidateValue<string>(value, nameof(Corticosteroids)); } }
        /// <sumary>
        /// Meses de uso de Glucocorticoides
        /// </sumary>
        private string _MonthsUseGlucocorticoids;
        [Order]
        [Regex(@"^[0-9]{1,2}$", "La longitud del campo excede el valor permitido. Validar la variable 104 Meses de uso de Glucocorticoides")] public string MonthsUseGlucocorticoids { get { return _MonthsUseGlucocorticoids; } set { _MonthsUseGlucocorticoids = ValidateValue<string>(value, nameof(MonthsUseGlucocorticoids)); } }
        /// <sumary>
        /// Calcio
        /// </sumary>
        private string _Calcium;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 105 Calcio.")] public string Calcium { get { return _Calcium; } set { _Calcium = ValidateValue<string>(value, nameof(Calcium)); } }
        /// <sumary>
        /// Vitamina D
        /// </sumary>
        private string _VitaminD;
        [Order]
        [Regex(@"^[0-1]$", "La longitud del campo excede el valor permitido. Validar la variable 106 Vitamina D")] public string VitaminD { get { return _VitaminD; } set { _VitaminD = ValidateValue<string>(value, nameof(VitaminD)); } }
        /// <sumary>
        /// Analgésicos Opioides (Codeina, Tramadol)
        /// </sumary>
        private string _InitialDateCurrentTreatmentDMARD;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "La longitud del campo excede el valor permitido. Validar la variable 100 Analgésicos Opioides (Codeina, Tramadol)")] public string InitialDateCurrentTreatmentDMARD { get { return _InitialDateCurrentTreatmentDMARD; } set { _InitialDateCurrentTreatmentDMARD = ValidateValue<string>(value, nameof(InitialDateCurrentTreatmentDMARD)); } }
        /// <sumary>
        /// Azatioprina
        /// </sumary>
        private string _Azathioprine;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 108 Azatioprina")] public string Azathioprine { get { return _Azathioprine; } set { _Azathioprine = ValidateValue<string>(value, nameof(Azathioprine)); } }
        /// <sumary>
        /// Ciclosporina
        /// </sumary>
        private string _Cyclosporine;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 109 Ciclosporina")] public string Cyclosporine { get { return _Cyclosporine; } set { _Cyclosporine = ValidateValue<string>(value, nameof(Cyclosporine)); } }
        /// <sumary>
        /// Ciclofosfamida
        /// </sumary>
        private string _Cyclophosphamide;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 110 Ciclofosfamida")] public string Cyclophosphamide { get { return _Cyclophosphamide; } set { _Cyclophosphamide = ValidateValue<string>(value, nameof(Cyclophosphamide)); } }
        /// <sumary>
        /// Cloroquina
        /// </sumary>
        private string _Chloroquine;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 111 Cloroquina")] public string Chloroquine { get { return _Chloroquine; } set { _Chloroquine = ValidateValue<string>(value, nameof(Chloroquine)); } }
        /// <sumary>
        /// D-penicilamina
        /// </sumary>
        private string _Dpenicillamine;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 112 D-penicilamina")] public string Dpenicillamine { get { return _Dpenicillamine; } set { _Dpenicillamine = ValidateValue<string>(value, nameof(Dpenicillamine)); } }
        /// <sumary>
        /// Etanercept
        /// </sumary>
        private string _EtanerceptTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 113 Etanercept")] public string EtanerceptTwo { get { return _EtanerceptTwo; } set { _EtanerceptTwo = ValidateValue<string>(value, nameof(EtanerceptTwo)); } }
        /// <sumary>
        /// Leflunomida
        /// </sumary>
        private string _Leflunomide;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 114 Leflunomida")] public string Leflunomide { get { return _Leflunomide; } set { _Leflunomide = ValidateValue<string>(value, nameof(Leflunomide)); } }
        /// <sumary>
        /// Metotrexate
        /// </sumary>
        private string _Methotrexate;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 115 Metotrexate")] public string Methotrexate { get { return _Methotrexate; } set { _Methotrexate = ValidateValue<string>(value, nameof(Methotrexate)); } }
        /// <sumary>
        /// Rituximab
        /// </sumary>
        private string _RituximabTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 116 Rituximab")] public string RituximabTwo { get { return _RituximabTwo; } set { _RituximabTwo = ValidateValue<string>(value, nameof(RituximabTwo)); } }
        /// <sumary>
        /// Sulfasalazina
        /// </sumary>
        private string _Sulfasalazine;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 117 Sulfasalazina")] public string Sulfasalazine { get { return _Sulfasalazine; } set { _Sulfasalazine = ValidateValue<string>(value, nameof(Sulfasalazine)); } }
        /// <sumary>
        /// Abatacept
        /// </sumary>
        private string _AbataceptTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 118 Abatacept")] public string AbataceptTwo { get { return _AbataceptTwo; } set { _AbataceptTwo = ValidateValue<string>(value, nameof(AbataceptTwo)); } }
        /// <sumary>
        /// Adalimumab
        /// </sumary>
        private string _AdalimumabTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 119 Adalimumab")] public string AdalimumabTwo { get { return _AdalimumabTwo; } set { _AdalimumabTwo = ValidateValue<string>(value, nameof(AdalimumabTwo)); } }
        /// <sumary>
        /// Certolizumab
        /// </sumary>
        private string _CertolizumabTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 120 Certolizumab")] public string CertolizumabTwo { get { return _CertolizumabTwo; } set { _CertolizumabTwo = ValidateValue<string>(value, nameof(CertolizumabTwo)); } }
        /// <sumary>
        /// Golimumab
        /// </sumary>
        private string _GolimumabTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 121 Golimumab")] public string GolimumabTwo { get { return _GolimumabTwo; } set { _GolimumabTwo = ValidateValue<string>(value, nameof(GolimumabTwo)); } }
        /// <sumary>
        /// Hidroxicloroquina
        /// </sumary>
        private string _Hydroxychloroquine;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 122 Hidroxicloroquina")] public string Hydroxychloroquine { get { return _Hydroxychloroquine; } set { _Hydroxychloroquine = ValidateValue<string>(value, nameof(Hydroxychloroquine)); } }
        /// <sumary>
        /// Infliximab
        /// </sumary>
        private string _InfliximabTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 123 Infliximab")] public string InfliximabTwo { get { return _InfliximabTwo; } set { _InfliximabTwo = ValidateValue<string>(value, nameof(InfliximabTwo)); } }
        /// <sumary>
        /// Sales de oro 1
        /// </sumary>
        private string _GoldSaltsOne;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 124 Sales de oro")] public string GoldSaltsOne { get { return _GoldSaltsOne; } set { _GoldSaltsOne = ValidateValue<string>(value, nameof(GoldSaltsOne)); } }
        /// <sumary>
        /// Tocilizumab
        /// </sumary>
        private string _TocilizumabTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 125 Tocilizumab")] public string TocilizumabTwo { get { return _TocilizumabTwo; } set { _TocilizumabTwo = ValidateValue<string>(value, nameof(TocilizumabTwo)); } }
        /// <sumary>
        /// Tofacitinib
        /// </sumary>
        private string _TofacitinibTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 126 Tofacitinib")] public string TofacitinibTwo { get { return _TofacitinibTwo; } set { _TofacitinibTwo = ValidateValue<string>(value, nameof(TofacitinibTwo)); } }
        /// <sumary>
        /// Anakinra
        /// </sumary>
        private string _AnakinraTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 127 Anakinra")] public string AnakinraTwo { get { return _AnakinraTwo; } set { _AnakinraTwo = ValidateValue<string>(value, nameof(AnakinraTwo)); } }
        /// <sumary>
        /// Otro medicamento NO POS 1
        /// </sumary>
        private string _OtherMedicineNotPosOne;
        [Order]
        [Regex(@"^.{1,20}$", "La longitud del campo excede el valor permitido. Validar la variable 128 Otro medicamento NO POS 1")] public string OtherMedicineNotPosOne { get { return _OtherMedicineNotPosOne; } set { _OtherMedicineNotPosOne = ValidateValue<string>(value, nameof(OtherMedicineNotPosOne)); } }
        /// <sumary>
        /// Otro medicamento NO POS 2
        /// </sumary>
        private string _OtherMedicineNotPosTwo;
        [Order]
        [Regex(@"^.{1,20}$", "La longitud del campo excede el valor permitido. Validar la variable 129 Otro medicamento NO POS 2")] public string OtherMedicineNotPosTwo { get { return _OtherMedicineNotPosTwo; } set { _OtherMedicineNotPosTwo = ValidateValue<string>(value, nameof(OtherMedicineNotPosTwo)); } }
        /// <sumary>
        /// Otro medicamento NO POS 3
        /// </sumary>
        private string _OtherMedicineNotPosThree;
        [Order]
        [Regex(@"^.{1,20}$", "La longitud del campo excede el valor permitido. Validar la variable 130 Otro medicamento NO POS 3")] public string OtherMedicineNotPosThree { get { return _OtherMedicineNotPosThree; } set { _OtherMedicineNotPosThree = ValidateValue<string>(value, nameof(OtherMedicineNotPosThree)); } }
        /// <sumary>
        /// Otro medicamento NO POS 4
        /// </sumary>
        private string _OtherMedicineNotPosFour;
        [Order]
        [Regex(@"^.{1,20}$", "La longitud del campo excede el valor permitido. Validar la variable 131 Otro medicamento NO POS 4")] public string OtherMedicineNotPosFour { get { return _OtherMedicineNotPosFour; } set { _OtherMedicineNotPosFour = ValidateValue<string>(value, nameof(OtherMedicineNotPosFour)); } }
        /// <sumary>
        /// Número de consultas con reumatólogo en el último año
        /// </sumary>
        private string _RheumatologistConsultNumberLastYear;
        [Order]
        [Regex(@"^[0-9]{1,2}$", "La longitud del campo excede el valor permitido. Validar la variable 132 Número de consultas con reumatólogo en el último año")] public string RheumatologistConsultNumberLastYear { get { return _RheumatologistConsultNumberLastYear; } set { _RheumatologistConsultNumberLastYear = ValidateValue<string>(value, nameof(RheumatologistConsultNumberLastYear)); } }
        /// <sumary>
        /// Número de consultas con Internista por AR en el último año
        /// </sumary>
        private string _InternistConsultNumberLastYear;
        [Order]
        [Regex(@"^[0-9]{1,2}$", "La longitud del campo excede el valor permitido. Validar la variable 133 Número de consultas con Internista por AR en el último año")] public string InternistConsultNumberLastYear { get { return _InternistConsultNumberLastYear; } set { _InternistConsultNumberLastYear = ValidateValue<string>(value, nameof(InternistConsultNumberLastYear)); } }
        /// <sumary>
        /// Número de consultas con Médico Familiar por AR en el último año
        /// </sumary>
        private string _FamilyDoctorConsultNumberLastYear;
        [Order]
        [Regex(@"^[0-9]{1,2}$", "La longitud del campo excede el valor permitido. Validar la variable 134 Número de consultas con Médico Familiar por AR en el último año")] public string FamilyDoctorConsultNumberLastYear { get { return _FamilyDoctorConsultNumberLastYear; } set { _FamilyDoctorConsultNumberLastYear = ValidateValue<string>(value, nameof(FamilyDoctorConsultNumberLastYear)); } }
        /// <sumary>
        /// Reemplazo articular 1 por AR
        /// </sumary>
        private string _JointReplacementOne;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 135 Reemplazo articular 1 por AR")] public string JointReplacementOne { get { return _JointReplacementOne; } set { _JointReplacementOne = ValidateValue<string>(value, nameof(JointReplacementOne)); } }
        /// <sumary>
        /// Reemplazo articular 2 por AR
        /// </sumary>
        private string _JointReplacementTwo;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 136 Reemplazo articular 2 por AR")] public string JointReplacementTwo { get { return _JointReplacementTwo; } set { _JointReplacementTwo = ValidateValue<string>(value, nameof(JointReplacementTwo)); } }
        /// <sumary>
        /// Reemplazo articular 3 por AR
        /// </sumary>
        private string _JointReplacementThree;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 137 Reemplazo articular 3 por AR")] public string JointReplacementThree { get { return _JointReplacementThree; } set { _JointReplacementThree = ValidateValue<string>(value, nameof(JointReplacementThree)); } }
        /// <sumary>
        /// Reemplazo articular 4 por AR
        /// </sumary>
        private string _JointReplacementFour;
        [Order]
        [Regex(@"^[0-5]$", "La longitud del campo excede el valor permitido. Validar la variable 138 Reemplazo articular 4 por AR")] public string JointReplacementFour { get { return _JointReplacementFour; } set { _JointReplacementFour = ValidateValue<string>(value, nameof(JointReplacementFour)); } }
        /// <sumary>
        /// No de hospitalizaciones por AR en último año
        /// </sumary>
        private string _NumberHospitalizationsLastYear;
        [Order]
        [Regex(@"^[0-9]{1,2}$", "La longitud del campo excede el valor permitido. Validar la variable 139 No de hospitalizaciones por AR en último año")] public string NumberHospitalizationsLastYear { get { return _NumberHospitalizationsLastYear; } set { _NumberHospitalizationsLastYear = ValidateValue<string>(value, nameof(NumberHospitalizationsLastYear)); } }
        /// <sumary>
        /// Codigo habilitación de la IPS
        /// </sumary>
        private string _HabilitationCode;
        [Order]
        [Regex(@"^.{1,12}$", "Validar longitud de codigo. Validar la variable 140 Codigo habilitación de la IPS")] public string HabilitationCode { get { return _HabilitationCode; } set { _HabilitationCode = ValidateValue<string>(value, nameof(HabilitationCode)); } }
        /// <sumary>
        /// Codigo municipio de la IPS
        /// </sumary>
        private string _MunicipalyIPS;
        [Order]
        [Regex(@"^.{1,5}$", "Validar longitud de codigo. Validar la variable 141 Codigo municipio de la IPS")] public string MunicipalyIPS { get { return _MunicipalyIPS; } set { _MunicipalyIPS = ValidateValue<string>(value, nameof(MunicipalyIPS)); } }
        /// <sumary>
        /// Fecha de ingreso a la IPS actual donde se hace el seguimiento y atención de la AR al paciente
        /// </sumary>
        private string _InitialDateCurrentIPSAR;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 142 Fecha de ingreso a la IPS actual donde se hace el seguimiento y atención de la AR al paciente")] public string InitialDateCurrentIPSAR { get { return _InitialDateCurrentIPSAR; } set { _InitialDateCurrentIPSAR = ValidateValue<string>(value, nameof(InitialDateCurrentIPSAR)); } }
        /// <sumary>
        /// Quién hace la atención clínica para AR al paciente actualmente
        /// </sumary>
        private string _CurrentDoctorAR;
        [Order]
        [Regex(@"^[1-7]$", "La longitud del campo excede el valor permitido. Validar la variable 143 Quién hace la atención clínica para AR al paciente actualmente")] public string CurrentDoctorAR { get { return _CurrentDoctorAR; } set { _CurrentDoctorAR = ValidateValue<string>(value, nameof(CurrentDoctorAR)); } }
        /// <sumary>
        /// Novedad del paciente respecto al reporte anterior
        /// </sumary>
        private string _NoveltyPatient;
        [Order]
        [Regex(@"^[1-9]{1,2}$", "La longitud del campo excede el valor permitido. Validar la variable 144 Novedad del paciente respecto al reporte anterior")] public string NoveltyPatient { get { return _NoveltyPatient; } set { _NoveltyPatient = ValidateValue<string>(value, nameof(NoveltyPatient)); } }
        /// <sumary>
        /// Fecha de desafiliación de la EAPB
        /// </sumary>
        private string _DisenrollmentDate;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Validar longitud de fecha. Validar la variable 145 Fecha de desafiliación de la EAPB |  Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 145 Fecha de desafiliación de la EAPB")] public string DisenrollmentDate { get { return _DisenrollmentDate; } set { _DisenrollmentDate = ValidateValue<string>(value, nameof(DisenrollmentDate)); } }
        /// <sumary>
        /// EAPB
        /// </sumary>
        private string _EAPB;
        [Order]
        public string EAPB { get { return _EAPB; } set { _EAPB = ValidateValue<string>(value, nameof(EAPB)); } }
        /// <sumary>
        /// fecha de muerte
        /// </sumary>
        private string _DeathDate;
        [Order]
        [Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", "Validar longitud de fecha. Validar la variable 147 Fecha de muerte |  Debe ingresar una fecha valida en formato AAAA-MM-DD. Validar la variable 147 Fecha de muerte")] public string DeathDate { get { return _DeathDate; } set { _DeathDate = ValidateValue<string>(value, nameof(DeathDate)); } }
        /// <sumary>
        /// Causa de muerte
        /// </sumary>
        private string _DeathCause;
        [Order]
        [Regex(@"^[0-3]$", "La longitud del campo excede el valor permitido. Validar la variable 148 Causa de muerte")] public string DeathCause { get { return _DeathCause; } set { _DeathCause = ValidateValue<string>(value, nameof(DeathCause)); } }
        /// <sumary>
        /// Costo anual de DMARD POS
        /// </sumary>
        private string _AnnualCostDMARDPOS;
        [Order]
        [Regex(@"^[0-9]{1,11}$", "La longitud del costo no es correcto. Validar la variable 149 Costo anual de DMARD POS")] public string AnnualCostDMARDPOS { get { return _AnnualCostDMARDPOS; } set { _AnnualCostDMARDPOS = ValidateValue<string>(value, nameof(AnnualCostDMARDPOS)); } }
        /// <sumary>
        /// Costo anual de DMARD NO POS
        /// </sumary>
        private string _AnnualCostDMARDNOPOS;
        [Order]
        [Regex(@"^[0-9]{1,11}$", "La longitud del costo no es correcto. Validar la variable 150 Costo anual de DMARD NO POS")] public string AnnualCostDMARDNOPOS { get { return _AnnualCostDMARDNOPOS; } set { _AnnualCostDMARDNOPOS = ValidateValue<string>(value, nameof(AnnualCostDMARDNOPOS)); } }
        /// <sumary>
        /// Costo anual de DMARD NO POS
        /// </sumary>
        private string _TotalAnnualCostAR;
        [Order]
        [Regex(@"^[0-9]{1,11}$", "La longitud del costo no es correcto. Validar la variable 150 Costo anual de DMARD NO POS")] public string TotalAnnualCostAR { get { return _TotalAnnualCostAR; } set { _TotalAnnualCostAR = ValidateValue<string>(value, nameof(TotalAnnualCostAR)); } }
        /// <sumary>
        /// Costo anual de Incapacidades laborales relacionadas AR
        /// </sumary>
        private string _TotalAnnualCostInabilities;
        [Order]
        [Regex(@"^[0-9]{1,11}$", "La longitud del costo no es correcto. Validar la variable 152 Costo anual de Incapacidades laborales relacionadas AR")] public string TotalAnnualCostInabilities { get { return _TotalAnnualCostInabilities; } set { _TotalAnnualCostInabilities = ValidateValue<string>(value, nameof(TotalAnnualCostInabilities)); } }
        #endregion

        #region Builders
        public ENT_Resolution1393() : base(null) { ExtrictValidation = false; }
        public ENT_Resolution1393(object obj) : base(obj) { ExtrictValidation = false; }
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