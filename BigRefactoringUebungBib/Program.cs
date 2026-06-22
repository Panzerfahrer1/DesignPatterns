using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using static LibrarySystem;
public class LibrarySystem {
    public class Book {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public bool IsBorrowed { get; set; }
    }

    public class BookRepository : IBookRepository {
        private List<Book> books = new();

        public void AddBook(Book book) {
            books.Add(book);
        }

        public void BorrowBook(Book book) {
            book.IsBorrowed = true;
        }

        public Book GetBook(string isbn) {
            var book = books.FirstOrDefault(n => n.ISBN == isbn);

            if (book == null)
                throw new Exception("Book not found");
            if (book.IsBorrowed)
                throw new Exception("Already borrowed");

            return book;
        }
    }

    public interface IBookRepository {
        void AddBook(Book book);
        Book GetBook(string isbn);
        void BorrowBook(Book book);
    }

    public interface IEmailNotifier {
        void Notify(IEmailMessage message);
    }

    public interface IEmailMessage : IMessage {
        string Sender { get; set; }
        string Reciver { get; set; }
        string Title { get; set; }
    }

    public abstract class EmailMessage : IEmailMessage {
        protected EmailMessage(string sender, string reciver, string title, string message) {
            Sender = sender;
            Reciver = reciver;
            Title = title;
            Message = message;
        }

        public string Sender { get; set; }
        public string Reciver { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class BorrowedBookEmail() : EmailMessage("sender", "reciver", "title", "borrowed Book");

    public abstract class EmailNotifier() : IEmailNotifier {
        public virtual void Notify(IEmailMessage message) {
            using (var client =
            new SmtpClient(message.Sender)) {
                client.Send(
                message.Sender,
                message.Reciver,
                message.Title,
                message.Message);
            }
        }
    }

    public abstract class BorrowedNotifier : EmailNotifier;

    public class Library {
        private IBookRepository _repo;
        public ILogger _nextLogger { get; set; }
        public IEmailNotifier _notifier;

        public Library(ILogger nextLogger, BookRepository repos, IEmailNotifier notifier) {
            _nextLogger = nextLogger;
            _repo = repos;
            _notifier = notifier;
        }


        public void AddBook(Book book) {
            _repo.AddBook(book);

            _nextLogger.Log($"Added {book.Title}");
        }
        public void BorrowBook(string isbn, string memberId, IEmailMessage msg) {
            var book = _repo.GetBook(isbn);

            _repo.BorrowBook(book);

            _nextLogger.Log($"{book.Title} borrowed by {memberId}");

            _notifier.Notify(msg);
        }
    }
}

public interface IUser {
    public decimal OverdueCost { get; }
    public void CalculateCost(int overdueDays);
}

public abstract class User {
    public decimal OverdueCost { get; }
    ICostCalculator _calculator;

    public User(decimal cost, ICostCalculator calculator) {
        OverdueCost = cost;
        _calculator = calculator;
    }

    public virtual void CalculateCost(int overdueDays) => _calculator.CalculateFee(overdueDays, OverdueCost);
}

public class Strudent(ICostCalculator calculator) : User(0.2m, calculator);
public class Adult(ICostCalculator calculator) : User(0.3m, calculator);
public class Premium(ICostCalculator calculator) : User(0.1m, calculator);
public class SuperMaxPremiumUltraMoneyBoyMega911User(ICostCalculator calculator) : User(9.11m, calculator);

public interface ICostCalculator {
    public decimal CalculateFee(int overdueDays, decimal cost);
}

public class CostCalculator() : ICostCalculator {
    public decimal CalculateFee(int overdueDays, decimal cost) => overdueDays * cost;
}

public interface IExporter {
    void Export();
}

// zu viel?
public interface IMessage {
    string Message { get; set; }
}

public class ExportContent : IMessage {
    public string? Message { get; set; }
}

public abstract class FileExporter(IMessage content, string fileName) : IExporter {
    public virtual void Export() {
        File.WriteAllText(fileName, content.Message);
    }
}

public abstract class CSVExporter(IMessage content, string fileName) : FileExporter(content, fileName + ".csv");

public interface ILogger {
    void Log(string message);
}

public abstract class FileLogger(string file) : ILogger {
    public virtual void Log(string message) {
        File.WriteAllText(file, message);
    }
}

// Ist das SOLID oder kann ich das weg lassen?
public class CSVLogger(string file) : FileLogger(file + ".csv");