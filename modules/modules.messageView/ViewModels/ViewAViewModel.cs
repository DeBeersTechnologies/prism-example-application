using System.Windows.Input;
using application.events;
using application.models;
using modules.messaging;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace modules.messageView.ViewModels;
public class ViewAViewModel : RegionAwareViewModel
{
    private readonly RestartTheApplicationEvent _restartTheApplicationEvent;
    private readonly RollbackUpdateEvent _rollbackUpdateEvent;
    private string _message;

    public ViewAViewModel(IRegionManager regionManager
                        , IEventAggregator eventAggregator
                        , IMessageService messageService) : base(regionManager)
    {
        Message = messageService.GetMessage();
        _restartTheApplicationEvent = eventAggregator.GetEvent<RestartTheApplicationEvent>();
        _rollbackUpdateEvent = eventAggregator.GetEvent<RollbackUpdateEvent>();
    }

    public override void OnNavigatedTo(NavigationContext navigationContext) { }

    public ICommand ReloadApplicationCommand => new DelegateCommand(_restartTheApplicationEvent.Publish);
    public ICommand RollbackApplicationCommand => new DelegateCommand(_rollbackUpdateEvent.Publish);

    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }
}
