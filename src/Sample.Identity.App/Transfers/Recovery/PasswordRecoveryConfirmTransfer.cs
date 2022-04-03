namespace Sample.Identity.App.Transfers.Recovery
{
    public class PasswordRecoveryConfirmTransfer
    {
        public string UserName { get; set; }
        public string ConfirmationCode { get; set; }
    }
}