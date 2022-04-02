namespace Sample.Identity.Domain.Commands
{
    public class UpdateUserCommand
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string CultureCode { get; set; }
    }
}