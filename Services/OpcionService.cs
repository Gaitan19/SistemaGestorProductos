using SistemaGestorProductos.Data;
using SistemaGestorProductos.Models;
using SistemaGestorProductos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGestorProductos.Services
{
    public class OpcionService
    {
        /// <summary>
        /// Obtiene una lista de opciones asociadas a un producto específico.
        /// </summary>
        /// <param name="productoId">ID del producto para el cual se desean obtener las opciones.</param>
        /// <returns>Lista de opciones del producto.</returns>
        public List<Opcion> ObtenerOpcionesPorProducto(int productoId)
        {
            using (var context = new ProductosDbContext())
            {
                return context.Opciones
                    .Where(o => o.ProductoId == productoId)
                    .ToList();
            }
        }

        /// <summary>
        /// Crea una nueva opción.
        /// </summary>
        /// <param name="opcion">Objeto Opcion que contiene los datos de la nueva opción.</param>
        /// <exception cref="ValidationException">Lanzada si el nombre de la opción es inválido.</exception>
        public void CrearOpcion(Opcion opcion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(opcion.Nombre))
                    throw new ValidationException("El nombre de la opción es obligatorio", "Nombre", "Grande");

                if (opcion.Nombre.Length > 50)
                    throw new ValidationException("El nombre no puede exceder 50 caracteres", "Nombre", "Nombre más corto");

                using (var context = new ProductosDbContext())
                {
                    context.Opciones.Add(opcion);
                    context.SaveChanges();
                }
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la opción: " + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza una opción existente.
        /// </summary>
        /// <param name="opcion">Objeto Opcion que contiene los datos actualizados.</param>
        /// <exception cref="ValidationException">Lanzada si el nombre de la opción es inválido o si la opción no se encuentra.</exception>
        public void ActualizarOpcion(Opcion opcion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(opcion.Nombre))
                    throw new ValidationException("El nombre de la opción es obligatorio", "Nombre", "Grande");

                using (var context = new ProductosDbContext())
                {
                    var existente = context.Opciones.Find(opcion.Id);
                    if (existente == null)
                        throw new ValidationException("Opción no encontrada", "ID", opcion.Id.ToString());

                    existente.Nombre = opcion.Nombre;
                    existente.Estado = opcion.Estado;
                    context.SaveChanges();
                }
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la opción: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una opción por su ID.
        /// </summary>
        /// <param name="opcionId">ID de la opción a buscar.</param>
        /// <returns>Objeto Opcion correspondiente al ID proporcionado.</returns>
        public Opcion ObtenerOpcionPorId(int opcionId)
        {
            using (var context = new ProductosDbContext())
            {
                return context.Opciones.Find(opcionId);
            }
        }
    }
}