using CustomerRegistration.API.Database;
using CustomerRegistration.API.Domain;
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
                Name = customer.Name
            });
        }

        return response;

    }

}

public static class CustomerPostEndpoint
{
    public static void InsertAsync(CustomerPostEndpointRequest customer)
    {
        CustomerDatabase.Insert(new Customer
        {
            Name = customer.Name
        });
    }

}