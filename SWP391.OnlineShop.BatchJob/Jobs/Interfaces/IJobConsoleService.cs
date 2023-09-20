using SWP391.OnlineShop.BatchJob.Jobs.BaseJobs;

namespace SWP391.OnlineShop.BatchJob.Jobs.Interfaces;

public interface IJobConsoleService : IBaseJob
{
    void RunJob();
}