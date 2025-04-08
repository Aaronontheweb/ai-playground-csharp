using AI.Telemetry;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;

var builder = new HostApplicationBuilder();

builder.Logging.ConfigureAiLogging();

builder.AddOllamaApiClient("chat")
    .AddChatClient()
    .UseFunctionInvocation()
    .UseOpenTelemetry()
    .UseLogging();

builder.Services.ConfigureAiTelemetry();


var app = builder.Build();

await app.StartAsync();

var chatClient = app.Services.GetRequiredService<IChatClient>();

using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
var resp = await chatClient.GetResponseAsync("How many feet are in a meter?", cancellationToken:cts.Token);
    
Console.WriteLine(resp);
await app.WaitForShutdownAsync();