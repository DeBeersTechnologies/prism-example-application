using System;
using System.Timers;
using modules.timekeeper.events;
using Prism.Events;

namespace modules.timekeeper.services;
public class ClockService : IClockService
{
    private readonly Timer _timer = new(){ Interval = 1000};
    private readonly TimeUpdateEvent _timeUpdateEvent;

    public ClockService(IEventAggregator eventAggregator)
    {
        _timeUpdateEvent = eventAggregator.GetEvent<TimeUpdateEvent>();
        _timer.Elapsed += (_, _) => _timeUpdateEvent.Publish(DateTime.Now);
        _timer.Start();
    }
}

