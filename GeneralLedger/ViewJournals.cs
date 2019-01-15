namespace GeneralLedger
{
    public class ViewJournals
    {
        private readonly IJournalReader _journalGateway;

        public ViewJournals(IJournalReader journalGateway)
        {
            _journalGateway = journalGateway;
        }

        public ViewJournalsResponse Execute()
        {
            var journals = _journalGateway.All();

            if (journals.Count == 1)
            {
                return new ViewJournalsResponse
                {
                    Journals = new[]
                    {
                        new ViewJournalsResponse.PresentableJournal()
                    }
                }; 
            }
            
            return new ViewJournalsResponse
            {
                Journals = new ViewJournalsResponse.PresentableJournal[]{}
            };
        }
    }
}