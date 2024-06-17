using application.menubar.Events;
using application.menubar.Factories;
using application.menubar.Services;
using application.menubar.ViewModels;
using application.menubar.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace application.menubar;

[Module(ModuleName = ModuleDescription.ModuleName)]
public class Menubar : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        var regionManager = containerProvider.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion(ApplicationRegionNames.ApplicationMenuBar, typeof(MenuBar));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IMenuRegistrar, MenuRegistrar>();
        containerRegistry.RegisterSingleton<IMenuInfoFactory, MenuInfo>();
        containerRegistry.RegisterSingleton<IMenuItemFactory, MenuItemFactory>();
        containerRegistry.RegisterSingleton<IMenuFacade, MenuFacade>();

        containerRegistry.RegisterSingleton<IMenuItemViewModelFactory, MenuItemViewModel>();
        containerRegistry.RegisterSingleton<IMenuVisibilityEventFacade, MenuVisibilityEventFacade>();
        containerRegistry.RegisterSingleton<MenuVisibilityEventArgs>();
        containerRegistry.RegisterSingleton<MenuService>();

        containerRegistry.Register<IMenuService, MenuService>();
        containerRegistry.Register<IMenuServiceInterpreter, MenuService>();
        containerRegistry.Register<IMenuVisibilityEventPublisher, MenuVisibilityEventArgs>();
        containerRegistry.Register<IMenuVisibilityEventSubscriber, MenuVisibilityEventArgs>();
    }
}