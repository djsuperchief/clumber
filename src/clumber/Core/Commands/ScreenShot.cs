using Microsoft.Playwright;

namespace Clumber.Core.Commands;

public class ScreenShot : AbstractCommand
{
    // TODO: Remove this and do this properly.
    private readonly string _screenshotsFolder = "./TestPacks/test01/screenshots";

    public ScreenShot(BrowserV2 browser) : base(browser)
    {

    }

    protected override async Task RunCommand(string arguments)
    {
        if (!Directory.Exists(_screenshotsFolder))
        {
            Directory.CreateDirectory(_screenshotsFolder);
        }

        var name = $"{_screenshotsFolder}/{Guid.NewGuid().ToString("N")}.png";
        await _browser.CurrentPage.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = name,
            FullPage = false
        });
    }
}