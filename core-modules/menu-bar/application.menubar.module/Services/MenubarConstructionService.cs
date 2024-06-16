using application.events;
using Prism.Commands;
using Prism.Events;

namespace application.menubar.Services;

internal class MenubarConstructionService
{
    public MenubarConstructionService(IMenuInfoFactory infoFactory, IEventAggregator eventAggregator, IMenuRegistrar menuRegistrar, IMenuService menuService)
    {
        menuRegistrar.AddMenuItem(infoFactory.CreateMenuInfo("_Application",
                ApplicationMenuNames.ApplicationMenu,
                ApplicationMenuNames.ApplicationMenuBar)
            .And()
            .SetOwningModuleName(ModuleDescription.ModuleName));

        menuRegistrar.AddMenuItem(infoFactory.CreateMenuInfo("E_xit",
                "ExitButton",
                ApplicationMenuNames.ApplicationMenu,
                new DelegateCommand(() => eventAggregator.GetEvent<ShutDownTheApplicationEvent>().Publish(), () => true))
            .And()
            .SetOwningModuleName(ModuleDescription.ModuleName));

        menuService.DisplayMenuItemsForModule(ModuleDescription.ModuleName);
    }
}