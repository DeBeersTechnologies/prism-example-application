using application.events;
using application.models;
using modules.messaging;
using Prism.Regions;

namespace modules.messageView.ViewModels;
public class ViewAViewModel : RegionAwareViewModel
{
    //private readonly RestartTheApplicationEvent _restartTheApplicationEvent;
    //private readonly RollbackUpdateEvent _rollbackUpdateEvent;
    private string _message;

    public ViewAViewModel(IRegionManager regionManager, IMessageService messageService) : base(regionManager) 
        => Message = messageService.GetMessage();

    public override void OnNavigatedTo(NavigationContext navigationContext) { }

    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }
}
