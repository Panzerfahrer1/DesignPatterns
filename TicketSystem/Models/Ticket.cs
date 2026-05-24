using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using TicketSystem.Enums;
using TicketSystem.Models.Employee;

namespace TicketSystem.Models {
    public class Ticket {
        private static int currentId = 0;

        public int Id { get; }
        public TicketType TicketType { get; }
        public string Description {  get; }
        public int Complexity { get; }
        public List<ProtocolPoint> History { get; } = new();

        public Ticket(TicketType ticketType, string description, int complexity) {
            Id = currentId++;
            TicketType = ticketType;
            Description = description;
            Complexity = complexity;
        }
        
        public void AddHistory(Actor actor, ActionType type, string message) {
            ProtocolPoint point = new(DateTime.Now, actor, type, message);
            History.Add(point);
        }
    }
}
