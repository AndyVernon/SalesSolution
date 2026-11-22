using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text;

namespace DailySalesProcessor.Function;

public class ProcessSalesQueue
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ProcessSalesQueue> _logger;

    public ProcessSalesQueue(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ProcessSalesQueue> logger)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    [Function(nameof(ProcessSalesQueue))]
    public async Task Run([QueueTrigger("daily-sales", Connection = "StorageConnection")] QueueMessage dailySales)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", dailySales.MessageText);
        var client = _httpClientFactory.CreateClient("SalesProcessing");
        var endpoint = _configuration["SalesProcessing:SubmitSalesEndpoint"];
        var content = new StringContent(
            dailySales.MessageText,
            Encoding.UTF8,
            "application/json");
        var response = await client.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
    }
}