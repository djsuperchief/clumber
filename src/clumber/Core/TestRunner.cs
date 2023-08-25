using System.IO;
using Clumber.Contracts;
using Microsoft.Playwright;

namespace Clumber.Core;

public class TestRunner
{
    private readonly string _testPackLocation;
    private IBrowser _chromeInstance = default!;
    private IBrowser _firefoxInstance = default!;
    private IBrowser _webkitInstance = default!;
    private readonly IBrowserFactory _browserFactory;
    private readonly ILogger _logger = new ConsoleLogger();

    public TestRunner(IBrowserFactory browserFactory, string testPackLocation)
    {
        _testPackLocation = testPackLocation;
        _browserFactory = browserFactory;
    }

    public async Task Run()
    {
        // Create an initial instance of all browsers.
        _logger.Info("--- Starting Tests ---");
        _chromeInstance = await _browserFactory.CreateBrowserInstance(Enums.BrowserType.Chromium);
        _firefoxInstance = await _browserFactory.CreateBrowserInstance(Enums.BrowserType.Firefox);
        _webkitInstance = await _browserFactory.CreateBrowserInstance(Enums.BrowserType.Webkit);
        if (!Directory.Exists(_testPackLocation)) throw new Exceptions.TestLoaderException(string.Format(Resources.TestLocationNotFound, _testPackLocation));
        foreach (var pack in Directory.EnumerateDirectories(_testPackLocation))
        {
            await RunPack(pack);
        }
    }

    private async Task RunPack(string packLocation)
    {
        _logger.Info($"---- Running Pack {Path.GetDirectoryName(packLocation)} ----");
        var testPack = Entities.TestPack.Load(packLocation, _logger);
        var browserContext = await _chromeInstance.NewContextAsync();
        var browserHelper = new Core.BrowserHelper(browserContext, testPack.Identifiers);
        var commandFactory = new Core.Commands.Factory(browserHelper, packLocation);

        foreach (var test in testPack.Tests)
        {
            _logger.Warn("Running Test...");
            await test.Run(commandFactory);
            _logger.Warn("------ END ------");
        }
    }
}