using ChatHistory.Domain.Events;
using ChatHistory.Domain.EventTypes;
using ChatHistory.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatHistory.Persistence
{
    public class DbContextGenerator
    {
        public static void Initialize(IServiceCollection serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.BuildServiceProvider()
                .GetRequiredService<DbContextOptions<AppDbContext>>());

            if (context.Users.Any()) return;

            SeedData(context);
        }

        public static void SeedData(AppDbContext context)
        {
            var users = new List<User>
            {
                new User(1, "Bob"),
                new User(2, "Kate"),
                new User(3, "Tom"),
                new User(4, "Anna")
            };

            var eventTypes = new List<EventType>
            {
                new EventType(1, "Enter"),
                new EventType(2, "Leave"),
                new EventType(3, "Comment"),
                new EventType(4, "HighFive")
            };

            var employees = new List<Event>
            {
                new Event(1, string.Empty, new DateTime(2022, 12, 5, 13, 27, 33), 1, 1, null),
                new Event(2, string.Empty, new DateTime(2022, 12, 6, 14, 28, 01), 1, 2, null),
                new Event(3, "Hey, Kate - high five?", new DateTime(2022, 12, 6, 13, 28, 47), 3, 1, null),
                new Event(4, string.Empty, new DateTime(2022, 12, 6, 14, 29, 05), 4, 2, 1),
                new Event(5, string.Empty, new DateTime(2022, 12, 6, 14, 29, 20), 1, 3, null),
                new Event(6, string.Empty, new DateTime(2022, 12, 6, 14, 29, 38), 1, 4, null),
                new Event(7, "Hello there, is everyone okay?", new DateTime(2022, 12, 6, 14, 30, 19), 3, 4, null),
                new Event(8, "Everything is fine here.", new DateTime(2022, 12, 6, 14, 30, 59), 3, 3, null),
                new Event(9, string.Empty, new DateTime(2022, 12, 6, 14, 31, 04), 4, 1, 4),
                new Event(10, string.Empty, new DateTime(2022, 12, 6, 14, 31, 25), 2, 2, null),
                new Event(11, string.Empty, new DateTime(2022, 12, 6, 14, 31, 44), 2, 1, null),
                new Event(12, string.Empty, new DateTime(2022, 12, 6, 14, 32, 18), 2, 3, null),
                new Event(13, "Where is everybody?", new DateTime(2022, 12, 6, 14, 33, 00), 3, 4, null),
                new Event(14, string.Empty, new DateTime(2022, 12, 6, 14, 33, 15), 2, 4, null)
            };

            context.Users.AddRange(users);
            context.EventTypes.AddRange(eventTypes);
            context.Events.AddRange(employees);

            context.SaveChanges();
        }
    }
}
