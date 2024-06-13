using application.events;
using application.models;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Windows.Input;

namespace modules.buttons.ViewModels;
public sealed class ButtonsViewModel(IRegionManager regionManage, IEventAggregator eventAggregator) : RegionAwareViewModel(regionManage)
{
    private readonly RestartTheApplicationEvent _restartTheApplicationEvent = eventAggregator.GetEvent<RestartTheApplicationEvent>();
    private readonly ShutDownTheApplicationEvent _exitTheApplicationEvent = eventAggregator.GetEvent<ShutDownTheApplicationEvent>();
    private readonly RollbackUpdatesEvent _rollbackUpdatesEvent = eventAggregator.GetEvent<RollbackUpdatesEvent>();

    public ICommand ExitTheApplicationCommand => new DelegateCommand(_exitTheApplicationEvent.Publish);
    public ICommand ReloadApplicationCommand => new DelegateCommand(_restartTheApplicationEvent.Publish);
    public ICommand RollbackApplicationCommand => new DelegateCommand(_rollbackUpdatesEvent.Publish);

}
