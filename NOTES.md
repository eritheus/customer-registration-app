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
