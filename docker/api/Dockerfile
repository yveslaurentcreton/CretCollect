# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0.304 AS build

WORKDIR /source
COPY . .
RUN dotnet publish src/CretCollect.WebApi/CretCollect.WebApi.csproj --configuration Release --output out

# Run
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Install Microsoft fonts
RUN echo "deb http://deb.debian.org/debian bookworm contrib non-free" > /etc/apt/sources.list
RUN apt update
RUN apt -y install fontconfig
RUN apt -y install ttf-mscorefonts-installer
RUN fc-cache -f

WORKDIR /app
COPY --from=build /source/out .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "CretCollect.WebApi.dll"]
