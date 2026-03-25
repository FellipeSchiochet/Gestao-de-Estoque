using System.Security.Cryptography;
using Estoque.Application.DTOs.Auth;
using Estoque.Application.Interfaces.Repositories;
using Estoque.Application.Interfaces.Services;
using Estoque.Domain.Entities;

namespace Estoque.Application.Services;

public class AuthService : IAuthService
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> RegistrarAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(email, cancellationToken);

        if (usuarioExistente is not null)
        {
            throw new InvalidOperationException("Já existe um usuário cadastrado com esse e-mail.");
        }

        var senhaHash = GerarHashSenha(request.Senha);
        var usuario = new Usuario(request.Nome, email, senhaHash);

        await _usuarioRepository.AdicionarAsync(usuario, cancellationToken);
        await _usuarioRepository.SalvarAlteracoesAsync(cancellationToken);

        return GerarResposta(usuario);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var usuario = await _usuarioRepository.ObterPorEmailAsync(email, cancellationToken);

        if (usuario is null || !VerificarSenha(request.Senha, usuario.SenhaHash))
        {
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
        }

        return GerarResposta(usuario);
    }

    private AuthResponse GerarResposta(Usuario usuario)
    {
        var (token, expiracao) = _tokenService.GerarToken(usuario);

        return new AuthResponse
        {
            Token = token,
            Expiracao = expiracao,
            Usuario = new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCadastro = usuario.DataCadastro
            }
        };
    }

    private static string GerarHashSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
        {
            throw new ArgumentException("A senha é obrigatória.", nameof(senha));
        }

        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(senha, salt, Iterations, HashAlgorithm, KeySize);

        return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    private static bool VerificarSenha(string senhaInformada, string senhaHash)
    {
        var partes = senhaHash.Split('.', StringSplitOptions.RemoveEmptyEntries);

        if (partes.Length != 3 || !int.TryParse(partes[0], out var iterations))
        {
            return false;
        }

        var salt = Convert.FromBase64String(partes[1]);
        var hashEsperado = Convert.FromBase64String(partes[2]);
        var hashInformado = Rfc2898DeriveBytes.Pbkdf2(senhaInformada, salt, iterations, HashAlgorithm, hashEsperado.Length);

        return CryptographicOperations.FixedTimeEquals(hashInformado, hashEsperado);
    }
}
