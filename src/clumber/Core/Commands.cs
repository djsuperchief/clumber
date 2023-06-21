using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clumber;
using Microsoft.Playwright;

namespace Clumber.Core;

public class Commands
{
    private readonly Browser browser;
    private IPage? page;
    private readonly Dictionary<string, Func<string, Task>> commands = new();
    private readonly IEnumerable<Entities.Identifier> _identifiers;

    public Commands(Browser inboundBrowser, IEnumerable<Entities.Identifier> identifiers)
    {
        browser = inboundBrowser;
        _identifiers = identifiers;

        commands.Add("goto", Goto);
        commands.Add("click", Click);
        commands.Add("pagetitle", GetPageTitle);
        commands.Add("is", Is);
    }

    public async Task RunCommand(string command, string arguments)
    {
        var parsed = command.ToLower();
        if (commands.ContainsKey(parsed))
        {
            await OpenPage();
            await commands[parsed](arguments);
        }
        else
        {
            throw new Exceptions.CommandNotFoundException(string.Format(Resources.ExceptionCommandNotFound, command));
        }

        await Task.CompletedTask;
    }

    private async Task Goto(string url)
    {

        await page.GotoAsync(url);
        await Task.CompletedTask;
    }

    private async Task Click(string identifier)
    {
        var locator = string.Empty;
        if (_identifiers.Any(x => x.Name == identifier))
        {
            locator = _identifiers.Single(x => x.Name == identifier).ToString();
        }

        await page.ClickAsync(locator);
    }

    private async Task GetPageTitle(string title)
    {
        var currentTitle = await page.TitleAsync();
        if (currentTitle == title)
        {
            Console.WriteLine($"Title is correct ('{title}')");
            return;
        }

        Console.WriteLine($"Expected '{title}' but found '{currentTitle}'");
    }

    private async Task Is(string assertion)
    {
        var options = assertion.Split(" ");
        await commands[options[0].Trim()](string.Join(" ", options[1..]).Trim());
    }

    private async Task OpenPage(bool force = false)
    {
        if (force)
        {
            if (page != null)
            {
                await page.CloseAsync();
                page = null;
            }
        }

        if (page == null)
        {
            page = await browser.PlBrowser.NewPageAsync();
        }
    }
}