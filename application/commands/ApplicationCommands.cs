using Prism.Commands;

namespace application.commands;

internal class ApplicationCommands : IApplicationCommands
{
    public CompositeCommand CloseApplicationGracefully { get; } = new CompositeCommand();
}
