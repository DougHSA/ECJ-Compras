using SendGrid.Helpers.Mail;
using SendGrid;
using ECJ_Compras.Services.Interfaces;

namespace ECJ_Compras.Services
{
    public class EmailService:IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarEmail()
        {
            var apiKey = _config.GetSection("Smtp").GetSection("Key").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("doughsa97@gmail.com", "EJC-Compras");
            var subject = "Nova transação de Entrada";
            var to = new EmailAddress("doughsa97@gmail.com", "Douglas");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
