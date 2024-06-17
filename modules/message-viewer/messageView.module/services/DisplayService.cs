using application;
using messageView.Views;
using Prism.Commands;
using Prism.Regions;

namespace messageView.services;

internal class DisplayService(IRegionManager regionManager) : IDisplayService
{
    public DelegateCommand DisplayClock()
        => new(() => regionManager.RequestNavigate(ApplicationRegionNames.BottomLeft, nameof(ClockView)));
    public DelegateCommand DisplayMessage()
        => new(() => regionManager.RequestNavigate(ApplicationRegionNames.FullPageRegion, nameof(ViewA)));
}
