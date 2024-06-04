using System.Diagnostics;
using application.events;
using application.services;
using Prism.Events;

namespace application.module.updater;

public class UpdaterService : IUpdaterService
{
    private readonly IApplicationService _applicationService;

    public UpdaterService(IEventAggregator eventAggregator, IApplicationService applicationService)
    {
        _applicationService = applicationService;
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
        var updatesFolder = _applicationService.UpdatesDirectory;
        var rollbackFolder = _applicationService.RollbackDirectory;

        var rolls = Directory.GetFiles(rollbackFolder);
        foreach (var roll in rolls) 
        {
            var rollName = roll.Substring(roll.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            var fileName = $"{rollName.Substring(0, rollName.IndexOf('-'))}=rb";
            var rollbackLocation = Path.Combine(updatesFolder, fileName);

            File.Move(roll, rollbackLocation, true);            
        }

    }

    public void ApplyUpdates()
    {
        var modulesFolder = _applicationService.CoreModulesDirectory;
        var updatesFolder = _applicationService.UpdatesDirectory;
        var rollbackFolder = _applicationService.RollbackDirectory;

        if (!Directory.Exists(updatesFolder)) Directory.CreateDirectory(updatesFolder);
        if (!Directory.Exists(rollbackFolder)) Directory.CreateDirectory(rollbackFolder);

        var updates = Directory.GetFiles(updatesFolder);
        if (updates.Length > 0)
        {
            var isRollback = false;
            foreach (var update in updates)
            {
                if (update.EndsWith("*rb"))
                {
                    isRollback = true;
                }

                var fileName = isRollback 
                    ? update.Substring(update.LastIndexOf(Path.DirectorySeparatorChar) + 1, update.IndexOf('='))
                    : update.Substring(update.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                var originalFileLocation = Path.Combine(modulesFolder, fileName);
                var rollbackFileName = $"{fileName}-{DateTime.Now:O}".Replace(':', '-');
                var rollBackLocation = Path.Combine(rollbackFolder, rollbackFileName);
                

                if (!isRollback && File.Exists(originalFileLocation))
                    File.Copy(originalFileLocation, rollBackLocation);


                File.Move(update, originalFileLocation, true);
                isRollback = false;
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