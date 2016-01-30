using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Util
{
    public interface ILogProvider
    {
        string LoggerName { get; }

        void Log(string category, LogLevel level, string message);

        void Log(LogLevel level, string message, Exception exception = null);
    }
    public enum LogLevel
    {
        Info = 1,
        Debug = 2,
        Warn = 3,
        Error = 4,
        Fatal = 5,
        Local = 6,
    }
}
