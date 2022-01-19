using Newtonsoft.Json.Linq;

namespace BackgroundJobService.DataProviders.Interfaces
{
    /// <summary>
    /// Data provider to store and fetch documents. Used to store and retrieve job definitions.
    /// </summary>
    public interface IDocumentDataProvider
    {
        /// <summary>
        /// Get document for the provided id.
        /// </summary>
        /// <param name="id">Document Id.</param>
        /// <returns>Requested Document.</returns>
        public JObject GetDocumentById(string id);

        /// <summary>
        /// Create a document with given id.
        /// </summary>
        /// <param name="document">Document object.</param>
        /// <param name="documentId">Document Id.</param>
        public void CreateDocument(JObject document, string documentId);

        /// <summary>
        /// Delete the document for the provided id.
        /// </summary>
        /// <param name="id">Document Id.</param>
        /// <returns>The document that was deleted.</returns>
        public JObject DeleteDocument(string id);
    }
}
