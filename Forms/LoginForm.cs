using SistemaGestorProductos.Services;
using System;
using System.Windows.Forms;

namespace SistemaGestorProductos.Forms
{
    /// <summary>
    /// Clase que representa el formulario de inicio de sesión.
    /// </summary>
    public partial class LoginForm : Form
    {
        private AuthService _authService;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="LoginForm"/>.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthService();
            this.AcceptButton = btnLogin; // Establece el botón de inicio de sesión como el botón de aceptación.
        }

        /// <summary>
        /// Maneja el evento de clic del botón de inicio de sesión.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var usuario = _authService.IniciarSesion(txtUsuario.Text, txtContraseña.Text);
            if (usuario != null)
            {
                MessageBox.Show("inicio de sesion correcto");
            }
            else
            {
                MessageBox.Show("Credenciales inválidas");
            }
        }

        /// <summary>
        /// Maneja el evento de clic del enlace de registro.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void linkRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistroForm registro = new RegistroForm();
            registro.ShowDialog(); // Muestra el formulario de registro.
        }


    }
}