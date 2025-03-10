using Microsoft.AspNetCore.SignalR;
using Moq;
using SignalingService.Hubs;
using Xunit;

namespace SignalingService.test.Hubs
{
    public class SignalingHubTests
    {
        private readonly Mock<IHubCallerClients> _clientsMock;
        private readonly Mock<HubCallerContext> _hubContextMock;
        private readonly SignalingHub _hub;

        public SignalingHubTests()
        {
            // Mock dependencies
            _clientsMock = new Mock<IHubCallerClients>();
            _hubContextMock = new Mock<HubCallerContext>();

            // Initialize the hub with mock dependencies
            _hub = new SignalingHub
            {
                Clients = _clientsMock.Object,
                Context = _hubContextMock.Object
            };
        }

        [Fact]
        public async Task GetConnectionId_ShouldReturnConnectionId()
        {
            // Arrange
            string expectedConnectionId = "testconnectionid";
            _hubContextMock.Setup(ctx => ctx.ConnectionId).Returns(expectedConnectionId);

            // Act
            var connectionId = await _hub.GetConnectionId();

            // Assert
            Assert.Equal(expectedConnectionId, connectionId);
        }
    }
}
