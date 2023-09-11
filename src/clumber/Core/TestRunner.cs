using System.IO;
using Clumber.Contracts;
using Microsoft.Playwright;

namespace Clumber.Core;

public class TestRunner
{
    private readonly string _testPackLocation;

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
        if (!Directory.Exists(_testPackLocation)) throw new Exceptions.TestLoaderException(string.Format(Resources.TestLocationNotFound, _testPackLocation));
        foreach (var pack in Directory.EnumerateDirectories(_testPackLocation))
        {
            await RunPack(pack);
        }
    }

    private async Task RunPack(string packLocation)
    {
        _logger.Info($"---- Running Pack {Path.GetDirectoryName(packLocation)} ----");
        var testPack = Entities.TestPack.Load(packLocation, _logger, _browserFactory);
        await testPack.RunTests();
    }
}