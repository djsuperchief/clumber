using Microsoft.Playwright;

namespace Clumber.Contracts;

public interface IBrowserFactory
{
    Task<IBrowser> CreateBrowserInstance(Enums.BrowserType browserType);
}