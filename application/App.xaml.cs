using System.IO;
using System.Windows;
using application.commands;
using application.services;
using application.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace application;
public partial class App
{
    protected override Window CreateShell()
        => Container.Resolve<MainWindow>();

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
        containerRegistry.RegisterSingleton<IApplicationService, ApplicationService>();
        containerRegistry.RegisterSingleton<IModuleUpdateService, ModuleUpdateService>();
        Container.Resolve<IModuleUpdateService>();
    }

    protected override IModuleCatalog CreateModuleCatalog()
        => new DirectoryModuleCatalog() { ModulePath = Directory.GetCurrentDirectory() };
}
