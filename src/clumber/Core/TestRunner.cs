using System.IO;
using Microsoft.Playwright;

namespace Clumber.Core;

public class TestRunner
{
    private readonly string _testPackLocation;
    private IBrowser _chromeInstance;
    private IBrowser _firefoxInstance;
    private IBrowser _webkitInstance;

    public TestRunner(string testPackLocation)
    {
        _testPackLocation = testPackLocation;
    }

    public async Task Run()
    {
        // Line too long
        if (!Directory.Exists(_testPackLocation)) throw new Exceptions.TestLoaderException(string.Format(Resources.TestLocationNotFound, _testPackLocation));
        foreach (var pack in Directory.EnumerateDirectories(_testPackLocation))
        {
            
        }
    }

    private async Task RunPack(string packLocation)
    {

    }
}