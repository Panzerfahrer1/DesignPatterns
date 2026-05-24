using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using TicketSystem.Enums;
using TicketSystem.Models.Employee;

namespace TicketSystem.Models {
    public class ProtocolPoint {
        public DateTime Timestamp { get; }
        public Actor Actor { get; }
        public ActionType Action { get; }
        public string? Description { get; }

        public ProtocolPoint(DateTime timestamp, Actor actor, ActionType action, string? description) {
            Timestamp = timestamp;
            Actor = actor;
            Action = action;
            Description = description;
        }
    }
}
