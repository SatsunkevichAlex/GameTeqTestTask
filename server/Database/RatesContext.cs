using DatabaseAbstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Database
{
    public class RatesContext : DbContext
    {
        private readonly DatabaseConfig _config;

        public RatesContext(IOptions<DatabaseConfig> config)
        {
            _config = config.Value;
        }

        internal DbSet<CurrencyRow> Rates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_config.ConnectionString);
        }
    }
}