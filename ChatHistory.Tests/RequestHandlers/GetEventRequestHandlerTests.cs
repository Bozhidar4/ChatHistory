using AutoMapper;
using ChatHistory.Api.Dtos;
using ChatHistory.Api.Requests;
using ChatHistory.Api.Requests.Handlers;
using ChatHistory.Api.Services.Interfaces;
using ChatHistory.Domain.Events;
using Moq;
using Xunit;

namespace ChatHistory.Tests.RequestHandlers
{
    public class GetEventRequestHandlerTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IEventRepository> _eventRepository;
        private readonly Mock<IFormatDataService> _formatDataService;

        public GetEventRequestHandlerTests() 
        {
            _mapper = new Mock<IMapper>();
            _eventRepository = new Mock<IEventRepository>();
            _formatDataService = new Mock<IFormatDataService>();
        }

        [Fact]
        public async Task Handle_Should_Call_GetByDateAsync_From_IEventRepository_Async()
        {
            // Arrange
            var sut = new GetEventRequestHandler(
                _mapper.Object,
                _eventRepository.Object,
                _formatDataService.Object);
            var date = new DateTime(2022, 12, 06);
            var aggregationLevel = AggregationLevelEnum.Continuously;

            // Act
            await sut.Handle(NewGetEventRequest(date, aggregationLevel), new CancellationToken());

            // Assert
            _eventRepository.Verify(r => r.GetByDateAsync(It.IsAny<DateTime>()), Times.Once);
        }

        [Theory]
        [InlineData(AggregationLevelEnum.Continuously)]
        [InlineData(AggregationLevelEnum.Hourly)]
        public async Task Handle_Should_Call_Correct_Aggregation_Level_Method_From_IFormatDataService_Async(
            AggregationLevelEnum aggregationLevel)
        {
            // Arrange
            var sut = new GetEventRequestHandler(
                _mapper.Object,
                _eventRepository.Object,
                _formatDataService.Object);
            var date = new DateTime(2022, 12, 06);

            // Act
            await sut.Handle(NewGetEventRequest(date, aggregationLevel), new CancellationToken());

            // Assert
            switch (aggregationLevel)
            {
                case AggregationLevelEnum.Continuously:
                    _formatDataService.Verify(r => r.FormatDataContinuously(
                        It.IsAny<IEnumerable<EventDto>>()), Times.Once);
                    break;
                case AggregationLevelEnum.Hourly:
                    _formatDataService.Verify(r => r.FormatDataHourly(
                        It.IsAny<IEnumerable<EventDto>>()), Times.Once);
                    break;
            }
        }

        [Fact]
        public async Task Handle_Should_Call_Map_From_IMapper_Async()
        {
            // Arrange
            var sut = new GetEventRequestHandler(
                _mapper.Object,
                _eventRepository.Object,
                _formatDataService.Object);
            var date = new DateTime(2022, 12, 06);
            var aggregationLevel = AggregationLevelEnum.Continuously;

            // Act
            await sut.Handle(NewGetEventRequest(date, aggregationLevel), new CancellationToken());

            // Assert
            _mapper.Verify(r => r.Map<IEnumerable<EventDto>>(
                It.IsAny<IEnumerable<Event>>()), Times.Once);
        }

        private GetEventRequest NewGetEventRequest(DateTime date, AggregationLevelEnum aggregationLevel)
        {
            return new GetEventRequest(date, aggregationLevel) { };
        }
    }
}
