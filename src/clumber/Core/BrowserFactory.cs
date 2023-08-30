using Clumber.Entities;
using Microsoft.Playwright;

namespace Clumber.Core;

public class BrowserFactory : Contracts.IBrowserFactory
{
    private IPlaywright? _playwright;

    private readonly Dictionary<string, Func<string, Entities.Config, Task<IBrowser>>> _browserTypes = new();

    public BrowserFactory()
    {
        _browserTypes.Add("chrome", CreateChromeBrowser);
        _browserTypes.Add("edge", CreateEdgeBrowser);
        _browserTypes.Add("msedge", CreateEdgeBrowser);
        _browserTypes.Add("firefox", CreateFirefoxBrowser);
        _browserTypes.Add("safari", CreateWebKitBrowser);
        _browserTypes.Add("webkit", CreateWebKitBrowser);
    }


    public async Task<IBrowser> CreateBrowserInstance(Enums.BrowserType browserType, Entities.Config config, string channel = "chrome")
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = config.Headless,
        };

        IBrowser? response;

        switch (browserType)
        {
            case Enums.BrowserType.Chromium:
                launchOptions.Channel = channel;
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

    public async Task<IBrowser> CreateBrowserInstance(Entities.Config config)
    {
        var browser = config.Browsers[0].ToLower();
        if (_browserTypes.ContainsKey(browser))
        {
            return await _browserTypes[browser](browser, config);
        }

        throw new Exceptions.ConfigException(Resources.ConfigBrowserException);
    }

    private async Task<IBrowser> CreateChromeBrowser(string channel, Entities.Config config)
    {
        return await CreateBrowserInstance(Enums.BrowserType.Chromium, config, channel);
    }

    private async Task<IBrowser> CreateWebKitBrowser(string channel, Entities.Config config)
    {
        return await CreateBrowserInstance(Enums.BrowserType.Webkit, config, channel);
    }

    private async Task<IBrowser> CreateFirefoxBrowser(string channel, Entities.Config config)
    {
        return await CreateBrowserInstance(Enums.BrowserType.Firefox, config, channel);
    }

    private async Task<IBrowser> CreateEdgeBrowser(string channel, Entities.Config config)
    {
        return await CreateBrowserInstance(Enums.BrowserType.Chromium, config, channel);
    }

}