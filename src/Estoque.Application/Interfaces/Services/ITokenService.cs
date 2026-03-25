using Estoque.Domain.Entities;

namespace Estoque.Application.Interfaces.Services;

public interface ITokenService
{
    (string Token, DateTime Expiracao) GerarToken(Usuario usuario);
}
