using Microsoft.AspNetCore.Mvc;
using Sales.Contracts;
using SalesSubmission.Api.Services;

namespace SalesSubmission.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitSalesController : ControllerBase
    {
        private readonly ISubmitSalesService _submitSalesService;
        private readonly ILogger<SubmitSalesController> _logger;

        public SubmitSalesController(ISubmitSalesService submitSalesService, ILogger<SubmitSalesController> logger)
        {
            _submitSalesService = submitSalesService;
            _logger = logger;
        }

        [HttpPost]
        [Route("submit")]
        public async Task<ActionResult<BaseResponse>> SubmitSalesAsync([FromBody] SalesRequest request, CancellationToken cancellationToken)
        {
            BaseResponse response;
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("Received null SalesRequest.");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                response = await _submitSalesService.SubmitSalesAsync(request, cancellationToken);
                return StatusCode(StatusCodes.Status202Accepted, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while submitting sales.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
