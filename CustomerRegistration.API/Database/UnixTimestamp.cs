namespace CustomerRegistration.API.Database;

public struct UnixTimestamp
{
    private long _seconds;

    public UnixTimestamp(long seconds) => _seconds = seconds;

    // Implicit: DateTimeOffset -> UnixTimestamp
    public static implicit operator UnixTimestamp(DateTimeOffset dto) 
        => new UnixTimestamp(dto.ToUnixTimeSeconds());

    // Implicit: UnixTimestamp -> DateTimeOffset
    public static implicit operator DateTimeOffset(UnixTimestamp ut) 
        => DateTimeOffset.FromUnixTimeSeconds(ut._seconds);

    // Implicit: UnixTimestamp -> long (What DynamoDB sees)
    public static implicit operator long(UnixTimestamp ut) => ut._seconds;

    // Implicit: long -> UnixTimestamp (What DynamoDB sets)
    public static implicit operator UnixTimestamp(long seconds) => new UnixTimestamp(seconds);
}