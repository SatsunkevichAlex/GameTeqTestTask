using ServicesAbstractions.Models;

namespace ServicesAbstractions.Interfaces
{
    public interface IRatesService
    {
        public Task<ExchangeRates> GetExchangeRateAsync(DateTimeOffset date);
    }
}
