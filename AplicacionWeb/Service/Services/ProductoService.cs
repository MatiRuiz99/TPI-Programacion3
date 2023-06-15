using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductoService : IProductService
    {

        private readonly CafeteriaContext _context;

        public ProductoService(CafeteriaContext _context)
        {
            this._context = _context;
        }

        public List<ProductDTO> GetProductList()
        {
            var product = _context.Producto.ToList();
            var productResponse = new List<ProductDTO>();

            foreach (var p in product)
            {
                //Preguntar por que no puedo filtrar por true
                productResponse.Add(new ProductDTO()
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = (decimal)p.Precio,
                    Estado = p.Estado
                });
            }

            return productResponse;
            
        }
        public Producto GetProductById(int id)
        {
            var producto = _context.Producto.FirstOrDefault(p => p.IdProducto == id);

            return producto;
        }

        public string CreateProduct(ProductViewModel producto)
        {
            string response = string.Empty;

            if (new[] { "Disponible", "Pausado", "Archivado" }.Contains(producto.Estado, StringComparer.OrdinalIgnoreCase))
            {
                _context.Producto.Add(new Producto()
                {
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Estado = producto.Estado

                });
                _context.SaveChanges();
                response = "Poducto añadido exitosamente";
            } else
            {
                response = "Error al seleccionar estado del producto (Estado no existente)";
            }
            return response;
        }

        public string ModifyProduct(int id, ProductViewModel productoAModificar)
        {
            var producto = _context.Producto.FirstOrDefault(p => p.IdProducto == id);

            if (producto != null)
            {
                // Verificar si el estado proporcionado es válido
                if (new[] { "Disponible", "Pausado", "Archivado" }.Contains(productoAModificar.Estado, StringComparer.OrdinalIgnoreCase))
                {
                    producto.Nombre = productoAModificar.Nombre;
                    producto.Descripcion = productoAModificar.Descripcion;
                    producto.Precio = productoAModificar.Precio;
                    producto.Estado = productoAModificar.Estado;

                    _context.SaveChanges();
                    return "Producto modificado exitosamente";
                }
                else
                {
                    return "Error al seleccionar estado del producto (Estado no existente)";
                }
            }
            else
            {
                return "Producto no encontrado";
            }
        }

        public string DeleteProduct(int id)
        {
            string response = string.Empty;

            var producto = _context.Producto.FirstOrDefault(p => p.IdProducto == id);

            if (producto != null)
            {
                producto.Estado = "Archivado"; 
                _context.SaveChanges();
                response = "Producto dado de baja exitosamente";
            }
            else
            {
                response = "Producto no encontrado";
            }
            return response;
        }

    }
}
