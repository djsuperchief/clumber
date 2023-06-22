namespace Clumber.Core.Commands;

public abstract class AbstractAssertion : AbstractCommand
{
    protected readonly Factory _commandFactory;
    protected AbstractAssertion(Browser browser, Factory commandFactory) : base(browser)
    {
        _commandFactory = commandFactory;
    }
}