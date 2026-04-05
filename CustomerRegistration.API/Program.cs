using Amazon.DynamoDBv2;
using CustomerRegistration.API.Endpoints.Models;
using CustomerRegistration.API.Endpoints;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// 1. Pulls AWS options from appsettings.json
var awsOptions = builder.Configuration.GetAWSOptions();

// ========================= Dependency Injection Setup =========================
// AddTransient, AddScoped, or AddSingleton depending on your needs
// AddTransient: A new instance is provided every time it's requested.
// AddScoped: A new instance is created once per request.
// AddSingleton: A single instance is created and shared throughout the application's lifetime.

// 2. Register the IAmazonDynamoDB client
builder.Services.AddAWSService<IAmazonDynamoDB>(awsOptions);

// 3. Register your custom service into our Dependency Injection container
// (Transient or Scoped is usually best for DynamoDBContext)
builder.Services.AddScoped<DynamoDbService>();
// ========================= Dependency Injection Setup =========================

var app = builder.Build();

app.MapGet("/", (DynamoDbService service) => CustomerGetEndpoint.FetchAsync(service));
app.MapPost("/", ([FromBody] CustomerPostEndpointRequest request, DynamoDbService service) => CustomerPostEndpoint.InsertAsync(request, service));

app.Run();