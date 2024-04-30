namespace ProcessMonitoring;

public interface IProcessLogger
{
    void Log(string message, string processName);
}