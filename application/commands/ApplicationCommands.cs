using Prism.Commands;

namespace application.commands;

internal sealed class ApplicationCommands : IApplicationCommands
{
    public CompositeCommand CloseApplicationGracefully { get; } = new CompositeCommand();
}
