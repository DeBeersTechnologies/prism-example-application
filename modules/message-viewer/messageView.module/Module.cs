using messageView.menubar;
using messageView.services;
using messageView.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace messageView;

[Module(ModuleName = ModuleDescription.ModuleName)]
[ModuleDependency(ModuleDependencies.MessagingModule)]
[ModuleDependency(ModuleDependencies.MenuBar)]
[ModuleDependency(ModuleDependencies.TimekeeperModule)]
public sealed class Module() : IModule
{
    public void OnInitialized(IContainerProvider containerProvider) 
        => containerProvider.Resolve<MenubarConstructionService>();

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<ViewA>();
        containerRegistry.RegisterForNavigation<ClockView>();

        containerRegistry.RegisterSingleton<MenubarConstructionService>()
                         .RegisterSingleton<IDisplayService, DisplayService>();       

    }
}