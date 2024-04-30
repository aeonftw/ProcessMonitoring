using ProcessMonitoring;

public class Program
{
    public static async Task Main()
    {
        var logger = new FileProcessLogger();
        var userInput = new ConsoleUserInputHandler();
        var monitor = new ProcessMonitor(logger);

        var processName = userInput.GetProcessName();
        var maxLifetime = userInput.GetMaxLifetime();
        var frequency = userInput.GetMonitoringFrequency();
       

        await monitor.MonitorAsync(processName, maxLifetime, frequency);
    }
}