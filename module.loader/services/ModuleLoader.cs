using Prism.Modularity;
using prism_application.core;

namespace module.loader.services;

internal class ModuleLoader : IModuleLoader
{
    private readonly IModuleManager _moduleManager;
    private readonly IModuleCatalog _catalogOfLoadedModules;
    private readonly IModuleLocator _moduleLocator;

    private readonly List<IModuleInfo> _safeToLoadModules;
    private readonly List<IModuleInfo> _modulesWithMissingDependencies;

    public ModuleLoader(IModuleManager moduleManager, IModuleCatalog loadedCatalog, IModuleLocator moduleLocator)
    {
        _moduleManager = moduleManager;
        _catalogOfLoadedModules = loadedCatalog;
        _moduleLocator = moduleLocator;
        _modulesWithMissingDependencies = [];
        _safeToLoadModules = [];
    }

    public void LoadModules(string path)
    {
        FileAttributes fileAttributes;
        try { fileAttributes = File.GetAttributes(path); }
        catch { return; }

        if (fileAttributes.HasFlag(FileAttributes.Directory))
            ScanAndLoadModules(in path);
        else
            LoadModule(in path);
    }


    public IEnumerable<IModuleInfo> ModulesWithMissingDependencies => _modulesWithMissingDependencies;

    public int LoadModule(in string filePath)
    {
        _safeToLoadModules.Clear();
        _modulesWithMissingDependencies.Clear();
        var uri = new System.Uri(filePath).AbsoluteUri;
        var directory = new FileInfo(filePath).DirectoryName;
        var moduleInfos = _moduleLocator.ParseDirectoriesForModulesToLoad(in directory, false).Where(info => info.Ref == uri);

        ParseModuleCatalogForThoseWhichAreSafeToLoad(in moduleInfos);
        AddLoadableModulesToTheApplicationModuleCatalog();
        return _safeToLoadModules.Count;
    }

    public int ScanAndLoadModules(in string path, bool doNotScanChildDirectories = false)
    {
        _safeToLoadModules.Clear();
        _modulesWithMissingDependencies.Clear();
        var moduleInfos = _moduleLocator.ParseDirectoriesForModulesToLoad(in path, !doNotScanChildDirectories);
        ParseModuleCatalogForThoseWhichAreSafeToLoad(in moduleInfos);
        AddLoadableModulesToTheApplicationModuleCatalog();
        return _safeToLoadModules.Count;
    }

    private void ParseModuleCatalogForThoseWhichAreSafeToLoad(in IEnumerable<IModuleInfo> temporaryCatalog)
    {
        var notYetLoadedModules = NonLoadedModulesInThisCatalog(temporaryCatalog).ToArray();
        _modulesWithMissingDependencies.AddRange(WhichModulesHaveMissingDependencies(notYetLoadedModules));
        _safeToLoadModules.AddRange(notYetLoadedModules.Except(_modulesWithMissingDependencies));
    }

    private IEnumerable<IModuleInfo> NonLoadedModulesInThisCatalog(IEnumerable<IModuleInfo> catalog)
        => catalog
            .Where(moduleInfo
                => !_catalogOfLoadedModules.Modules
                    .Any(info => info.ModuleName.Equals(moduleInfo.ModuleName)));

    private IEnumerable<IModuleInfo> WhichModulesHaveMissingDependencies(IList<IModuleInfo> catalogReference)
        => catalogReference
            .Where(info
                => info.DependsOn.Count != 0
                   && info.DependsOn.Any(dependency
                       => _catalogOfLoadedModules.Modules.All(moduleInfo => moduleInfo.ModuleName != dependency)
                          && catalogReference.All(moduleInfo => moduleInfo.ModuleName != dependency)));

    private void AddLoadableModulesToTheApplicationModuleCatalog()
    {
        _safeToLoadModules.ForEach(moduleInfo => _catalogOfLoadedModules.AddModule(moduleInfo));
        _catalogOfLoadedModules.Initialize();
        _safeToLoadModules.ForEach(info => _moduleManager.LoadModule(info.ModuleName));
    }
}
