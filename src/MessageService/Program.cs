using DatabaseLib;
using DatabaseLib.Builders;
using DatabaseLib.Factories;
using DatabaseLib.Services;
using MessageService.Factories;
using MessageService.Models;
using MessageService.Repository;
using MessageService.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IQueryService, PostgresQueryService>();
builder.Services.AddScoped<IConnectionFactory, PostgresConnectionFactory>();
builder.Services.AddScoped<IRepository<TextMessage>, TextMessageRepository>();
builder.Services.AddScoped<PostgresQueryCommandBuilderFactory>();
builder.Services.AddScoped<IQueryCommandBuilder, PostgresQueryCommandBuilder>();
builder.Services
    .AddScoped<IQueryCommandBuilderFactory<PostgresQueryCommandBuilder>, PostgresQueryCommandBuilderFactory>();

builder.Services.AddSingleton(_ =>
{
    var mappingService = new MappingService();
    mappingService.RegisterClass<TextMessage>(
        new Tuple<string, string>(nameof(TextMessage.Id), "id"),
        new Tuple<string, string>(nameof(TextMessage.Content), "content"));
    return mappingService;
});

builder.Services.AddSingleton<TextMessageRepository>();
builder.Services.AddSingleton<IMessageFactory, TextMessageFactory>();
builder.Services.AddScoped<IMessageValidator, MessageValidator>();

var app = builder.Build();

app.MapPost("/", (
    [FromBody] string content,
    IMessageFactory messageFactory,
    IMessageValidator messageValidator) => Results.Ok());

app.Run();
