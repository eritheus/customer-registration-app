# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build

# Run the API
dotnet run --project CustomerRegistration.API/CustomerRegistration.API.csproj
```

There are no tests at this time.

## Architecture

Single ASP.NET Core Minimal API project (`CustomerRegistration.API`) targeting .NET 10, backed by AWS DynamoDB.

**Request flow:** HTTP endpoint → static endpoint class → `DynamoDbService` (DynamoDB) or `CustomerDatabase` (in-memory fallback)

**Key layers:**

- `Program.cs` — bootstraps DI, registers `IAmazonDynamoDB` and `DynamoDbService`, maps routes
- `Endpoints/CustomerEndpoints.cs` — static endpoint handler classes (`CustomerGetEndpoint`, `CustomerPostEndpoint`)
- `Endpoints/Models/` — request/response DTOs
- `Domain/Customer.cs` — core domain model
- `Database/DynamoDbService.cs` — generic DynamoDB wrapper using `DynamoDBContext`; also defines `DatabaseCustomer` (the DynamoDB-mapped model with `[DynamoDBTable("Customers")]`)
- `Database/CustomerDatabase.cs` — in-memory list store (currently used only by POST, which does not persist to DynamoDB)

**Note:** There is an inconsistency — GET reads from DynamoDB via `DynamoDbService`, but POST writes only to the in-memory `CustomerDatabase`. Aligning POST to use `DynamoDbService.SaveItemAsync<DatabaseCustomer>()` is likely needed.

## AWS Configuration

AWS region and credential profile are set in `appsettings.json`:

```json
"AWS": {
  "Region": "us-east-2",
  "Profile": "default"
}
```

The app uses the AWS SDK's `GetAWSOptions()` / `AddAWSService<IAmazonDynamoDB>()` pattern for DI. Ensure the `default` AWS profile is configured locally (`~/.aws/credentials`) before running.
