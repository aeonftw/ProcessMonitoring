using System.Diagnostics;

namespace ProcessMonitoring;

public interface IProcessProvider
{
    Process[] GetProcessesByName(string name);
}
