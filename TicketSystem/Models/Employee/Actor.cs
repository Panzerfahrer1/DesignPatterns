using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Enums;
using TicketSystem.Interfaces;

namespace TicketSystem.Models.Employee {
    public abstract class Actor : IActor {
        // TODO: Add blonde to Ticket types and add myself as a employee. So i do a blonde atleast in my code
        // (Sad autistic developer noises)
        public List<TicketType> doableTypes { get; }
        public IActor _nextActor { get; set; }
        public int MaxComplexity { get; }

        public Actor(List<TicketType> doableTypes, int maxComplexity) {
            this.doableTypes = doableTypes;
            MaxComplexity = maxComplexity;
        }

        public virtual void Handle(Ticket ticket) {
            Receive(ticket);

            if (CanHandle(ticket)) {
                Process(ticket);
            }
            else if (_nextActor != null) {
                Forward(ticket);
            }
            else {
                Escalated(ticket);
            }
        }

        protected virtual bool CanHandle(Ticket ticket) => ticket.Complexity <= MaxComplexity && doableTypes.Contains(ticket.TicketType);
        protected void Receive(Ticket ticket) {
            ticket.AddHistory(this, ActionType.recived, "ticket recived");
        }

        protected void Process(Ticket ticket) {
            ticket.AddHistory(this, ActionType.processed, "ticket processed");
        }

        protected void Forward(Ticket ticket) {
            ticket.AddHistory(this, ActionType.forwarded, "ticket forwarded");

            _nextActor.Handle(ticket);
        }

        protected void Escalated(Ticket ticket) {
            ticket.AddHistory(this, ActionType.escalated, "ticket escalated");
        }
    }
}