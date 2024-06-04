using System.ComponentModel;
using application.commands;
using application.events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace application.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IApplicationCommands _applicationCommands;

        private string _title = "Prism Application";

        public MainWindowViewModel(IEventAggregator eventAggregator, IApplicationCommands applicationCommands)
        {
            _applicationCommands = applicationCommands;
            CloseApplicationGracefully = new DelegateCommand(() => System.Windows.Application.Current.Shutdown());

            _applicationCommands.CloseApplicationGracefully.RegisterCommand(CloseApplicationGracefully);         
            eventAggregator.GetEvent<ShutDownTheApplication>().Subscribe(AppExit, ThreadOption.UIThread);
        }

        private DelegateCommand CloseApplicationGracefully { get; }

        private void AppExit()
        {
            var e = new CancelEventArgs();
            if (_applicationCommands.CloseApplicationGracefully.CanExecute(e))
                _applicationCommands.CloseApplicationGracefully.Execute(e);
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
