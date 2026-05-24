using System;
using System.Collections.Generic;
using System.Text;

namespace Logger {
    public class LoggerFactory {
        const string FILE_NAME = @"C:\Users\olive\Downloads\ILoggerAids.txt";

        public static ILogger CreateLogger(LoggerTypes type) {
            switch (type) {
                case LoggerTypes.Console:
                    return new ConsoleLogger(LogLevels.All);
                case LoggerTypes.File:
                    return new FileLogger(LogLevels.Information | LogLevels.Warning, FILE_NAME);
                default:
                    throw new NotSupportedException("invalid logger type");
            }
        }

        public static ILogger CreateLoggerChain() {
            ILogger fileLogger = CreateLogger(LoggerTypes.File);
            ILogger consoleLogger = CreateLogger(LoggerTypes.Console);

            consoleLogger.NextLogger = fileLogger;

            return consoleLogger;

            // Diese Funktion ersetzt das erstellen von (unten)
            // ich kann sofort ein neues Objekt erstellen ohne dass ich die Chain manuel aufbauen muss

            //FileLogger fileLogger = new(LogLevels.Information | LogLevels.Warning, FILENAME);
            //ConsoleLogger consoleLogger = new(LogLevels.All);
            //consoleLogger.NextLogger = fileLogger;

            //consoleLogger.Log("Log", LogLevels.Debug);
            //consoleLogger.Log("Warnung", LogLevels.Warning);
        }
    }

    public enum LoggerTypes {
        Console,
        File
    }
}
