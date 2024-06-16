using System.Windows.Input;

namespace application.menubar;

public class MenuInfo : IMenuInfo, IOwnerSetter, IMenuInfoFactory
{
    public MenuInfo() { }

    private MenuInfo(string text, string name, string parentName, ICommand? command = null, object? commandParameter = null)
    {
        Command = command;
        CommandParameter = commandParameter;
        DisplayText = text;
        Name = name;
        ParentName = parentName;
    }

    private MenuInfo(string text, string name, string parentName, string toolTip, ICommand? command = null, object? commandParameter = null)
                : this(text, name, parentName, command, commandParameter) { ToolTip = toolTip; }

    private MenuInfo(string text, string name, string parentName, dynamic icon, ICommand? command = null, object? commandParameter = null)
                : this(text, name, parentName, command, commandParameter) { Icon = icon; }

    private MenuInfo(string text, string name, string parentName, dynamic icon, string toolTip, ICommand? command = null, object? commandParameter = null)
                : this(text, name, parentName, command, commandParameter)
    {
        Icon = icon;
        ToolTip = toolTip;
    }

    public dynamic? Icon { get; }
    public string? DisplayText { get; }
    public string? Name { get; }

    public string? OwningModuleName { get; private set; }

    public void SetModuleName(string moduleName)
        => OwningModuleName = moduleName;

    public string? ParentName { get; }
    public string? ToolTip { get; }
    public ICommand? Command { get; }
    public object? CommandParameter { get; }

    public IMenuInfo CreateMenuInfo(string displayText, string name, string parentMenuName, ICommand? command = null, dynamic? commandParameter = null)
        => new MenuInfo(displayText, name, parentMenuName, command, commandParameter);

    public IMenuInfo CreateMenuInfo(string displayText, string name, string parentMenuName, string toolTip, ICommand? command = null, dynamic? commandParameter = null)
        => new MenuInfo(displayText, name, parentMenuName, toolTip, command, commandParameter);

    public IMenuInfo CreateMenuInfo(string displayText, string name, string parentMenuName, dynamic icon, ICommand? command = null, dynamic? commandParameter = null)
        => new MenuInfo(displayText, name, parentMenuName, icon, command, commandParameter);

    public IMenuInfo CreateMenuInfo(string displayText, string name, string parentMenuName, dynamic icon, string toolTip, ICommand? command = null, dynamic? commandParameter = null)
        => new MenuInfo(displayText, name, parentMenuName, icon, toolTip, command, commandParameter);

    public IMenuInfo CreateMenuSeparatorInfo(string parentMenuName)
        => new MenuInfo(string.Empty, ApplicationMenuNames.ApplicationMenuSeparator, parentMenuName);
}