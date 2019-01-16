using System;

namespace GeneralLedger
{
    public class ViewJournalsResponse
    {
        public PresentableJournal[] Journals { get; set; }

        public class PresentableJournal
        {
            public DateTime Date { get; set; }
            public string Description { get; set; }
        }
    }
}