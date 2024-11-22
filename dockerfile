FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["*.sln", "./"]
COPY ["SolarEnergyStore.Api/SolarEnergyStore.Api.csproj", "SolarEnergyStore.Api/"]
COPY ["SolarEnergyStore.Models/SolarEnergyStore.Models.csproj", "SolarEnergyStore.Models/"]
COPY ["SolarEnergyStore.Repositories/SolarEnergyStore.Repositories.csproj", "SolarEnergyStore.Repositories/"]
COPY ["SolarEnergyStore.Services/SolarEnergyStore.Services.csproj", "SolarEnergyStore.Services/"]
COPY ["SolarEnergyStore.Tests/SolarEnergyStore.Tests.csproj", "SolarEnergyStore.Tests/"]
RUN dotnet restore

COPY . ./
WORKDIR ./SolarEnergyStore.Api

RUN mv appsettings.docker.json appsettings.json
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

FROM build as migrations
RUN dotnet tool install --version 6.0.9 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet-ef database update

COPY --from=build /app .
EXPOSE 80

ENTRYPOINT ["dotnet", "SolarEnergyStore.Api.dll"]