using application.models;
using modules.messaging;
using Prism.Regions;

namespace modules.messageView.ViewModels;
public sealed class ViewAViewModel : RegionAwareViewModel
{
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
