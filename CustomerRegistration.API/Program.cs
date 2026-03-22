using CustomerRegistration.API.Endpoints;
using CustomerRegistration.API.Endpoints.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => CustomerGetEndpoint.FetchAsync());
app.MapPost("/", ([FromBody] CustomerPostEndpointRequest request) => CustomerPostEndpoint.InsertAsync(request));

app.Run();