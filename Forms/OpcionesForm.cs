using SistemaGestorProductos.Services;
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
    /// Formulario para gestionar las opciones de un producto.
    /// </summary>
    public partial class OpcionesForm : Form
    {
        private int _productoId; // Identificador del producto
        private string _nombreProducto; // Nombre del producto
        private OpcionService _opcionService; // Servicio para gestionar opciones

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OpcionesForm"/>.
        /// </summary>
        /// <param name="productoId">Identificador del producto.</param>
        /// <param name="nombreProducto">Nombre del producto.</param>
        public OpcionesForm(int productoId, string nombreProducto)
        {
            InitializeComponent();
            _productoId = productoId;
            _nombreProducto = nombreProducto;
            _opcionService = new OpcionService();

            lblTitulo.Text = $"Opciones del producto: {_nombreProducto}";

            CargarOpciones();
        }

        /// <summary>
        /// Carga las opciones disponibles para el producto seleccionado.
        /// </summary>
        private void CargarOpciones()
        {
            var opciones = _opcionService.ObtenerOpcionesPorProducto(_productoId);

            if (opciones.Count == 0)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "No hay opciones registradas para este producto";
                dgvOpciones.Visible = false;
                return;
            }

            lblMensaje.Visible = false;
            dgvOpciones.Visible = true;

            var opcionesConEstado = opciones.Select(o => new
            {
                o.Id,
                o.Nombre,
                Estado = o.Estado ? "Activo" : "Inactivo"
            }).ToList();

            dgvOpciones.DataSource = opcionesConEstado;
        }

        /// <summary>
        /// Maneja el evento de clic en el botón Agregar.
        /// Abre el formulario para agregar una nueva opción.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var form = new EditarOpcionForm(productoId: _productoId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarOpciones();
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón Editar.
        /// Abre el formulario para editar la opción seleccionada.
        /// </summary>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvOpciones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una opción para editar",
                               "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int opcionId = (int)dgvOpciones.SelectedRows[0].Cells["Id"].Value;
            var form = new EditarOpcionForm(opcionId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarOpciones();
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón Cerrar.
        /// Cierra el formulario actual.
        /// </summary>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
