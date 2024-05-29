using Prism.Regions;
using System;

namespace prism_application.core.Mvvm
{
    public class RegionViewModelBase(IRegionManager regionManager)
        : ViewModelBase, INavigationAware, IConfirmNavigationRequest
    {
        protected IRegionManager RegionManager { get; private set; } = regionManager;

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
