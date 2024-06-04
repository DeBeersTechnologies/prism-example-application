using application.module.loader.comparers;
using application.module.loader.factories;
using application.module.loader.services;
using application.services;
using Prism.Ioc;
using Prism.Modularity;

namespace application.module.loader.modules;

[Module(ModuleName = "CoreModuleLoadingModule")]
public class ModuleLoadingModule : IModule
{
    public void OnInitialized(IContainerProvider containerProvider) =>
        containerProvider.Resolve<IModuleLoader>()
                         .ScanAndLoadModules(containerProvider.Resolve<IApplicationService>().CoreModulesDirectory);

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IModuleInfoComparer, ModuleInfoComparer>();

        containerRegistry.Register<IModuleLoader, ModuleLoader>();
        containerRegistry.Register<IModuleLocator, ModuleLocator>();
        containerRegistry.Register<IModuleInfoFactory, ModuleInfoFactory>();
    }
}