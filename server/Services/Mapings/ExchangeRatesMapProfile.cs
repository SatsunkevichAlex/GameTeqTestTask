using AutoMapper;
using DatabaseAbstractions.Models;
using ServicesAbstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapings
{
    public class ExchangeRatesMapProfile : Profile
    {
        public ExchangeRatesMapProfile()
        {
            CreateMap<CurrencyRow, ExchangeRates>();
        }
    }
}
