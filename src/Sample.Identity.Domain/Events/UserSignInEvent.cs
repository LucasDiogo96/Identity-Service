using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.Domain.Events
{
    public class UserSignInEvent
    {
        public UserSignInEvent(string userId, string username, DateTime createDate, DateTime expiryDate, Coordinates coordinates)
        {
            UserId = userId;
            Username = username;
            CreateDate = createDate;
            ExpiryDate = expiryDate;
            Coordinates = coordinates;
        }

        public string UserId { get; set; }
        public string Username { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}