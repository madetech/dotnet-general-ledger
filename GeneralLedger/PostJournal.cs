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
            _journalGateway.Save(new Journal
            {
                Description = postJournalRequest.Description,
                PostingDate = postJournalRequest.PostingDateTime
            });
        }
    }
}