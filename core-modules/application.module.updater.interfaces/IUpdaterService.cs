namespace application.module.updater;

public interface IUpdaterService
{
    void CheckForAvailableUpdates();
    void RestartIfNecessary();
}