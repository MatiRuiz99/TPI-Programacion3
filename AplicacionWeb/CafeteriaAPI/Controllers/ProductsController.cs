using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.ViewModel;
using Service.IServices;
using Service.Services;
using System.Collections.Generic;

namespace CafeteriaAPI.Controllers
{
    [Route("api/ControladorProducto")]
    [ApiController]
    public class ProductsController : ControllerBase
    {  
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }


        [HttpGet("GetProductList")]

        public ActionResult<List<ProductDTO>> GetProductList()
        {
            
            var response = _service.GetProductList();

            return Ok(response);

        }

        [HttpGet("GetProductById/{id}")]

        public ActionResult<ProductDTO> GetProductById(int id)
        {
            
            var response = _service.GetProductById(id);

            return Ok(response);

        }

        [HttpPost("CreateProducto")]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductViewModel producto)
        {
            var response = _service.CreateProduct(producto);

            return Ok(response);
        }

        [HttpPut("PutProducto/{id}")]

        public ActionResult<ProductDTO> ModifyProduct(int id, [FromBody] ProductViewModel producto)
        {
            var response = _service.ModifyProduct(id,producto);

            return Ok(response);
        }

        [HttpDelete("DeleteProducto/{id}")]

        public ActionResult<ProductDTO> DeleteProduct(int id)
        {
            var response = _service.DeleteProduct(id);

            return Ok(response);
        }
    }
}
