using Prism.Events;

namespace timekeeper.events;

public class TimeUpdateEvent : PubSubEvent<DateTime> { }

