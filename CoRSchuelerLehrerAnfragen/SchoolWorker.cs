using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoRSchuelerLehrerAnfragen {
    public abstract class SchoolWorker : ISchoolWorker {
        public ISchoolWorker? _nextWorker { get; set; }
        public RequestTypeFlags HandableTypes { get; private set; }

        public SchoolWorker(RequestTypeFlags types) {
           HandableTypes = types;
        }

        public void Work(RequestTypeFlags requestType) {
            if (HandableTypes.HasFlag(requestType)) {
                Log(requestType);
                Check(requestType);
                Responde(requestType);
                Close(requestType);

                return;
            }

            _nextWorker?.Work(requestType);
        }

        protected abstract void Log(RequestTypeFlags requestType);
        protected abstract void Check(RequestTypeFlags requestType);
        protected abstract void Responde(RequestTypeFlags requestType);
        protected abstract void Close(RequestTypeFlags requestType);
    }
}
