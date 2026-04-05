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

    // Use the converter here
    [DynamoDBProperty(typeof(DateTimeOffsetToTimestampConverter))]
    public DateTimeOffset CreatedAt { get; set; }

    public string Name { get; set; }

    public string TaxId { get; set; }

    public bool IsActive { get; set; }
}



public class DateTimeOffsetToTimestampConverter : IPropertyConverter
{
    // C# -> DynamoDB (DateTimeOffset to Numeric)
    public DynamoDBEntry ToEntry(object value)
    {
        if (value is DateTimeOffset dto)
        {
            return new Primitive { Value = dto.ToUnixTimeSeconds() };
        }
        return new Primitive { Value = null };
    }

    // DynamoDB -> C# (Numeric to DateTimeOffset)
    public object FromEntry(DynamoDBEntry entry)
    {
        var primitive = entry as Primitive;
        if (primitive != null && long.TryParse(primitive.Value.ToString(), out long timestamp))
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp);
        }
        return default(DateTimeOffset);
    }
}