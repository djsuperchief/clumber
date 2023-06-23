using BenchmarkDotNet.Attributes;
using Microsoft.Playwright;

namespace Clumber;

[MemoryDiagnoser]
public class Benchmarker
{
    private readonly string screenshotFolder;
    private readonly string testFilePath;
    private readonly string identFile;
    private IBrowser playwright;
    Clumber.Core.IdentifierParser identParser;

    public Benchmarker()
    {
        var folderName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        screenshotFolder = $"{folderName}/BenchmarkPack/bbc/screenshots";
        testFilePath = $"{folderName}/BenchmarkPack/bbc/Tests/test1.ctf";
        identFile = $"{folderName}/BenchmarkPack/bbc/Identifiers.cvf";
        identParser = new Clumber.Core.IdentifierParser(identFile);

    }

    [GlobalSetup]
    public async Task Setup()
    {
        playwright = await Clumber.Core.BrowserV2.CreateChromeBrowser();
    }

    [Benchmark]
    public async Task RunDelegateTestRunner()
    {
        var identifiers = identParser.Parse();
        var context = await playwright.NewContextAsync();
        var browser = new Core.BrowserV2(context, identifiers);

        var parser = new Clumber.Core.TestFileParser(testFilePath);
        var test = parser.Parse();
        var commands = new Clumber.Core.Delegates(browser, identifiers, screenshotFolder);
        foreach (var instruction in test)
        {
            await commands.RunCommand(instruction.Command, instruction.Inputs);
        }

        await context.CloseAsync();
    }

    [Benchmark]
    public async Task RunFactoryCommandTestRunner()
    {
        var identifiers = identParser.Parse();
        var context = await playwright.NewContextAsync();
        var browser = new Core.BrowserV2(context, identifiers);
        var commandFactory = new Clumber.Core.Commands.Factory(browser);
        var parser = new Clumber.Core.TestFileParser(testFilePath);
        var test = parser.Parse();
        foreach (var instruction in test)
        {
            await commandFactory.CreateCommand(instruction.Command).Run(instruction.Inputs);
        }
        await context.CloseAsync();
    }
}