using Sample.Identity.Domain.Commands;
using Sample.Identity.Domain.Entities;

namespace Sample.Identity.App.Contracts
{
    public interface IUserService
    {
        public Task<User> Get(string id);

        public void Add(CreateUserCommand model);

        public Task Update(UpdateUserCommand model);
    }
}