using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestorProductos.Models
{
    public class Opcion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
