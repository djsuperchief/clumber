﻿using System.Collections.Generic;

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

    public string Name { get; init; } = default!;

    public string Value { get; init; } = default!;

    public override string ToString()
    {
        return $"{_identifierOutput[IdentifierType]}={Value}";
    }
}
