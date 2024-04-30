
using System.ComponentModel;
using System.Diagnostics;


namespace ProcessMonitoring
{

    public class ProcessMonitor : IProcessMonitor
    {
        private readonly IProcessLogger _logger;

        public ProcessMonitor(IProcessLogger logger)
        {
            _logger = logger;
        }

        public async Task MonitorAsync(string processName, int maxLifetime, int frequency)
        {
            Console.WriteLine($"Monitoring {processName} processes every {frequency} minutes. Press 'Q' to quit.");
            var cts = new CancellationTokenSource();
            var keyPressTask = Task.Run(() =>
            {
                while (!cts.Token.IsCancellationRequested)
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                    {
                        Console.WriteLine("Stopping monitor.");
                        cts.Cancel();
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
                            var runtime = DateTime.Now - process.StartTime;
                            var timeLeft = maxLifetime - (int)runtime.TotalMinutes;
                            var processDetails = $"{process.ProcessName} ({process.MainWindowTitle})";

                            if (timeLeft <= 0)
                            {
                                Console.WriteLine(
                                    $"Monitoring {processDetails}: running for {runtime.TotalMinutes} minutes, stopping now.");
                                process.Kill();
                                _logger.Log(
                                    $"{DateTime.Now}: Stopped {processDetails} running for {runtime.TotalMinutes} minutes.",
                                    processName);
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"Monitoring {processDetails}: running for {runtime.TotalMinutes} minutes, stopping in {timeLeft} minutes.");
                            }
                        }
                    }

                    await Task.Delay(frequency * 60000,
                        cts.Token); // Convert minutes to milliseconds and respect cancellation
                }
            }
            catch (TaskCanceledException)
            {
                // Expected when the task is cancelled
            }
            catch (Win32Exception)
            {
                Console.WriteLine("Error accessing the process details. Ensure you have the necessary permissions.");
            }
            finally
            {
                cts.Dispose();
            }
        }
        }
    }

   

