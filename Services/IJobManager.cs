using BackgroundJobService.Models;
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
        public string QueueJob(string jobName, JObject jobMetadata);

        /// <summary>
        /// Get the job status for the job with given job id.
        /// Throws error if no such job is found.
        /// </summary>
        /// <param name="jobId">JobId for the requested job.</param>
        /// <returns>Status of the job.</returns>
        public JobStatus GetJobStatus(string jobId);
    }
}
