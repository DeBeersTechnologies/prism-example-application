using application;
using messageView.menubar;
using messageView.services;
using messageView.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace messageView;

[Module(ModuleName = ModuleDescription.ModuleName)]
[ModuleDependency(ModuleDependencies.MessagingModule)]
[ModuleDependency(ModuleDependencies.MenuBar)]
[ModuleDependency(ModuleDependencies.TimekeeperModule)]
public sealed class Module() : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    //=> containerProvider.Resolve<MenubarConstructionService>();
    {
        var regionManager = containerProvider.Resolve<IRegionManager>();
        regionManager.RequestNavigate(ApplicationRegionNames.FullPageRegion, nameof(ClockView));
        regionManager.RequestNavigate(ApplicationRegionNames.TopRight, nameof(ViewA));

    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<ViewA>();
        containerRegistry.RegisterForNavigation<ClockView>();

        //containerRegistry.Register<MenubarConstructionService>()
        //                 .RegisterSingleton<IDisplayService, DisplayService>();

    }
}