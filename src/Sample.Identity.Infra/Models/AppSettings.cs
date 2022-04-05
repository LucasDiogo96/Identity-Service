namespace Sample.Identity.Infra.Models
{
    public class AppSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public int TokenExpirationTime { get; set; }
        public int RefreshExpirationTime { get; set; }
        public bool UserLockoutEnabledByDefault { get; set; }
        public int AccountLockoutTimeSpan { get; set; }
        public int MaxFailedAttempts { get; set; }
        public int PasswordRecoveryTimespan { get; set; }
    }
}