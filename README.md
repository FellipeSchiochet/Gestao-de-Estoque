# Estoque

Projeto composto por uma API ASP.NET Core + front-end MudBlazor. A API expõe produtos e movimentações, enquanto o cliente consome os endpoints protegidos por JWT.

## Links de acesso

- Front-end: `https://gestao-de-estoque-1.onrender.com`
- API: `https://gestao-de-estoque-qpyp.onrender.com`

## Requisitos

- .NET 8 SDK instalado (para desenvolvimento use `dotnet --list-sdks` e, se necessário, o script `scripts/install-dotnet.sh` em sistemas Unix).
- Supabase (PostgreSQL) configurado e acesso válido para a string de conexão.

## Variáveis de ambiente (API)

A API não deve armazenar segredos no `appsettings.json`. Defina os valores obrigatórios em variáveis de ambiente (Render) ou via `dotnet user-secrets` (local):

| Chave | Descrição |
| --- | --- |
| `ConnectionStrings__DefaultConnection` | String de conexão completa para o PostgreSQL do Supabase (Host/Port/Database/User/Password/SSL). |
| `Jwt__Key` | Chave secreta com pelo menos 32 caracteres usada para assinar tokens. |
| `Jwt__Issuer` | Nome do emissor (ex.: `Estoque.API`). |
| `Jwt__Audience` | Público esperado (ex.: `Estoque.Client`). |
| `Jwt__ExpirationMinutes` | Tempo de expiração do token (padrão 60). |

> O `Program.cs` valida `ConnectionStrings:DefaultConnection` e lança se estiver ausente. Garanta que esses valores estejam definidos antes de executar a API.

## Variáveis de ambiente (front-end)

A aplicação Blazor roda no Render e faz chamadas HTTP para a API. Configure:

- `Api__BaseUrl`: URL pública da API hospedada no Render (ex.: `https://minha-api.onrender.com/`). Em local, o `appsettings.json` já aponta para `http://localhost:5020/`, mas o valor deve ser sobrescrito no ambiente de produção.

## Executando localmente

1. Defina os segredos usando `dotnet user-secrets` dentro do projeto `Estoque.API`:
   ```powershell
   cd src/Estoque.API
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=...;Port=5432;Database=...;Username=...;Password=...;SSL Mode=Require;Trust Server Certificate=true"
   dotnet user-secrets set "Jwt:Key" "<chave de 32 caracteres>"
   dotnet user-secrets set "Jwt:Issuer" "Estoque.API"
   dotnet user-secrets set "Jwt:Audience" "Estoque.Client"
   dotnet user-secrets set "Jwt:ExpirationMinutes" "60"
   ```
2. (Opcional) Defina `Api__BaseUrl` para o endereço local da API se quiser testar o frontend (`http://localhost:7048/`).
3. Execute `dotnet build Estoque.sln` e, depois, `dotnet run` nos projetos `Estoque.API` e `Estoque.Web` conforme necessário.

## Ambientes

| Ambiente | URL |
|----------|-----|
| Produção | https://gestao-de-estoque-1.onrender.com |
| Dev | https://gestao-de-estoque-dev-2.onrender.com |

## Deploy

Commits na branch `production` disparam deploy automático em produção via GitHub Actions.

Commits na branch `dev` disparam deploy automático em dev via GitHub Actions.

- No Render (API), configure as variáveis acima e ative `DOTNET_ENVIRONMENT=Production`.
- No Render (front-end), defina `Api__BaseUrl` com a URL da API deployada.

Com esses ajustes, o projeto não depende de segredos versionados e funciona em ambientes locais e cloud.
