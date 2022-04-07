namespace Sample.Identity.Domain.Events
{
    public class UserSignInEvent
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}