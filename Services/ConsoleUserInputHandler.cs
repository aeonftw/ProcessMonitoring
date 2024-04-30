namespace ProcessMonitoring;

public class ConsoleUserInputHandler : IUserInputHandler
{
    public string GetProcessName()
    {
        Console.Write("Enter the process name: ");
        return Console.ReadLine();
    }

    public int GetMaxLifetime()
    {
        Console.Write("Enter the maximum lifetime (in minutes): ");
        if (int.TryParse(Console.ReadLine(), out var maxLifetime))
        {
            return maxLifetime;
        }
        throw new ArgumentException("Invalid maximum lifetime.");
    }

    public int GetMonitoringFrequency()
    {
        Console.Write("Enter the monitoring frequency (in minutes): ");
        if (int.TryParse(Console.ReadLine(), out var frequency))
        {
            return frequency;
        }
        throw new ArgumentException("Invalid frequency.");
    }
}