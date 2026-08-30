FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY CustomerRegistration.slnx ./
COPY CustomerRegistration.API/CustomerRegistration.API.csproj CustomerRegistration.API/
RUN dotnet restore CustomerRegistration.API/CustomerRegistration.API.csproj

COPY CustomerRegistration.API/ CustomerRegistration.API/
RUN dotnet publish CustomerRegistration.API/CustomerRegistration.API.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "CustomerRegistration.API.dll"]
