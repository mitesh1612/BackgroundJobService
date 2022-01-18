using BackgroundJobService;
using BackgroundJobService.DataProviders;
using BackgroundJobService.DataProviders.Interfaces;
using BackgroundJobService.SampleJobs;
using BackgroundJobService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IQueueProvider, ChannelBasedQueueProvider>();
        services.AddSingleton<IDocumentDataProvider, FileBasedDocumentProvider>();
        services.AddSingleton<IJobManager, JobManager>();
        services.AddHostedService<RandomJobQueuer>();
        services.AddHostedService<JobDispatcher>();
    })
    .Build();

await host.RunAsync();
