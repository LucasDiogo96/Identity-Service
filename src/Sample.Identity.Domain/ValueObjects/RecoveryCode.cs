namespace Sample.Identity.Domain.ValueObjects
{
    public class RecoveryCode
    {
        public RecoveryCode()
        { }

        public RecoveryCode(int minutes)
        {
            Identifier = Guid.NewGuid().ToString();
            CreatedOn = DateTime.UtcNow;
            Code = new Random().Next(0, 1000000).ToString("D6");
            ExpiresOn = CreatedOn.AddMinutes(minutes);
            ExpireTime = TimeSpan.FromMinutes(minutes);
            Active = true;
        }

        public string Identifier { get; set; }
        public string Code { get; set; }
        public TimeSpan ExpireTime { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public bool Active { get; set; }

        public void Verify()
        {
            VerifiedOn = DateTime.UtcNow;
        }

        public void Inactivate()
        {
            Active = false;
        }

        public bool Equals(string code)
        {
            return Code.Equals(code);
        }
    }
}