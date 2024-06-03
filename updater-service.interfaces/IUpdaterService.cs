namespace updater_service;

public interface IUpdaterService
{
    void CheckForAvailableUpdates();
    //void LoadModules(string path);
    void RestartIfNecessary();
}