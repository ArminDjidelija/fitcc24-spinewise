using MailKit.Net.Smtp;
using MimeKit;

namespace SpineWise.Web.Helpers.Email_sender
{
    public class EmailSenderService
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string subject, string body, string receiver, string receiverName="")
        {
            string fromMail = _configuration.GetValue<string>("EmailSettings:Email");
            string fromPassword = _configuration.GetValue<string>("EmailSettings:Password");
            string host = _configuration.GetValue<string>("EmailSettings:Host");

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("ImagineCup SpineWise NO-REPLY", fromMail));
            email.To.Add(new MailboxAddress(receiverName, receiver));

            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(host, 465, true);

                // Note: only needed if the SMTP server requires authentication
                await smtp.AuthenticateAsync(fromMail, fromPassword);

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
