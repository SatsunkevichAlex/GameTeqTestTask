using AutoMapper;
using RatesApi.Responses;
using ServicesAbstractions.Models;

namespace RatesApi.Mappings
{
    public class ExchangeRateResponseMapProfile : Profile
    {
        public ExchangeRateResponseMapProfile()
        {
            CreateMap<ExchangeRates, ExchangeRateResponse>();
        }
    }
}
