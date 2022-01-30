namespace ProjectBlu.Services.Interfaces;

public interface IMailService
{
    void SendResetPasswordEmail(string email, string token);
    Task SendMailsAsync();
}
