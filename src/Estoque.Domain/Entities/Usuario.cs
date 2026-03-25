namespace Estoque.Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    public DateTime DataCadastro { get; private set; }

    private Usuario()
    {
        Nome = string.Empty;
        Email = string.Empty;
        SenhaHash = string.Empty;
    }

    public Usuario(string nome, string email, string senhaHash, DateTime? dataCadastro = null)
    {
        Nome = ValidarNome(nome);
        Email = ValidarEmail(email);
        SenhaHash = ValidarSenhaHash(senhaHash);
        DataCadastro = dataCadastro ?? DateTime.UtcNow;
    }

    public void AtualizarDados(string nome, string email)
    {
        Nome = ValidarNome(nome);
        Email = ValidarEmail(email);
    }

    public void AtualizarSenhaHash(string senhaHash)
    {
        SenhaHash = ValidarSenhaHash(senhaHash);
    }

    private static string ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome do usuário é obrigatório.", nameof(nome));
        }

        return nome.Trim();
    }

    private static string ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("O e-mail do usuário é obrigatório.", nameof(email));
        }

        return email.Trim().ToLowerInvariant();
    }

    private static string ValidarSenhaHash(string senhaHash)
    {
        if (string.IsNullOrWhiteSpace(senhaHash))
        {
            throw new ArgumentException("O hash da senha é obrigatório.", nameof(senhaHash));
        }

        return senhaHash;
    }
}
