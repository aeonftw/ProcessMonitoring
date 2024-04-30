using System.Diagnostics;

namespace ProcessMonitoring;

public class ProcessProvider : IProcessProvider
{
    public Process[] GetProcessesByName(string name)
    {
        return Process.GetProcessesByName(name);
    }
}
