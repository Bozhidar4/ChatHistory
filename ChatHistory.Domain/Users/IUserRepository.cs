using ChatHistory.Domain.Core;

namespace ChatHistory.Domain.Users
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
