using System.Windows.Input;

namespace application.menubar;

public static class MenuInfoExtensions
{
    public static IMenuInfo And(this IMenuInfo menuInfo) => menuInfo;
    public static IMenuInfo SetOwningModuleName(this IMenuInfo menuInfo, string owningModuleName)
    {
        if (menuInfo is IOwnerSetter info)
        {
            info.SetModuleName(owningModuleName);
        }

        return menuInfo;
    }
}


public interface IMenuInfo
{
    ICommand Command { get; }
    object CommandParameter { get; }
    string DisplayText { get; }
    dynamic Icon { get; }
    string Name { get; }
    string OwningModuleName { get; }
    string ParentName { get; }
    string ToolTip { get; }
}

internal interface IOwnerSetter
{
    void SetModuleName(string moduleName);
}