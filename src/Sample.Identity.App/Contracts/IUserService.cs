using Sample.Identity.App.Transfers.User;
using Sample.Identity.Domain.Commands;

namespace Sample.Identity.App.Contracts
{
    public interface IUserService
    {
        public Task<UserResponseTransfer> Get(string id);

        public Task Add(CreateUserCommand model);

        public Task Update(UpdateUserCommand model);
    }
}