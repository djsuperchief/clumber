using System.IO;

namespace Clumber.Core;

public class TestRunner
{
    private readonly string _testPackLocation;

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
}