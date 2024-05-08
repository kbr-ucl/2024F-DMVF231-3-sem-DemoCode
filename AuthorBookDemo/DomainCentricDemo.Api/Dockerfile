#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DomainCentricDemo.Api/DomainCentricDemo.Api.csproj", "DomainCentricDemo.Api/"]
COPY ["DomainCentricDemo.Application/DomainCentricDemo.Application.csproj", "DomainCentricDemo.Application/"]
COPY ["DomainCentricDemo.Domain/DomainCentricDemo.Domain.csproj", "DomainCentricDemo.Domain/"]
COPY ["DomainCentricDemo.Infrastructure/DomainCentricDemo.Infrastructure.csproj", "DomainCentricDemo.Infrastructure/"]
RUN dotnet restore "./DomainCentricDemo.Api/./DomainCentricDemo.Api.csproj"
COPY . .
WORKDIR "/src/DomainCentricDemo.Api"
RUN dotnet build "./DomainCentricDemo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DomainCentricDemo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DomainCentricDemo.Api.dll"]