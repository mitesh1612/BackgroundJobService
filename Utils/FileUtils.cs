namespace BackgroundJobService.Utils
{
    public class FileUtils
    {
        private readonly string _baseDirectory;
        private readonly string _defaultFileExtension;

        public FileUtils(string baseDirectory = ".", string defaultFileExtension = ".json")
        {
            _baseDirectory = baseDirectory;
            _defaultFileExtension = defaultFileExtension.StartsWith(".") ? defaultFileExtension : "." + defaultFileExtension;
            this.CreateDirectoryIfNotExists();
        }

        public void WriteToFile(string fileName, string fileContent, bool throwIfExists = false)
        {
            var filePath = this.GetFilePathWithExtension(fileName);
            var fileExists = File.Exists(filePath);
            if (fileExists && throwIfExists)
            {
                throw new InvalidOperationException($"File {filePath} already exists.");
            }

            File.WriteAllText(filePath, fileContent);
        }

        public string ReadFile(string fileName, bool throwIfNotExists = false)
        {
            var filePath = this.GetFilePathWithExtension(fileName);
            var fileExists = File.Exists(filePath);
            if (!fileExists && throwIfNotExists)
            {
                throw new InvalidOperationException($"{filePath} file doesn't exist.");
            }

            var fileData = string.Empty;
            try
            {
                fileData = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    return fileData;
                }

                throw;
            }

            return fileData;
        }

        public void DeleteFile(string fileName)
        {
            var filePath = this.GetFilePathWithExtension(fileName);
            File.Delete(filePath);
        }

        private string GetFilePathWithExtension(string fileName)
        {
            fileName = fileName + this._defaultFileExtension;
            var filePath = Path.Combine(this._baseDirectory, fileName);
            return filePath;
        }

        private void CreateDirectoryIfNotExists()
        {
            if (!Directory.Exists(_baseDirectory) || !_baseDirectory.Equals("."))
            {
                Directory.CreateDirectory(_baseDirectory);
            }
        }
    }
}
