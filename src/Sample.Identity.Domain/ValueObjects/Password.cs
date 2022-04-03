using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
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
            if (ValidatePasswordPattern(password))
                throw new InvalidDataException("Password requirements do not match the security patterns.");

            Salt = BCrypt.Net.BCrypt.GenerateSalt();

            Hash = BCrypt.Net.BCrypt.HashPassword(password, Salt);
        }

        /// <summary>
        /// The string must contain at least 1 lowercase alphabetical character
        /// The string must contain at least 1 uppercase alphabetical character
        /// The string must contain at least 1 numeric character
        /// The string must contain at least one special character
        /// The string must be eight characters or longer
        /// </summary>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        public static bool ValidatePasswordPattern(string password)
        {
            if (!string.IsNullOrWhiteSpace(password))
            {
                Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");

                return regex.IsMatch(password);
            }

            return false;
        }
    }
}