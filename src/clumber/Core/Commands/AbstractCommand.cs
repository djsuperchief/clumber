namespace Clumber.Core.Commands;

public abstract class AbstractCommand
{
    protected readonly BrowserV2 _browser;

    public AbstractCommand(BrowserV2 browser)
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