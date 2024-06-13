using Prism.Events;

namespace modules.timekeeper.events;

public class TimeUpdateEvent : PubSubEvent<DateTime> { }

