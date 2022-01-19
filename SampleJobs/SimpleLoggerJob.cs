using BackgroundJobService.Models;
using BackgroundJobService.SampleJobs.SampleJobMetadata;

namespace BackgroundJobService.SampleJobs
{
    [JobCallbackName(nameof(SimpleLoggerJob))]
    public class SimpleLoggerJob : BaseJobCallback<SimpleLoggerJobMetadata>
    {
        public override JobExecutionResult Execute()
        {
            Console.WriteLine($"Secret String: {this.JobMetadata?.SecretString}");
            return new JobExecutionResult { Message = "Done.", Status = JobExecutionStatus.Completed };
        }
    }
}
