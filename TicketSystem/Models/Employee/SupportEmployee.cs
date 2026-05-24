using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using TicketSystem.Enums;
using TicketSystem.Interfaces;

namespace TicketSystem.Models.Employee {
    public class SupportEmployee : Actor {
        public SupportEmployee() : base(new List<TicketType>() { TicketType.Support}, 5) {

        }
    }
}
