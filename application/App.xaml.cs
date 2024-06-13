using System.IO;
using System.Windows;
using application.commands;
using application.services;
using application.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace application;
public sealed partial class App
{
    protected override Window CreateShell()
        => Container.Resolve<MainWindow>();

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>()
                         .RegisterSingleton<IApplicationShutdownService, ApplicationShutdownService>()
                         .RegisterSingleton<IModuleUpdateService, ModuleUpdateService>()
                         .Register<IApplicationDirectoryService, ApplicationDirectoryService>();

        Container.Resolve<IApplicationShutdownService>();
        Container.Resolve<IModuleUpdateService>();
    }

    protected override IModuleCatalog CreateModuleCatalog()
        => new DirectoryModuleCatalog() { ModulePath = Directory.GetCurrentDirectory() };
}
