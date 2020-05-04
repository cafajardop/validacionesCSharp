
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpheliaSuiteV2.BRMRuntime
{
    /// <sumary>
    /// VC_Retorno_Expression
    /// </sumary> 
    public sealed class VC_Retorno_Expression
    {
        #region Fields
        /// <sumary>
        /// FileName
        /// </sumary>
        private string FileName;
        /// <sumary>
        /// parámetro de entrada de plantilla
        /// </sumary>
        private string param1;
        /// <sumary>
        /// param2
        /// </sumary>
        private string param2;
        /// <sumary>
        /// Result
        /// </sumary>
        private string Result;
        /// <sumary>
        /// VC_Retorno
        /// </sumary>
        private bool VC_Retorno;
        #endregion

        #region Members
        #endregion

        #region Builder
        /// <sumary>
        /// Inicializa una instancia de la clase
        /// </sumary>
        public VC_Retorno_Expression() { }
        #endregion

        #region Public Methods
        /// <sumary>
        /// VC_Retorno_Expression
        /// </sumary>
        /// <param name="param1">parámetro de entrada de plantilla</param>
        /// <param name="param2">param2</param>
        public RuntimeResult<bool> Execute(string param1, string param2)
        {
            try
            {
                // Resolución de prerequisitos
                #region Fields
                this.param1 = param1;
                this.param2 = param2;
                this.Result = FUNC_Result();
                this.VC_Retorno = FUNC_VC_Retorno();
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
        private string FUNC_Result()
        {
            return "";
        }
        /// <sumary>
        ///	
        /// </sumary>
        private bool FUNC_VC_Retorno()
        {
            return Helper.USR_V25Prueba(param1, param2);

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
    /// Clase para definición de funciones
    /// </sumary>
    public static class Helper
    {
        /// <sumary>
        /// Plantilla de Función vacía
        /// </sumary> 
        /// <param name="param1">Variable de parámetro de función vacía</param>
        /// <param name="param2">param2</param>
        public static bool USR_V25Prueba(string param1, string param2)
        {

            string[] regimeType = new string[] { "C", "S" };
            int[] valorType = new int[] { 96, 98, 99 };
            int numerador = 55;

            if (param1.Length == 12)
            {
                return true;//{ throw new ArgumentException($"El campo codigo habilitacion"); }
            }
            else
            {
                if (regimeType.Contains(param2) && Convert.ToInt32(param1) != numerador)
                    return false; //{ throw new ArgumentException($"El campo param1 es invalido ya que envian numerador 55"); }
                else
                {
                    if (!(valorType.Contains(Convert.ToInt32(param1))))
                    {
                        return false; //{ throw new ArgumentException($"El campo param1 es invalido no existe en la lista"); }
                    }
                }
            }
            return true;
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

}