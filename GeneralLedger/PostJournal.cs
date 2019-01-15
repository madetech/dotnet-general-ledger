namespace GeneralLedger
{
    public class PostJournal
    {
        private readonly IJournalWriter _journalGateway;

        public PostJournal(IJournalWriter journalGateway)
        {
            _journalGateway = journalGateway;
        }

        public void Execute(PostJournalRequest postJournalRequest)
        {
            _journalGateway.Save();
        }
    }
}