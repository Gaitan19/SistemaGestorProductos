using SistemaGestorProductos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestorProductos.Data
{
    /// <summary>
    /// Contexto de la base de datos para la gestión de productos.
    /// Esta clase hereda de DbContext y se utiliza para interactuar con la base de datos.
    /// </summary>
    public class ProductosDbContext : DbContext
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ProductosDbContext"/>.
        /// </summary>
        public ProductosDbContext() : base("name=ProductosDBConnection") { }

        /// <summary>
        /// Obtiene o establece el conjunto de entidades de tipo <see cref="Producto"/>.
        /// </summary>
        public DbSet<Producto> Productos { get; set; }

        /// <summary>
        /// Obtiene o establece el conjunto de entidades de tipo <see cref="Usuario"/>.
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Obtiene o establece el conjunto de entidades de tipo <see cref="Opcion"/>.
        /// </summary>
        public DbSet<Opcion> Opciones { get; set; }
    }
}
