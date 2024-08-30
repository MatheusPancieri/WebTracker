using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

public class EmailNotification
{
    public async Task CriarEmail(string destinatario, string newPrice)
    {
        // Cria a mensagem de e-mail
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Matheus", "pancierimatheus21@gmail.com"));
        message.To.Add(new MailboxAddress("", destinatario));
        message.Subject = "O preço do produto mudou!";

        // Corpo do e-mail com o novo preço
        var bodyMessage = new TextPart("html")
        {
            Text = $@"
                <h1>Produto em Promoção!</h1>
                <p>O preço do produto na sua lista de desejos mudou.</p>
                <p><strong>Agora o preço é: {newPrice}</strong></p>
                <p><a href=''>Confira agora!</a></p>
                <p>Visite nossa loja para mais detalhes.</p>"
        };
        message.Body = bodyMessage;

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("pancierimatheus21@gmail.com", "ghsl ezbe yyeh fgrb"); // Atualize para sua senha correta

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

    public async Task EnviarEmail(string newPrice)
    {
        // Pergunta ao usuário para qual e-mail ele quer enviar a notificação
        Console.Write("Digite o e-mail do destinatário: ");
        string emailDestinatario = Console.ReadLine();
        // Chama o método para enviar a notificação por e-mail com o e-mail capturado e o novo preço
        await CriarEmail(emailDestinatario, newPrice);
        Console.WriteLine("E-mail de notificação enviado!");
    }
}
