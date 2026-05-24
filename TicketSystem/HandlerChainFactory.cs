using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Interfaces;
using TicketSystem.Models.Employee;

namespace TicketSystem {
    public static class HandlerChainFactory {
        public static IActor CreateChain() {
            var me = new Me();
            var sup = new SupportEmployee();
            var junior = new JuniorDev();
            var senior = new SeniorDev();

            me._nextActor = sup;
            sup._nextActor = junior;
            junior._nextActor = senior;
            senior._nextActor = null;

            return me;
        }
    }
}
