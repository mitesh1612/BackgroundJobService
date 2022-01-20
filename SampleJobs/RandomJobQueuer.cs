using BackgroundJobService.SampleJobs.SampleJobMetadata;
using BackgroundJobService.Services;
using Newtonsoft.Json.Linq;

namespace BackgroundJobService.SampleJobs
{
    public class RandomJobQueuer : BackgroundService
    {
        private readonly ILogger<RandomJobQueuer> _logger;
        private readonly IJobManager _jobManager;
        private int currentCount = 0;
        private int maxCount = 5;

        public RandomJobQueuer(IJobManager jobManager, ILogger<RandomJobQueuer> logger)
        {
            _jobManager = jobManager;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await QueueJob();
                await Task.Delay(1000);
                currentCount++;
                if (currentCount > maxCount)
                    break;
            }
        }

        private async Task QueueJob()
        {
            var secretString = GetRandomString();
            var jobMetadata = new SimpleLoggerJobMetadata()
            {
                SecretString = secretString,
            };

            _logger.LogInformation($"Queueing Job with Secret String: {secretString}");

            _jobManager.QueueJob(nameof(SimpleLoggerJob), JObject.FromObject(jobMetadata));
            await Task.CompletedTask;
        }

        public static List<string> WordsPart1 = new List<string>
        {
            "Melodie",
            "Helena",
            "Harrietta",
            "Lorenza",
            "Briney",
            "Dorthy",
            "Shannah",
            "Heida",
            "Joane",
            "Noelle"
        };

        public static List<string> WordsPart2 = new List<string>
        {
            "Kenna",
            "Ekaterina",
            "Roberta",
            "Vivia",
            "Elfreda",
            "Leeanne",
            "Kayla",
            "Chandra",
            "Violet",
            "Hayley"
        };

        public static List<string> WordsPart3 = new List<string>
        {
            "Darci",
            "Clementine",
            "Yetty",
            "Fernande",
            "Joleen",
            "Melina",
            "Paule",
            "Tracey",
            "Merry",
            "Steffane"
        };

        public static string GetRandomString()
        {
            var rng = new Random();
            var firstWord = WordsPart1[rng.Next(0, WordsPart1.Count)];
            var secondWord = WordsPart2[rng.Next(0, WordsPart2.Count)];
            var thirdWord = WordsPart3[rng.Next(0, WordsPart3.Count)];

            return $"{firstWord.ToLower()}-{secondWord.ToLower()}-{thirdWord.ToLower()}";
        }
    }
}
