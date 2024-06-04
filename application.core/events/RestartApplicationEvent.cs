using Prism.Events;

namespace application.events;

public class RestartApplicationEvent : PubSubEvent { }
public class RollbackApplicationEvent : PubSubEvent { }

public sealed class ShutDownTheApplication : PubSubEvent { }
