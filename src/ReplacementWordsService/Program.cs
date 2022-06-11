using DatabaseLib;
using DatabaseLib.Builders;
using DatabaseLib.Factories;
using DatabaseLib.Services;
using Microsoft.AspNetCore.Mvc;
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

app.MapPost("/", async (
    [FromBody] ReplacementWord model,
    ReplacementWordRepository replacementWordRepository) =>
{
    
})
.WithName("Create");

app.MapPut("/", async (
    [FromBody] ReplacementWord model,
    ReplacementWordRepository replacementWordRepository) =>
{
    
})
.WithName("Update");

app.MapDelete("/{id}", async (
    [FromRoute] Guid id,
    ReplacementWordRepository replacementWordRepository) =>
{
    
})
.WithName("Delete");



app.MapGet("/{id}", async (
    [FromRoute] Guid id,
    ReplacementWordRepository replacementWordRepository) =>
{
    
})
.WithName("Get By Id");

app.MapGet("/{oldspeak}", async (
    [FromRoute] string oldspeak,
    ReplacementWordRepository replacementWordRepository) =>
{
    
})
.WithName("Get By Oldspeak");

app.MapGet("/all", async (
    [FromQuery] int? skip, 
    [FromQuery] int? take,
    ReplacementWordRepository replacementWordRepository) =>
{
    
})
.WithName("Get All");

app.Run();
