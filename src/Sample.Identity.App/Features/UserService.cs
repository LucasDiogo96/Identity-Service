using Mapster;
using Sample.Identity.App.Contracts;
using Sample.Identity.Domain.Commands;
using Sample.Identity.Domain.Entities;
using Sample.Identity.Infra.Contracts;

namespace Sample.Identity.App.Features
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User Get(string id)
        {
            return unitOfWork.UserRepository.GetById(id);
        }

        public void Add(CreateUserCommand model)
        {
            User user = model.Adapt<User>();

            unitOfWork.UserRepository.Insert(user);

            unitOfWork.Save();
        }

        public void Update(UpdateUserCommand model)
        {
            User user = unitOfWork.UserRepository.GetById(model.Id);

            user.Update(model.FirstName, model.LastName, model.PhoneNumber, model.CultureCode);

            unitOfWork.UserRepository.Update(user);

            unitOfWork.Save();
        }
    }
}