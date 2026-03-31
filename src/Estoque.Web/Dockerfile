FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o csproj e restaura antes de copiar o restante para aproveitar cache
COPY ["src/Estoque.Web/Estoque.Web.csproj", "src/Estoque.Web/"]
RUN dotnet restore "src/Estoque.Web/Estoque.Web.csproj"

COPY . .
RUN dotnet publish "src/Estoque.Web/Estoque.Web.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Estoque.Web.dll"]
