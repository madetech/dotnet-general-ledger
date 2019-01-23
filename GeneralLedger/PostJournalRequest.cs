using System;

namespace GeneralLedger
{
    public class PostJournalRequest
    {

        public class Entry
        {
            
        }
        public Entry[] Entries { get; set; }
        public DateTime PostingDateTime { get; set; }
        public string Description { get; set; }
    }
}