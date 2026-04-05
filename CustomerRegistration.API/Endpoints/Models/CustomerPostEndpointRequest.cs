namespace CustomerRegistration.API.Endpoints.Models;

public class CustomerPostEndpointRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string TaxId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}