namespace application.services;

public interface IApplicationDirectoryService
{
    string ApplicationDirectory { get; }
    string ModulesDirectory { get; }
    string RollbackDirectory { get; }
    string UpdatesDirectory { get; }
}

