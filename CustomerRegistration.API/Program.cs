using Amazon.DynamoDBv2;
using CustomerRegistration.API.Endpoints.Models;
using CustomerRegistration.API.Endpoints;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// 1. Pulls AWS options from appsettings.json
var awsOptions = builder.Configuration.GetAWSOptions();

// 2. Register the IAmazonDynamoDB client
builder.Services.AddAWSService<IAmazonDynamoDB>(awsOptions);

// 3. Register your custom service 
// (Transient or Scoped is usually best for DynamoDBContext)
builder.Services.AddScoped<DynamoDbService>();

var app = builder.Build();

app.MapGet("/", (DynamoDbService service) => CustomerGetEndpoint.FetchAsync(service));
app.MapPost("/", ([FromBody] CustomerPostEndpointRequest request) => CustomerPostEndpoint.InsertAsync(request));

app.Run();