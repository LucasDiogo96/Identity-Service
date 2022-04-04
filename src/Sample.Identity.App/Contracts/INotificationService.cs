namespace Sample.Identity.App.Contracts
{
    public interface INotificationService
    {
        public Task SendRecoverySms(string code, string phone);

        public Task SendRecoveryEmail(string code, string email, string name);
    }
}