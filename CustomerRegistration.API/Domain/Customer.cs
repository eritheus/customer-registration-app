namespace CustomerRegistration.API.Domain;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string TaxId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}