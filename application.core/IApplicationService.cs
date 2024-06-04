namespace prism_application.core;

public interface IApplicationService
{
    string CoreModulesDirectory { get; }

    string GetRootPath();
    string GetSubDirectory(params string[] directories);
}
