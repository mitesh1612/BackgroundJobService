using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundJobService.Models
{
    public interface IJobCallback
    {
        public JobExecutionResult Execute();
    }
}
