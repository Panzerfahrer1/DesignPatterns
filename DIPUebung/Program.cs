namespace DIPUebung {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
        }

        public interface ISender {
            void Send(IMessage message);
        }

        public interface IMessage {
            public string Message { get; set; }
            public string Reciever { get; set; }
        }

        public class EmailMessage : IMessage {
            public string Message { get; set; }
            public string Reciever { get; set; }
        }

        public class EmailMessageSender(ISender sender) {
            public void Send(IMessage message) {
                sender.Send(message);
            }
        }

        public class OrderService(ISender sender) {
            public void CompleteOrder(IMessage message) {
                sender.Send(message);
            }
        }
    }
}

// Nicht klar ist die Implementierung wie bei Aufgabe 1