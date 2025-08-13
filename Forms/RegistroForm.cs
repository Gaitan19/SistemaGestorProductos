using SistemaGestorProductos.Models;
using SistemaGestorProductos.Services;
using SistemaGestorProductos.Utils;
using System;
using System.Windows.Forms;

namespace SistemaGestorProductos.Forms
{
    /// <summary>
    /// Clase que representa el formulario de registro de usuarios.
    /// </summary>
    public partial class RegistroForm : Form
    {
        private AuthService _authService;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RegistroForm"/>.
        /// </summary>
        public RegistroForm()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        /// <summary>
        /// Maneja el evento de clic del botón de registro.
        /// Valida los datos ingresados y registra un nuevo usuario.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = new Usuario
                {
                    NombreUsuario = txtUsuario.Text,
                    Contraseña = txtContraseña.Text,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Correo = txtCorreo.Text == "ejemplo@gmail.com" ? "" : txtCorreo.Text,
                    Telefono = txtTelefono.Text == "0000-0000" ? "" : txtTelefono.Text,
                    FechaCreacion = DateTime.Now
                };

                _authService.RegistrarUsuario(usuario);

                MessageBox.Show("Registro exitoso");
                this.Close();
            }
            catch (ValidationException vex)
            {
                string mensaje = $"{vex.Message}\n\nEjemplo: {vex.Example}";

                switch (vex.FieldName)
                {
                    case "Correo":
                        MessageBox.Show(mensaje, "Error en Correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCorreo.Focus();
                        txtCorreo.SelectAll();
                        break;

                    case "Teléfono":
                        MessageBox.Show(mensaje, "Error en Teléfono", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTelefono.Focus();
                        txtTelefono.SelectAll();
                        break;

                    case "NombreUsuario":
                        MessageBox.Show(mensaje, "Error en Nombre de Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsuario.Focus();
                        txtUsuario.SelectAll();
                        break;

                    case "Contraseña":
                        MessageBox.Show(mensaje, "Error en Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtContraseña.Focus();
                        txtContraseña.SelectAll();
                        break;

                    case "Nombre":
                        MessageBox.Show(mensaje, "Error en Nombre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNombre.Focus();
                        txtNombre.SelectAll();
                        break;

                    case "Apellido":
                        MessageBox.Show(mensaje, "Error en Apellido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtApellido.Focus();
                        txtApellido.SelectAll();
                        break;

                    default:
                        MessageBox.Show(vex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}