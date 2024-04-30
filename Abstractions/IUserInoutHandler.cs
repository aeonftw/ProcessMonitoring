namespace ProcessMonitoring;

public interface IUserInputHandler
{
    string GetProcessName();
    int GetMaxLifetime();
    int GetMonitoringFrequency();
}