using System.Windows.Input;

namespace application.menubar;

public interface IMenuInfoFactory
{
    IMenuInfo CreateMenuInfo(string displayText, string name, string parentMenuName, ICommand? command = null, dynamic? commandParameter = null);
    IMenuInfo CreateMenuInfo(string displayText, string name, string parentMenuName, string toolTip, ICommand? command = null, dynamic? commandParameter = null);
    IMenuInfo CreateMenuInfo(string displayText, string name, string parentMenuName, dynamic icon, ICommand? command = null, dynamic? commandParameter = null);
    IMenuInfo CreateMenuInfo(string displayText, string name, string parentMenuName, dynamic icon, string toolTip, ICommand? command = null, dynamic? commandParameter = null);
    IMenuInfo CreateMenuSeparatorInfo(string parentMenuName);
}