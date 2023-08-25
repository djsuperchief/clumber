using System.Collections.Generic;
using System.IO;
using Clumber.Core;

namespace Clumber.Entities;

public class TestPack
{
    private const string TestsDirectory = "Tests";
    private const string IdentifiersFile = "Identifiers.cvf";
    private readonly IdentifierParser _identifierParser = new();

    public IEnumerable<Entities.Identifier>? Identifiers { get; private set; }

    public List<Entities.TestFile> Tests { get; private set; } = default!;

    public string Location { get; private set; } = default!;

    public static TestPack Load(string directory, ILogger logger)
    {
        var tests = Directory.GetFiles(Path.Combine(directory, TestsDirectory), "*.ctf");

        if (tests.Length == 0)
        {
            throw new Exceptions.TestLoaderException(Resources.NoTestsFound);
        }

        var response = new TestPack()
        {
            Location = directory,
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
