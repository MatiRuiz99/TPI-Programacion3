using Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductoService
    {   
        //Temporal mientras que no se tiene database
        static readonly List<ProductDTO> listadoProductos = new List<ProductDTO>();
        public List<ProductDTO> GetProductList()
        {
           
            return listadoProductos;
        }

        public ProductDTO GetProductById(int id)
        {
            

            ProductDTO producto = listadoProductos.Where(x => x.IdProducto == id).First();

            return producto;
        }

        public ProductDTO CreateProduct(ProductDTO producto)
        {
            

            listadoProductos.Add(producto);

            return producto;
        }

        public List<ProductDTO> ModifyProduct(int id,ProductDTO producto)
        {
            

            var productoamodificar = listadoProductos.Where(x => x.IdProducto == id).First();
            productoamodificar.Nombre = producto.Nombre;
            productoamodificar.Descripcion = producto.Descripcion;
            productoamodificar.Precio = producto.Precio;
            productoamodificar.IdProducto = producto.IdProducto;
            productoamodificar.Estado = producto.Estado;

            

            return listadoProductos;
        }

        public List<ProductDTO> DeleteProduct(int id)
        {
            

            var productoABorrar = listadoProductos.Where(x => x.IdProducto == id).First();
            listadoProductos.Remove(productoABorrar);

            return listadoProductos;
        }

    }
}
