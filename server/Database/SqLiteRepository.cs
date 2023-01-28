using DatabaseAbstractions;
using DatabaseAbstractions.Models;

namespace Database
{
    public class SqLiteRepository : IRepository
    {
        private readonly RatesContext _db;

        public SqLiteRepository(RatesContext db)
        {
            _db = db;
        }

        public IEnumerable<CurrencyRow> GetRatesList()
        {
            return _db.Rates;
        }

        public async Task<CurrencyRow?> GetRateAsync(DateTimeOffset date)
        {
            var rate = await _db.Rates.FindAsync(date);
            return rate;
        }

        public async Task CreateAsync(CurrencyRow CurrencyRow)
        {
            await _db.Rates.AddAsync(CurrencyRow);
        }

        public async Task CreateRangeAsync(IEnumerable<CurrencyRow> currencyRows)
        {
            foreach (var row in currencyRows)
            {
                if (await _db.Rates.FindAsync(row.Date) == null)
                {

                    await _db.Rates.AddAsync(row);
                }
            }
        }

        public async Task DeleteAsync(DateTimeOffset date)
        {
            var ccrrencyRow = await _db.Rates.FindAsync(date);
            if (ccrrencyRow != null)
            {
                _db.Rates.Remove(ccrrencyRow);
            }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
