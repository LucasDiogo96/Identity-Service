namespace Sample.Identity.App.Transfers
{
    public class IdentityTransferModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime RefreshDate { get; set; }
    }
}