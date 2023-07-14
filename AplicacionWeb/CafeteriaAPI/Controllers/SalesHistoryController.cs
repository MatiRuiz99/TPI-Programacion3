using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CafeteriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesHistoryController : ControllerBase
    {
        private readonly ISalesService _salesService;
        private readonly ILogger<SalesHistoryController> _logger;

        public SalesHistoryController(ISalesService salesService, ILogger<SalesHistoryController> logger)
        {
            _salesService = salesService;
            _logger = logger;
        }

        [HttpGet("GetSalesHistory")]
        [Authorize]
        public ActionResult<List<SalesHistoryDTO>> GetSalesHistory()
        {
            try
            {
                var response = _salesService.GetSalesHistory();
                if (response == null || response.Count == 0)
                {
                    return NotFound("No Purchases found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in GetSalesHistory: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("CreateRecord")]
        [Authorize]
        public ActionResult<SalesHistoryDTO> CreateRecord([FromBody] SalesViewModel record)
        {
            try
            {           
                 var response = _salesService.CreateRecord(record);
                if (response == null)
                {
                   return NotFound("Error in purchase (product or user not found");
                }
                string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string apiAndEndpointUrl = $"api/SalesHistory/GetSaleById";
                string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}/{response.id}";
                return Created(locationUrl, response);
                              
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in CreateRecord: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetSaleById/{id}")]
        [Authorize]
        public ActionResult<SalesHistoryDTO> GetSaleById(int id)
        {
            try
            {   
                    var response = _salesService.GetSaleById(id);
                if (response == null)
                {
                    return NotFound("Purchase not found");
                }
                    return Ok(response);   
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in GetSaleById: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("ModifySale/{id}")]
        [Authorize]
        public ActionResult<string> ModifySaleHistory(int id, [FromBody] SalesViewModel record)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _salesService.ModifySaleHistory(id, record);
                    if (response == "Error modifying sale history (user or product not found)")
                    {
                        return BadRequest(response);
                    } else if (response == "Sale history not found")
                    {
                        return NotFound(response);
                    }
                    return Ok(response);
                }
                throw new Exception("You don't have an Administrator role");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in ModifySale: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteRecord/{id}")]
        [Authorize]
        public ActionResult<string> DeleteSaleHistory(int id)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _salesService.DeleteSaleHistory(id);
                    if (response == null)
                    {
                        return NotFound("Sale History not found");
                    }
                    return Ok(response);
                }
                throw new Exception("You don't have an Administrator role");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in DeleteRecord: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetTopSellingItems")]
        public ActionResult<List<BestSellingProdDTO>> GetTopSellingItems()
        {
            try
            {
                var response = _salesService.GetTopSellingItems();
                if (response == null || response.Count == 0)
                {
                    return NotFound("There are no products");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in GetTopSellingItems: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }


    }
}
