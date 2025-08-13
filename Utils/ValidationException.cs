using System;

namespace SistemaGestorProductos.Utils
{
    /// <summary>
    /// Excepción que se lanza cuando hay un error de validación.
    /// </summary>
    /// <remarks>
    /// Esta clase hereda de <see cref="Exception"/> y proporciona información adicional
    /// sobre el campo que falló la validación y un ejemplo de valor válido.
    /// </remarks>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Obtiene el nombre del campo que falló la validación.
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Obtiene un ejemplo de un valor válido para el campo.
        /// </summary>
        public string Example { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ValidationException"/>.
        /// </summary>
        /// <param name="message">El mensaje de error que describe la excepción.</param>
        /// <param name="fieldName">El nombre del campo que falló la validación.</param>
        /// <param name="example">Un ejemplo de un valor válido para el campo.</param>
        public ValidationException(string message, string fieldName, string example)
            : base(message)
        {
            FieldName = fieldName;
            Example = example;
        }
    }
}