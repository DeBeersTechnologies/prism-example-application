using Prism.Ioc;
using Prism.Modularity;

namespace modules.messaging;

[Module(ModuleName = "MessagingModule")]
public class Module : IModule
{
    public void OnInitialized(IContainerProvider containerProvider){ }

    public void RegisterTypes(IContainerRegistry containerRegistry)
        => containerRegistry.RegisterSingleton<IMessageService, MessageService>();
}
