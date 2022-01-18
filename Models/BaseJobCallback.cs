namespace BackgroundJobService.Models
{
    public abstract class BaseJobCallback<T> : IJobCallback
        where T : JobMetadata
    {
        public T _jobMetadata;

        public BaseJobCallback()
        {
            this._jobMetadata = null;
        }

        public BaseJobCallback(T jobMetadata)
        {
            this._jobMetadata = jobMetadata;
        }

        public abstract JobExecutionResult Execute();
    }
}
