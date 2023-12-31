using Microsoft.Playwright;

namespace Clumber.Core;

public class BrowserHelper
{
    public IBrowserContext Browser { get; private set; }

    public IPage? CurrentPage { get; private set; }

    public IEnumerable<Entities.Identifier> Identifiers { get; private set; }

    public BrowserHelper(IBrowserContext playwrightBrowser, IEnumerable<Entities.Identifier>? identifiers)
    {
        Browser = playwrightBrowser;
        Identifiers = identifiers ?? new List<Entities.Identifier>();
    }

    public async Task CreatePage()
    {
        if (CurrentPage is null)
        {
            CurrentPage = await Browser.NewPageAsync();
        }
    }

    public async Task ClosePage()
    {
        if (CurrentPage is not null)
        {
            await CurrentPage.CloseAsync();
            CurrentPage = null;
        }
    }

    public static async Task<IBrowser> CreateChromeBrowser()
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        return browser;
    }
}