using BackgroundJobService.DataProviders.Interfaces;
using BackgroundJobService.Utils;
using Newtonsoft.Json.Linq;

namespace BackgroundJobService.DataProviders
{
    public class FileBasedDocumentProvider : IDocumentDataProvider
    {
        private readonly string _collectionName;

        private readonly FileUtils _fileUtils;

        public FileBasedDocumentProvider(string collectionName = "data", string baseDirectory = "DocDatabase")
        {
            this._collectionName = collectionName;
            var fullBasePath = Path.Combine(baseDirectory, _collectionName);
            Directory.CreateDirectory(fullBasePath);
            this._fileUtils = new FileUtils(fullBasePath);
        }

        public void CreateDocument(JObject document, string documentId)
        {
            _fileUtils.WriteToFile(documentId, document.ToString());
        }

        public JObject DeleteDocument(string id)
        {
            var fileContent = this.GetDocumentById(id);
            _fileUtils.DeleteFile(id);
            return fileContent;
        }

        public JObject GetDocumentById(string id)
        {
            var readData = _fileUtils.ReadFile(id);
            return JObject.Parse(readData);
        }
    }
}
