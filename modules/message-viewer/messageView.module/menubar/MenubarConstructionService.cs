using application.menubar;
using messageView.services;

namespace messageView.menubar;

internal class MenubarConstructionService
{
    public MenubarConstructionService(IMenuInfoFactory infoFactory
                                    , IMenuRegistrar menuRegistrar
                                    , IMenuService menuService
                                    , IDisplayService displayService)
    {
        menuRegistrar.AddMenuItem(infoFactory.CreateMenuInfo("Message _Viewer",
                MessagingViewMenuItems.MessageViewerMenu,
                ApplicationMenuNames.ApplicationMenuBar)
            .And()
            .SetOwningModuleName(ModuleDescription.ModuleName));

        menuRegistrar.AddMenuItem(infoFactory.CreateMenuInfo("Show _Clock",
                MessagingViewMenuItems.ShowClockMenuItem,
                MessagingViewMenuItems.MessageViewerMenu,
                displayService.DisplayClock())
            .And()
            .SetOwningModuleName(ModuleDescription.ModuleName));

        menuRegistrar.AddMenuItem(infoFactory.CreateMenuInfo("Show _Message",
                MessagingViewMenuItems.ShowMessageMenuItem,
                MessagingViewMenuItems.MessageViewerMenu,
                displayService.DisplayMessage())
            .And()
            .SetOwningModuleName(ModuleDescription.ModuleName));

        menuService.DisplayMenuItemsForModule(ModuleDescription.ModuleName);
    }
}
