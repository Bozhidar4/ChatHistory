using ChatHistory.Domain.Events;
using ChatHistory.Persistence;
using ChatHistory.Persistence.Repositories;
using FluentAssertions;
using Xunit;

namespace ChatHistory.Tests.Repositories
{
    public class EventRepositoryTests
    {
        public static void AddChatHistoryEntities(InMemoryDbContext<AppDbContext> context)
        {
            var users = DbContextGenerator.GenerateUsers();
            var eventTypes = DbContextGenerator.GenerateEventTypes();
            var events = DbContextGenerator.GenerateEvents();

            context.AddEntities(users);
            context.AddEntities(eventTypes);
            context.AddEntities(events);
        }

        [Theory]
        [InlineData("2022-12-06", 12)]
        [InlineData("2022-12-05", 3)]
        public void GetByDateAsync_Should_Return_All_Records_For_The_Give_Date_In_Ascending_Order(string date,
                                                                                                  int expectedRecords)
        {
            // Arrange
            InMemoryDbContext<AppDbContext> _context =
                new InMemoryDbContext<AppDbContext>("ChatHistoryDB_InMemory_Repo");
            AddChatHistoryEntities(_context);
            var dbContext = _context.GetDbContext();
            var eventRepository = new EventRepository(dbContext);
            var dateParsed = DateTime.Parse(date);

            // Act
            var result = eventRepository.GetByDateAsync(dateParsed).Result;

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().AllBeAssignableTo<Event>();
            result.Should().BeInAscendingOrder(c => c.DateTime);
            result.ToList().Count().Should().Be(expectedRecords);

            dbContext.Database.EnsureDeleted();
        }
    }
}
