# BackgroundJobService

A PoC of implementing a basic BackgroundJobService from Scratch.
The code doesnt rely on any particular document storage and queue service for its operation and exposes basic interfaces for working with such data providers.
A sample in-memory/file system based implementation is also provided for demo purposes.

## How to use

Using the background job service requires you to follow the following steps

### Creating Data Providers

First you need to create a class that implements `IDocumentDataProvider` for storing job definitions.
See the sample `FileBasedDocumentProvider` for reference

Then you need to create a wrapper class around a queue that stores strings that implements `IQueueProvider` to use for job queue.
See the sample `ChannelBasedQueueProvider` for reference.

### Creating Job Callbacks

To create JobCallbacks, first define the metadata for the job. If your job needs no metadata, you can use the base `JobMetadata` class provided.
After that create a class that inherits `BaseJobCallback<T>` where T is the type of your job metadata.
Then apply the `JobCallbackName` attribute with a string that represents the name of the job.

Inheriting this requires you to implement the `Execute` method in your callback which will contain the job code.
It gets the job metadata object as a public member of the callback class. Return appropriate `JobExecutionResult`.

Example:

```cs
public NewJobCallback : BaseJobCallback<NewJobMetadata>
{
	public override JobExecutionResult Execute()
	{
		// Use metadata directly as this.JobMetadata
		return new JobExecutionResult();
	}
}
```

Can also refer to the `SimpleLoggerJob` provided.

### Queuing jobs using Job Manager

Use the `JobManager` class methods to queue jobs. It requires a `IDocumentDataProvider` and `IQueueProvider`.
Use the same name provided to your job callback using the `JobCallbackName` attribute while queuing the job and pass appropriate metadata.

Note these providers should write to the same queue and document store that the worker is going to read from.

### Running the Background Job Service as a worker service

To run the background job service as a worker service, do the following:

1. Create a host using `HostBuilder`.
2. Add the implemented data providers in the services of the host.
3. Add the `JobDispatcher` class as a hosted service to the service collection using `AddHostedService<T>()` extension method.
4. Run the code.

Hopefully your worker service starts. After that use the job manager to queue jobs and they should be picked up by the worker service.

## Future/Pending Work and Improvements

1. Allow jobs to consume any injected service in the service collection.
2. Add more features like scheduling jobs.
3. Refactoring the code to make it usable as an SDK and modifying documentation accordingly.
4. Use concrete models and proper serialization for Document Models
5. Build adapters for common document databases and common queue services
6. Use subscribe to queue mechanisms for job dispatcher if possible, instead of polling the queue constantly.