
using application.menubar;
using buttons.services;

namespace buttons.menubar;

internal class MenubarConstructionService
{
    public MenubarConstructionService(IMenuInfoFactory infoFactory
                                    , IMenuRegistrar menuRegistrar
                                    , IMenuService menuService
                                    , IButtonsModuleService buttonsService)
    {
        menuRegistrar.AddMenuItem(infoFactory.CreateMenuInfo("_Application",
                ButtonModuleMenuItems.ButtonsModuleMenu,
                ApplicationMenuNames.ApplicationMenuBar)
            .And()
            .SetOwningModuleName(ModuleDescription.ModuleName));

        menuRegistrar.AddMenuItem(infoFactory.CreateMenuInfo("E_xit",
                ButtonModuleMenuItems.ExitApplicationMenuItem,
                ButtonModuleMenuItems.ButtonsModuleMenu,
                buttonsService.ExitTheApplication())
            .And()
            .SetOwningModuleName(ModuleDescription.ModuleName));

        menuService.DisplayMenuItemsForModule(ModuleDescription.ModuleName);
    }
}
