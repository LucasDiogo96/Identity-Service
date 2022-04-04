namespace Sample.Identity.Infra.Services.Sendgrid.Models
{
    public class SendgridSettings
    {
        public string From { get; set; }
        public string Name { get; set; }
        public string SecretKey { get; set; }
    }
}