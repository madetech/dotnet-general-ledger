using GeneralLedger.Boundary;
using GeneralLedger.Domain;

namespace GeneralLedger.UseCase
{
    public class PostJournal
    {
        private readonly IJournalWriter _journalGateway;

        public PostJournal(IJournalWriter journalGateway)
        {
            _journalGateway = journalGateway;
        }

        public PostJournalResponse Execute(PostJournalRequest postJournalRequest)
        {
            _journalGateway.Save(new Journal
            {
                Description = postJournalRequest.Description,
                PostingDate = postJournalRequest.PostingDateTime
            });

            return null;
        }
    }

    public interface IJournalWriter
    {
        void Save(Journal journal);
    }
}