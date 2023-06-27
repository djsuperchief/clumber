namespace Clumber.Core.Commands;

public abstract class AbstractCommand
{
    protected readonly BrowserHelper _browser;

    public AbstractCommand(BrowserHelper browser)
    {
        _browser = browser;
    }

    public async Task Run(string arguments)
    {
        await OpenPage();
        await RunCommand(arguments);
    }

    protected abstract Task RunCommand(string arguments);

    protected async Task OpenPage(bool force = false)
    {
        if (force)
        {
            await _browser.ClosePage();
        }

        await _browser.CreatePage();
    }
}