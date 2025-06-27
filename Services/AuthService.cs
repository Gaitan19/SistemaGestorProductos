using SistemaGestorProductos.Data;
using SistemaGestorProductos.Models;
using SistemaGestorProductos.Utils;
using System;
using System.Linq;

namespace SistemaGestorProductos.Services
{
    /// <summary>
    /// Servicio de autenticación para gestionar el registro e inicio de sesión de usuarios.
    /// </summary>
    public class AuthService
    {
        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="usuario">El objeto Usuario que contiene la información del nuevo usuario.</param>
        /// <exception cref="ValidationException">Lanza una excepción si hay errores de validación.</exception>
        public void RegistrarUsuario(Usuario usuario)
        {
            try
            {
                Validaciones.ValidarCorreo(usuario.Correo);
                Validaciones.ValidarTelefono(usuario.Telefono);

                if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                    throw new ValidationException("El nombre de usuario es obligatorio", "NombreUsuario", "minombre");

                if (string.IsNullOrWhiteSpace(usuario.Contraseña))
                    throw new ValidationException("La contraseña es obligatoria", "Contraseña", "");

                if(string.IsNullOrWhiteSpace(usuario.Nombre))
                    throw new ValidationException("El nombre es obligatorio", "Nombre", "Jonh");

                if (string.IsNullOrWhiteSpace(usuario.Apellido))
                    throw new ValidationException("El apellido es obligatorio", "Apellido", "Doe");


                if (usuario.Contraseña.Length < 6)
                    throw new ValidationException("La contraseña debe tener al menos 6 caracteres", "Contraseña", "");

                using (var context = new ProductosDbContext())
                {
                    if (context.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
                        throw new ValidationException("El nombre de usuario ya está en uso", "NombreUsuario", "otro_nombre");

                    if (context.Usuarios.Any(u => u.Correo == usuario.Correo))
                        throw new ValidationException("El correo electrónico ya está registrado", "Correo", "otro@correo.com");
                   
                    if (context.Usuarios.Any(u => u.Telefono == usuario.Telefono))
                        throw new ValidationException("El número de teléfono ya está registrado", "Teléfono", "0000-0000");
                }

                usuario.Contraseña = Encriptacion.Encriptar(usuario.Contraseña);

                using (var context = new ProductosDbContext())
                {
                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                }
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el usuario: " + ex.Message);
            }
        }

        /// <summary>
        /// Inicia sesión de un usuario en el sistema.
        /// </summary>
        /// <param name="nombreUsuario">El nombre de usuario del usuario que intenta iniciar sesión.</param>
        /// <param name="contraseña">La contraseña del usuario que intenta iniciar sesión.</param>
        /// <returns>El objeto Usuario si las credenciales son válidas; de lo contrario, null.</returns>
        public Usuario IniciarSesion(string nombreUsuario, string contraseña)
        {
            using (var context = new ProductosDbContext())
            {
                string contraseñaEncriptada = Encriptacion.Encriptar(contraseña);
                return context.Usuarios
                    .FirstOrDefault(u => u.NombreUsuario == nombreUsuario &&
                                         u.Contraseña == contraseñaEncriptada);
            }
        }
    }
}