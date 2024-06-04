namespace application.services;

public interface IApplicationService
{
    string CoreModulesDirectory { get; }
    string UpdatesDirectory { get; }
    string RollbackDirectory { get; }

    string GetRootPath();
    string GetSubDirectory(params string[] directories);
}
