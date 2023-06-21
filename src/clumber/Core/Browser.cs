using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Clumber.Core;
public class Browser : IDisposable, IAsyncDisposable
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
            /*var disposeTask = Task.Run(async () =>
            {
                await PlBrowser.CloseAsync();
                await PlBrowser.DisposeAsync();
            });
            disposeTask.RunSynchronously();*/


            _ = Task.FromResult(result: async () =>
            {
                await PlBrowser.CloseAsync();
                return PlBrowser.DisposeAsync();
            });

        }
    }

    public static async Task<Browser> CreateChromeBrowser()
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
        return new Browser(browser);
    }

    public async ValueTask DisposeAsync()
    {
        await PlBrowser.CloseAsync();
        await PlBrowser.DisposeAsync();
    }
}
