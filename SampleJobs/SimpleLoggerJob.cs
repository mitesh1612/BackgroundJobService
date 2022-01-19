using BackgroundJobService.Models;
using BackgroundJobService.SampleJobs.SampleJobMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundJobService.SampleJobs
{
    [JobCallbackName(nameof(SimpleLoggerJob))]
    public class SimpleLoggerJob : BaseJobCallback<SimpleLoggerJobMetadata>
    {
        // This should not be required. Find another way to call the base class constructor and initialize metadata
        // and remove duplication
        public SimpleLoggerJob(string serializedJobMetadata) : base(serializedJobMetadata)
            { }

        public override JobExecutionResult Execute()
        {
            Console.WriteLine($"Secret String: {this._jobMetadata?.SecretString}");
            return new JobExecutionResult { Message = "Done.", Status = JobExecutionStatus.Completed };
        }
    }
}
