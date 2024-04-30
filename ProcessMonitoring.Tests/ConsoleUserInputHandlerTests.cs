using Moq;

[TestFixture]
public class ConsoleUserInputHandlerTests
{
    private Mock<IConsole> _mockConsole;
    private ConsoleUserInputHandler _inputHandler;

    [SetUp]
    public void Setup()
    {
        _mockConsole = new Mock<IConsole>();
        _inputHandler = new ConsoleUserInputHandler(_mockConsole.Object);
    }

    [Test]
    public void GetProcessName_ReturnsInputName()
    {
        // Arrange
        string expected = "notepad.exe";
        _mockConsole.Setup(m => m.ReadLine()).Returns(expected);

        // Act
        var result = _inputHandler.GetProcessName();

        // Assert
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void GetMaxLifetime_ValidInput_ReturnsLifetime()
    {
        // Arrange
        _mockConsole.Setup(m => m.ReadLine()).Returns("120");

        // Act
        var result = _inputHandler.GetMaxLifetime();

        // Assert
        Assert.AreEqual(120, result);
    }

    [Test]
    public void GetMaxLifetime_InvalidInput_ThrowsArgumentException()
    {
        // Arrange
        _mockConsole.Setup(m => m.ReadLine()).Returns("invalid");

        // Assert
        Assert.Throws<ArgumentException>(() => _inputHandler.GetMaxLifetime());
    }

    [Test]
    public void GetMonitoringFrequency_ValidInput_ReturnsFrequency()
    {
        // Arrange
        _mockConsole.Setup(m => m.ReadLine()).Returns("5");

        // Act
        var result = _inputHandler.GetMonitoringFrequency();

        // Assert
        Assert.AreEqual(5, result);
    }

    [Test]
    public void GetMonitoringFrequency_InvalidInput_ThrowsArgumentException()
    {
        // Arrange
        _mockConsole.Setup(m => m.ReadLine()).Returns("abc");

        // Assert
        Assert.Throws<ArgumentException>(() => _inputHandler.GetMonitoringFrequency());
    }
}