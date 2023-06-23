namespace Clumber.Core.Commands;

public class Is : AbstractAssertion
{
    public Is(BrowserV2 browser, Factory commandFactory) : base(browser, commandFactory)
    {

    }

    protected override async Task RunCommand(string arguments)
    {
        var options = arguments.Split(" ");
        await _commandFactory.CreateCommand(options[0].Trim()).Run(string.Join(" ", options[1..]).Trim());
    }
}