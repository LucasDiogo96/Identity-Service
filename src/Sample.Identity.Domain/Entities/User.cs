using Sample.Identity.Domain.Common;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.Domain.Entities
{
    public class User : Entity
    {
        public User()
        {
            PasswordRecoveries = new List<RecoveryCode>();
        }

        public User(string firstName, string lastName, string userName, string email, string phoneNumber, string cultureCode, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            CultureCode = cultureCode;
            Password = new Password(password);
            Active = true;
            Tenant = "sample";
            PasswordRecoveries = new List<RecoveryCode>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string CultureCode { get; set; }
        public bool Trustable { get; set; }
        public bool Blocked { get; set; }
        public bool Active { get; set; }
        public string Tenant { get; set; }
        public int AccessFailedCount { get; set; }
        public Password Password { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public List<RecoveryCode> PasswordRecoveries { get; set; }

        public void Update(string firstname, string lastname, string phoneNumber, string culture, string password)
        {
            FirstName = firstname ?? FirstName;
            LastName = lastname ?? LastName;
            PhoneNumber = phoneNumber ?? PhoneNumber;
            CultureCode = culture ?? CultureCode;

            if (password != null)
                ChangePassword(password);
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

        public void ForgotPassword(RecoveryCode code)
        {
            // Verify recovery
            code.Verify();

            PasswordRecoveries.Add(code);
        }

        public void ChangePassword(string password)
        {
            Password = new Password(password);
        }

        public void ConfirmEmail()
        {
            EmailConfirmed = true;
            Trustable = true;
        }

        public void ConfirmPhone()
        {
            PhoneNumberConfirmed = true;
            Trustable = true;
        }
    }
}