/*
* Folgender Code ist sehr schlecht strukturiert und nur sehr schwer via Unit-Tests zu testen.
* Ihre Aufgaben:
* - Finden Sie die Schwächen im Code nach den SOLID Prinzipien.
* - Setzen Sie bekannte Design Patterns ein, um den Code zu verbessern.
* - Refactorieren Sie den Code.
* - Schreiben Sie die notwendigen Unit-Tests.
*/
using System;
using System.Collections.Generic;

class Program {
    static void Main() {
        Console.WriteLine("Willkommen im Pizza-System!");

        // ELITE boy knowledge required !!!
        Console.WriteLine("... Magst du Pizza? (Yup) ...");
        Console.Write("Bitte wählen Sie Ihre Pizza (Margherita/Salami/Hawaii): ");
        //string userChoice = Console.ReadLine();

        IPizzaFactory factory = new PizzaFactory();
        IPizza pizza = factory.CreatePizza(PizzaType.Hawaii);
        IPizzaDisplayer displayer = new PizzaDisplayer();

        try {
            OrderProcessor orderProcessor = new OrderProcessor(displayer);
            orderProcessor.ProcessOrder(pizza);
        }
        catch (Exception ex) {
            Console.WriteLine("Fehler: " + ex.Message);
        }
    }
}

public interface IPizza {
    PizzaType Type { get; }
    double Price { get; }
}
public abstract class Pizza : IPizza {
    public PizzaType Type { get; }
    public double Price { get; }
    public Pizza(PizzaType type, double price) {
        Type = type;
        Price = price;
    }
}

public interface IPizzaDisplayer{
    void DisplayPizza(IPizza pizza);
}

public class PizzaDisplayer : IPizzaDisplayer {
    public void DisplayPizza(IPizza pizza) {
        Console.WriteLine($"Pizza: {pizza.Type.ToString()}, Preis: {pizza.Price} Euro");
    }
}

public class Calzone() : Pizza(PizzaType.Calzone, 10.5) { }
public class Hawaii() : Pizza(PizzaType.Hawaii, 9.5) { }
public class Margherita() : Pizza(PizzaType.Margherita, 10000.99) { }

public enum PizzaType {
    Margherita,
    Calzone,
    // Hawaii in its original language is pronounced as Hawai'i where the ' acts as a "stop sign"
    // so you say Hawai (short pause) i. or HA-WHY-EE
    Hawaii
}

public interface IPizzaFactory {
    public IPizza CreatePizza(PizzaType type);
}

public class PizzaFactory : IPizzaFactory {
    public IPizza CreatePizza(PizzaType type) {
        if (type == PizzaType.Margherita)
            return new Margherita();
        else if (type == PizzaType.Hawaii)
            return new Hawaii();
        else if (type == PizzaType.Calzone)
            return new Calzone();
        else
            throw new Exception("Ungültiger Pizza-Typ");
    }
}

public interface IOrderProcessor {
    public void ProcessOrder(IPizza pizzaType);
}

public class OrderProcessor(IPizzaDisplayer displayer) : IOrderProcessor {
    public void ProcessOrder(IPizza pizza) {
        displayer.DisplayPizza(pizza);
    }
}