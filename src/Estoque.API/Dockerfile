FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy referenced projects individually to allow restore
COPY src/Estoque.API/Estoque.API.csproj src/Estoque.API/
COPY src/Estoque.Application/Estoque.Application.csproj src/Estoque.Application/
COPY src/Estoque.Infrastructure/Estoque.Infrastructure.csproj src/Estoque.Infrastructure/

RUN dotnet restore "src/Estoque.API/Estoque.API.csproj"

COPY src ./src

RUN dotnet publish "src/Estoque.API/Estoque.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Estoque.API.dll"]
