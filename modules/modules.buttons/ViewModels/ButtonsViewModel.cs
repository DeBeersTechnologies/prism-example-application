using application.events;
using application.models;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Windows.Input;

namespace modules.buttons.ViewModels
{
    public class ButtonsViewModel : RegionAwareViewModel
    {
        private readonly RestartTheApplicationEvent _restartTheApplicationEvent;
        private readonly ShutDownTheApplicationEvent _exitTheApplicationEvent;
        private readonly RollbackUpdateEvent _rollbackUpdateEvent;

        public ButtonsViewModel(IRegionManager regionManage
                              , IEventAggregator eventAggregator) : base(regionManage)
        {
            _restartTheApplicationEvent = eventAggregator.GetEvent<RestartTheApplicationEvent>();
            _exitTheApplicationEvent = eventAggregator.GetEvent<ShutDownTheApplicationEvent>();
            _rollbackUpdateEvent = eventAggregator.GetEvent<RollbackUpdateEvent>();
        }

        public ICommand ExitTheApplicationCommand => new DelegateCommand(_exitTheApplicationEvent.Publish);
        public ICommand ReloadApplicationCommand => new DelegateCommand(_restartTheApplicationEvent.Publish);
        public ICommand RollbackApplicationCommand => new DelegateCommand(_rollbackUpdateEvent.Publish);

    }
}
