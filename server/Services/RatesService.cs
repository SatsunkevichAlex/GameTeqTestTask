using AutoMapper;
using DatabaseAbstractions;
using DatabaseAbstractions.Models;
using ServicesAbstractions.Interfaces;
using ServicesAbstractions.Models;

namespace Services
{
    public class RatesService : IRatesService
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public RatesService(
            IRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ExchangeRates> GetExchangeRateAsync(DateTimeOffset date)
        {
            var rates = _repo.GetRatesList();
            if (rates.All(it => it.Date < date))
            {
                throw new ArgumentException("Requested date not ready yet.");
            }
            var rate = await _repo.GetRateAsync(date) ?? HanleDayOff(rates, date);
            var mapped = _mapper.Map<ExchangeRates>(rate);
            return mapped;
        }

        private static CurrencyRow HanleDayOff(IEnumerable<CurrencyRow> rates, DateTimeOffset date)
        {
            CurrencyRow? result = null;
            while (result == null && rates.Any(it => it.Date < date))
            {
                date = date.AddDays(-1);
                result = rates.SingleOrDefault(it => it.Date == date);
            }
            if (result == null)
            {
                throw new ArgumentException("Requested date to far in past.");
            }
            return result;
        }
    }
}