# Process Monitoring System

A C# application designed to monitor system processes based on user-defined names and criteria, featuring robust logging and interactive console input.

## Features

- **Process Monitoring**: Continuously monitor processes based on the name provided by the user. If a process exceeds a specified runtime, it is automatically terminated.
- **Logging**: Detailed logging of process activities, including terminations, with logs saved to text files.
- **User Interaction**: Users can dynamically set the process name, maximum runtime, and monitoring frequency via the console.
- **Graceful Termination**: Users can terminate the monitoring by pressing 'Q' at any time.

## Components

The system is composed of several interfaces and classes, structured as follows:

- `IProcessMonitor`: Interface to define process monitoring functionality.
- `IProcessProvider`: Interface to retrieve processes by name.
- `IProcessLogger`: Interface for logging process details.
- `IConsole`: Interface to abstract console operations.
- `IUserInputHandler`: Interface for handling user input through the console.

Implementations:
- `ProcessMonitor`: Implements `IProcessMonitor` to handle the monitoring logic.
- `ProcessProvider`: Implements `IProcessProvider` to fetch processes.
- `FileProcessLogger`: Implements `IProcessLogger` to log details to files.
- `SystemConsole`: Implements `IConsole` for console operations.
- `ConsoleUserInputHandler`: Implements `IUserInputHandler` for user input management.

## Setup

1. Clone the repository: git clone https://github.com/aeonftw/process-monitoring.git
2. Navigate to the project directory: cd process-monitoring
4. Build the project (ensure you have .NET SDK installed): dotnet build
5. Run the application: dotnet run


## Usage

After running the application, you will be prompted to enter the process name, maximum lifetime, and monitoring frequency. The application will start monitoring based on these parameters and log all activities. To stop monitoring, simply press 'Q'.

## Dependencies

- .NET 5.0 or higher
- System.Diagnostics to access process details

## Contributing

Feel free to fork this project and submit pull requests. You can also open issues if you find any bugs or have feature requests.



