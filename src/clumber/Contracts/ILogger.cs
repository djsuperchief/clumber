public interface ILogger
{
    void Info(string message);

    void Warn(string message);

    void Error(string message);

    void Fatal(string message);

    void Ok(string message);

    void TestInfo(string message);
}