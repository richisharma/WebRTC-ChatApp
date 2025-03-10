using MessagingService.Controllers;
using MessagingService.Data;
using MessagingService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

public class MessagesControllerTests
{
    private readonly MessagingDbContext _dbContext;
    private readonly MessagesController _controller;
    private readonly Mock<IHubContext<ChatHub>> _hubContextMock;
    private readonly Mock<IHubClients> _hubClientsMock;
    private readonly Mock<ISingleClientProxy> _singleClientProxyMock;

    public MessagesControllerTests()
    {
        // Setup in-memory database
        var options = new DbContextOptionsBuilder<MessagingDbContext>()
            .UseInMemoryDatabase(databaseName: "MessagingTestDb")
            .Options;

        _dbContext = new MessagingDbContext(options);

        // Setup SignalR Hub Mock
        _hubContextMock = new Mock<IHubContext<ChatHub>>();
        _hubClientsMock = new Mock<IHubClients>();
        _singleClientProxyMock = new Mock<ISingleClientProxy>();

        _hubContextMock.Setup(h => h.Clients).Returns(_hubClientsMock.Object);

        _hubClientsMock.Setup(clients => clients.Client(It.IsAny<string>())).Returns(_singleClientProxyMock.Object);

        _controller = new MessagesController(_dbContext, _hubContextMock.Object);
    }

    [Fact]
    public async Task SendMessage_ShouldReturnOk_WhenMessageIsValid()
    {
        // Arrange
        var message = new ChatMessage
        {
            Sender = "d8b8f4a7-2c2a-4582-8d29-e86823ef0b38",
            ReceiverId = "412ab19f-8c82-4ff3-9039-9efe4020fc4b",
            Message = "Hello!",
            RoomId = null // One-on-one chat
        };

        // Act
        var result = await _controller.SendMessage(message);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(await _dbContext.ChatMessages.FirstOrDefaultAsync(m => m.Sender == "d8b8f4a7-2c2a-4582-8d29-e86823ef0b38"));
        _hubClientsMock.Verify(clients => clients.Client(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task SendMessage_ShouldReturnBadRequest_WhenMessageIsNull()
    {
        // Act
        var result = await _controller.SendMessage(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task SendMessage_ShouldReturnBadRequest_WhenSenderOrReceiverIsMissing()
    {
        // Arrange
        var message = new ChatMessage
        {
            Sender = "d8b8f4a7-2c2a-4582-8d29-e86823ef0b38",
            ReceiverId = "",
            Message = "Hello!"
        };

        // Act
        var result = await _controller.SendMessage(message);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
