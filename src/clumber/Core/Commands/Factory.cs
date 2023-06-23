namespace Clumber.Core.Commands;

public class Factory
{
    private readonly BrowserV2 _browser;
    private readonly Dictionary<string, AbstractCommand> commands = new();

    public Factory(BrowserV2 browser)
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