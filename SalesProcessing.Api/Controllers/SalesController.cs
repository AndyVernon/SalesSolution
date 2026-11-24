using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.Contracts;
using SalesProcessing.Api.Context;
using System.Globalization;

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

        [HttpPost]
        [Route("weeklysales")]
        public async Task<IActionResult> WeeklySales(DailySalesDTO dailySales)
        {
            try
            {
                var yearNumber = ISOWeek.GetYear(dailySales.SalesDate);
                var weekNumber = ISOWeek.GetWeekOfYear(dailySales.SalesDate);
                var weeklySalesEntity = await _context.WeeklySales.FirstOrDefaultAsync(sale =>
                    sale.CustomerId == dailySales.CustomerId &&
                    sale.BranchId == dailySales.BranchId &&
                    sale.YearNumber == yearNumber &&
                    sale.WeekNumber == weekNumber);

                if (weeklySalesEntity is null)
                {
                    weeklySalesEntity = new WeeklySales
                    {
                        Id = dailySales.Id,
                        YearNumber = yearNumber,
                        WeekNumber = weekNumber,
                        CustomerId = dailySales.CustomerId,
                        BranchId = dailySales.BranchId,
                        Amount = dailySales.Amount
                    };
                    _context.WeeklySales.Add(weeklySalesEntity);
                }
                else
                {
                    weeklySalesEntity.Amount += dailySales.Amount;
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Weekly sales record added or updated successfully with Id: {Id}", weeklySalesEntity.Id);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the weekly sales record.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }
    }
}
