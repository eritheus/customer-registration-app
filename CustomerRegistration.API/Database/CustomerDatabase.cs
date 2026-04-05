using CustomerRegistration.API.Domain;

namespace CustomerRegistration.API.Database;

public static class CustomerDatabase
{

    private static List<Customer> _database = new List<Customer>();

    public static List<Customer> Fetch()
    {
        return _database;
    }

    public static void Insert(Customer customer)
    {
        _database.Add(customer);
    }

}