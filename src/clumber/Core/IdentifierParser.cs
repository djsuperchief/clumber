using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Clumber.Core;
public class IdentifierParser
{
    private readonly string _identifierFile;

    private readonly Regex _identRegex = new Regex(@"(?<=\[)(.*?)(?=\])", RegexOptions.Compiled);

    private readonly Dictionary<string, Enums.ObjectTypeIdentifier> _identifiers = new Dictionary<string, Enums.ObjectTypeIdentifier>()
    {
        { "id", Enums.ObjectTypeIdentifier.Id},
        { "css", Enums.ObjectTypeIdentifier.Css},
        { "tag", Enums.ObjectTypeIdentifier.Tag},
    };

    public IdentifierParser(string identifierFile)
    {
        _identifierFile = identifierFile;
    }

    public List<Entities.Identifier> Parse()
    {
        var entities = new List<Entities.Identifier>();
        using var reader = new StreamReader(_identifierFile);
        do
        {
            var unsplit = reader.ReadLine();
            var line = unsplit?.Split(" ");
            if (line.Length > 0)
            {
                var name = line[0].Trim();
                var rest = _identRegex.Match(unsplit)?.Value.Split('=');
                entities.Add(new Entities.Identifier()
                {
                    Name = name,
                    IdentifierType = _identifiers[rest[0]],
                    Value = rest[1]
                });

            }
        } while (reader.EndOfStream == false);

        return entities;
    }
}
