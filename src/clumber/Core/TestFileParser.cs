using System.Collections.Generic;
using System.IO;

namespace Clumber.Core;
public class TestFileParser
{
    private readonly string _testFile;

    public TestFileParser(string testFile)
    {
        _testFile = testFile;
    }

    public List<Entities.Instruction> Parse()
    {
        var response = new List<Entities.Instruction>();
        using var reader = new StreamReader(_testFile);
        do
        {
            var line = reader.ReadLine()?.Split(" ");
            if (line.Length > 0)
            {
                response.Add(new Entities.Instruction
                {
                    Command = line[0],
                    Inputs = string.Join(" ", line[1..]).Trim()
                });
            }
        } while (reader.EndOfStream == false);

        return response;
    }

    // This is probably a better option
    public static IEnumerable<Entities.Instruction> Parse(string testFile)
    {
        var response = new List<Entities.Instruction>();
        using var reader = new StreamReader(testFile);
        do
        {
            var line = reader.ReadLine()?.Split(" ");
            if (line.Length > 0)
            {
                response.Add(new Entities.Instruction
                {
                    Command = line[0],
                    Inputs = string.Join(" ", line[1..]).Trim()
                });
            }
        } while (reader.EndOfStream == false);

        return response;
    }
}
