using Microsoft.Playwright;

namespace Clumber.Contracts;

public interface IBrowserFactory
{
    Task<IBrowser> CreateBrowserInstance(Enums.BrowserType browserType, Entities.Config config, string channel = "chrome");

    Task<IBrowser> CreateBrowserInstance(Entities.Config config);
}