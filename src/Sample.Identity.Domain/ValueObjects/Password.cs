using System.Text.RegularExpressions;
using Sample.Identity.Domain.Common;

namespace Sample.Identity.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        public Password()
        { }

        public Password(string password)
        {
            GeneratePassword(password);
        }

        public string Salt { get; set; }
        public string Hash { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Salt;
            yield return Hash;
        }

        public bool Verify(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, this.Hash);
        }

        private void GeneratePassword(string password)
        {
            if (!ValidatePasswordPattern(password))
                throw new InvalidDataException("Password requirements do not match the security patterns.");

            Salt = BCrypt.Net.BCrypt.GenerateSalt();

            Hash = BCrypt.Net.BCrypt.HashPassword(password, Salt);
        }

        public static bool ValidatePasswordPattern(string password)
        {
            if (!string.IsNullOrWhiteSpace(password))
            {
                Regex regex = new Regex(GetPattern());

                return regex.IsMatch(password);
            }

            return false;
        }

        /// <summary>
        /// The string must contain at least 1 lowercase alphabetical character
        /// The string must contain at least 1 uppercase alphabetical character
        /// The string must contain at least 1 numeric character
        /// The string must contain at least one special character
        /// The string must be eight characters or longer
        /// </summary>
        /// <returns>regex pattern</returns>
        public static string GetPattern()
        {
            return @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})";
        }
    }
}