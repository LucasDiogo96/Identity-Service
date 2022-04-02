using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.Domain.Commands
{
    public class CreateUserCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CultureCode { get; set; }
        public string Password { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}