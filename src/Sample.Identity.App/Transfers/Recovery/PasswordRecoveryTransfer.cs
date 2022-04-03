namespace Sample.Identity.App.Transfers.Recovery
{
    public class PasswordRecoveryTransfer
    {
        public string UserName { get; set; }
        public string RecoveryId { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}