using Sample.Identity.App.Transfers.Recovery;
using Sample.Identity.Domain.ValueObjects;

namespace Sample.Identity.App.Contracts
{
    public interface IRecoveryService
    {
        public void SendRecoveryCode(PasswordRecoveryRequestTransfer model);

        public RecoveryCode ConfirmRecoveryCode(PasswordRecoveryConfirmTransfer model);

        public bool ChangePassword(PasswordRecoveryTransfer model);
    }
}