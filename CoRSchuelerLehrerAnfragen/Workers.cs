using System;
using System.Collections.Generic;
using System.Text;

namespace CoRSchuelerLehrerAnfragen {
    public class Teacher : SchoolWorker {
        private readonly ILogger logger;

        public Teacher(ILogger logger) : base(RequestTypeFlags.easy) {
            this.logger = logger;
        }

        protected override void Log(RequestTypeFlags requestType) {
            logger.Log($"LOGGING MESSAGE : {requestType.ToString()}");
        }
    }
}
