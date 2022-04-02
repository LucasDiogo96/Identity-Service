namespace Sample.Identity.Domain.ValueObjects
{
    public class RecoveryCode
    {
        public RecoveryCode()
        {
            Identifier = Guid.NewGuid().ToString();
            CreatedOn = DateTime.UtcNow;
            Code = new Random().Next(0, 1000000).ToString("D6");
        }

        public void Verify()
        {
            VerifiedOn = DateTime.UtcNow;
        }

        public string Identifier { get; set; }
        public string Code { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? VerifiedOn { get; set; }
    }
}