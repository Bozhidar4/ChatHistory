using ChatHistory.Domain.Core;

namespace ChatHistory.Domain.Users
{
    public class User : Entity<int>
    {
        public string? Name { get; set; }

        public User(
            int id,
            string name) : base(id)
        {
            Name = name;
        }
    }
}
