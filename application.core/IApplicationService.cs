namespace application;

public interface IApplicationService
{
    string CoreModulesDirectory { get; }

    string GetRootPath();
    string GetSubDirectory(params string[] directories);
}
