using Prism.Ioc;
using Prism.Modularity;
using prism_application.services.service_one.interfaces;
using prism_application.Views;
using System.Windows;
using prism_application.services.service_one;

namespace prism_application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<modules.module_one.Module> ();
        }
    }
}
