namespace Logger
{
    public interface ILogger
    {
        void Log(string text);
        void Success(string text);
        void Error(string text);
    }
}
