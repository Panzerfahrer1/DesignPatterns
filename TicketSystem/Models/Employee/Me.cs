using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Enums;
using TicketSystem.Interfaces;

namespace TicketSystem.Models.Employee {
    internal class Me : Actor {
        public Me() : base(new List<TicketType>() { TicketType.Blonde}, 20) {
        }
    }
}
