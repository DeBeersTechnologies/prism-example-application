using Prism.Commands;

namespace prism_application.core.events;

public interface IApplicationCommands
{
    CompositeCommand CloseApplicationGracefully { get; }
}
