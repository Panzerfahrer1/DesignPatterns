using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Enums;
using TicketSystem.Interfaces;

namespace TicketSystem.Models.Employee {
    public class SeniorDev : Actor {
        public SeniorDev() : base(new List<TicketType>() { TicketType.Bug, TicketType.Support, TicketType.Critical}, 20) {
        }

        protected override bool CanHandle(Ticket ticket) => true;
    }
}
