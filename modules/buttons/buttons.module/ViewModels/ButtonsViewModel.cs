using application.models;
using buttons.services;
using Prism.Commands;
using Prism.Regions;

namespace buttons.ViewModels;
public sealed class ButtonsViewModel(IRegionManager regionManage, IButtonsModuleService buttonsModuleService) : RegionAwareViewModel(regionManage)
{
    public DelegateCommand ExitApplicationCommand => buttonsModuleService.ExitTheApplication();
    public DelegateCommand ReloadApplicationCommand => buttonsModuleService.ReloadTheApplication();
    public DelegateCommand RollbackApplicationCommand => buttonsModuleService.RollbackUpdates();

}
