using System;
using System.Collections.Generic;
using System.Text;

namespace Logger {
    public abstract class AbstractLogger : ILogger {
        public LogLevels Levels { get; private set; }
        public ILogger NextLogger { get; set; }

        public AbstractLogger(LogLevels levels) {
            Levels = levels;
        }

        public void Log(string message, LogLevels levels) {
            if (this.Levels.HasFlag(levels)) {
                Console.WriteLine($"{message}");
            }

            NextLogger?.Log(message, levels);
        }

        protected abstract void DoLog(string message);
    }
}
