using application;
using modules.messageView.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace modules.messageView;

[Module(ModuleName = "MessageViewModule")]
[ModuleDependency("MessagingModule")]
public sealed  class Module(IRegionManager regionManager) : IModule
{
    public void OnInitialized(IContainerProvider containerProvider) 
        => regionManager.RequestNavigate(ApplicationRegionNames.FullPageRegion, nameof(ViewA));

    public void RegisterTypes(IContainerRegistry containerRegistry) 
        => containerRegistry.RegisterForNavigation<ViewA>();
}