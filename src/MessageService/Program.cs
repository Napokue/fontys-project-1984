using DatabaseLib;
using DatabaseLib.Builders;
using DatabaseLib.Factories;
using DatabaseLib.Services;
using MessageService.Factories;
using MessageService.Models;
using MessageService.Models.Rules;
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
builder.Services.AddScoped(provider =>
{
    var messageFactory = provider.GetService<IMessageFactory>()!;

    return new AbstractRule[]
    {
        new LowercaseRule(messageFactory),
        new UppercaseSpecificWordBaseRule(messageFactory, "Big Brother")
    };
});

var app = builder.Build();

app.MapPost("/", async (
    [FromBody] string content,
    IMessageFactory messageFactory,
    IMessageValidator messageValidator,
    AbstractRule[] rules) =>
{
    try
    {
        var message = messageFactory.Create(content);
        await messageValidator.ValidateMessage(message, rules);
        return Results.Ok();
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
});

app.Run();
