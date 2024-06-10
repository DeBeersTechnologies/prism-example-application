using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using application.commands;
using application.events;
using application.services;
using Prism.Commands;
using Prism.Events;

namespace application;

public class ModuleUpdateService : IModuleUpdateService
{
    private readonly IApplicationService _applicationService;
    private readonly IApplicationCommands _applicationCommands;
    private readonly ShutDownTheApplication _shutDownTheApplicationEvent;

    public ModuleUpdateService(IEventAggregator eventAggregator
                             , IApplicationService applicationService
                             , IApplicationCommands applicationCommands)
    {
        _applicationService = applicationService;
        _applicationCommands = applicationCommands;
        _shutDownTheApplicationEvent = eventAggregator.GetEvent<ShutDownTheApplication>();
        eventAggregator.GetEvent<RestartApplicationEvent>().Subscribe(() =>
        {
            RestartRequired = true;
            AppExit();
        });

        eventAggregator.GetEvent<ApplyUpdatesEvent>().Subscribe(ApplyUpdates);

        eventAggregator.GetEvent<RollbackApplicationEvent>().Subscribe(() =>
        {
            RestartRequired = true;
            Rollback();
            AppExit();
        });

        _applicationCommands.CloseApplicationGracefully.RegisterCommand(CommandCloseApplicationGracefully);
        ApplyUpdates();
    }

    private DelegateCommand CommandCloseApplicationGracefully
        => new(() =>
        {
            _shutDownTheApplicationEvent.Publish();
            RestartIfNecessary();
        });

    private void AppExit()
    {
        var e = new CancelEventArgs();
        if (_applicationCommands.CloseApplicationGracefully.CanExecute(e))
            _applicationCommands.CloseApplicationGracefully.Execute(e);
    }

    private void Rollback()
    {
        var updatesFolder = _applicationService.UpdatesDirectory;
        var rollbackFolder = _applicationService.RollbackDirectory;

        var rolls = Directory.GetFiles(rollbackFolder);
        foreach (var roll in rolls)
        {
            var rollName = roll.Substring(roll.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            var fileName = $"{rollName.Substring(0, rollName.IndexOf('-'))}!rb";
            var rollbackLocation = Path.Combine(updatesFolder, fileName);

            File.Move(roll, rollbackLocation, true);
        }

    }

    private void ApplyUpdates()
    {
        var modulesFolder = _applicationService.CoreModulesDirectory;
        var updatesFolder = _applicationService.UpdatesDirectory;
        var rollbackFolder = _applicationService.RollbackDirectory;

        if (!Directory.Exists(updatesFolder)) Directory.CreateDirectory(updatesFolder);
        if (!Directory.Exists(rollbackFolder)) Directory.CreateDirectory(rollbackFolder);

        var updates = Directory.GetFiles(updatesFolder);
        if (updates.Length <= 0) return;

        var isRollback = false;
        foreach (var update in updates)
        {
            if (update.EndsWith("!rb"))
            {
                isRollback = true;
            }

            var fileName = isRollback
                ? update.Substring(update.LastIndexOf(Path.DirectorySeparatorChar) + 1,
                    update.LastIndexOf('!') - (update.LastIndexOf(Path.DirectorySeparatorChar) + 1))
                : update[(update.LastIndexOf(Path.DirectorySeparatorChar) + 1)..];
            var originalFileLocation = Path.Combine(modulesFolder, fileName);
            var rollbackFileName = $"{fileName}-{DateTime.Now:O}".Replace(':', '-');
            var rollBackLocation = Path.Combine(rollbackFolder, rollbackFileName);


            if (!isRollback && File.Exists(originalFileLocation))
                File.Copy(originalFileLocation, rollBackLocation);


            File.Move(update, originalFileLocation, true);
            isRollback = false;
        }
    }

    private void RestartIfNecessary()
    {
        if (!RestartRequired) return;
        Process.Start(Environment.ProcessPath!);
    }

    private bool RestartRequired { get; set; }
}