using Mapster;
using MassTransit;
using Sample.Identity.App.Contracts;
using Sample.Identity.App.Transfers.User;
using Sample.Identity.Domain.Commands;
using Sample.Identity.Domain.Contracts;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Domain.Enumerators;
using Sample.Identity.Domain.Events;
using Sample.Identity.Infra.Contracts;

namespace Sample.Identity.App.Features
{
    public class UserService : IUserService
    {
        private readonly IPublishEndpoint publisher;
        private readonly INotification notification;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork, INotification notification, IPublishEndpoint publisher)
        {
            this.unitOfWork = unitOfWork;
            this.notification = notification;
            this.publisher = publisher;
        }

        public async Task<UserResponseTransfer> Get(string id)
        {
            User user = await unitOfWork.UserRepository.GetById(id);

            // Map it to not return sensitive data
            return user.Adapt<UserResponseTransfer>();
        }

        public async Task Add(CreateUserCommand model)
        {
            bool exists = unitOfWork.UserRepository.Get(e => e.UserName == model.Username).Any();

            // Check if user exists
            if (exists)
            {
                notification.AddNotification(MappedErrorsEnum.UserNameAlreadyInUse);

                return;
            }

            User user = new User(model.FirstName, model.LastName, model.Username, model.Email, model.PhoneNumber, model.CultureCode, model.Password);

            unitOfWork.UserRepository.Insert(user);

            unitOfWork.Save();

            // Raise event
            UserAddedEvent @event = user.Adapt<UserAddedEvent>();

            await publisher.Publish<UserAddedEvent>(@event);
        }

        public async Task Update(UpdateUserCommand model)
        {
            User user = await unitOfWork.UserRepository.GetById(model.Id);

            user.Update(model.FirstName, model.LastName, model.PhoneNumber, model.CultureCode, model.Password);

            unitOfWork.UserRepository.Update(user);

            unitOfWork.Save();
        }
    }
}