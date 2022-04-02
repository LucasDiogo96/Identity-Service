namespace Sample.Identity.Domain.Events
{
    public class UserAuthenticated
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}