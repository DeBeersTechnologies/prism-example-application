using System;
using System.Collections.Generic;
using System.IO;
using application.services;

namespace application;

public sealed class ApplicationService : IApplicationService
{
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