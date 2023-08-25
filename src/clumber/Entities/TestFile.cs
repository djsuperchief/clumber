using System.Collections.Generic;
using Clumber.Core.Commands;

namespace Clumber.Entities;

public class TestFile
{
    public List<Instruction> Instructions { get; private set; } = default!;

    public string Title { get; private set; }

    public string FileLocation { get; init; } = default!;
    public string PackRoot { get; init; } = default!;

    private readonly ILogger _logger;

    public TestFile(string location, string packRoot, ILogger logger)
    {
        FileLocation = location;
        Instructions = new();
        PackRoot = packRoot;
        _logger = logger;
        Title = "No title set";
    }

    public async Task Run(Factory commandFactory)
    {
        Parse();
        _logger.TestInfo($"TEST: {Title}");
        foreach (var instruction in Instructions)
        {
            await commandFactory.CreateCommand(instruction.Command).Run(instruction.Inputs, PackRoot);
        }
    }

    private void Parse()
    {
        using var reader = new StreamReader(FileLocation);
        do
        {
            var readLine = reader.ReadLine();
            if (readLine?.Substring(0, 1) == "-")
            {
                ParseHeaderValue(readLine);
                continue;
            }

            var line = readLine?.Split(" ");
            if (line.Length > 0)
            {
                Instructions.Add(new Entities.Instruction
                {
                    Command = line[0],
                    Inputs = string.Join(" ", line[1..]).Trim()
                });
            }
        } while (reader.EndOfStream == false);
    }

    private void ParseHeaderValue(string line)
    {
        if (line.Substring(0, 4) == "----")
        {
            return;
        }

        var item = line.Substring(1).Split(" ");
        switch (item[0].ToLower())
        {
            case "title":
                Title = string.Join(" ", item[1..]);
                break;
        }
    }
}