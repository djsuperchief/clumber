using System.Collections.Generic;
using System.IO;
using Clumber.Contracts;
using Clumber.Core;
using Microsoft.Playwright;

namespace Clumber.Entities;

public class TestPack
{
    private const string TestsDirectory = "Tests";
    private const string IdentifiersFile = "Identifiers.cvf";

    private const string ConfigFile = "Config.ccf";
    private readonly IdentifierParser _identifierParser = new();
    private readonly IBrowserFactory _browserFactory;
    private readonly ILogger _logger;
    private readonly Config _config;

    private IBrowser _browserInstance = default!;

    public IEnumerable<Entities.Identifier>? Identifiers { get; private set; }

    public List<Entities.TestFile> Tests { get; private set; } = default!;

    public string Location { get; private set; } = default!;

    private TestPack(IBrowserFactory browserFactory, ILogger logger, string location)
    {
        _browserFactory = browserFactory;
        _logger = logger;
        Location = location;
        _config = Entities.Config.Load(Path.Combine(Location, ConfigFile));
    }

    public async Task RunTests()
    {
        _browserInstance = await _browserFactory.CreateBrowserInstance(_config);
        var browserContext = await _browserInstance.NewContextAsync();
        var browserHelper = new Core.BrowserHelper(browserContext, Identifiers);
        var commandFactory = new Core.Commands.Factory(browserHelper, Location);

        foreach (var test in Tests)
        {
            _logger.Warn("Running Test...");
            await test.Run(commandFactory);
            _logger.Warn("------ END ------");
        }

    }

    public static TestPack Load(string directory, ILogger logger, IBrowserFactory browserFactory)
    {
        var tests = Directory.GetFiles(Path.Combine(directory, TestsDirectory), "*.ctf");

        if (tests.Length == 0)
        {
            throw new Exceptions.TestLoaderException(Resources.NoTestsFound);
        }

        var response = new TestPack(browserFactory, logger, directory)
        {
            Tests = new()
        };

        if (Path.Exists(Path.Combine(directory, IdentifiersFile)))
        {
            response._identifierParser.Parse(Path.Combine(directory, IdentifiersFile));
        }

        foreach (var test in tests)
        {
            response.Tests.Add(new Entities.TestFile(test, directory, logger));
        }

        return response;
    }
}
