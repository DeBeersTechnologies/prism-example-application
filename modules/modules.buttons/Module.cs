using application;
using modules.buttons.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace modules.buttons;

[Module(ModuleName = "ButtonsModule")]
public sealed class Module(IRegionManager regionManager) : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
        => regionManager.RequestNavigate(ApplicationRegionNames.ButtonsRegion, nameof(ButtonsView));

    public void RegisterTypes(IContainerRegistry containerRegistry)
        => containerRegistry.RegisterForNavigation<ButtonsView>();
}
