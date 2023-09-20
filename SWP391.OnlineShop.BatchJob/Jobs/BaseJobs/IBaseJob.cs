namespace SWP391.OnlineShop.BatchJob.Jobs.BaseJobs;

public interface IBaseJob
{
    string JobName { get; }
    string CronExpression { get; }
}