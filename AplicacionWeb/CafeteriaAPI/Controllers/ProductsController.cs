using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service;
using System.Collections.Generic;

namespace CafeteriaAPI.Controllers
{
    [Route("api/ControladorProducto")]
    [ApiController]
    public class ProductsController : ControllerBase
    {   //TEST Asi se utiliza una Inyecccion de dependencia
        private readonly ProductoService _productoService = new ProductoService();
        public ProductsController()
        {
           
        }


        [HttpGet("GetProductList")]

        public ActionResult<List<ProductDTO>> GetProductList()
        {
            //var producto = new ProductoService();
            var response = _productoService.GetProductList();

            return Ok(response);

        }

        [HttpGet("GetProductById/{id}")]

        public ActionResult<ProductDTO> GetProductById(int id)
        {
            //var producto = new ProductoService();
            var response = _productoService.GetProductById(id);

            return Ok(response);

        }

        [HttpPost("CreateProducto")]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductDTO producto)
        {
            var response = _productoService.CreateProduct(producto);

            return Ok(response);
        }

        [HttpPut("PutProducto/{id}")]

        public ActionResult<ProductDTO> ModifyProduct(int id, [FromBody] ProductDTO producto)
        {
            var response = _productoService.ModifyProduct(id,producto);

            return Ok(response);
        }

        [HttpDelete("DeleteProducto/{id}")]

        public ActionResult<ProductDTO> DeleteProduct(int id)
        {
            var response = _productoService.DeleteProduct(id);

            return Ok(response);
        }
    }
}
