using module.loader.comparers;
using module.loader.factories;
using Prism.Ioc;
using Prism.Modularity;
using prism_application.core;
using module.loader.services;

namespace module.loader.modules;

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