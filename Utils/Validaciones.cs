using System.Text.RegularExpressions;

namespace SistemaGestorProductos.Utils
{
    public static class Validaciones
    {
        /// <summary>
        /// Valida el formato de un correo electrónico.
        /// </summary>
        /// <param name="correo">El correo electrónico a validar.</param>
        /// <exception cref="ValidationException">Se lanza si el correo es nulo, vacío o no tiene un formato válido.</exception>
        public static void ValidarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new ValidationException("El correo electrónico es obligatorio", "Correo", "ejemplo@gmail.com");

            string patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(correo, patron))
                throw new ValidationException("Formato de correo inválido", "Correo", "ejemplo@gmail.com");
        }

        /// <summary>
        /// Valida el formato de un número de teléfono.
        /// </summary>
        /// <param name="telefono">El número de teléfono a validar.</param>
        /// <exception cref="ValidationException">Se lanza si el teléfono es nulo, vacío o no tiene un formato válido.</exception>
        public static void ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ValidationException("El teléfono es obligatorio", "Teléfono", "0000-0000");

            string patron = @"^\d{4}-\d{4}$";

            if (!Regex.IsMatch(telefono, patron))
                throw new ValidationException("Formato de teléfono inválido", "Teléfono", "0000-0000");
        }
    }
}