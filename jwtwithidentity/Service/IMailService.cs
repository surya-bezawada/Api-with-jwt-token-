using jwtwithidentity.Model;

namespace jwtwithidentity.Service
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
