namespace BackgroundJobService.Models
{
    /// <summary>
    /// Class representing the execution result of a job.
    /// </summary>
    public class JobExecutionResult
    {
        public JobExecutionStatus Status { get; set; }

        public string Message { get; set; }
    }

    /// <summary>
    /// Enum representing the status of a completed job.
    /// </summary>
    public enum JobExecutionStatus
    {
        Completed,
        Failed
    }
}