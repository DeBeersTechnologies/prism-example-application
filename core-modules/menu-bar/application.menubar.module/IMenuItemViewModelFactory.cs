using System;

namespace application.menubar;

internal interface IMenuItemViewModelFactory
{
    Func<IMenuInfo, IMenuItemViewModel> CreateMenuViewModel { get; }
}