using System;
using System.Collections.Generic;
using System.IO;

namespace application.services;

public sealed class ApplicationDirectoryService : IApplicationDirectoryService
{
    public string ApplicationDirectory => Environment.CurrentDirectory;
    public string ModulesDirectory => GetSubDirectory("modules");
    public string RollbackDirectory => GetSubDirectory("roll-back");
    public string UpdatesDirectory => GetSubDirectory("updates");

    private string GetSubDirectory(params string[] directories)
    {
        var allDirectories = new List<string> { ApplicationDirectory };
        allDirectories.AddRange(directories);
        return Path.Join(allDirectories.ToArray());
    }
}