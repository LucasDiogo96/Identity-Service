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

        public async Task<User> Get(string id)
        {
            return await unitOfWork.UserRepository.GetById(id);
        }

        public void Add(CreateUserCommand model)
        {
            User user = new User(
                model.FirstName,
                model.LastName,
                model.Username,
                model.Email,
                model.PhoneNumber,
                model.CultureCode,
                model.Password);

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