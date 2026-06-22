namespace ISPUebung {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
        }

        public interface IWorkable {
            void Work();
        }

        public interface ISleepable {
            void Sleep();
        }

        public interface IEatable {
            void Eat();
        }

        public interface IVacationable {
            void RequestVacation();
        }

        public class HumanWorker : IWorkable, IEatable, ISleepable, IVacationable {
            public void Work() { }
            public void Eat() { }
            public void Sleep() { }
            public void RequestVacation() { }
        }
        public class RobotWorker : IWorkable {
            public void Work() { }
        }
    }
}

// Schwierigkeiten:
// Bei dieser Aufgabe keine.