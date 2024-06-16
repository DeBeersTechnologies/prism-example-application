using messaging.services;
using Prism.Ioc;
using Prism.Modularity;

namespace messaging;

[Module(ModuleName = "MessagingModule")]
public sealed class Module : IModule
{
    public void OnInitialized(IContainerProvider containerProvider) { }

    public void RegisterTypes(IContainerRegistry containerRegistry)
        => containerRegistry.RegisterSingleton<IMessageService, MessageService>();
}
