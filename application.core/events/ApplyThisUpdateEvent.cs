using Prism.Events;

namespace application.events;

/// <summary>
/// Use this event to apply a specific update
/// </summary>
public sealed class ApplyThisUpdateEvent : PubSubEvent<string> { }