using messageView.ViewModels;
using messaging;
using Moq;
using Prism.Events;
using Prism.Regions;
using Xunit;

namespace modules.messageView.tests.ViewModels;

public class ViewAViewModelFixture
{
    Mock<IMessageService> _messageServiceMock;
    Mock<IRegionManager> _regionManagerMock;
    private Mock<IEventAggregator> _eventAggregatorMock;
    const string MessageServiceDefaultMessage = "Some Value";

    public ViewAViewModelFixture()
    {
        var messageService = new Mock<IMessageService>();
        messageService.Setup(x => x.GetMessage()).Returns(MessageServiceDefaultMessage);
        _messageServiceMock = messageService;

        _regionManagerMock = new Mock<IRegionManager>();
        _eventAggregatorMock = new Mock<IEventAggregator>();
    }

    [Fact]
    public void MessagePropertyValueUpdated()
    {
        var vm = new ViewAViewModel(_regionManagerMock.Object, _messageServiceMock.Object);

        _messageServiceMock.Verify(x => x.GetMessage(), Times.Once);

        Assert.Equal(MessageServiceDefaultMessage, vm.Message);
    }

    [Fact]
    public void MessageINotifyPropertyChangedCalled()
    {
        var model = new ViewAViewModel(_regionManagerMock.Object, _messageServiceMock.Object);
        Assert.PropertyChanged(model, nameof(model.Message), () => model.Message = "Changed");
    }
}
