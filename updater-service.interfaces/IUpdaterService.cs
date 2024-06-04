namespace updater_service;

public interface IUpdaterService
{
    void CheckForAvailableUpdates();
    void RestartIfNecessary();
}