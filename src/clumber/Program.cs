// See https://aka.ms/new-console-template for more information
using System;

Console.WriteLine("MVP of UI test suite for non devs");

var screenshotFolder = "./TestPacks/test01/screenshots";
var testFilePath = "./TestPacks/test01/Tests/test1.ctf";
var identFile = "./TestPacks/test01/Identifiers.cvf";
var identParser = new Clumber.Core.IdentifierParser(identFile);

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