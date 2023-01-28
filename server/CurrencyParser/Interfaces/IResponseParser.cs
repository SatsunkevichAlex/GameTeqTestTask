using DatabaseAbstractions.Models;

namespace CurrencyParser.Interfaces
{
    public interface IResponseParser
    {
        IEnumerable<CurrencyRow> ParseResponse(string data);
    }
}
