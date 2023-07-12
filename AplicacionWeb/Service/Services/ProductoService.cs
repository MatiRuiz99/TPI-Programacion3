using AutoMapper;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Mappings;
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
        private readonly IMapper _mapper;

        public ProductoService(CafeteriaContext _context)
        {
            this._context = _context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<ProductDTO> GetProductList()
        {
            var product = _context.Producto.Where(p => p.Estado != "archivado").ToList();
            var productResponse = new List<ProductDTO>();

            foreach (var p in product)
            {
                ProductDTO Response = _mapper.Map<ProductDTO>(p);
                productResponse.Add(Response);
            }

            return productResponse;
        }

        public ProductDTO GetProductById(int id)
        {
            ProductDTO Response = _mapper.Map<ProductDTO>(_context.Producto.FirstOrDefault(p => p.IdProducto == id));
            return Response;
            
        }

        public string CreateProduct(ProductViewModel producto)
        {
            string response = string.Empty;

            if (new[] { "disponible", "pausado" }.Contains(producto.Estado.ToLower()))
            {
                Producto Response = _mapper.Map<Producto>(producto);
                _context.Producto.Add(Response);
                _context.SaveChanges();
                response = "Producto añadido exitosamente";
            }
            else
            {
                response = "Error al asignar el estado";
            }
            return response;
        }

        public string ModifyProduct(int id, ProductViewModel productoAModificar)
        {
            var producto = _context.Producto.First(p => p.IdProducto == id);

            if (producto != null)
            {
                if (new[] { "disponible", "pausado", "archivado" }.Contains(productoAModificar.Estado.ToLower()))
                {
                    producto.Nombre = productoAModificar.Nombre;
                    producto.Descripcion = productoAModificar.Descripcion;
                    producto.Precio = productoAModificar.Precio;
                    producto.Estado = productoAModificar.Estado.ToLower();

                    _context.SaveChanges();
                    return "Producto modificado exitosamente";
                }
                else
                {
                    return "Error al seleccionar estado del producto (estado no existente)";
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
                producto.Estado = "archivado";
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

