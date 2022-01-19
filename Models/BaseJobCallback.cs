using Newtonsoft.Json;

namespace BackgroundJobService.Models
{
    public abstract class BaseJobCallback<T> : IJobCallback
        where T : JobMetadata
    {
        public T _jobMetadata;

        public BaseJobCallback(string serializedJobMetadata)
        {
            this.InitializeWithMetadata(serializedJobMetadata);
        }

        public BaseJobCallback()
        {
            this._jobMetadata = null;
        }

        protected void InitializeWithMetadata(string serializedJobMetadata)
        {
            this._jobMetadata = JsonConvert.DeserializeObject<T>(serializedJobMetadata);
        }

        public abstract JobExecutionResult Execute();
    }
}
