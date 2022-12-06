using ChatHistory.Domain.Users;
using ChatHistory.Persistence.Core;

namespace ChatHistory.Persistence.Repositories
{
    public class UserRepository :
        RepositoryBase<User, AppDbContext, int>,
        IUserRepository
    {
        public UserRepository(AppDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
