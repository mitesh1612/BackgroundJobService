using Newtonsoft.Json;

namespace BackgroundJobService.Models
{
    /// <summary>
    /// All job callbacks should inherit this abstract class to make itself a background job.
    /// </summary>
    /// <typeparam name="T">Type of job metadata. Should inherit from the JobMetadata class.</typeparam>
    public abstract class BaseJobCallback<T> : IJobCallback
        where T : JobMetadata
    {
        public T JobMetadata;

        public BaseJobCallback(string serializedJobMetadata)
        {
            this.InitializeWithMetadata(serializedJobMetadata);
        }

        public BaseJobCallback()
        {
            this.JobMetadata = null;
        }

        protected void InitializeWithMetadata(string serializedJobMetadata)
        {
            this.JobMetadata = JsonConvert.DeserializeObject<T>(serializedJobMetadata);
        }

        public abstract JobExecutionResult Execute();
    }
}
