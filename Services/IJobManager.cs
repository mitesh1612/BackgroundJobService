using Newtonsoft.Json.Linq;

namespace BackgroundJobService.Services
{
    /// <summary>
    /// Job Manager interface. Could be impleted to write your own job manager logic.
    /// </summary>
    public interface IJobManager
    {
        /// <summary>
        /// Queue a job given its name and metadata.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="jobMetadata">Job Metadata for the job.</param>
        public void QueueJob(string jobName, JObject jobMetadata);
    }
}
