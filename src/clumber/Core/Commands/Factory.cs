namespace Clumber.Core.Commands;

public class Factory
{
    private readonly Browser _browser;
    private readonly Dictionary<string, AbstractCommand> commands = new();

    public Factory(Browser browser)
    {
        _browser = browser;
        AddFactoryClasses();
    }

    public AbstractCommand CreateCommand(string command)
    {
        if (commands.ContainsKey(command))
        {
            return commands[command];
        }

        throw new Exceptions.CommandNotFoundException(string.Format(Resources.ExceptionCommandNotFound, command));
    }

    private void AddFactoryClasses()
    {
        commands.Add("goto", new Goto(_browser));
        commands.Add("click", new Click(_browser));
        commands.Add("pagetitle", new GetPageTitle(_browser));
        commands.Add("is", new Is(_browser, this));
        commands.Add("screenshot", new ScreenShot(_browser));
    }
}