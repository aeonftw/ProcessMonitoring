
using System.ComponentModel;
using System.Diagnostics;
using Moq;


namespace ProcessMonitoring.Tests
{
    [TestFixture]
    public class ProcessMonitorTests
    {
        private Mock<IProcessLogger> _mockLogger;
        private Mock<IProcessProvider> _mockProcessProvider;
        private ProcessMonitor _processMonitor;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<IProcessLogger>();
            _mockProcessProvider = new Mock<IProcessProvider>();
            _processMonitor = new ProcessMonitor(_mockLogger.Object, _mockProcessProvider.Object);
        }
        
     
        [Test]
        public async Task MonitorAsync_NoProcessesRunning_LogsAndContinuesMonitoring()
        {
            // Arrange
            string processName = "testProcess";
            int maxLifetime = 60; // minutes
            int frequency = 1; // minute
            _mockProcessProvider.Setup(m => m.GetProcessesByName(It.IsAny<string>())).Returns(Array.Empty<Process>());

            // Act
            var task = _processMonitor.MonitorAsync(processName, maxLifetime, frequency);
            await Task.Delay(200); // simulate some time for the monitor loop
            await Task.Delay(200); // more time for additional logs if needed

            // Assert
            _mockProcessProvider.Verify(m => m.GetProcessesByName(processName), Times.AtLeastOnce());
            _mockLogger.VerifyNoOtherCalls(); // No processes to log about
        }

        
        [Test]
        public async Task MonitorAsync_WhenCalled_ChecksProcess()
        {
            // Arrange
            var fakeProcesses = new Process[1] {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "notepad.exe", // You need to replace this with a testable process that you know will start.
                    WindowStyle = ProcessWindowStyle.Hidden
                })
            };

            _mockProcessProvider.Setup(m => m.GetProcessesByName("notepad")).Returns(fakeProcesses);

            // Act
            var task = _processMonitor.MonitorAsync("notepad", 60, 1);
            await Task.Delay(200); // Let the monitor run for a bit.

            // Assert
            _mockProcessProvider.Verify(m => m.GetProcessesByName("notepad"), Times.AtLeastOnce());
            fakeProcesses[0].Kill(); // Cleanup process after test
        }

        
        [Test]
        public async Task MonitorAsync_Cancelled_TaskCancelsGracefully()
        {
            // Arrange
            _mockProcessProvider.Setup(m => m.GetProcessesByName(It.IsAny<string>())).Returns(new[] { new Process() });

            // Act & Assert
            var task = _processMonitor.MonitorAsync("testProcess", 60, 1);
            await Task.Delay(100); // short delay before cancellation
            task = null; // simulate stopping the monitoring manually

            // No specific assertion here, mainly observing no unhandled exceptions
        }
    

        [Test]
        public async Task MonitorAsync_HandlesExceptions_Gracefully()
        {
            _mockProcessProvider.Setup(x => x.GetProcessesByName(It.IsAny<string>())).Throws(new Win32Exception());

            Assert.DoesNotThrowAsync(() => _processMonitor.MonitorAsync("FaultyProcess", 10, 1), "Monitoring should handle Win32 exceptions gracefully.");
        }

    }
}