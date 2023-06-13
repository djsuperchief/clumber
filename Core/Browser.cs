using Microsoft.Playwright;

namespace Clumber.Core;
public class Browser : IDisposable
{
    public IBrowser PlBrowser { get; private set; }

    public Browser(IBrowser browser)
    {
        this.PlBrowser = browser;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _ = Task.FromResult(result: () =>
            {
                return PlBrowser.CloseAsync();
            });
        }
    }

    public static async Task<Browser> CreateChromeBrowser()
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        return new Browser(browser);
    }
}
