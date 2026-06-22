using System;
using System.Collections.Generic;
using System.Text;

namespace CoRSchuelerLehrerAnfragen {
    public interface ISchoolWorker {
        void Work(RequestTypeFlags requestType);
    }
}