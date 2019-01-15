using System;
using System.Collections.Generic;

namespace GeneralLedger
{
    public interface IJournalGateway : IJournalReader, IJournalWriter
    {
    }
}