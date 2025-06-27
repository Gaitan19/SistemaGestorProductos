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
    /// Clase principal del formulario que gestiona los productos.
    /// </summary>
    public partial class MainForm : Form
    {
        private ProductoService _productoService;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MainForm"/>.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            CargarProductos();
        }

        /// <summary>
        /// Carga la lista de productos en el DataGridView.
        /// </summary>
        private void CargarProductos()
        {
            var productos = _productoService.ObtenerProductos();

            dgvProductos.DataSource = productos.Select(p => new
            {
                p.Id,
                p.Codigo,
                p.Nombre,
                p.NombreProveedor,
                p.Existencia,
                Estado = p.Estado ? "Activo" : "Inactivo"
            }).ToList();
        }

        /// <summary>
        /// Maneja el evento de clic del botón para agregar un nuevo producto.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var form = new AgregarProductoForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarProductos();
            }
        }

        /// <summary>
        /// Maneja el evento de clic del botón para filtrar productos.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            bool? estado = null;
            if (cmbEstado.SelectedIndex == 1) estado = true;
            if (cmbEstado.SelectedIndex == 2) estado = false;

            List<Producto> productos = _productoService.ObtenerProductos(
                txtBuscar.Text,
                estado
            );
            dgvProductos.DataSource = productos.Select(p => new
            {
                p.Id,
                p.Codigo,
                p.Nombre,
                p.NombreProveedor,
                p.Existencia,
                Estado = p.Estado ? "Activo" : "Inactivo"
            }).ToList();
        }

        /// <summary>
        /// Maneja el evento de carga del formulario.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: Esta línea de código carga datos en la tabla 'sistemaGestorProductosDBDataSet.Productos'. Puede moverla o eliminarla, según sea necesario.
            this.productosTableAdapter.Fill(this.sistemaGestorProductosDBDataSet.Productos);
        }

        /// <summary>
        /// Maneja el evento de clic del botón para eliminar un producto.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para desactivar",
                               "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvProductos.SelectedRows[0];
            int productoId = (int)selectedRow.Cells["Id"].Value;
            string nombreProducto = selectedRow.Cells["Nombre"].Value.ToString();

            var confirm = MessageBox.Show($"¿Está seguro de desactivar el producto '{nombreProducto}'?",
                                        "Confirmar desactivación",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _productoService.DesactivarProducto(productoId);
                    MessageBox.Show("Producto desactivado exitosamente",
                                   "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProductos();
                }
                catch (ValidationException vex)
                {
                    MessageBox.Show(vex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
