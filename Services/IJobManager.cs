using Newtonsoft.Json.Linq;

namespace BackgroundJobService.Services
{
    // Interface used to queue jobs
    public interface IJobManager
    {
        public void QueueJob(string jobName, JObject jobMetadata);
    }
}
