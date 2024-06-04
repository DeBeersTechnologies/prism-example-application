using System.Windows.Input;
using application.events;
using application.models;
using modules.messaging;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace modules.messageView.ViewModels
{
    public class ViewAViewModel : RegionViewModelBase
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ICommand ReloadApplicationCommand { get; }
        public ICommand RollbackApplicationCommand { get; }

        public ViewAViewModel(IRegionManager regionManager, IMessageService messageService, IEventAggregator eventAggregator) :
            base(regionManager)
        {
            Message = messageService.GetMessage();
            ReloadApplicationCommand = new DelegateCommand(() => eventAggregator.GetEvent<RestartApplicationEvent>().Publish());
            RollbackApplicationCommand = new DelegateCommand(() => eventAggregator.GetEvent<RollbackApplicationEvent>().Publish());
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
