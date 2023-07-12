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
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en GetSalesHistory: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("CreateRecord")]
        [Authorize]
        public ActionResult<SalesHistoryDTO> CreateRecord([FromBody] SalesViewModel record)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Usuario" || HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _salesService.CreateRecord(record);
                    return Ok(response);
                }
                throw new Exception("Necesita registrarse para realizar una compra");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en CreateRecord: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetSaleById/{id}")]
        [Authorize]
        public ActionResult<SalesHistoryDTO> GetSaleById(int id)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Usuario" || HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _salesService.GetSaleById(id);
                    return Ok(response);
                }
                throw new Exception("Necesita registrarse para realizar esta acción");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en GetSaleById: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("ModifySale/{id}")]
        [Authorize]
        public ActionResult<SalesHistoryDTO> ModifySaleHistory(int id, [FromBody] SalesViewModel record)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _salesService.ModifySaleHistory(id, record);
                    return Ok(response);
                }
                throw new Exception("No tiene rol Administrador");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en ModifySale: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteRecord/{id}")]
        [Authorize]
        public ActionResult<SalesHistoryDTO> DeleteSaleHistory(int id)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _salesService.DeleteSaleHistory(id);
                    return Ok(response);
                }
                throw new Exception("No tiene rol Administrador");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en DeleteRecord: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetTopSellingItems")]
        public ActionResult<List<BestSellingProdDTO>> GetTopSellingItems()
        {
            try
            {
                var response = _salesService.GetTopSellingItems();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en GetTopSellingItems: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }


    }
}
