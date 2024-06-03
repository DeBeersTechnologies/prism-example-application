using System.Windows;
using Prism.Events;
using prism_application.core.events;

namespace updater_service;

public class UpdaterService : IUpdaterService
{
    public UpdaterService(IEventAggregator eventAggregator)
    {
        eventAggregator.GetEvent<RestartApplicationEvent>().Subscribe(() =>
        {
            RestartRequired = true;
            Application.Current.Shutdown();
        });
    }

    public bool RestartRequired { get; set; }
}