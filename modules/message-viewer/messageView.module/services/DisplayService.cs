using application;
using messageView.Views;
using Prism.Commands;
using Prism.Regions;

namespace messageView.services;

internal class DisplayService : IDisplayService
{
    private readonly IRegionManager _regionManager;

    public DisplayService(IRegionManager regionManager) 
        => _regionManager = regionManager;

    public DelegateCommand DisplayClock()
        => new(() => _regionManager.RequestNavigate(ApplicationRegionNames.BottomLeft, nameof(ClockView)));
    public DelegateCommand DisplayMessage()
        => new(() => _regionManager.RequestNavigate(ApplicationRegionNames.FullPageRegion, nameof(ViewA)));
}
