using ProcessMonitoring;

public class Program
{
    public static async Task Main()
    {
        var logger = new FileProcessLogger();
        var systemConsole = new SystemConsole();
        var userInput = new ConsoleUserInputHandler(systemConsole);
        var processProvider = new ProcessProvider();
        var monitor = new ProcessMonitor(logger,processProvider);

        var processName = userInput.GetProcessName();
        var maxLifetime = userInput.GetMaxLifetime();
        var frequency = userInput.GetMonitoringFrequency();
       

        await monitor.MonitorAsync(processName, maxLifetime, frequency);
    }
}