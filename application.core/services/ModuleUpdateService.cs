using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using application.commands;
using application.events;
using Prism.Commands;
using Prism.Events;

namespace application.services;

public sealed class ModuleUpdateService : IModuleUpdateService
{
    private readonly IApplicationCommands _applicationCommands;
    private readonly IApplicationDirectoryService _applicationDirectoryService;
    private readonly ShutDownTheApplicationEvent _shutDownTheApplicationEventEvent;

    public ModuleUpdateService(IEventAggregator eventAggregator
                             , IApplicationDirectoryService applicationDirectoryService
                             , IApplicationCommands applicationCommands)
    {
        _applicationDirectoryService = applicationDirectoryService;
        _applicationCommands = applicationCommands;

        _applicationCommands.CloseApplicationGracefully.RegisterCommand(CommandCloseApplicationGracefully);

        _shutDownTheApplicationEventEvent = eventAggregator.GetEvent<ShutDownTheApplicationEvent>();
        eventAggregator.GetEvent<ApplyUpdatesEvent>().Subscribe(ApplyUpdates);
        eventAggregator.GetEvent<RestartTheApplicationEvent>().Subscribe(() =>
        {
            RestartRequired = true;
            AppExit();
        });
        eventAggregator.GetEvent<RollbackUpdatesEvent>().Subscribe(() =>
        {
            RestartRequired = true;
            Rollback();
            AppExit();
        });

        ApplyUpdates();
    }

    private DelegateCommand CommandCloseApplicationGracefully
        => new(() =>
        {
            _shutDownTheApplicationEventEvent.Publish();
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
        var updatesFolder = _applicationDirectoryService.UpdatesDirectory;
        var rollbackFolder = _applicationDirectoryService.RollbackDirectory;

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
        var modulesFolder = _applicationDirectoryService.ModulesDirectory;
        var updatesFolder = _applicationDirectoryService.UpdatesDirectory;
        var rollbackFolder = _applicationDirectoryService.RollbackDirectory;

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