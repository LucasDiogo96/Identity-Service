using Sample.Identity.Domain.Entities;

namespace Sample.Identity.Infra.Models
{
    public class UserIdentity
    {
        public UserIdentity()
        { }

        public UserIdentity(User user, int tokenExpirationTime)
        {
            UserId = user.Id;
            Username = user.UserName;
            CreateDate = DateTime.UtcNow;
            RefreshToken = Guid.NewGuid().ToString();
            ExpiryDate = CreateDate.AddMinutes(tokenExpirationTime);
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime RefreshDate { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }

        public bool ValidateRefreshToken(string accessToken, string refreshToken, string userId)
        {
            return AccessToken.Equals(accessToken) &&
                   RefreshToken.Equals(refreshToken) &&
                   UserId == userId;
        }
    }
}