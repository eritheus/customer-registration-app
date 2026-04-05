using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

public class DynamoDbService
{
    private readonly DynamoDBContext _context;

    public DynamoDbService(IAmazonDynamoDB client)
    {
        // The Context handles the mapping between your class and the DB table
        _context = new DynamoDBContext(client);
    }

    /// <summary>
    /// Inserts or updates an item in the table.
    /// </summary>
    public async Task SaveItemAsync<T>(T item) where T : class
    {
        await _context.SaveAsync(item);
    }

    /// <summary>
    /// Retrieves all rows from the table. 
    /// Note: Use 'Scan' sparingly on large tables as it can be expensive.
    /// </summary>
    public async Task<List<T>> GetAllItemsAsync<T>() where T : class
    {
        // An empty list of conditions fetches everything
        var conditions = new List<ScanCondition>();
        return await _context.ScanAsync<T>(conditions).GetRemainingAsync();
    }
}

// Example Data Model
[DynamoDBTable("customer-registration-dev")]
public class DatabaseCustomer
{
    [DynamoDBHashKey] // Partition Key
    public Guid Id { get; set; }

    public long ExpiresAt { get; set; }

    // 1. This is the property DynamoDB will see. 
    // Being a 'long', it perfectly matches the 'Numeric' type in your DB.
    [DynamoDBRangeKey] // Sort Key
    public long CreatedAt { get; set; }

    // 2. This is a helper to let you work with DateTimeOffset in your code.
    // [DynamoDBIgnore] ensures the SDK doesn't try to save this property too.
    [DynamoDBIgnore]
    public DateTimeOffset CreatedAtDate
    {
        get => DateTimeOffset.FromUnixTimeSeconds(CreatedAt);
        set => CreatedAt = value.ToUnixTimeSeconds();
    }

    public string Name { get; set; }

    public string TaxId { get; set; }

    public bool IsActive { get; set; }
}
