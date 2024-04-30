namespace ProcessMonitoring;

public class FileProcessLogger : IProcessLogger
{
    private readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

    public void Log(string message, string processName)
    {
        var logFilePath = GetLogFilePath(processName);
        Console.WriteLine(message);
        File.AppendAllText(logFilePath, message + Environment.NewLine);
    }

    private string GetLogFilePath(string processName)
    {
        var projectRoot = Path.GetFullPath(Path.Combine(_baseDirectory, @"..\..\..\Logs"));
        EnsureDirectoryExists(projectRoot);
        return Path.Combine(projectRoot, $"{processName}{DateTime.Now:yyyyMMddHHmmss}.txt");
    }

    private static void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    }
}
