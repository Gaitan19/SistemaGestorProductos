using SistemaGestorProductos.Data;
using SistemaGestorProductos.Models;
using SistemaGestorProductos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SistemaGestorProductos.Services
{
    public class ProductoService
    {
        /// <summary>
        /// Obtiene una lista de productos filtrados por nombre o código y estado.
        /// </summary>
        /// <param name="filtro">Cadena para filtrar productos por nombre o código.</param>
        /// <param name="estado">Estado del producto (activo/inactivo).</param>
        /// <returns>Lista de productos que cumplen con los criterios de filtrado.</returns>
        public List<Producto> ObtenerProductos(string filtro = null, bool? estado = null)
        {
            using (var context = new ProductosDbContext())
            {
                IQueryable<Producto> query = context.Productos;

                if (!string.IsNullOrEmpty(filtro))
                {
                    query = query.Where(p => p.Nombre.Contains(filtro) ||
                                            p.Codigo.Contains(filtro));
                }

                if (estado.HasValue)
                {
                    query = query.Where(p => p.Estado == estado.Value);
                }

                return query.ToList();
            }
        }

        /// <summary>
        /// Desactiva un producto dado su ID.
        /// </summary>
        /// <param name="productoId">ID del producto a desactivar.</param>
        /// <exception cref="ValidationException">Lanzada si el producto no se encuentra.</exception>
        /// <exception cref="Exception">Lanzada si ocurre un error al desactivar el producto.</exception>
        public void DesactivarProducto(int productoId)
        {
            try
            {
                using (var context = new ProductosDbContext())
                {
                    var producto = context.Productos.Find(productoId);
                    if (producto == null)
                        throw new ValidationException("Producto no encontrado", "ID", productoId.ToString());

                    producto.Estado = false;
                    context.SaveChanges();
                }
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desactivar el producto: " + ex.Message);
            }
        }

        /// <summary>
        /// Crea un nuevo producto en el sistema.
        /// </summary>
        /// <param name="producto">Objeto producto a crear.</param>
        /// <exception cref="ValidationException">Lanzada si hay errores de validación en el producto.</exception>
        /// <exception cref="Exception">Lanzada si ocurre un error al crear el producto.</exception>
        public void CrearProducto(Producto producto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(producto.Codigo))
                    throw new ValidationException("El código es obligatorio", "Código", "PROD001");

                if (string.IsNullOrWhiteSpace(producto.Nombre))
                    throw new ValidationException("El nombre es obligatorio", "Nombre", "Vaso de cristal");

                if (string.IsNullOrWhiteSpace(producto.NombreProveedor))
                    throw new ValidationException("El proveedor es obligatorio", "Proveedor", "Proveedor XYZ");

                if (producto.Existencia < 0)
                    throw new ValidationException("La existencia no puede ser negativa", "Existencia", "100");

                if (!Regex.IsMatch(producto.Codigo, @"^[A-Z]{3,4}\d{3,5}$"))
                    throw new ValidationException("Formato de código inválido. Debe ser letras + números", "Código", "ABC123");

                using (var context = new ProductosDbContext())
                {
                    if (context.Productos.Any(p => p.Codigo == producto.Codigo))
                        throw new ValidationException("El código ya está registrado", "Código", "NUEVO123");
                }

                using (var context = new ProductosDbContext())
                {
                    context.Productos.Add(producto);
                    context.SaveChanges();
                }
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el producto: " + ex.Message);
            }
        }
    }
}