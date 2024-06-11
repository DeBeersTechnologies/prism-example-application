using System.Windows;
using application.events;
using Prism.Events;

namespace application.services;

public sealed class ApplicationShutdownService : IApplicationShutdownService
{
    public ApplicationShutdownService(IEventAggregator eventAggregator)
    {
        eventAggregator.GetEvent<ShutDownTheApplicationEvent>()
            .Subscribe(Application.Current.Shutdown, ThreadOption.UIThread);
    }
}