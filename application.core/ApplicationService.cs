﻿using System.Collections.Generic;
using System.IO;

namespace prism_application.core;

public class ApplicationService : IApplicationService
{
    public string CoreModulesDirectory => GetSubDirectory("modules");

    public string GetRootPath()
        => Directory.GetCurrentDirectory();

    public string GetSubDirectory(params string[] directories)
    {
        var allDirectories = new List<string> { GetRootPath() };
        allDirectories.AddRange(directories);
        return Path.Join(allDirectories.ToArray());
    }
}