namespace GeneralLedger
{
    public interface IJournalWriter
    {
        void Save(Journal journal);
    }
}