using Newtonsoft.Json;

namespace RLH.JsonUtility
{
    /// <summary>
    /// Manages reading and Deserializing of single or collection of Class types from json files.
    /// </summary>
    public sealed class JsonFileParser : IJsonFileParser
    {
        private bool disposedValue;

        /// <summary>
        /// Path where json files are located. Set in constructor and defaults to the current executing directory
        /// </summary>
        private string _basePath;

        /// <summary>
        /// Create an instance of JsonFileParser WITH a custom base path.
        /// This can be a full or relative path from the current executing directory
        /// </summary>
        /// <param name="basePath">Full or partial path where json files are located</param>
        /// <exception cref="ArgumentException">Thrown if the basePath parameter is null,blank or whitespace</exception>
        public JsonFileParser(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                throw new ArgumentException($"'{nameof(basePath)}' cannot be null or whitespace.", nameof(basePath));
            }

            _basePath = Path.IsPathRooted(basePath) ? basePath : Path.Combine(Environment.CurrentDirectory, basePath);
        }

        /// <summary>
        /// Create an instance of JsonFileParser with a default path
        /// set as the current executing directory
        /// </summary>
        public JsonFileParser()
        {
            _basePath = Environment.CurrentDirectory;
        }

        /// <summary>
        /// Deserializes and returns 'TClass' type
        /// </summary>
        /// <typeparam name="TClass">Type of class to deserialize</typeparam>
        /// <param name="fileName">(Optional) exact file name, if none provided the name of 'TClass' is used</param>
        /// <returns>New TClass from file</returns>
        public TClass Parse<TClass>(string? fileName = null) where TClass : class
        {
            return JsonConvert.DeserializeObject<TClass>(ReadAllFromFile(GetFileNameWithExtension<TClass>(fileName)));
        }


        /// <summary>
        /// Deserializes and returns a collection of 'TClass' type
        /// </summary>
        /// <typeparam name="TClass">Type of class to deserialize</typeparam>
        /// <param name="fileName">(Optional) exact file name, if none provided the name of 'TClass' is used</param>
        /// <returns>IEnumerable of TClass from file</returns>
        public IEnumerable<TClass> ParseCollection<TClass>(string? fileName = null) where TClass : class
        {
            return JsonConvert.DeserializeObject<TClass[]>(ReadAllFromFile(GetFileNameWithExtension<TClass>(fileName)));
        }



        /// <summary>
        /// Takes a provided fileName and attempts to locate and read a file with this name
        /// at the directory provided in the constructor.
        /// </summary>
        /// <param name="fileName">fileName provided by user</param>
        /// <returns>string content of the whole file</returns>
        /// <exception cref="ArgumentNullException">Thrown if the fileName parameter is null</exception>
        /// <exception cref="IOException">Thrown if unable to read the file via streamreader</exception>
        /// <exception cref="NullReferenceException">Thrown if the file cannot be found at the configured path</exception>
        private string ReadAllFromFile(string fileName)
        {
            // Check that a filename value has been passed
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            // Create the full path from the base + filename
            var path = Path.Combine(_basePath, fileName);

            // If this file exists read to end and return string
            if (File.Exists(path) == true)
            {
                try
                {
                    using (var reader = new StreamReader(path))
                    {
                        return reader.ReadToEnd();
                    }
                }
                catch (IOException e)
                {
                    throw new IOException($"Failed to read external file at: {path} with exception: {e.Message}");
                }
            }
            else
            {
                throw new NullReferenceException($"File at location:{path} doesnt exist.");
            }
        }

        /// <summary>
        /// Takes a passed fileName and ensures the correct .json file extension is added
        /// if it wasnt already present.
        /// If the passed value is null then the filename is assumed to be the name
        /// of the passed 'TClass'.
        /// </summary>
        /// <typeparam name="TClass">Type of class being parsed, used as the fileName if none provided</typeparam>
        /// <param name="fileName">fileName provided by user, can be blank</param>
        /// <returns>string representing the fileName WITH .json extension</returns>
        private string GetFileNameWithExtension<TClass>(string fileName)
        {
            // if NO filename has been passed set as the name of the class passed through
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = typeof(TClass).Name;
            }
            fileName = fileName.Trim('\\');
            return Path.HasExtension(fileName) ? fileName : $"{fileName}.json";
        }










        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~JsonFileParser()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}
