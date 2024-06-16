using application;
using messageView.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace messageView;

[Module(ModuleName = "MessageViewModule")]
[ModuleDependency("MessagingModule")]
[ModuleDependency("TimekeeperModule")]
public sealed class Module(IRegionManager regionManager) : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        regionManager.RequestNavigate(ApplicationRegionNames.FullPageRegion, nameof(ViewA));
        regionManager.RequestNavigate(ApplicationRegionNames.BottomLeft, nameof(ClockView));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<ViewA>();
        containerRegistry.RegisterForNavigation<ClockView>();
    }
}