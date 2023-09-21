using Hangfire;
using SWP391.OnlineShop.BatchJob.Jobs.Interfaces;
using SWP391.OnlineShop.ServiceInterface.Loggers;

namespace SWP391.OnlineShop.BatchJob.Jobs.Implements;

/// <summary>
/// This function use for IJob Registration 
/// </summary>
public class RecurringRegistrationJob : IJobRegistration
{
    public IEnumerable<IJob> Jobs { get; set; }
    public ILoggerService Logger { get; set; }

    public RecurringRegistrationJob(
        IEnumerable<IJob> jobs,
        ILoggerService logger)
    {
        Jobs = jobs;
        Logger = logger;
    }

    public void InitiateJob()
    {
        try
        {
            foreach (var job in Jobs)
            {
                RecurringJob.AddOrUpdate(job.JobName,
                    () => job.RunJob(),
                    job.CronExpression);
            }
        }
        catch (Exception e)
        {
            Logger.LogError($"Error in IJob Initiate: {e}");
        }
    }
}

/// <summary>
/// This function use for IJobWindowService Registration
/// </summary>
public class RecurringRegistrationWindowServiceJob : IJobRegistration
{
    public IEnumerable<IJobWebService> Jobs { get; set; }
    public ILoggerService Logger { get; set; }

    public RecurringRegistrationWindowServiceJob(
        IEnumerable<IJobWebService> jobs,
        ILoggerService logger
    )
    {
        Jobs = jobs;
        Logger = logger;
    }

    public void InitiateJob()
    {
        try
        {
            foreach (var job in Jobs)
            {
                RecurringJob.AddOrUpdate(job.JobName,
                    () => job.RunJob(),
                    job.CronExpression);
            }
        }
        catch (Exception e)
        {
            Logger.LogError($"Error in IJobWindowService Initiate: {e}");
        }
    }
}