// See https://aka.ms/new-console-template for more information
using System;
using BenchmarkDotNet.Running;

Console.WriteLine("MVP of UI test suite for non devs");
for (var i = 0; i < args.Length; i++)
{
    // ewww
    switch (args[i].ToLower())
    {
        case "-b":
            Console.WriteLine("Benchmarking");
            var summary = BenchmarkRunner.Run<Clumber.Benchmarker>();
            break;
        case "-t":
            var testLocation = args[i + 1];
            var testRunner = new Clumber.Core.TestRunner(new Clumber.Core.BrowserFactory(), testLocation);
            await testRunner.Run();
            i++;
            continue;
        default:
            Console.WriteLine("Switch not recognised.");
            return;
    }
}