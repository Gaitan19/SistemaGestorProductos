using System.Security.Cryptography;
using System.Text;

namespace SistemaGestorProductos.Utils
{
    public static class Encriptacion
    {
        /// <summary>
        /// Encripta una cadena de texto utilizando el algoritmo SHA256.
        /// </summary>
        /// <param name="input">La cadena de texto a encriptar.</param>
        /// <returns>La cadena encriptada en formato hexadecimal.</returns>
        public static string Encriptar(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}