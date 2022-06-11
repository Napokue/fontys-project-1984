using DatabaseLib;
using DatabaseLib.Builders;
using DatabaseLib.Factories;
using DatabaseLib.Services;
using ReplacementWordsService.Models;
using ReplacementWordsService.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IQueryService, PostgresQueryService>();
builder.Services.AddScoped<IConnectionFactory, PostgresConnectionFactory>();
builder.Services.AddScoped<IRepository<ReplacementWord>, ReplacementWordRepository>();
builder.Services.AddScoped<PostgresQueryCommandBuilderFactory>();
builder.Services.AddScoped<IQueryCommandBuilder, PostgresQueryCommandBuilder>();
builder.Services
    .AddScoped<IQueryCommandBuilderFactory<PostgresQueryCommandBuilder>, PostgresQueryCommandBuilderFactory>();

builder.Services.AddSingleton(_ =>
{
    var mappingService = new MappingService();
    mappingService.RegisterClass<ReplacementWord>(
        new Tuple<string, string>(nameof(ReplacementWord.Id), "id"),
        new Tuple<string, string>(nameof(ReplacementWord.Oldspeak), "oldspeak"),
        new Tuple<string, string>(nameof(ReplacementWord.Newspeak), "newspeak"));
    return mappingService;
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
