using BenchmarkDotNet.Attributes;

namespace Clumber;

[MemoryDiagnoser]
public class Benchmarker
{
    private readonly string screenshotFolder;
    private readonly string testFilePath;
    private readonly string identFile;
    Clumber.Core.IdentifierParser identParser;

    public Benchmarker()
    {
        identParser = new Clumber.Core.IdentifierParser(identFile);
        var folderName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().FullName);
        screenshotFolder = $"{folderName}/test01/screenshots";
        testFilePath = $"{folderName}/test01/Tests/test1.ctf";
        identFile = "{folderName}/test01/Identifiers.cvf";
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