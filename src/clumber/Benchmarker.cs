using BenchmarkDotNet.Attributes;

namespace Clumber;

[MemoryDiagnoser]
public class Benchmarker
{
    private readonly string screenshotFolder = "./TestPacks/test01/screenshots";
    private readonly string testFilePath = "./TestPacks/test01/Tests/test1.ctf";
    private readonly string identFile = "./TestPacks/test01/Identifiers.cvf";
    Clumber.Core.IdentifierParser identParser;

    public Benchmarker()
    {
        identParser = new Clumber.Core.IdentifierParser(identFile);
    }

    [Benchmark]
    public async Task RunDelegateTestRunner()
    {
        var identifiers = identParser.Parse();
        await using (var browser = await Clumber.Core.Browser.CreateChromeBrowser(identifiers))
        {
            var parser = new Clumber.Core.TestFileParser(testFilePath);
            var test = parser.Parse();
            var commands = new Clumber.Core.Delegates(browser, identifiers, screenshotFolder);
            foreach (var instruction in test)
            {
                await commands.RunCommand(instruction.Command, instruction.Inputs);
            }
            System.Threading.Thread.Sleep(5000);
        }
    }

    [Benchmark]
    public async Task RunFactoryCommandTestRunner()
    {
        await using (var browser = await Clumber.Core.Browser.CreateChromeBrowser(identParser.Parse()))
        {
            var commandFactory = new Clumber.Core.Commands.Factory(browser);
            var parser = new Clumber.Core.TestFileParser(testFilePath);
            var test = parser.Parse();
            foreach (var instruction in test)
            {
                await commandFactory.CreateCommand(instruction.Command).Run(instruction.Inputs);
            }
            System.Threading.Thread.Sleep(5000);
        }
    }
}