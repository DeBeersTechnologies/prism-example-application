using Prism.Events;

namespace application.events;

/// <summary>
/// USe this event to roll back a specific update
/// </summary>
public sealed class RollbackThisUpdateEvent : PubSubEvent<string> { }