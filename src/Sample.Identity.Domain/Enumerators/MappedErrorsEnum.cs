using System.ComponentModel;

namespace Sample.Identity.Domain.Enumerators
{
    public enum MappedErrorsEnum
    {
        [Description("The user name or password is incorrect.")]
        UsernameOrPasswordIncorrect = 1000,

        [Description("The user have been blocked.")]
        UserBlocked = 1001,

        [Description("The refresh token was expired.")]
        RefreshTokenExpired = 1002,

        [Description("The user was inactivated by account administrator.")]
        UserInactive = 1003,

        [Description("The old password doesn't match.")]
        ChangePasswordUnmatch = 1004,

        [Description("The verification code doesn't match.")]
        VerificationCodeUnmatch = 1005,

        [Description("The verification code was expired.")]
        VerificationCodeExpired = 1006,

        [Description("Password does not meet criteria.")]
        ChangePasswordCriteria = 1007,

        [Description("The terms of gpdr have already been accepted.")]
        GpdrAlreadyAccepted = 1008,

        [Description("Too many failed login attempts. Please try again in a few minutes.")]
        UserBlockedForManyFailedAttempts = 1009
    }
}