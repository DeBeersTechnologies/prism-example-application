using Prism.Commands;

namespace application.commands;

public interface IApplicationCommands
{
    CompositeCommand CloseApplicationGracefully { get; }
}
