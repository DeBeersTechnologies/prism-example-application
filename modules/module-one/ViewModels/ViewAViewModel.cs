using System;
using System.IO;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using prism_application.core.events;
using prism_application.core.Mvvm;
using prism_application.services.service_one.interfaces;

namespace prism_application.modules.module_one.ViewModels
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
