﻿using modules.messageView.ViewModels;
using modules.messaging;
using Moq;
using Prism.Events;
using Prism.Regions;
using Xunit;

namespace modules.messageView.tests.ViewModels
{
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
            var vm = new ViewAViewModel(_regionManagerMock.Object, _messageServiceMock.Object, _eventAggregatorMock.Object);

            _messageServiceMock.Verify(x => x.GetMessage(), Times.Once);

            Assert.Equal(MessageServiceDefaultMessage, vm.Message);
        }

        [Fact]
        public void MessageINotifyPropertyChangedCalled()
        {
            var vm = new ViewAViewModel(_regionManagerMock.Object, _messageServiceMock.Object, _eventAggregatorMock.Object);
            Assert.PropertyChanged(vm, nameof(vm.Message), () => vm.Message = "Changed");
        }
    }
}