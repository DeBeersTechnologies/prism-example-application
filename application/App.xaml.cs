using System;
using System.Diagnostics;
using System.IO;
using Prism.Ioc;
using Prism.Modularity;
using prism_application.services.service_one.interfaces;
using prism_application.Views;
using System.Windows;
using prism_application.services.service_one;
using updater_service;

namespace prism_application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IUpdaterService _updaterService;
        internal bool RestartRequired { set; get; }

        protected override Window CreateShell()
        {
            _updaterService = Container.Resolve<IUpdaterService>();
            return Container.Resolve<MainWindow>();
        }

        private void CheckForAvailableUpdates()
        {
            var applicationFolder = Environment.CurrentDirectory;
            var updatesFolder = Path.Combine(applicationFolder, "updates");
            if (!Directory.Exists(updatesFolder)) return;

            var updates = Directory.GetFiles(updatesFolder);
            if (updates.Length > 0)
            {
                foreach (var update in updates)
                {
                    var fileName = update.Substring(update.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    var updateLocation = Path.Combine(applicationFolder, fileName);
                    File.Move(update, updateLocation, true);
                }
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            CheckForAvailableUpdates();
            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterSingleton<IUpdaterService, UpdaterService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<modules.module_one.Module> ();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (_updaterService.RestartRequired)
            {
                Process.Start(Environment.ProcessPath);
            }
        }
    }
}
