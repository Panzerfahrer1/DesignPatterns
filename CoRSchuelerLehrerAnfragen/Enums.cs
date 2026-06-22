using System;
using System.Collections.Generic;
using System.Text;

namespace CoRSchuelerLehrerAnfragen {
    public enum RequestType {
        easy,
        medium,
        hard
    }

    [Flags]
    public enum RequestTypeFlags {
        easy = 0,
        medium = 1,
        hard = 2
    }
}
