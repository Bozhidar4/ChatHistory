using ChatHistory.Api.Dtos;
using ChatHistory.Api.Services;
using ChatHistory.Domain.EventTypes;
using ChatHistory.Domain.Users;
using FluentAssertions;
using Xunit;

namespace ChatHistory.Tests.Services
{
    public class FormatDataServiceTests
    {
        [Theory]
        [InlineData(1, "", "12:57: Bob enters the room")]
        [InlineData(2, "", "12:57: Bob leaves")]
        [InlineData(3, "Test Comment", "12:57: Bob comments: 'Test Comment'")]
        [InlineData(4, "", "12:57: Bob high-fives Kate")]
        public void FormatDataContinuously_Should_Return_Results_Correctly(int eventType, string comment, string message)
        {
            var eventsMapped = new List<EventDto>
            {
                new EventDto(
                    1,
                    comment,
                    new DateTime(2022, 12, 5, 12, 57, 33),
                    eventType,
                    new EventType(1, EventTypeEnumDto.Enter.ToString()),
                    new User(1, "Bob"),
                    new User(2, "Kate"))
            };

            var formatDataService = new FormatDataService();

            // Act
            var result = formatDataService.FormatDataContinuously(eventsMapped);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().AllBeAssignableTo<string>();
            result.First().Should().Be(message);
        }

        [Theory]
        [InlineData(1, "", "12:00: 1 person entered")]
        [InlineData(2, "", "12:00: 1 person left")]
        [InlineData(3, "Test Comment", "12:00: 1 comment")]
        [InlineData(4, "", "12:00: 1 person high-fived")]
        public void FormatDataHourly_Should_Return_Results_Correctly(int eventType, string comment, string message)
        {
            var eventsMapped = new List<EventDto>
            {
                new EventDto(
                    1,
                    comment,
                    new DateTime(2022, 12, 5, 12, 57, 33),
                    eventType,
                    new EventType(1, EventTypeEnumDto.Enter.ToString()),
                    new User(1, "Bob"),
                    new User(2, "Kate"))
            };

            var formatDataService = new FormatDataService();

            // Act
            var result = formatDataService.FormatDataHourly(eventsMapped);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().AllBeAssignableTo<string>();
            result.First().Should().Be(message);
        }
    }
}
