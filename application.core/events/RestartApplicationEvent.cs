using Prism.Events;

namespace prism_application.core.events;

public class RestartApplicationEvent : PubSubEvent { }
public sealed class ShutDownTheApplication : PubSubEvent { }
