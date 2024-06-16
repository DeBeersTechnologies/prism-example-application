using application.module.loader.comparers;
using application.module.loader.factories;
using Prism.Modularity;

namespace application.module.loader.services;

internal sealed class ModuleLocator(IModuleInfoFactory moduleInfoFactory, IModuleInfoComparer moduleInfoComparer)
    : IModuleLocator
{
    private readonly List<IModuleInfo> _moduleList = [];

    public IEnumerable<IModuleInfo> ParseDirectoriesForModulesToLoad(in string path, bool scanChildDirectories = true)
    {
        _moduleList.Clear();
        _moduleList.AddRange(FindModulesToLoad(in path));

        if (scanChildDirectories)
            ParseChildDirectoriesForModulesToLoad(in path);

        return _moduleList.OrderBy(info => info.ModuleName).Reverse().Distinct(moduleInfoComparer);
    }

    private void ParseChildDirectoriesForModulesToLoad(in string path)
    {
        foreach (var directory in Directory.GetDirectories(path))
            ParseChildDirectoriesForModulesToLoad(in directory);

        _moduleList.AddRange(FindModulesToLoad(in path));
    }

    private IEnumerable<IModuleInfo> FindModulesToLoad(in string path)
    {
        var directory = new DirectoryInfo(path);
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        var moduleReflectionOnlyAssembly = loadedAssemblies.First(asm => asm.FullName == typeof(IModule).Assembly.FullName);
        var moduleType = moduleReflectionOnlyAssembly.GetType(typeof(IModule).FullName!);
        var modules = moduleInfoFactory.GetModuleInfos(in directory, moduleType!);
        var array = modules.ToArray();
        return array;
    }
}