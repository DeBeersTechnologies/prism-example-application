using Prism.Modularity;

namespace application.module.loader.services;

internal interface IModuleLocator
{
    IEnumerable<IModuleInfo> ParseDirectoriesForModulesToLoad(in string path, bool scanChildDirectories = true);
}
