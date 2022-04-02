namespace Sample.Identity.Infra.Contracts
{
    public interface IEmailService
    {
        public Task SendAsync(string email, string subject, string message);
    }
}