using BackgroundJobService.DataProviders.Interfaces;
using BackgroundJobService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace BackgroundJobService.Services
{
    /// <summary>
    /// Job Dispatcher service that reads from the queue and runs the jobs.
    /// Modeled as a BackgroundService to run as a hosted service.
    /// </summary>
    public class JobDispatcher : BackgroundService
    {
        private readonly ILogger<JobDispatcher> _logger;

        private readonly IDocumentDataProvider _jobDefStore;

        private readonly IQueueProvider _jobQueueProvider;

        private List<Type> _jobTypes;

        public JobDispatcher(IDocumentDataProvider defStore, IQueueProvider queueProvider, ILogger<JobDispatcher> logger)
        {
            _jobDefStore = defStore;
            _jobQueueProvider = queueProvider;
            _logger = logger;
            InitializeJobTypes();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                await RunJobFromQueue();
            }
        }

        private async Task RunJobFromQueue()
        {
            var jobId = _jobQueueProvider.ReadFromQueue();

            if (!string.IsNullOrWhiteSpace(jobId))
            {
                var serializedJobDef = _jobDefStore.GetDocumentById(jobId);
                var jobDef = JsonConvert.DeserializeObject<JobDefinition>(serializedJobDef.ToString());
                if (jobDef == null)
                {
                    _logger.LogError($"Couldn't find job definition for Job Id: {jobId}");
                    return;
                }

                var jobType = FindJobAssemblyForCallbackName(jobDef.JobCallbackName);
                if (jobType == null)
                {
                    _logger.LogError($"Couldn't find job assembly for job id {jobId} and callback name {jobDef.JobCallbackName}.");
                    return;
                }

                var serializedJobMetadata = jobDef.JobMetadata.ToString();
                var jobInstance = this.CreateJobCallbackInstanceFromType(jobType, serializedJobMetadata);
                _logger.LogInformation($"Running job with job id: {jobDef.JobId} with Callback: {jobDef.JobCallbackName}");
                try
                {
                    this.UpdateJobDefinitionStatus(jobDef, JobStatus.InProgress);
                    Task.Run(() => this.RunJobAndUpdateDefinition(jobDef, jobInstance));
                }
                catch (Exception ex)
                {
                    this.UpdateJobDefinitionStatus(jobDef, JobStatus.Failed);
                    _logger.LogError($"Job Execution Failed for Job Id: {jobId} with the following exception: {ex.ToString()}");
                }
            }

            await Task.CompletedTask;
        }

        private async Task RunJobAndUpdateDefinition(JobDefinition jobDefinition, IJobCallback jobInstance)
        {
            var executionResult = await Task.Run(jobInstance.Execute);
            switch (executionResult?.Status)
            {
                case JobExecutionStatus.Completed:
                    this.UpdateJobDefinitionStatus(jobDefinition, JobStatus.Completed);
                    break;
                case JobExecutionStatus.Failed:
                    this.UpdateJobDefinitionStatus(jobDefinition, JobStatus.Failed);
                    break;
                default:
                    this.UpdateJobDefinitionStatus(jobDefinition, JobStatus.Failed);
                    break;
            }
        }

        private void UpdateJobDefinitionStatus(JobDefinition jobDefinition, JobStatus newStatus)
        {
            if (jobDefinition != null)
            {
                jobDefinition.JobStatus = newStatus;
                this._jobDefStore.ReplaceDocument(JObject.FromObject(jobDefinition), jobDefinition.JobId);
            }
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

        private IJobCallback CreateJobCallbackInstanceFromType(Type jobType, string serializedJobMetadata)
        {
            var nonIntializedInstance = Activator.CreateInstance(jobType);
            var initializeMethod = nonIntializedInstance.GetType().GetMethod("InitializeWithMetadata", BindingFlags.NonPublic | BindingFlags.Instance);
            _ = initializeMethod.Invoke(nonIntializedInstance, new object[] { serializedJobMetadata });
            var jobInstance = nonIntializedInstance as IJobCallback;
            return jobInstance;
        }
    }
}
