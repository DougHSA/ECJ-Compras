using Dominio.Models;

namespace ECJ_Compras.Services.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmailNovaDoacao(Doacao doacao, string nomeAutor);
        Task EnviarEmailDeletarDoacao(Doacao doacao , string nomeAutor);
        Task EnviarEmailDeletarTransacao(Transacao transacao);
        Task EnviarEmailNovaTransacao(Transacao transacao);
    }
}
