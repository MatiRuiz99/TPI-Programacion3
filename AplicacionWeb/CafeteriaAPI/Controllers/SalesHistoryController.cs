using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.ViewModel;
using Service.IServices;
using Service.Services;
using System;
using System.Collections.Generic;

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
        public ActionResult<List<SalesHistoryDTO>> GetSalesHistory()
        {
            try
            {
                var response = _salesService.GetSalesHistory();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en GetSalesHistory: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("CreateRecord")]
        public ActionResult<SalesHistoryDTO> CreateRecord([FromBody] SalesViewModel record)
        {
            try
            {
                var response = _salesService.CreateRecord(record);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en CreateRecord: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetSaleById/{id}")]
        public ActionResult<SalesHistoryDTO> GetSaleById(int id)
        {
            try
            {
                var response = _salesService.GetSaleById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en GetSaleById: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("ModifySale/{id}")]
        public ActionResult<SalesHistoryDTO> ModifySaleHistory(int id, [FromBody] SalesViewModel record)
        {
            try
            {
                var response = _salesService.ModifySaleHistory(id, record);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en ModifySale: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteRecord/{id}")]
        public ActionResult<SalesHistoryDTO> DeleteSaleHistory(int id)
        {
            try
            {
                var response = _salesService.DeleteSaleHistory(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en DeleteRecord: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
