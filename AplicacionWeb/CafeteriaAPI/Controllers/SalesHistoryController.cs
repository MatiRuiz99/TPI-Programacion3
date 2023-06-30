using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service.IServices;
using Service.Services;

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

        [HttpGet]
        public ActionResult<List<SalesHistoryDTO>> GetSalesHistory()
        {
            var response = _salesService.GetSalesHistory();

            return Ok(response);
        }

        [HttpPut]
        public ActionResult<SalesHistoryDTO> CreateRecord (SalesHistoryDTO record)
        {
            var response = _salesService.CreateRecord(record);

            return Ok(response);
        }
    }
}
