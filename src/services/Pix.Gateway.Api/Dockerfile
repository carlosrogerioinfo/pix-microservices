FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/Pix.Gateway.Api/Pix.Gateway.Api.csproj", "src/services/Pix.Gateway.Api/"]
COPY ["src/building blocks/Pix.Microservices.Core/Pix.Microservices.Core.csproj", "src/building blocks/Pix.Microservices.Core/"]
COPY ["src/building blocks/Pix.Microservices.Domain/Pix.Microservices.Domain.csproj", "src/building blocks/Pix.Microservices.Domain/"]
COPY ["src/building blocks dlls/", "src/building blocks dlls/"]
RUN dotnet restore "src/services/Pix.Gateway.Api/Pix.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/src/services/Pix.Gateway.Api"
RUN dotnet build "Pix.Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pix.Gateway.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pix.Gateway.Api.dll"]
