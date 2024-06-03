using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using prism_application.core;
using prism_application.modules.module_one.Views;

namespace prism_application.modules.module_one
{
    public class Module(IRegionManager regionManager) : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RequestNavigate(ApplicationRegionNames.ContentRegion, "ViewA");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
        }
    }
}