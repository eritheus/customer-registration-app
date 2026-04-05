using CustomerRegistration.API.Endpoints.Models;

namespace CustomerRegistration.API.Endpoints;

public static class CustomerGetEndpoint
{

    public static async Task<List<CustomerPostEndpointResponse>> FetchAsync(DynamoDbService service)
    {
        var customers = await service.GetAllItemsAsync<DatabaseCustomer>();

        var response = new List<CustomerPostEndpointResponse>();
        foreach (DatabaseCustomer customer in customers)
        {
            response.Add(new CustomerPostEndpointResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                CreatedAt = customer.CreatedAt
            });
        }

        return response;

    }

}

public static class CustomerPostEndpoint
{
    public static async Task InsertAsync(CustomerPostEndpointRequest customer, DynamoDbService service)
    {
      await service.SaveItemAsync(new DatabaseCustomer
      {
        Id = Guid.NewGuid(),
        CreatedAt = DateTimeOffset.UtcNow,
        Name = customer.Name
      });

    }

}