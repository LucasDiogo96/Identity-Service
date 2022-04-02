using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Domain.Extensions;
using Sample.Identity.Infra.Contracts;
using Sample.Identity.Infra.Models;

namespace Sample.Identity.Infra.Providers
{
    public class IdentityProvider : IIdentityProvider
    {
        private readonly AppSettings settings;

        public IdentityProvider(IOptions<AppSettings> settings)
        {
            this.settings = settings.Value;
        }

        public UserIdentity SignIn(User user)
        {
            UserIdentity identity = new UserIdentity(user, settings.TokenExpirationTime);

            identity.AccessToken = CreateToken(identity);
            identity.RefreshDate = DateTime.UtcNow.AddMinutes(settings.RefreshExpirationTime);

            return identity;
        }

        private string CreateToken(UserIdentity identity)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, identity.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.Nbf, identity.CreateDate.ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, identity.ExpiryDate.ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, identity.CreateDate.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),
                }),
                NotBefore = identity.CreateDate,
                Expires = identity.ExpiryDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.SecretKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}