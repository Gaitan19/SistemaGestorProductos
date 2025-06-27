using SistemaGestorProductos.Models;
using SistemaGestorProductos.Services;
using SistemaGestorProductos.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaGestorProductos.Forms
{
    /// <summary>
    /// Formulario para editar o crear una opción de producto.
    /// </summary>
    public partial class EditarOpcionForm : Form
    {
        private int? _opcionId; // Identificador de la opción a editar
        private int _productoId; // Identificador del producto asociado
        private Opcion _opcion; // Objeto que representa la opción
        private OpcionService _opcionService; // Servicio para manejar opciones

        /// <summary>
        /// Constructor del formulario.
        /// </summary>
        /// <param name="opcionId">Identificador de la opción (opcional).</param>
        /// <param name="productoId">Identificador del producto.</param>
        public EditarOpcionForm(int? opcionId = null, int productoId = 0)
        {
            InitializeComponent();
            _opcionId = opcionId;
            _productoId = productoId;
            _opcionService = new OpcionService();
            CargarDatos();
        }

        /// <summary>
        /// Maneja el evento de clic en el botón Guardar.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                _opcion.Nombre = txtNombre.Text.Trim();
                _opcion.Estado = chkEstado.Checked;

                if (_opcionId.HasValue)
                {
                    _opcionService.ActualizarOpcion(_opcion);
                }
                else
                {
                    _opcionService.CrearOpcion(_opcion);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ValidationException vex)
            {
                MostrarError(vex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Muestra un mensaje de error de validación.
        /// </summary>
        /// <param name="vex">Excepción de validación.</param>
        private void MostrarError(ValidationException vex)
        {
            MessageBox.Show($"{vex.Message}\n\nEjemplo: {vex.Example}",
                           $"Error en {vex.FieldName}",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);

            txtNombre.Focus();
            txtNombre.SelectAll();
        }

        /// <summary>
        /// Carga los datos de la opción en el formulario.
        /// </summary>
        private void CargarDatos()
        {
            if (_opcionId.HasValue)
            {
                _opcion = _opcionService.ObtenerOpcionPorId(_opcionId.Value);
                Text = "Editar Opción";
            }
            else
            {
                _opcion = new Opcion
                {
                    ProductoId = _productoId,
                    Estado = true
                };
                Text = "Nueva Opción";
            }

            txtNombre.Text = _opcion.Nombre;
            chkEstado.Checked = _opcion.Estado;
        }

        /// <summary>
        /// Maneja el evento de clic en el botón Cancelar.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Maneja el evento de cambio de texto en el campo Nombre.
        /// </summary>
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            // Lógica adicional puede ser implementada aquí si es necesario
        }
    }
}
