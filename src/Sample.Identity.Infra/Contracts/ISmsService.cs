namespace Sample.Identity.Infra.Contracts
{
    public interface ISmsService
    {
        public Task SendAsync(string phone, string message);
    }
}