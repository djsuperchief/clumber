using Microsoft.Playwright;

namespace Clumber.Core.Commands;

public class ScreenShot : AbstractCommand
{

    public ScreenShot(BrowserHelper browser) : base(browser)
    {
    }

    protected override async Task RunCommand(string arguments, string packContext)
    {
        var screenshotsFolder = Path.Combine(packContext, "Screenshots");
        if (!Directory.Exists(screenshotsFolder))
        {
            Directory.CreateDirectory(screenshotsFolder);
        }

        var name = $"{screenshotsFolder}/{Guid.NewGuid().ToString("N")}.png";
        await _browser.CurrentPage.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = name,
            FullPage = false
        });
    }
}