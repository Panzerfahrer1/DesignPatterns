using System;
using System.Collections.Generic;
using System.Text;

namespace CoRSchuelerLehrerAnfragen {
    public interface ILogger {
        void Log(string message);
    }

    public class Logger : ILogger {
        public void Log(string message) {
            Console.WriteLine(message);
        }
    }

    public interface IChecker {
        public void Check(string message);
    }

    public class Checker : IChecker {
        public void Check(string message) {
            Console.WriteLine(message);
        }
    }
}
