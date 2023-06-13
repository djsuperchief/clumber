namespace Clumber.Entities;
public class Identifier
{
    private readonly Dictionary<Enums.ObjectTypeIdentifier, string> _identifierOutput = new Dictionary<Enums.ObjectTypeIdentifier, string>()
    {
        {Enums.ObjectTypeIdentifier.Css,"css"},
        {Enums.ObjectTypeIdentifier.Id, "id" },
        {Enums.ObjectTypeIdentifier.Tag,"tag"}
    };
    public Enums.ObjectTypeIdentifier IdentifierType { get; init; }

    public string Name { get; init; }

    public string Value { get; init; }

    public override string ToString()
    {
        return $"{_identifierOutput[IdentifierType]}={Value}";
    }
}
