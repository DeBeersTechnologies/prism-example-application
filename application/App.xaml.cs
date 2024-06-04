using System.IO;
using System.Windows;
using application.commands;
using application.module.updater;
using application.services;
using application.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IUpdaterService _updaterService;

        protected override Window CreateShell() 
            => (Window)Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
            containerRegistry.RegisterSingleton<IApplicationService, ApplicationService>();
            containerRegistry.RegisterSingleton<IUpdaterService, UpdaterService>();
            _updaterService = Container.Resolve<IUpdaterService>();
            _updaterService.ApplyUpdates();
        }

        protected override IModuleCatalog CreateModuleCatalog()
            => new DirectoryModuleCatalog() {  ModulePath = Directory.GetCurrentDirectory() };

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _updaterService.RestartIfNecessary();
        }

    }
}
