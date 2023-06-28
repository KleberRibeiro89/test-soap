#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["1-Host_Layer/MiniHubApi.Host/MiniHubApi.Host.csproj", "1-Host_Layer/MiniHubApi.Host/"]
COPY ["2-Application_Layer/MiniHubApi.Application/MiniHubApi.Application.csproj", "2-Application_Layer/MiniHubApi.Application/"]
RUN dotnet restore "1-Host_Layer/MiniHubApi.Host/MiniHubApi.Host.csproj"
COPY . .
WORKDIR "/src/1-Host_Layer/MiniHubApi.Host"
RUN dotnet build "MiniHubApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiniHubApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniHubApi.Host.dll"]