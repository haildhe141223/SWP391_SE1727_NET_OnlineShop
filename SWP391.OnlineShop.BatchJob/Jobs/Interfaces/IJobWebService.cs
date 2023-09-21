namespace SWP391.OnlineShop.BatchJob.Jobs.Interfaces;

public interface IJobWebService
{
    void RunJob();
    string JobName { get; }
    string CronExpression { get; }
}