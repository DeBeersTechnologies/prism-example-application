using application.events;
using application.models;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace buttons.ViewModels;
public sealed class ButtonsViewModel(IRegionManager regionManage, IEventAggregator eventAggregator) : RegionAwareViewModel(regionManage)
{
    private readonly RestartTheApplicationEvent _restartTheApplicationEvent = eventAggregator.GetEvent<RestartTheApplicationEvent>();
    private readonly RollbackUpdatesEvent _rollbackUpdatesEvent = eventAggregator.GetEvent<RollbackUpdatesEvent>();

    public DelegateCommand ReloadApplicationCommand => new(_restartTheApplicationEvent.Publish);
    public DelegateCommand RollbackApplicationCommand => new(_rollbackUpdatesEvent.Publish);

}
