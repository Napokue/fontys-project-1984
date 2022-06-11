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

builder.Services.AddSingleton<ReplacementWordRepository>();

var app = builder.Build();

app.MapPost("/", async (
    [FromBody] ReplacementWord model,
    ReplacementWordRepository replacementWordRepository) =>
{
    try
    {
        var success = await replacementWordRepository.AddAsync(model);
        return success ? Results.Ok() : Results.Problem();
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
})
.WithName("Create");

app.MapPut("/", async (
    [FromBody] ReplacementWord model,
    ReplacementWordRepository replacementWordRepository) =>
{
    try
    {
        var success = await replacementWordRepository.UpdateAsync(model);
        return success ? Results.Ok() : Results.Problem();
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
})
.WithName("Update");

app.MapDelete("/{id:guid}", async (
    [FromRoute] Guid id,
    ReplacementWordRepository replacementWordRepository) =>
{
    try
    {
        var success = await replacementWordRepository.DeleteByIdAsync(id);
        return success ? Results.Ok() : Results.Problem();
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
})
.WithName("Delete");

app.MapGet("/{id:guid}", async (
    [FromRoute] Guid id,
    ReplacementWordRepository replacementWordRepository) =>
{
    try
    {
        var replacementWord = await replacementWordRepository.GetByIdAsync(id);
        return replacementWord == null ? Results.Problem("Replacement Word not found") : Results.Ok(replacementWord);
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
})
.WithName("Get By Id");

app.MapGet("/all", async (
    [FromQuery] int? skip, 
    [FromQuery] int? take,
    ReplacementWordRepository replacementWordRepository) =>
{
    try
    {
        skip ??= 0;
        take ??= 10;
        
        var replacementWords = await replacementWordRepository.GetAllAsync(skip.Value, take.Value);
        return Results.Ok(replacementWords);
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
})
.WithName("Get All");

app.Run();
