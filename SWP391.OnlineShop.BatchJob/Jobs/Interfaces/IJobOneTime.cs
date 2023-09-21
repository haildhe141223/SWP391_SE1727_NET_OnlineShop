namespace SWP391.OnlineShop.BatchJob.Jobs.Interfaces;

public interface IJobOneTime<in T> where T : new()
{
    void RunJob(T data);
    string JobName { get; }
    string CronExpression { get; }
}