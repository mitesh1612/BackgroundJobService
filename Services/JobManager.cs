﻿using BackgroundJobService.DataProviders.Interfaces;
using BackgroundJobService.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundJobService.Services
{
    public class JobManager : IJobManager
    {
        private readonly IDocumentDataProvider _jobDefinitionStore;

        private readonly IQueueProvider _jobQueueProvider;

        public JobManager(IDocumentDataProvider jobDefStore, IQueueProvider jobQueueProvider)
        {
            this._jobDefinitionStore = jobDefStore;
            this._jobQueueProvider = jobQueueProvider;
        }

        public void QueueJob(string jobCallbackName, JObject jobMetadata)
        {
            var jobId = Guid.NewGuid().ToString();
            var jobDefinition = new JobDefinition(jobId, jobCallbackName, jobMetadata, DateTime.Now);
            _jobDefinitionStore.CreateDocument(JObject.FromObject(jobDefinition), jobId);
            _jobQueueProvider.WriteToQueue(jobId);
        }
    }
}
