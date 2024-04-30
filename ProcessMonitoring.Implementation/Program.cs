using ProcessMonitoring;

public class Program
{
    public static async Task Main()
    {
        var logger = new FileProcessLogger();
        var systemconsole = new SystemConsole();
        var userInput = new ConsoleUserInputHandler(systemconsole);
        var monitor = new ProcessMonitor(logger);

        var processName = userInput.GetProcessName();
        var maxLifetime = userInput.GetMaxLifetime();
        var frequency = userInput.GetMonitoringFrequency();
       

        await monitor.MonitorAsync(processName, maxLifetime, frequency);
    }
}