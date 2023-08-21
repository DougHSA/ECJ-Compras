using SendGrid.Helpers.Mail;
using SendGrid;
using ECJ_Compras.Services.Interfaces;
using Dominio.Models;

namespace ECJ_Compras.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly string ApiKey;
        private readonly SendGridClient Client;
        private readonly EmailAddress From;
        private readonly EmailAddress To;

        public EmailService(IConfiguration config)
        {
            _config = config;
            ApiKey = _config.GetSection("Smtp").GetSection("Key").Value;
            Client = new SendGridClient(ApiKey);
            From =  new EmailAddress("doughsa97@gmail.com", "EJC-Compras");
            To = new EmailAddress("doughsa97@gmail.com", "Douglas");
        }

        public async Task EnviarEmailDeletarTransacao(Transacao transacao)
        {
            var subject = $"Exclusão de transação de {transacao.Tipo}";
            var plainTextContent = $"Descrição: {transacao.Descricao}\nValor: {string.Format("{0:C}", transacao.Valor)}\nMétodo de Pagamento: {transacao.MetodoPagamento}\nData: {transacao.Data}\nAutor: {transacao.Usuario.Nome}";
            var msg = MailHelper.CreateSingleEmail(From, To, subject, plainTextContent, null);
            var response = await Client.SendEmailAsync(msg);
        }

        public async Task EnviarEmailNovaTransacao(Transacao transacao)
        {
            var subject = $"Nova transação de {transacao.Tipo}";
            var plainTextContent = $"Descrição: {transacao.Descricao}\nValor: {string.Format("{0:C}", transacao.Valor)}\nMétodo de Pagamento: {transacao.MetodoPagamento}\nData: {transacao.Data}\nAutor: {transacao.Usuario.Nome}";
            var msg = MailHelper.CreateSingleEmail(From, To, subject, plainTextContent, null);
            var response = await Client.SendEmailAsync(msg);
        }

        public async Task EnviarEmailDeletarDoacao(Doacao doacao, string nomeAutor)
        {
            var subject = $"Exclusão de Doação";
            var plainTextContent = $"Equipe: {doacao.Equipe.Nome}\nProduto: {doacao.Produto.Categoria}\nQuantidade: {doacao.Quantidade}{doacao.Produto.Unidade}\nData: {doacao.Data}\nAutor: {nomeAutor}";
            var msg = MailHelper.CreateSingleEmail(From, To, subject, plainTextContent, null);
            var response = await Client.SendEmailAsync(msg);
        }

        public async Task EnviarEmailNovaDoacao(Doacao doacao, string nomeAutor)
        {
            var subject = $"Nova Doação";
            var plainTextContent = $"Equipe: {doacao.Equipe.Nome}\nProduto: {doacao.Produto.Categoria}\nQuantidade: {doacao.Quantidade}{doacao.Produto.Unidade}\nData: {doacao.Data}\nAutor: {nomeAutor}";
            var msg = MailHelper.CreateSingleEmail(From, To, subject, plainTextContent, null);
            var response = await Client.SendEmailAsync(msg);
        }
    }
}
