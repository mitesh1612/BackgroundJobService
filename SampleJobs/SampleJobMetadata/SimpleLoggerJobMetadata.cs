using BackgroundJobService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundJobService.SampleJobs.SampleJobMetadata
{
    public class SimpleLoggerJobMetadata : JobMetadata
    {
        public string SecretString { get; set; }

        public SimpleLoggerJobMetadata()
        { }

        public SimpleLoggerJobMetadata(string ss)
        {
            SecretString = ss;
        }
    }
}
