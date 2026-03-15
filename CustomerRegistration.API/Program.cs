using CustomerRegistration.API.Sample;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => HelloWorld.FetchHelloWorld() );

app.Run();