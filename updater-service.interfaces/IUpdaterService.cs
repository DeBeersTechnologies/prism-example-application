namespace updater_service;

public interface IUpdaterService
{
    bool RestartRequired { get; }
}