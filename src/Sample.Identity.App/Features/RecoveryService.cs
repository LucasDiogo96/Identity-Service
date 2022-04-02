using Microsoft.Extensions.Logging;
using Sample.Identity.App.Contracts;
using Sample.Identity.Domain.Contracts;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Domain.ValueObjects;
using Sample.Identity.Infra.Contracts;

namespace Sample.Identity.App.Features
{
    public class RecoveryService : IRecoveryService
    {
        private readonly ISmsService smsService;
        private readonly IEmailService emailService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserDomainService domainService;
        private readonly ILogger<RecoveryService> logger;

        public RecoveryService(ISmsService smsService, IEmailService emailService, IUnitOfWork unitOfWork, IUserDomainService domainService, ILogger<RecoveryService> logger)
        {
            this.smsService = smsService;
            this.emailService = emailService;
            this.unitOfWork = unitOfWork;
            this.domainService = domainService;
            this.logger = logger;
        }
    }
}