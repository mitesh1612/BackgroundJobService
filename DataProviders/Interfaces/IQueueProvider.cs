namespace BackgroundJobService.DataProviders.Interfaces
{
    public interface IQueueProvider
    {
        public void WriteToQueue(string value);

        public string ReadFromQueue();
    }
}
