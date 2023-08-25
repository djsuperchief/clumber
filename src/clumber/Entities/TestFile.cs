using System.Collections.Generic;
using Clumber.Core.Commands;

namespace Clumber.Entities;

public class TestFile
{
    public List<Instruction> Instructions { get; private set; } = default!;

    public string FileLocation { get; init; } = default!;
    public string PackRoot { get; init; } = default!;

    public TestFile(string location, string packRoot)
    {
        FileLocation = location;
        Instructions = new();
        PackRoot = packRoot;
    }

    public async Task Run(Factory commandFactory)
    {
        Parse();
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
            var line = reader.ReadLine()?.Split(" ");
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
}