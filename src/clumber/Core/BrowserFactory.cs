using Microsoft.Playwright;

namespace Clumber.Core;

public class BrowserFactory : Contracts.IBrowserFactory
{
    private IBrowser? _browser;

    private IPlaywright? _playwright;

    public async Task<IBrowser> CreateBrowserInstance(Enums.BrowserType browserType)
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = true
        };
        IBrowser? response;

        switch (browserType)
        {
            case Enums.BrowserType.Chromium:
                response = await _playwright.Chromium.LaunchAsync(launchOptions);
                break;
            case Enums.BrowserType.Firefox:
                response = await _playwright.Firefox.LaunchAsync(launchOptions);
                break;
            case Enums.BrowserType.Webkit:
                response = await _playwright.Webkit.LaunchAsync(launchOptions);
                break;
            default:
                throw new Exceptions.BrowserException(Resources.UnknownBrowserSelection);
        }

        return response;
    }
}