using Prism.Events;
using prism_application.core.events;
using System.Diagnostics;

namespace updater_service;

public class UpdaterService : IUpdaterService
{

    public UpdaterService(IEventAggregator eventAggregator)
    {
        eventAggregator.GetEvent<RestartApplicationEvent>().Subscribe(() =>
        {
            RestartRequired = true;
            eventAggregator.GetEvent<ShutDownTheApplication>().Publish();
        });

        eventAggregator.GetEvent<RollbackApplicationEvent>().Subscribe(() =>
        {
            RestartRequired = true;
            Rollback();
            eventAggregator.GetEvent<ShutDownTheApplication>().Publish();
        });
    }

    private void Rollback()
    {
        var applicationFolder = Environment.CurrentDirectory;
        var updatesFolder = Path.Combine(applicationFolder, "updates");
        var rollbackFolder = Path.Combine(applicationFolder, "roll-back");

        var rolls = Directory.GetFiles(rollbackFolder);
        foreach (var roll in rolls) 
        {
            var rollName = roll.Substring(roll.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            var fileName = rollName.Substring(0, rollName.IndexOf('l') + 2);
            var rollbackLocation = Path.Combine(updatesFolder, fileName);

            File.Move(roll, rollbackLocation, true);            
        }

    }

    public void CheckForAvailableUpdates()
    {
        var applicationFolder = Environment.CurrentDirectory;
        var modulesFolder = Path.Combine(applicationFolder, "modules");
        var updatesFolder = Path.Combine(applicationFolder, "updates");
        var rollbackFolder = Path.Combine(applicationFolder, "roll-back");

        if (!Directory.Exists(updatesFolder)) Directory.CreateDirectory(updatesFolder);
        if (!Directory.Exists(rollbackFolder)) Directory.CreateDirectory(rollbackFolder);

        var updates = Directory.GetFiles(updatesFolder);
        if (updates.Length > 0)
        {
            foreach (var update in updates)
            {
                var fileName = update.Substring(update.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                var originalFileLocation = Path.Combine(modulesFolder, fileName);
                var rollbackFileName = $"{fileName}-{DateOnly.FromDateTime(DateTime.Now).ToString().Replace('/', '-')}";
                var rollBackLocation = Path.Combine(rollbackFolder, rollbackFileName);

                File.Copy(originalFileLocation, rollBackLocation, true);
                File.Move(update, originalFileLocation, true);
            }
        }
    }

    public void RestartIfNecessary()
    {
        if (!RestartRequired) return;
        Process.Start(Environment.ProcessPath!);
    }

    private bool RestartRequired { get; set; }
}