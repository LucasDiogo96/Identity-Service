using Sample.Identity.Domain.Common;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CultureCode { get; set; }
        public bool Trustable { get; set; }
        public bool Blocked { get; set; }
        public bool Active { get; set; }
        public string Tenant { get; set; }
        public int AccessFailedCount { get; set; }
        public Password Password { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }

        public void Update(string firstname, string lastname, string phoneNumber, string culture)
        {
            FirstName = firstname;
            LastName = lastname;
            PhoneNumber = phoneNumber;
            CultureCode = culture;
        }

        public void Block()
        {
            Blocked = true;
        }

        public void Unlock()
        {
            Blocked = false;
        }

        public void Inactivate()
        {
            Active = false;
        }

        public void ResetSignInAttempts()
        {
            AccessFailedCount = 0;
            LockoutEndDateUtc = null;
        }

        public void OnFailedSignInAttempt()
        {
            AccessFailedCount++;
        }
    }
}