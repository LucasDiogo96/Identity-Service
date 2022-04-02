using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.App.Transfers
{
    public class IdentitySignInTransfer
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Tenant { get; set; }
        public string IpAddress { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}