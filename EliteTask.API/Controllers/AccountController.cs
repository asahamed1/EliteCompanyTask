using EliteTask.Application.IServices;
using EliteTask.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliteTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IBalanceService _balanceService;

        public AccountController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet("GetAccountsList")]
        
        public async Task<IActionResult> GetAccountsList(string? query, int page = 1, int pageSize = 20) { 
            return Ok( await _balanceService.GetAllBalances(query , page , pageSize));    
        }

        [HttpPost("GetAccountBalanceHistory")]
        public async Task<IActionResult> GetAccountBalanceHistory(GetAccountBalanceHistoryModel model)

        {
            if (!ModelState.IsValid)
            {
                // Return validation errors
                return BadRequest(ModelState);
            }
            return Ok(await _balanceService.GetAccountBalanceHistory(model));
        }
        [HttpPost("ExportBalanceHistory")]
        public async Task<IActionResult> ExportBalanceHistory(GetAccountBalanceHistoryModel model)
        {

            if (!ModelState.IsValid)
            {
                // Return validation errors
                return BadRequest(ModelState);
            }
            string fileName = "Report.xlsx";

            return File(await _balanceService.ExportBalanceHistory(model),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
        [HttpPost("ExportBalanceHistoryAsPdf")]
        public async Task<IActionResult> ExportBalanceHistoryAsPdf(GetAccountBalanceHistoryModel model)
        {

            if (!ModelState.IsValid)
            {
                // Return validation errors
                return BadRequest(ModelState);
            }
            string fileName = "Report.pdf";

            return File(await _balanceService.ExportBalanceHistoryAsPDF(model),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
        [HttpGet("GetHistoryDetails")]
        public async Task<IActionResult> GetHistoryDetails(int id)
        {
            return Ok(await _balanceService.GetHistoryDetails(id));
        }
    }
}
