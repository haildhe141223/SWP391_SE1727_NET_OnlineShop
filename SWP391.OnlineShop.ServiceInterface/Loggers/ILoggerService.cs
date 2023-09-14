namespace SWP391.OnlineShop.ServiceInterface.Loggers;

public interface ILoggerService
{
    void LogError(string message);
    void LogError(Exception e, string? message = null);
    void LogWarning(string message);
    void LogDebug(string message);
    void LogInfo(string message);
}