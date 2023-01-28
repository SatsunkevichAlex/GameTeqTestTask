using CurrencyParser;
using CurrencyParser.Interfaces;
using Database;
using DatabaseAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

string year = DateTime.Now.Year.ToString();
if (args.Length > 0)
{
    year = args[0];
}

var configuration = new ConfigurationBuilder();
configuration.AddJsonFile("appsettings.json");
var config = configuration.Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddOptions<DatabaseConfig>()
    .Bind(config.GetSection("Database"));
serviceCollection.AddSingleton<RatesContext>();
serviceCollection.AddSingleton<IRepository, SqLiteRepository>();
serviceCollection.AddSingleton<IResponseParser, ResponseParser>();
var serviceProvider = serviceCollection.BuildServiceProvider();

var url = $"https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/year.txt?year={year}";
var http = new HttpClient();
var resp = await http.GetStringAsync(url);

var parser = serviceProvider.GetService<IResponseParser>()!;
var currencyRows = parser.ParseResponse(resp);

var repo = serviceProvider.GetService<IRepository>()!;
await repo.CreateRangeAsync(currencyRows);
await repo.SaveAsync();