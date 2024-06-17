using application.events;
using Prism.Commands;
using Prism.Events;

namespace buttons.services;

    internal class ButtonsModuleService(IEventAggregator eventAggregator) : IButtonsModuleService
    {
        public DelegateCommand ExitTheApplication()
            => new(eventAggregator.GetEvent<ShutDownTheApplicationEvent>().Publish);
        public DelegateCommand ReloadTheApplication() 
            => new(eventAggregator.GetEvent<RestartTheApplicationEvent>().Publish);
        public DelegateCommand RollbackUpdates()
            => new(eventAggregator.GetEvent<RollbackUpdatesEvent>().Publish);

}