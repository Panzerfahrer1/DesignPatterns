using Logger;
using System.Globalization;

namespace Logger {
    internal class Program {
        public const string FILENAME = @"C:\Users\olive\Downloads\ILoggerAids.txt";
        static void Main(string[] args) {
            FileLogger fileLogger = new(LogLevels.Information | LogLevels.Warning, FILENAME);
            ConsoleLogger consoleLogger = new(LogLevels.All);
            consoleLogger.NextLogger = fileLogger;

            consoleLogger.Log("Log", LogLevels.Debug);
            consoleLogger.Log("Warnung", LogLevels.Warning);

            ILogger factoryLogger = LoggerFactory.CreateLoggerChain();
        }
    }
}

public class FileLogger : AbstractLogger {
    public string FilePath { get; set; }
    public FileLogger(LogLevels levels, string filePath) : base(levels) {
        FilePath = filePath;
    }

    protected override void DoLog(string message) {
        if (this.Levels.HasFlag(base.Levels)) {
            File.AppendAllLines(this.FilePath, new string[] { message });
        }

        NextLogger?.Log(message, base.Levels);
    }
}

public class ConsoleLogger : AbstractLogger {
    public ConsoleLogger(LogLevels levels) : base(levels) {

    }

    protected override void DoLog(string message) {
        Console.WriteLine(message);
    }
}

public interface ILogger {
    public ILogger NextLogger { get; set; }
    void Log(string message, LogLevels levels);
}


[Flags]
public enum LogLevels {
    Debug = 1,
    Information = 2,
    Warning = 4,
    Error = 8,
    Critical = 16,
    All = Debug | Information | Warning | Error | Critical
}