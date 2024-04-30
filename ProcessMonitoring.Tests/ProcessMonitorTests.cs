
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
        public async Task MonitorAsync_HandlesExceptions_Gracefully()
        {
            _mockProcessProvider.Setup(x => x.GetProcessesByName(It.IsAny<string>())).Throws(new Win32Exception());

            Assert.DoesNotThrowAsync(() => _processMonitor.MonitorAsync("FaultyProcess", 10, 1), "Monitoring should handle Win32 exceptions gracefully.");
        }

    }
}