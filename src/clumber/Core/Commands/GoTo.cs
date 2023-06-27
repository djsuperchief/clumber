namespace Clumber.Core.Commands;

public class Goto : AbstractCommand
{
    public Goto(BrowserHelper browser) : base(browser)
    {

    }

    protected override async Task RunCommand(string arguments)
    {
        await _browser.CurrentPage.GotoAsync(arguments);
    }
}