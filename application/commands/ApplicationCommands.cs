using Prism.Commands;

namespace prism_application.core.events;

internal class ApplicationCommands : IApplicationCommands
{
    public CompositeCommand CloseApplicationGracefully { get; } = new CompositeCommand();
}
