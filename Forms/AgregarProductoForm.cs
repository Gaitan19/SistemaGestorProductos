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
    /// Formulario para agregar un nuevo producto al sistema.
    /// </summary>
    public partial class AgregarProductoForm : Form
    {
        private ProductoService _productoService;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AgregarProductoForm"/>.
        /// </summary>
        public AgregarProductoForm()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            numExistencia.Value = 1; 
        }

        /// <summary>
        /// Maneja el evento de clic del botón Guardar.
        /// Crea un nuevo producto y lo guarda en el sistema.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var producto = new Producto
                {
                    Codigo = txtCodigo.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Existencia = (int)numExistencia.Value,
                    Estado = true,
                    NombreProveedor = txtProveedor.Text.Trim()
                };

                _productoService.CrearProducto(producto);

                MessageBox.Show("Producto creado exitosamente", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ValidationException vex)
            {
                MostrarError(vex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Muestra un mensaje de error basado en la excepción de validación.
        /// </summary>
        /// <param name="vex">La excepción de validación que contiene información sobre el error.</param>
        private void MostrarError(ValidationException vex)
        {
            string mensaje = $"{vex.Message}\n\nEjemplo: {vex.Example}";

            switch (vex.FieldName)
            {
                case "Código":
                    MessageBox.Show(mensaje, "Error en Código",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigo.Focus();
                    txtCodigo.SelectAll();
                    break;

                case "Nombre":
                    MessageBox.Show(mensaje, "Error en Nombre",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombre.Focus();
                    txtNombre.SelectAll();
                    break;

                case "Existencia":
                    MessageBox.Show(mensaje, "Error en Existencia",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    numExistencia.Focus();
                    numExistencia.Select(0, numExistencia.Text.Length);
                    break;

                case "Proveedor":
                    MessageBox.Show(mensaje, "Error en Proveedor",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProveedor.Focus();
                    txtProveedor.SelectAll();
                    break;

                default:
                    MessageBox.Show(vex.Message, "Error de Validación",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        /// <summary>
        /// Maneja el evento de clic del botón Cancelar.
        /// Cierra el formulario sin guardar cambios.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
