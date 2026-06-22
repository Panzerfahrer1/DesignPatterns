using System;
using System.Collections.Generic;

namespace SOLID_UE2 {
    internal class Program {
        static void Main(string[] args) {
            var paymentMethods = new Dictionary<string, IPaymentMethod>
            {
                { "CreditCard", new CreditCardPayment(new CreditCardValidator()) },
                { "PayPal", new PayPalPayment(new PayPalValidator()) },
                { "Cash", new CashPayment(new CashValidator()) },
                { "Crypto", new CryptoPayment(new CryptoValidator()) },
                { "BankTransfer", new BankTransferPayment(new BankTransferValidator()) }
            };

            var orderProcessor = new OrderProcessor(paymentMethods);

            orderProcessor.ProcessOrder("CreditCard", 500);
            orderProcessor.ProcessOrder("PayPal", 1200);
            orderProcessor.ProcessOrder("Cash", 200);
            orderProcessor.ProcessOrder("Crypto", 50);
            orderProcessor.ProcessOrder("BankTransfer", 3000);
        }
    }

    public class OrderProcessor {
        private readonly Dictionary<string, IPaymentMethod> paymentMethods;

        public OrderProcessor(Dictionary<string, IPaymentMethod> paymentMethods) {
            this.paymentMethods = paymentMethods;
        }

        public void ProcessOrder(string paymentMethodName, double amount) {
            if (!paymentMethods.ContainsKey(paymentMethodName)) {
                Console.WriteLine("Unbekannte Zahlungsmethode.");
                return;
            }

            IPaymentMethod paymentMethod = paymentMethods[paymentMethodName];

            ValidationResult validationResult = paymentMethod.Validate(amount);

            if (!validationResult.IsValid) {
                Console.WriteLine(validationResult.ErrorMessage);
                return;
            }

            paymentMethod.Pay(amount);
        }
    }
    public abstract class PaymentMethod : IPaymentMethod {
        private readonly IPaymentValidator validator;

        protected PaymentMethod(IPaymentValidator validator) {
            this.validator = validator;
        }

        public ValidationResult Validate(double amount) {
            return validator.Validate(amount);
        }

        public abstract void Pay(double amount);
    }

    public interface IPaymentMethod {
        ValidationResult Validate(double amount);
        void Pay(double amount);
    }

    public interface IPaymentValidator {
        ValidationResult Validate(double amount);
    }

    public class ValidationResult {
        public bool IsValid { get; }
        public string ErrorMessage { get; }

        private ValidationResult(bool isValid, string errorMessage) {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        public static ValidationResult Success() {
            return new ValidationResult(true, "");
        }

        public static ValidationResult Failed(string errorMessage) {
            return new ValidationResult(false, errorMessage);
        }
    }


    #region PaymentMethods
    public class CreditCardPayment : PaymentMethod {
        public CreditCardPayment(IPaymentValidator validator) : base(validator) {
        }

        public override void Pay(double amount) {
            Console.WriteLine($"Zahlung über Kreditkarte von {amount}€ erfolgreich durchgeführt.");
        }
    }

    public class PayPalPayment : PaymentMethod {
        public PayPalPayment(IPaymentValidator validator) : base(validator) {
        }

        public override void Pay(double amount) {
            Console.WriteLine($"Zahlung über PayPal von {amount}€ erfolgreich durchgeführt.");
        }
    }

    public class CashPayment : PaymentMethod {
        public CashPayment(IPaymentValidator validator) : base(validator) {
        }

        public override void Pay(double amount) {
            Console.WriteLine($"Zahlung über Bargeld von {amount}€ erfolgreich durchgeführt.");
        }
    }

    public class CryptoPayment : PaymentMethod {
        public CryptoPayment(IPaymentValidator validator) : base(validator) {
        }

        public override void Pay(double amount) {
            Console.WriteLine($"Zahlung über Crypto von {amount}€ erfolgreich durchgeführt.");
        }
    }

    public class BankTransferPayment : PaymentMethod {
        public BankTransferPayment(IPaymentValidator validator) : base(validator) {
        }

        public override void Pay(double amount) {
            Console.WriteLine($"Zahlung per Banküberweisung von {amount}€ erfolgreich durchgeführt.");
        }
    }
    #endregion
    #region Validators
    public class CreditCardValidator : IPaymentValidator {
        public ValidationResult Validate(double amount) {
            if (amount > 1000) {
                return ValidationResult.Failed("Zahlung abgelehnt: Kreditkartenlimit überschritten.");
            }

            return ValidationResult.Success();
        }
    }

    public class PayPalValidator : IPaymentValidator {
        public ValidationResult Validate(double amount) {
            if (amount < 10) {
                return ValidationResult.Failed("Zahlung abgelehnt: Mindestbetrag für PayPal ist 10€.");
            }

            return ValidationResult.Success();
        }
    }

    public class CashValidator : IPaymentValidator {
        public ValidationResult Validate(double amount) {
            if (amount > 500) {
                return ValidationResult.Failed("Zahlung abgelehnt: Bargeldlimit überschritten.");
            }

            return ValidationResult.Success();
        }
    }

    public class CryptoValidator : IPaymentValidator {
        public ValidationResult Validate(double amount) {
            if (amount < 25) {
                return ValidationResult.Failed("Zahlung abgelehnt: Mindestbetrag für Crypto ist 25€.");
            }

            return ValidationResult.Success();
        }
    }

    public class BankTransferValidator : IPaymentValidator {
        public ValidationResult Validate(double amount) {
            if (amount > 10000) {
                return ValidationResult.Failed("Zahlung abgelehnt: Banküberweisungslimit überschritten.");
            }

            return ValidationResult.Success();
        }
    }
    #endregion
}