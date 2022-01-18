namespace BackgroundJobService.Models
{
    public class JobExecutionResult
    {
        public JobExecutionStatus Status { get; set; }

        public string Message { get; set; }
    }

    public enum JobExecutionStatus
    {
        Queued,
        Completed,
        Failed
    }
}