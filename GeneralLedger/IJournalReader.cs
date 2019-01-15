using System;
using System.Collections.Generic;

namespace GeneralLedger
{
    public interface IJournalReader
    {
        List<Object> All();
    }
}