using Prism.Commands;

namespace buttons.services;

public interface IButtonsModuleService
{
    DelegateCommand ExitTheApplication();
    DelegateCommand ReloadTheApplication();
    DelegateCommand RollbackUpdates();

}