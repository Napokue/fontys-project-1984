using MessageService.Factories;
using MessageService.Repository;
using MessageService.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TextMessageRepository>();
builder.Services.AddSingleton<IMessageFactory, TextMessageFactory>();
builder.Services.AddScoped<IMessageValidator, MessageValidator>();

var app = builder.Build();

app.MapPost("/", (
    [FromBody] string content,
    IMessageFactory messageFactory,
    IMessageValidator messageValidator) => Results.Ok());

app.Run();
