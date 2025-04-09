using AI.Telemetry;
using Microsoft.Extensions.AI;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ConfigureAiLogging();
builder.Services.ConfigureAiTelemetry();

builder.AddOllamaApiClient("chat")
    .AddChatClient()
    .UseFunctionInvocation()
    .UseOpenTelemetry()
    .UseLogging();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prompt API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/prompt", async (string prompt, IChatClient chatClient, CancellationToken ct) =>
    {
        var resp = await chatClient.GetResponseAsync(prompt, cancellationToken: ct);
        return Results.Ok(resp);
    })
    .WithName("SendPrompt");

app.Run();