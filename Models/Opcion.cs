using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestorProductos.Models
{
    [Table("Opciones")]
    public class Opcion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
