namespace ProcessMonitoring.Tests;

using NUnit.Framework;
using System;
using System.IO;
using ProcessMonitoring;

[TestFixture]
public class FileProcessLoggerTests
{
    private string _baseDirectory;
    private string _logDirectory;
    private FileProcessLogger _logger;

    [SetUp]
    public void Setup()
    {
        // Set base directory typically to a test-specific location
        _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        _logDirectory = Path.GetFullPath(Path.Combine(_baseDirectory, @"..\..\..\Logs"));

        // Ensure clean directory
        if (Directory.Exists(_logDirectory))
        {
            Directory.Delete(_logDirectory, true);
        }

        Directory.CreateDirectory(_logDirectory);

        _logger = new FileProcessLogger();
    }

    [Test]
    public void Log_WritesCorrectContent()
    {
        var processName = "TestProcess";
        var message = "This is a test log message.";

        _logger.Log(message, processName);

        var expectedFileName = $"{processName}{DateTime.Now:yyyyMMddHHmmss}.txt";
        var expectedFilePath = Path.Combine(_logDirectory, expectedFileName);
        var fileContent = File.ReadAllText(expectedFilePath);

        StringAssert.Contains(message, fileContent, "This is a test log message.");
    }


    [Test]
    public void Log_CreatesCorrectFilePath()
    {
        var processName = "TestProcess";
        var message = "This is a test log message.";
        _logger.Log(message, processName);

        var expectedFileName = $"{processName}{DateTime.Now:yyyyMMddHHmmss}.txt";
        var expectedFilePath = Path.Combine(_logDirectory, expectedFileName);

        Console.WriteLine("Expected path: " + expectedFilePath);
        Assert.IsTrue(File.Exists(expectedFilePath), "Log file should exist at: " + expectedFilePath);
    }



    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_logDirectory))
        {
            Directory.Delete(_logDirectory, true);
        }
    }
}
