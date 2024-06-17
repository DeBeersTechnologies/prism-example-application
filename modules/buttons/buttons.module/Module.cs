using application;
using buttons.menubar;
using buttons.services;
using buttons.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace buttons;

[Module(ModuleName = ModuleDescription.ModuleName)]
public sealed class Module(IRegionManager regionManager) : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        regionManager.RequestNavigate(ApplicationRegionNames.BottomRight, nameof(ButtonsView));
        //containerProvider.Resolve<MenubarConstructionService>();
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<ButtonsView>();

        containerRegistry.Register<IButtonsModuleService, ButtonsModuleService>();
        //                 .RegisterSingleton<MenubarConstructionService>();
    }
}