namespace Clumber.Core.Commands;

public abstract class AbstractCommand
{
    protected readonly BrowserHelper _browser;

    public AbstractCommand(BrowserHelper browser)
    {
        _browser = browser;
    }

    public async Task Run(string arguments, string packContext)
    {
        await OpenPage();
        await RunCommand(arguments, packContext);
    }

    protected abstract Task RunCommand(string arguments, string packContext);

    protected async Task OpenPage(bool force = false)
    {
        if (force)
        {
            await _browser.ClosePage();
        }

        await _browser.CreatePage();
    }
}