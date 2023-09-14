using NLog;

namespace SWP391.OnlineShop.ServiceInterface.Loggers;

public class LoggerService : ILoggerService
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public void LogError(string message)
    {
        Logger.Error(message);
    }

    public void LogError(Exception e, string? message = null)
    {
        Logger.Error(e, message);
    }

    public void LogWarning(string message)
    {
        Logger.Warn(message);
    }

    public void LogDebug(string message)
    {
        Logger.Debug(message);
    }

    public void LogInfo(string message)
    {
        Logger.Info(message);
    }
}