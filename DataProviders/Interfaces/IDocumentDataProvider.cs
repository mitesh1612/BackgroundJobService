using Newtonsoft.Json.Linq;

namespace BackgroundJobService.DataProviders.Interfaces
{
    public interface IDocumentDataProvider
    {
        public JObject GetDocumentById(string id);

        public void CreateDocument(JObject document, string documentId);

        public JObject DeleteDocument(string id);
    }
}
