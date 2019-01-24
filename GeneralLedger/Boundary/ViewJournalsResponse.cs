using System;

namespace GeneralLedger.Boundary
{
    public class ViewJournalsResponse
    {
        public PresentableJournal[] Journals { get; set; }

        public class PresentableJournal
        {
            public DateTime PostingDateTime { get; set; }
            public string Description { get; set; }
        }
    }
}