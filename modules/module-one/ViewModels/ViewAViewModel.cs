using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
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

        public ViewAViewModel(IRegionManager regionManager, IMessageService messageService, IEventAggregator eventAggregator) :
            base(regionManager)
        {
            Message = messageService.GetMessage();
            ReloadApplicationCommand = new ActionCommand(_ => eventAggregator.GetEvent<RestartApplicationEvent>().Publish());
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
