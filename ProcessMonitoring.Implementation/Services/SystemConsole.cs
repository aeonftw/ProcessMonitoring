public class SystemConsole : IConsole
{
    public void Write(string message)
    {
        Console.Write(message);
    }

    
    public string ReadLine()
    {
        return Console.ReadLine();
    }
}