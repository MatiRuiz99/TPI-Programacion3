using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.ViewModel;
using Service.IServices;
using System;
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
            try
            {
                var response = _service.GetProductList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetProductById/{id}")]
        public ActionResult<ProductDTO> GetProductById(int id)
        {
            try
            {
                var response = _service.GetProductById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("CreateProducto")]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductViewModel producto)
        {
            try
            {
                var response = _service.CreateProduct(producto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("PutProducto/{id}")]
        public ActionResult<ProductDTO> ModifyProduct(int id, [FromBody] ProductViewModel producto)
        {
            try
            {
                var response = _service.ModifyProduct(id, producto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteProducto/{id}")]
        public ActionResult<ProductDTO> DeleteProduct(int id)
        {
            try
            {
                var response = _service.DeleteProduct(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
