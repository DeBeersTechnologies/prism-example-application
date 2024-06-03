using Prism.Ioc;
using Prism.Modularity;
using prism_application.services.service_one.interfaces;

namespace prism_application.services.service_one.Modules;

[Module(ModuleName = "ServiceOneModule")]
public class Module : IModule
{
    public void OnInitialized(IContainerProvider containerProvider){ }

    public void RegisterTypes(IContainerRegistry containerRegistry)
        => containerRegistry.RegisterSingleton<IMessageService, MessageService>();
}
