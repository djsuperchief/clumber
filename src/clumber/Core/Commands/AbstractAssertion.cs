namespace Clumber.Core.Commands;

public abstract class AbstractAssertion : AbstractCommand
{
    protected readonly Factory _commandFactory;
    protected AbstractAssertion(BrowserV2 browser, Factory commandFactory) : base(browser)
    {
        _commandFactory = commandFactory;
    }
}