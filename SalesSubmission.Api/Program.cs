using SalesSubmission.Api.Services;
using Azure.Storage.Queues;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var storageConnection = builder.Configuration["StorageConnection"] 
    ?? throw new InvalidOperationException("StorageConnection is not configured.");
builder.Services.AddSingleton(_ => new QueueClient(storageConnection, "daily-sales"));
builder.Services.AddScoped<ISubmitSalesService, SubmitSalesService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
