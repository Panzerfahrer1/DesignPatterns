using TicketSystem.Enums;
using TicketSystem.Models;
using TicketSystem.Models.Employee;

namespace TicketSystem {
    internal class Program {
        static void Main(string[] args) {
            Ticket perfection = new(TicketType.Blonde, "OH NO! IM STUCK! I really hope a 160cm blonde haired blue eyed dominant girl doesnt come to resuce me!!", 20);
            Ticket PressAnyKey = new(TicketType.Critical, "Have you tried turning it on and off again?", 5);
            Ticket Orangensaft = new(TicketType.Support, "Es wurde Orangensaft verschüttet!", 88);

            var actor = HandlerChainFactory.CreateChain();

            actor.Handle(Orangensaft);

            foreach(var historyPoint in Orangensaft.History) {
                Console.WriteLine($"{historyPoint.Description} | {historyPoint.Actor.ToString()}");
            }
        }
    }
}
