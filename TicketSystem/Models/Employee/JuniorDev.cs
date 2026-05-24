using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Enums;
using TicketSystem.Interfaces;

namespace TicketSystem.Models.Employee {
    public class JuniorDev : Actor {
        public JuniorDev() : base(new List<TicketType>() { TicketType.Support, TicketType.Bug}, 10) {

        }
    }
}
