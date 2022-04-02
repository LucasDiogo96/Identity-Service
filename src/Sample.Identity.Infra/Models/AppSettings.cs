﻿namespace Sample.Identity.Infra.Models
{
    public class AppSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int TokenExpirationTime { get; set; }
        public int RefreshExpirationTime { get; set; }
        public bool UserLockoutEnabledByDefault { get; set; }
        public int DefaultAccountLockoutTimeSpan { get; set; }
        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }
    }
}