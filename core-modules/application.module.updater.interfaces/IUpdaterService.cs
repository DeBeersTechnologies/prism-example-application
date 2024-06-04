namespace application.module.updater;

public interface IUpdaterService
{
    void ApplyUpdates();
    void RestartIfNecessary();
}