using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RatesApi.Responses;
using ServicesAbstractions.Interfaces;

namespace RatesApi.Controllers
{
    [ApiController]
    [Route("rates")]
    public class RatesContoller : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRatesService _rateService;

        public RatesContoller(
            IRatesService ratesService,
            IMapper mapper)
        {
            _rateService = ratesService;
            _mapper = mapper;
        }

        [HttpGet("exhange-rate")]
        public async Task<ActionResult<ExchangeRateResponse>> Get(DateTimeOffset date)
        {
            try
            {
                var rate = await _rateService.GetExchangeRateAsync(date);
                var mapped = _mapper.Map<ExchangeRateResponse>(rate);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}