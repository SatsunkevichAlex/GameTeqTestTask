using Database;
using DatabaseAbstractions;
using RatesApi.Mappings;
using Services;
using Services.Mapings;
using ServicesAbstractions.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRatesService, RatesService>();
builder.Services.AddSingleton<IRepository, SqLiteRepository>();
builder.Services.AddSingleton<RatesContext>();
builder.Services.AddAutoMapper(
    typeof(ExchangeRatesMapProfile),
    typeof(ExchangeRateResponseMapProfile));
builder.Services.AddOptions();
var dbSection = builder.Configuration.GetSection("Database");
builder.Services.Configure<DatabaseConfig>(dbSection);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.Run();

