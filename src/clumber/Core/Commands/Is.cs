namespace Clumber.Core.Commands;

public class Is : AbstractAssertion
{
    public Is(BrowserHelper browser, Factory commandFactory) : base(browser, commandFactory)
    {

    }

    protected override async Task RunCommand(string arguments, string packContext)
    {
        var options = arguments.Split(" ");
        await _commandFactory.CreateCommand(options[0].Trim()).Run(string.Join(" ", options[1..]).Trim(), packContext);
    }
}