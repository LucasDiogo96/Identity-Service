namespace Sample.Identity.Domain.Events
{
    public class UserAddedEvent
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CultureCode { get; set; }
    }
}