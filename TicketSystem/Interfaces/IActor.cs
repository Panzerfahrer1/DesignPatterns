using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Enums;
using TicketSystem.Models;

namespace TicketSystem.Interfaces {
    public interface IActor {
        // TODO: Add blonde to Ticket types and add myself as a employee. So i do a blonde atleast in my code
        // (Sad autistic developer noises)
        List<TicketType> doableTypes { get; }
        IActor _nextActor { get; set; }
        void Handle(Ticket ticket);
    }
}
