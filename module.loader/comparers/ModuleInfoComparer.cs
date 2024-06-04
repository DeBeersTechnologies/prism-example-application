using Prism.Modularity;

namespace module.loader.comparers;

internal class ModuleInfoComparer : IModuleInfoComparer
{
    public bool Equals(IModuleInfo? left, IModuleInfo? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;

        return left.Ref.Equals(right.Ref) && left.ModuleName.Equals(right.ModuleName);
    }

    public int GetHashCode(IModuleInfo info)
    {
        var hashModuleName = info.ModuleName == null ? 0 : info.ModuleName.GetHashCode();
        var hashModuleRef = info.Ref.GetHashCode();

        return hashModuleName ^ hashModuleRef;
    }
}
