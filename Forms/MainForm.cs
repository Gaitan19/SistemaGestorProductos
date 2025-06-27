using SistemaGestorProductos.Models;
using SistemaGestorProductos.Services;
using SistemaGestorProductos.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaGestorProductos.Forms
{
    public partial class MainForm : Form
    {
        private ProductoService _productoService;

        public MainForm()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            ConfigurarDataGridView();
            CargarProductos();
            dgvProductos.CellClick += dgvProductos_CellClick;
            cmbEstado.SelectedIndex = 0;
        }

        private void ConfigurarDataGridView()
        {
            // Agregar columna de botones para opciones
            DataGridViewButtonColumn btnOpciones = new DataGridViewButtonColumn();
            btnOpciones.Name = "Opciones";
            btnOpciones.HeaderText = "Opciones";
            btnOpciones.Text = "Ver Opciones";
            btnOpciones.UseColumnTextForButtonValue = true;
            dgvProductos.Columns.Add(btnOpciones);
        }

        private void CargarProductos()
        {
            var productos = _productoService.ObtenerProductos();

            var productosConEstadoTexto = productos.Select(p => new
            {
                p.Id,
                p.Codigo,
                p.Nombre,
                p.NombreProveedor,
                p.Existencia,
                Estado = p.Estado ? "Activo" : "Inactivo"
            }).ToList();

            dgvProductos.DataSource = productosConEstadoTexto;
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var form = new AgregarProductoForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarProductos(); // Recargar lista después de agregar
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            bool? estado = null;
            if (cmbEstado.SelectedIndex == 1) estado = true;
            if (cmbEstado.SelectedIndex == 2) estado = false;

            List<Producto> productos = _productoService.ObtenerProductos(
                txtBuscar.Text,
                estado
            );

            var productosConEstadoTexto = productos.Select(p => new
            {
                p.Id,
                p.Codigo,
                p.Nombre,
                p.NombreProveedor,
                p.Existencia,
                Estado = p.Estado ? "Activo" : "Inactivo"
            }).ToList();

            dgvProductos.DataSource = productosConEstadoTexto;

            
        }

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

                    cmbEstado.SelectedIndex = 0; 
                    txtBuscar.Text = "";
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

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignorar clics en encabezados
            if (e.RowIndex < 0) return;

            // Si es la columna de opciones
            if (e.ColumnIndex == dgvProductos.Columns["Opciones"].Index)
            {
                int productoId = (int)dgvProductos.Rows[e.RowIndex].Cells["Id"].Value;
                string nombreProducto = dgvProductos.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

                var opcionesForm = new OpcionesForm(productoId, nombreProducto);
                opcionesForm.ShowDialog();

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sistemaGestorProductosDBDataSet.Productos' table. You can move, or remove it, as needed.
            this.productosTableAdapter.Fill(this.sistemaGestorProductosDBDataSet.Productos);
        }
    }
}