using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.ViewModel;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CafeteriaAPI.Controllers
{
    [Route("api/[controller]")]
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
                if (response.Count == 0)
                {
                    return NotFound("no products available");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in GetProductList: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetProductById/{id}")]
        public ActionResult<ProductDTO> GetProductById(int id)
        {
            try
            {
                var response = _service.GetProductById(id);
                if (response == null)
                {
                    return NotFound("Product not found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in GetProductById: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("CreateProduct")]
        [Authorize]
        public ActionResult<ProductDTO> CreateProduct([FromBody] ProductViewModel producto)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.CreateProduct(producto);
                    if (response == null)
                    {
                        return BadRequest(response);
                    }
                    string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    string apiAndEndpointUrl = $"api/Products/GetProductById";
                    string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}/{response.IdProducto}";
                    return Created(locationUrl, response);
                    
                }
                throw new Exception("You don't have an Administrator role");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in CreateProduct: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("ModifyProduct/{id}")]
        [Authorize]
        public ActionResult<string> ModifyProduct(int id, [FromBody] ProductViewModel producto)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.ModifyProduct(id, producto);
                    if (response == "Error selecting product state (non-existent state)")
                    {
                        return BadRequest(response);
                    } else if (response == "Product not found") {
                        return NotFound(response);
                    }
                    return Ok(response);
                }
                throw new Exception("You don't have an Administrator role");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in ModifyProduct: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        [Authorize]
        public ActionResult<string> DeleteProduct(int id)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.DeleteProduct(id);
                    if (response == "Product not found")
                    {
                       return NotFound(response);
                    }
                    return Ok(response);
                }
                throw new Exception("You don't have an Administrator role");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in DeleteProduct: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
