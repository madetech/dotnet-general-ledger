using GeneralLedger.UseCase;

namespace GeneralLedger
{
    public interface IJournalGateway : IJournalReader, IJournalWriter
    {
    }
}