using Newtonsoft.Json;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.App.Transfers
{
    public class IdentitySignInTransfer
    {
        [JsonIgnore]
        public string RemoteAddress { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}