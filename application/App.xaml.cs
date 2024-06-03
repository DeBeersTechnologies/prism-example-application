using Prism.Ioc;
using Prism.Modularity;
using prism_application.core.events;
using prism_application.Views;
using System;
using System.IO;
using System.Windows;
using updater_service;
using prism_application.core;

namespace prism_application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IUpdaterService _updaterService;

        protected override Window CreateShell() 
            => Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
            containerRegistry.RegisterSingleton<IApplicationService, ApplicationService>();
            containerRegistry.RegisterSingleton<IUpdaterService, UpdaterService>();
            _updaterService = Container.Resolve<IUpdaterService>();
            _updaterService.CheckForAvailableUpdates();
        }

        protected override IModuleCatalog CreateModuleCatalog()
            => new DirectoryModuleCatalog() {  ModulePath = Directory.GetCurrentDirectory() };

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _updaterService.RestartIfNecessary();
        }

    }
}
