using System;

namespace GeneralLedger.Domain
{
    public class Journal
    {
        public DateTime PostingDate { get; set; }
        public string Description { get; set; }
    }
}