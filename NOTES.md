# Notes

## File Extensions

Example:

- Solution: `.sln` / `.slnx`
- Project: `.csproj`
- CSharp: `.cs`

## Files Bootstrap

Solution:
> dotnet new sln -n SolutionName

Project:
> dotnet new <TYPE> -n ProjectName

Vínculo do project à solution:
> dotnet sln add <ProjectPath>

## Testing the App

```bash
# Building
dotnet build

# Runing
dotnet run --project CustomerRegistration.API/CustomerRegistration.API.csproj
```

## API Calls Examples

### GET

```bash
curl -s -X GET http://localhost:5297/ | jq '.'
```

### POST

```bash
curl -s -X POST -H 'Content-Type: application/json' http://localhost:5297/ --data '{"Name": "Eric Silva da nova geração", "TaxId": "38500000000","IsActive": true}'
```