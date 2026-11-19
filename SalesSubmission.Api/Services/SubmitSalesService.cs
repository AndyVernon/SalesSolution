using Sales.Contracts;
using System.Text.Json;
using Azure.Storage.Queues;

namespace SalesSubmission.Api.Services
{
    public interface ISubmitSalesService
    {
        Task<BaseResponse> SubmitSalesAsync(SalesRequest salesRequest, CancellationToken cancellationToken);
    }

    public class SubmitSalesService : ISubmitSalesService
    {
        private readonly QueueClient _queueClient;
        private readonly ILogger<SubmitSalesService> _logger;

        public SubmitSalesService(QueueClient queueClient, ILogger<SubmitSalesService> logger)
        {
            _queueClient = queueClient;
            _logger = logger;
        }

        public async Task<BaseResponse> SubmitSalesAsync(SalesRequest salesRequest, CancellationToken cancellationToken)
        {
            try
            {
                var message = JsonSerializer.Serialize(salesRequest);
                var result = await _queueClient.SendMessageAsync(message, cancellationToken);
                var resultStatus = result.GetRawResponse().Status;
                _logger.LogInformation(
                    "Queued sales submission. Status: {Status}, MessageId: {MessageId}, InsertedTime: {InsertedTime}",
                    resultStatus,
                    result.Value.MessageId,
                    result.Value.InsertionTime);

                return new BaseResponse
                {
                    StatusCode = resultStatus
                };  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting sales request to the queue.");
                return new BaseResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
