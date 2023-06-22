using System.Collections.Generic;

namespace Clumber.Entities;

public class TestFile
{
    public List<Instruction> Instructions { get; private set; } = default!;

    public string FileLocation { get; init; } = default!;

    public TestFile(string location)
    {
        FileLocation = location;
    }
}