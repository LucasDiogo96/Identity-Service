using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        public bool ValidatePassword(string password)
        {
            if (!ValidatePasswordPattern(password))
                return false;

            return true;
        }

        public static bool Verify(string password, string hash)
        {
            return true;
        }

        private void GeneratePassword(string password)
        {
            Salt = "";

            Hash = "";
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