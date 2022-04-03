using Sample.Identity.App.Contracts;
using Sample.Identity.Domain.Commands;
using Sample.Identity.Domain.Contracts;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Domain.Enumerators;
using Sample.Identity.Infra.Contracts;

namespace Sample.Identity.App.Features
{
    public class UserService : IUserService
    {
        private readonly INotification notification;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork, INotification notification)
        {
            this.unitOfWork = unitOfWork;
            this.notification = notification;
        }

        public async Task<User> Get(string id)
        {
            return await unitOfWork.UserRepository.GetById(id);
        }

        public void Add(CreateUserCommand model)
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
        }

        public async Task Update(UpdateUserCommand model)
        {
            User user = await unitOfWork.UserRepository.GetById(model.Id);

            user.Update(model.FirstName, model.LastName, model.PhoneNumber, model.CultureCode);

            unitOfWork.UserRepository.Update(user);

            unitOfWork.Save();
        }
    }
}