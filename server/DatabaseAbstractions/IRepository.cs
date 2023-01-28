using DatabaseAbstractions.Models;

namespace DatabaseAbstractions
{
    public interface IRepository
    {
        IEnumerable<CurrencyRow> GetRatesList();
        Task<CurrencyRow?> GetRateAsync(DateTimeOffset date);
        Task CreateAsync(CurrencyRow item);
        Task DeleteAsync(DateTimeOffset date);
        Task SaveAsync();
        Task CreateRangeAsync(IEnumerable<CurrencyRow> currencyRows);
    }
}
