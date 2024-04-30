namespace ProcessMonitoring;

public interface IProcessMonitor
{
    Task MonitorAsync(string processName, int maxLifetime, int frequency);
}