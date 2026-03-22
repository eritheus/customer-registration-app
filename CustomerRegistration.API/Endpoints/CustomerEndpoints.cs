using CustomerRegistration.API.Database;
using CustomerRegistration.API.Domain;
using CustomerRegistration.API.Endpoints.Models;

namespace CustomerRegistration.API.Endpoints;

public static class CustomerGetEndpoint
{

  public static List<CustomerPostEndpointResponse> FetchAsync()
  {
    var customers = CustomerDatabase.Fetch();
    var response = new List<CustomerPostEndpointResponse>();
    foreach(Customer customer in customers)
    {
      response.Add(new CustomerPostEndpointResponse{
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