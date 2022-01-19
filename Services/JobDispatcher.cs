using BackgroundJobService.DataProviders.Interfaces;
using BackgroundJobService.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace BackgroundJobService.Services
{
    // Long Running service that pick jobs from the queue and runs them.
    public class JobDispatcher : BackgroundService
    {
        private readonly IDocumentDataProvider _jobDefStore;

        private readonly IQueueProvider _jobQueueProvider;

        private List<Type> _jobTypes;

        public JobDispatcher(IDocumentDataProvider defStore, IQueueProvider queueProvider)
        {
            _jobDefStore = defStore;
            _jobQueueProvider = queueProvider;
            InitializeJobTypes();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                await RunJobs();
            }
        }

        private async Task RunJobs()
        {
            var jobId = _jobQueueProvider.ReadFromQueue();

            if (!string.IsNullOrWhiteSpace(jobId))
            {
                var serializedJobDef = _jobDefStore.GetDocumentById(jobId);
                var jobDef = JsonConvert.DeserializeObject<JobDefinition>(serializedJobDef.ToString());
                if (jobDef == null)
                {
                    Console.WriteLine($"Couldn't find job definition for Job Id: {jobId}");
                    await Task.CompletedTask;
                }

                var jobType = FindJobAssemblyForCallbackName(jobDef.JobCallbackName);
                if (jobType == null)
                {
                    Console.WriteLine($"Couldn't find job assembly for job id {jobId} and callback name {jobDef.JobCallbackName}.");
                    await Task.CompletedTask;
                }

                var serializedJobMetadata = jobDef.JobMetadata.ToString();
                var jobTypeCtor = jobType.GetConstructor(new Type[1] { typeof(string) });
                var jobInstance = (IJobCallback) jobTypeCtor.Invoke(new object[1] { serializedJobMetadata });
                await Task.Run(jobInstance.Execute);

                _jobDefStore.DeleteDocument(jobId);
            }

            await Task.CompletedTask;
        }

        private void InitializeJobTypes()
        {
            var jobTypes = Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.BaseType?.Name == typeof(BaseJobCallback<>).Name)
                    .ToList();
            this._jobTypes = jobTypes;
        }

        private Type FindJobAssemblyForCallbackName(string jobCallbackName)
        {
            foreach(var type in this._jobTypes)
            {
                var definedAttribute = type.GetCustomAttribute(typeof(JobCallbackNameAttribute), false) as JobCallbackNameAttribute;
                if (definedAttribute != null)
                {
                    if (definedAttribute.JobCallbackName.Equals(jobCallbackName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return type;
                    }
                }
            }

            return null;
        }
    }
}
