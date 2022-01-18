namespace BackgroundJobService.Models
{
    public class JobCallbackNameAttribute : Attribute
    {
        public string JobCallbackName { get; set; }

        public JobCallbackNameAttribute(string jobCallbackName)
        {
            JobCallbackName = jobCallbackName;
        }
    }
}
