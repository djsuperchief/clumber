using System.Collections.Generic;

namespace Clumber.Entities;

public class TestPack
{
    public IEnumerable<Entities.Identifier> Identifiers { get; private set; }

    public List<Entities.TestFile> Tests { get; private set; }

    public string Location { get; private set; }
}
