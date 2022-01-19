namespace BackgroundJobService.Models
{
    /// <summary>
    /// Use the attribute to give your job callback names which are referred while queuing and running.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class JobCallbackNameAttribute : Attribute
    {
        public string JobCallbackName { get; set; }

        public JobCallbackNameAttribute(string jobCallbackName)
        {
            JobCallbackName = jobCallbackName;
        }
    }
}
