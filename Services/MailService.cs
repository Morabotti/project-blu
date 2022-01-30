using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Settings;

namespace ProjectBlu.Services;

public class MailService : IMailService
{
    private readonly string _domain;
    private readonly MailSettings _settings;
    private readonly ILogger _logger;

    private const string NAME = "Project-Blu";
    private const string TITLE_RESET_PASSWORD = "Reset password";

    private readonly Queue<MailRequest> _mailQueue = new Queue<MailRequest>();

    public MailService(IConfiguration configuration, ILogger<MailService> logger)
    {
        this._settings = configuration.GetSection("MailSettings").Get<MailSettings>();
        this._domain = configuration.GetValue<string>("Domain");
        this._logger = logger;
    }

    public void SendResetPasswordEmail(string email, string token)
    {
        // TODO: Better solution
        var body = $"Reset password: \n {_domain}/auth/reset?token={token}";

        var request = new MailRequest
        {
            Body = body,
            Recipient = email,
            Subject = $"{NAME} - {TITLE_RESET_PASSWORD}"
        };

        _logger.LogDebug("Adding reset password email to queue ({}).", email);
        _mailQueue.Enqueue(request);
    }

    public async Task SendMailsAsync()
    {
        if (_mailQueue.Count == 0)
        {
            return;
        }

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.Username, _settings.Password);

        foreach (MailRequest mail in _mailQueue)
        {
            MimeMessage message = BuildMessage(mail);
            await smtp.SendAsync(message);
        }

        _logger.LogInformation("Clearing email queue. Queue size: {}", _mailQueue.Count);
        _mailQueue.Clear();
        smtp.Disconnect(true);
    }

    private MimeMessage BuildMessage(MailRequest request)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(NAME, _settings.Sender));
        email.To.Add(MailboxAddress.Parse(request.Recipient));
        email.Subject = request.Subject;
        email.Body = new TextPart(TextFormat.Plain) { Text = request.Body };

        return email;
    }
}