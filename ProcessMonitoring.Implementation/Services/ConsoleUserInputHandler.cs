using ProcessMonitoring;

public class ConsoleUserInputHandler : IUserInputHandler
{
    private readonly IConsole _console;

    public ConsoleUserInputHandler(IConsole console)
    {
        _console = console;
    }

    public string GetProcessName()
    {
        _console.Write("Enter the process name: ");
        return _console.ReadLine();
    }

    public int GetMaxLifetime()
    {
        _console.Write("Enter the maximum lifetime (in minutes): ");
        if (int.TryParse(_console.ReadLine(), out var maxLifetime))
        {
            return maxLifetime;
        }
        throw new ArgumentException("Invalid maximum lifetime.");
    }

    public int GetMonitoringFrequency()
    {
        _console.Write("Enter the monitoring frequency (in minutes): ");
        if (int.TryParse(_console.ReadLine(), out var frequency))
        {
            return frequency;
        }
        throw new ArgumentException("Invalid frequency.");
    }
}