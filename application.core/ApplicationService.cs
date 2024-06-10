using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using application.events;
using application.services;
using Prism.Events;

namespace application;

public sealed class ApplicationService : IApplicationService
{
    public ApplicationService(IEventAggregator eventAggregator)
    {
        eventAggregator.GetEvent<ShutDownTheApplicationEvent>()
                       .Subscribe(Application.Current.Shutdown, ThreadOption.UIThread);
    }

    public string CoreModulesDirectory => GetSubDirectory("modules");
    public string UpdatesDirectory => GetSubDirectory("updates");
    public string RollbackDirectory => GetSubDirectory("roll-back");

    public string GetRootPath()
        => Environment.CurrentDirectory;

    public string GetSubDirectory(params string[] directories)
    {
        var allDirectories = new List<string> { GetRootPath() };
        allDirectories.AddRange(directories);
        return Path.Join(allDirectories.ToArray());
    }
}