namespace BackgroundJobService.DataProviders.Interfaces
{
    /// <summary>
    /// A Queue Provider that adds and retrieves string values from a queue.
    /// Used for the job queue.
    /// </summary>
    public interface IQueueProvider
    {
        /// <summary>
        /// Add given value to the queue.
        /// </summary>
        /// <param name="value">Value to add.</param>
        public void WriteToQueue(string value);

        /// <summary>
        /// Read the first value from the queue.
        /// Returns empty string if no value exists in the queue.
        /// </summary>
        /// <returns>Read value from the queue. Empty string if the queue is empty.</returns>
        public string ReadFromQueue();
    }
}
