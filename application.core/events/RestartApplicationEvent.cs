using Prism.Events;

namespace prism_application.core.events;

public class RestartApplicationEvent : PubSubEvent { }
public class RollbackApplicationEvent : PubSubEvent { }

public sealed class ShutDownTheApplication : PubSubEvent { }
