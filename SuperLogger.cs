namespace Logger
{
    /// <summary>
    /// Logger for file and console
    /// </summary>
    public class SuperLogger : ILogger
    {
        private string Folder { get; }
        private string FileAll { get; }
        private string FileSuccess { get; }
        private string FileError { get; }

        private readonly bool _withConsole;

        private static bool _inited = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder">Folder for log files</param>
        /// <param name="withConsole">Write log to console with files?</param>
        /// <param name="removeIfExists">Remove old log dir, otherwise rename old log dir</param>
        public SuperLogger(string folder = "logs", bool withConsole = true, bool removeIfExists = true)
        {
            _withConsole = withConsole;
            Folder = folder;
            FileAll = folder + "/all.log";
            FileSuccess = folder + "/success.log";
            FileError = folder + "/error.log";

            if (!_inited)
            {
                if (Directory.Exists(Folder))
                {
                    if (removeIfExists)
                    {
                        Directory.Delete(Folder, true);
                    }
                    else
                    {
                        Directory.Move(Folder, $"{Folder}-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");
                    }
                }

                Directory.CreateDirectory(Folder);

                _inited = true;
            }
        }

        private static string GetFormattedText(string text)
        {
            return $"[{DateTime.UtcNow:s}] {text}\n";
        }

        private string LogAll(string text)
        {
            text = GetFormattedText(text);

            if (_withConsole)
            {
                Console.Write(text);
            }

            File.AppendAllText(FileAll, text);

            return text;
        }

        public void Log(string text)
        {
            LogAll(text);
        }

        public void Success(string text)
        {
            text = LogAll(text);

            File.AppendAllText(FileSuccess, text);
        }

        public void Error(string text)
        {
            text = LogAll(text);

            File.AppendAllText(FileError, text);
        }
    }
}