namespace Clumber.Core.Commands;

public class GetPageTitle : AbstractCommand
{
    public GetPageTitle(BrowserHelper browser) : base(browser)
    {

    }

    protected override async Task RunCommand(string arguments)
    {
        var currentTitle = await _browser.CurrentPage.TitleAsync();
        if (currentTitle == arguments)
        {
            Console.WriteLine($"Title is correct ('{arguments}')");
            return;
        }

        Console.WriteLine($"Expected '{arguments}' but found '{currentTitle}'");
    }
}