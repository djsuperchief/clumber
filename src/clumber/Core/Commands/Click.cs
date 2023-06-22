namespace Clumber.Core.Commands;

public class Click : AbstractCommand
{
    public Click(Browser browser) : base(browser)
    {

    }

    protected override async Task RunCommand(string arguments)
    {
        var locator = string.Empty;
        if (_browser.Identifiers.Any(x => x.Name == arguments))
        {
            locator = _browser.Identifiers.Single(x => x.Name == arguments).ToString();
        }

        await _browser.CurrentPage.ClickAsync(locator);
    }
}