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

        public SalesHistoryController(ISalesService salesService)
        {
            _salesService = salesService;
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
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
