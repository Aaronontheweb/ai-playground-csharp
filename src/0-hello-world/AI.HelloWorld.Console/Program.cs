using AI.Telemetry;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

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

AnsiConsole.WriteLine(":robot: AI Hello World is ready for your questions! :rocket:");
AnsiConsole.WriteLine("Type 'exit' to quit.");

var textPrompt = await AnsiConsole.AskAsync<string>("What would you like to ask the AI?");
while (!textPrompt.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
{
    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
    var resp = await chatClient.GetResponseAsync(textPrompt, cancellationToken: cts.Token);

    foreach (var u in resp.Messages)
    {
        AnsiConsole.WriteLine(u.Text);
    }

    textPrompt = await AnsiConsole.AskAsync<string>("What would you like to ask the AI?");
}

await app.WaitForShutdownAsync();