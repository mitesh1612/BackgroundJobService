using Newtonsoft.Json.Linq;

namespace BackgroundJobService.Models
{
    /// <summary>
    /// Job definition object that gets stored in the job definition store.
    /// </summary>
    public class JobDefinition
    {
        public string JobId { get; set; }

        public string JobCallbackName { get; set; }

        public JObject JobMetadata { get; set; }

        public DateTime JobQueueTime { get; set; }

        public JobDefinition()
        { }

        public JobDefinition(string jobId, string jobCallbackName, JObject jobMetadata, DateTime queueTime)
        {
            JobId = jobId;
            JobCallbackName = jobCallbackName;
            JobMetadata = jobMetadata;
            JobQueueTime = queueTime;
        }
    }
}
