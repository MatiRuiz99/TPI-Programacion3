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
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service, ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;
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
                _logger.LogError($"Ocurrio un error en GetProductList: {ex.Message}");
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
                _logger.LogError($"Ocurrio un error en GetProductById: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("CreateProduct")]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductViewModel producto)
        {
            try
            {
                var response = _service.CreateProduct(producto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en CreateProduct: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("ModifyProduct/{id}")]
        public ActionResult<ProductDTO> ModifyProduct(int id, [FromBody] ProductViewModel producto)
        {
            try
            {
                var response = _service.ModifyProduct(id, producto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en ModifyProduct: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public ActionResult<ProductDTO> DeleteProduct(int id)
        {
            try
            {
                var response = _service.DeleteProduct(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en DeleteProduct: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
