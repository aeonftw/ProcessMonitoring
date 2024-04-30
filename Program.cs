
using System.Diagnostics;

using Microsoft.Extensions.Configuration;

class ProcessMonitoring
{
    private static IConfiguration Configuration { get; set; }

   
    static async Task Main()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..")); 
        
       
        var jsonpath = $"{projectRoot}/appsettings.json";
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile( jsonpath,optional: false, reloadOnChange: true);

        Configuration = builder.Build();
        var logFilePath = Path.Combine(projectRoot, "Logs", "log.txt");
        

        Console.Write("Enter the process name: ");
        string processName = Console.ReadLine();

        Console.Write("Enter the maximum lifetime (in minutes): ");
        if (!int.TryParse(Console.ReadLine(), out int maxLifetime))
        {
            Console.WriteLine("Invalid maximum lifetime.");
            return;
        }

        Console.Write("Enter the monitoring frequency (in minutes): ");
        if (!int.TryParse(Console.ReadLine(), out int frequency))
        {
            Console.WriteLine("Invalid frequency.");
            return;
        }

        Console.WriteLine($"Monitoring {processName} processes every {frequency} minutes. Press 'q' to quit.");

        CancellationTokenSource cts = new CancellationTokenSource();
        Task keyPressTask = Task.Run(() =>
        {
            while (!cts.Token.IsCancellationRequested)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    Console.WriteLine("Stopping monitor.");
                    cts.Cancel();
                }
            }
        });

        try
        {
            while (!cts.Token.IsCancellationRequested)
            {
                var processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    Console.WriteLine($"Process {processName} is not running, continuing monitoring.");
                }
                else
                {
                    Console.WriteLine($"Found {processes.Length} instances of this process.");
                    foreach (var process in processes)
                    {
                        TimeSpan runtime = DateTime.Now - process.StartTime;
                        int timeLeft = maxLifetime - (int)runtime.TotalMinutes;
                        string processDetails = $"{process.ProcessName} ({process.MainWindowTitle})";

                        if (timeLeft <= 0)
                        {
                            Console.WriteLine($"Monitoring {processDetails}: running for {runtime.TotalMinutes} minutes, killing now.");
                            process.Kill();
                            Log($"{DateTime.Now}: Killed {processDetails} running for {runtime.TotalMinutes} minutes.", logFilePath);
                        }
                        else
                        {
                            Console.WriteLine($"Monitoring {processDetails}: running for {runtime.TotalMinutes} minutes, killing in {timeLeft} minutes.");
                        }
                    }
                }

                await Task.Delay(frequency * 60000, cts.Token); // Convert minutes to milliseconds and respect cancellation
            }
        }
        catch (TaskCanceledException)
        {
            // Expected when the task is cancelled
        }
        catch (System.ComponentModel.Win32Exception)
        {
            Console.WriteLine("Error accessing the process details. Ensure you have the necessary permissions.");
        }
        finally
        {
            cts.Dispose();
        }
    }
    
    private static void EnsureDirectoryExists(string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    private static void Log(string message, string filePath)
    {
        EnsureDirectoryExists(filePath);
        Console.WriteLine(message);
        File.AppendAllText(filePath, message + Environment.NewLine);
    }
    
    
    
    
}
