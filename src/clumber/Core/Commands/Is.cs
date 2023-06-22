namespace Clumber.Core.Commands;

public class Is : AbstractCommand
{
    public Is(Browser browser) : base(browser)
    {

    }

    protected override async Task RunCommand(string arguments)
    {
        /*var options = arguments.Split(" ");
        await commands[options[0].Trim()](string.Join(" ", options[1..]).Trim());*/
        Console.WriteLine("Needs to be moved to an assertion factory");
    }
}