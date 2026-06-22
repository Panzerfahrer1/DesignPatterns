using System;
using System.Diagnostics;

public class Program {
    public static void Main(string[] args) {
        OrderProcessor orderProcessor = OrderProcessorFactory();

        orderProcessor.ProcessOrder(CustomerType.Regular, 10);
        orderProcessor.ProcessOrder(CustomerType.Premium, 5);
    }

    private static OrderProcessor OrderProcessorFactory() {
        IInvoice invoice = new SimpleInvoice();
        IDiscount discount = new Discount();
        IPriceCalculator calculator = new PriceCalculator();

        return new OrderProcessor(discount, invoice, calculator);
    }
}

public class OrderProcessor(IDiscount discount, IInvoice invoice, IPriceCalculator calculator) {
    private readonly IDiscount discount = discount;
    private readonly IInvoice invoice = invoice;
    private readonly IPriceCalculator calculator = calculator;

    public void ProcessOrder(CustomerType customerType, int quantity) {
        double priceDiscount = discount.GetDiscount(customerType);
        double price = calculator.CalculatePrice(quantity, priceDiscount);
        invoice.PrintInvoice(customerType, quantity, priceDiscount, price);
    }
}

public interface IPriceCalculator {
    double CalculatePrice(int quantity, double discount);
}
public class PriceCalculator() : IPriceCalculator {
    public double CalculatePrice(int quantity, double discount) => (quantity * 100) - (quantity * 100 * discount);
}

public interface IDiscount {
    double GetDiscount(CustomerType customerType);
}

// KEIN OPEN CLOSE
// TODO: Überarbeiten denke ich
// Eventuell eigene Kunden Klasse
public class Discount : IDiscount {
    const double DEFAULT_DISCOUNT = 0.0;
    const double REGULAR_DISCOUNT = 0.1;
    const double PREMIUM_DISCOUNT = 0.2;

    public double GetDiscount(CustomerType customerType) {
        switch (customerType) {
            case CustomerType.Regular:
                return REGULAR_DISCOUNT;
            case CustomerType.Premium:
                return PREMIUM_DISCOUNT;
            default:
                return DEFAULT_DISCOUNT;
        }
    }
}

public interface IInvoice {
    void PrintInvoice(CustomerType customerType, int quantity, double discount, double total);
}

public class SimpleInvoice() : IInvoice {
    public void PrintInvoice(CustomerType customerType, int quantity, double discount, double total) {
        Console.WriteLine($"Kundentyp: {customerType}");
        Console.WriteLine($"Artikelanzahl: {quantity}");
        Console.WriteLine($"Rabatt: {discount * 100}%");
        Console.WriteLine($"Gesamtbetrag: {total}");
    }
}

public enum CustomerType {
    Regular = 0,
    Premium = 1
}