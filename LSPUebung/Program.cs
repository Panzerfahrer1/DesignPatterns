namespace LSPUebung {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
        }
    }
    public interface IFlyable {
        public void Fly();
    }

    public interface ISwimmable {
        public void Swim();
    }

    public interface IAllrounder : IFlyable, ISwimmable;

    public abstract class FlyableBird : Bird, IFlyable {
        public virtual void Fly() {
            Console.WriteLine("Flying ....");
        }
    }

    public abstract class SwimmableBird : Bird, ISwimmable {
        public virtual void Swim() {
            Console.WriteLine("Swimming ....");
        }
    }

    public abstract class Bird{ }
    public class Eagle : FlyableBird { }
    public class Penguin : SwimmableBird{ }
    //public class Duck : SwimmableBird, FlyableBird {

    //}

    // Oida des is doppelter code
    public class Duck : IAllrounder {
        public void Fly() {
            throw new NotImplementedException();
        }

        public void Swim() {
            throw new NotImplementedException();
        }
    }
}

// Schwierigkeit:
// Klassen richtig kombinieren.
// Was ist wenn ich zb einen Vogel habe der Schwimmen und Fliegen kann? Muss ich eine Klasse wie "Allrounder" machen und dabei wieder beide Implementieren? => DAS IST NICHT SOLID
// Dürfen wir Implementierungen im Interface machen?
// Anders ist es nicht "schön" möglich Fly und Swim zu implementieren