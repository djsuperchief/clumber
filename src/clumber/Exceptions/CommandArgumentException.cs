using System;

namespace Clumber.Exceptions;
public class CommandArgumentException : Exception
{
    public CommandArgumentException(string message) : base(message)
    { }

    public CommandArgumentException(string message, Exception innerException) : base(message, innerException)
    { }
}
