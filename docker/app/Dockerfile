# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0.304 AS build

WORKDIR /source
COPY . .
RUN dotnet publish src/CretCollect.App.Server/CretCollect.App.Server.csproj --configuration Release --output out

# Run
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /source/out .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "CretCollect.App.Server.dll"]