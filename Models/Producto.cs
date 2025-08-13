using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestorProductos.Models
{
    [Table("Productos")]

    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Existencia { get; set; }
        public bool Estado { get; set; }
        public string NombreProveedor { get; set; }
    }
}
