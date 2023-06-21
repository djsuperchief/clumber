using System;

namespace Clumber.Exceptions;
public class CommandNotFoundException : Exception
{
    public CommandNotFoundException(string message) : base(message)
    { }

    public CommandNotFoundException(string message, Exception innerException) : base(message, innerException)
    { }
}
