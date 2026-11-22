using Microsoft.AspNetCore.Mvc;
using Sales.Contracts;
using SalesProcessing.Api.Context;

namespace SalesProcessing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesDbContext _context;
        private readonly ILogger<SalesController> _logger;

        public SalesController(SalesDbContext context, ILogger<SalesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [Route("dailysales")]
        public async Task<IActionResult> DailySales(DailySalesDTO dailySales)
        {
            try
            {
                var dailySalesEntity = new DailySales
                {
                    Id = dailySales.Id, 
                    CustomerId = dailySales.CustomerId,
                    BranchId = dailySales.BranchId,
                    Date = dailySales.SalesDate,
                    Amount = dailySales.Amount,
                    UploadedDate = dailySales.SubmissionDate
                };
                _context.DailySales.Add(dailySalesEntity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Daily sales record added successfully with Id: {Id}", dailySalesEntity.Id);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the daily sales record.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }
    }
}
