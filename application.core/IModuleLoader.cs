using Prism.Modularity;
using System.Collections.Generic;

namespace prism_application.core;

public interface IModuleLoader
{
    //IEnumerable<IModuleInfo> ModulesWithMissingDependencies { get; }

    //int LoadModule(in string filePath);
    int ScanAndLoadModules(in string path, bool doNotScanChildDirectories = false);

    void LoadModules(string path);
}