using application;
using modules.messageView.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace modules.messageView.modules;

[Module(ModuleName = "MessageViewModule")]
[ModuleDependency("MessagingModule")]
public class Module(IRegionManager regionManager) : IModule
{
    public void OnInitialized(IContainerProvider containerProvider) 
        => regionManager.RequestNavigate(ApplicationRegionNames.FullPageRegion, "ViewA");

    public void RegisterTypes(IContainerRegistry containerRegistry) 
        => containerRegistry.RegisterForNavigation<ViewA>();
}