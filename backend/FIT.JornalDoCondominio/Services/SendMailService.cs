using FIT.JornalDoCondominio.Models;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace FIT.JornalDoCondominio.Services;

public class SendMailService
{
    public SendMailService(List<Assinatura> assinatura, Noticia noticia, CancellationToken ct = default)
    {
        foreach (var email in assinatura)
        {
            var mime = new MimeMessage();
            #region Inclusão do Alias / Remetente
            mime.From.Add(new MailboxAddress("Jornal do condomínio", "noreply@aprovapro.com.br"));
            #endregion

            #region Inclusão dos Destinatários

            mime.To.Add(MailboxAddress.Parse(email.Email.ToLower().Trim()));
            #endregion

            #region Definição do Assunto
            mime.Subject = "Nova notícia!";
            #endregion

            #region Inclusão do Html + Anexos
            var builder = new BodyBuilder();
            builder.HtmlBody = ObterHtmlNovaNoticia(noticia, email.Id);

            mime.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Timeout = 100000;

            var socketOptions = SecureSocketOptions.None;

            try
            {
                // Conecta (usa StartTLS para porta 587 normalmente)
                smtp.Connect("smtp.aprovapro.com.br", 587, socketOptions, ct);

                // Autentica se necessário
                if (!string.IsNullOrWhiteSpace("noreply@aprovapro.com.br"))
                    smtp.Authenticate("noreply@aprovapro.com.br", "oculteiASenha", ct);

                // Envia
                smtp.Send(mime, ct);

                // Desconecta com grace
                smtp.Disconnect(true, ct);
            }
            catch (Exception ex)
            {
            }

            #endregion
        }
    }

    private string ObterHtmlNovaNoticia(Noticia noticia, int assinaturaId)
    {
        var _htmlFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Htmls");
        var _filePath = Path.Combine(_htmlFolderPath, "novaPagina.html");

        using var render = new StreamReader(_filePath);

        var _htmlBody = render.ReadToEndAsync().Result;
        _htmlBody = _htmlBody.Replace("{{data.hora}}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        _htmlBody = _htmlBody.Replace("{{noticia.titulo}}", noticia.Titulo);
        _htmlBody = _htmlBody.Replace("{{noticia.mensagem}}", noticia.Texto);
        _htmlBody = _htmlBody.Replace("{{noticia.link}}", $"http://localhost:4200/noticias");
        _htmlBody = _htmlBody.Replace("{{noticia.link.cancelar.assinatura}}", $"http://localhost:4200/assinatura-cancelar/{assinaturaId}");

        return _htmlBody;
    }
}
