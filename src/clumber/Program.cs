// See https://aka.ms/new-console-template for more information
using System;

Console.WriteLine("MVP of UI test suite for non devs");

await using (var browser = await Clumber.Core.Browser.CreateChromeBrowser())
{
    var screenshotFolder = "./TestPacks/test01/screenshots";
    var testFilePath = "./TestPacks/test01/Tests/test1.ctf";
    var identFile = "./TestPacks/test01/Identifiers.cvf";
    var parser = new Clumber.Core.TestFileParser(testFilePath);
    var identParser = new Clumber.Core.IdentifierParser(identFile);
    var test = parser.Parse();
    var idents = identParser.Parse();
    var commands = new Clumber.Core.Commands(browser, idents, screenshotFolder);
    foreach (var instruction in test)
    {
        await commands.RunCommand(instruction.Command, instruction.Inputs);
    }
    System.Threading.Thread.Sleep(5000);
}