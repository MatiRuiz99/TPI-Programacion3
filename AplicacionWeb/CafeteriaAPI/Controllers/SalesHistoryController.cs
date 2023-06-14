using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service;

namespace CafeteriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesHistoryController : ControllerBase
    {
        private readonly SalesService _salesService = new SalesService();

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
