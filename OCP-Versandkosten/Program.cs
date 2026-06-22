using System.Diagnostics.CodeAnalysis;

namespace OCP_Versandkosten {
    internal class Program {
        static void Main(string[] args) {
            IShippingStrategy strategy = new ExpressShipping();

            Console.WriteLine(strategy.Calculate(500.0));
        }
    }
}

public enum Strategy {
    Standard,
    Express,
    Overnight,
    Drone
}

public interface IShippingStrategy {
    Strategy ShippingType { get; }
    double Calculate(double weight);
}

public abstract class ShippingStrategy : IShippingStrategy {
    public ShippingStrategy(Strategy shippingType, double price) {
        ShippingType = shippingType;
        Price = price;
        //MaxWeight = maxWeight;
    }
    public virtual double Price { get; }
   
    //public virtual double MaxWeight { get; }
    public Strategy ShippingType { get; }
    public virtual double Calculate(double weight) {
        if (this is IWeightRestricted) {
            if (((IWeightRestricted)this).MaxWeight > weight) {
                throw new ArgumentOutOfRangeException(nameof(weight), "Too much weight");
            }
        }

        return Price * weight;
    }
}

public interface IWeightRestricted {
    public double MaxWeight { get; }
}

public class DroneShipping() : ShippingStrategy(Strategy.Drone, 3.7), IWeightRestricted {
    public double MaxWeight => 5.0;
}


public class StandardShipping() : ShippingStrategy(Strategy.Standard, 1.2) { }

// Eine Zeile btw.
public class ExpressShipping() : ShippingStrategy(Strategy.Express, 2.5) { }