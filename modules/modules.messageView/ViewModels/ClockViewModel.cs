using application.models;
using Prism.Events;
using Prism.Regions;
using System;
using modules.timekeeper.events;

namespace modules.messageView.ViewModels;

public sealed class ClockViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) : RegionAwareViewModel(regionManager)
{
    private int _hours;
    private int _minutes;
    private int _seconds;

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        eventAggregator.GetEvent<TimeUpdateEvent>().Subscribe(SetTime, ThreadOption.UIThread);
    }

    public int Hours
    {
        get => _hours;
        set => SetProperty(ref _hours, value);
    }

    public int Minutes
    {
        get => _minutes;
        set => SetProperty(ref _minutes, value);
    }

    public int Seconds
    {
        get => _seconds;
        set => SetProperty(ref _seconds, value);
    }

    public void SetTime(DateTime time) 
    {
        Seconds = time.Second;
        Minutes = time.Minute;
        Hours = time.Hour;
    }
}
