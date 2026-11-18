using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DailySalesProcessor.Function;

public class ProcessSalesQueue
{
    private readonly ILogger<ProcessSalesQueue> _logger;

    public ProcessSalesQueue(ILogger<ProcessSalesQueue> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ProcessSalesQueue))]
    public void Run([QueueTrigger("daily-sales", Connection = "StorageConnection")] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}