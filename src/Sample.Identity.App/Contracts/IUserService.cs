using Sample.Identity.Domain.Commands;
using Sample.Identity.Domain.Entities;

namespace Sample.Identity.App.Contracts
{
    public interface IUserService
    {
        public User Get(string id);

        public void Add(CreateUserCommand model);

        public void Update(UpdateUserCommand model);
    }
}