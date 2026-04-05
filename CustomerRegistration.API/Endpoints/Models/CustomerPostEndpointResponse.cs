namespace CustomerRegistration.API.Endpoints.Models;

public class CustomerPostEndpointResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string TaxId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsActive { get; set; }
}