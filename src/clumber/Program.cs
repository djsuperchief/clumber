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
            // run test runner
            Console.WriteLine("Test runner not yet implemented");
            break;
        default:
            Console.WriteLine("Switch not recognised.");
            return;
    }
}