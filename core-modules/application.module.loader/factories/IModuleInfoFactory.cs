using Prism.Modularity;

namespace application.module.loader.factories;

internal interface IModuleInfoFactory
{
    IModuleInfo CreateModuleInfo(Type type);
    IEnumerable<IModuleInfo> GetModuleInfos(in DirectoryInfo directory, Type moduleType);
}

