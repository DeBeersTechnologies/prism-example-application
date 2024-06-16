using Prism.Commands;

namespace messageView.services;

internal interface IDisplayService
{
    DelegateCommand DisplayClock();
    DelegateCommand DisplayMessage();
}
