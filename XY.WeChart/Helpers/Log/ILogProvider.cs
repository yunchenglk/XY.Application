using System;

namespace XY.WeChart
{
    public interface ILogProvider
    {
        string LoggerName { get; }

        void Log(string category, LogLevel level, string message);

        void Log(LogLevel level, string message, Exception exception = null);
    }
}