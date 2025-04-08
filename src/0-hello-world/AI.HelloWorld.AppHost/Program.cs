var builder = DistributedApplication.CreateBuilder(args);

var ollama =
    builder.AddOllama("ollama")
        .WithDataVolume()
        .WithOpenWebUI();

/*
 * The set of available models from Ollama: https://ollama.com/library
 */
var chat = ollama
    .AddModel("chat", "llama3.2:3b"); // 3b parameter model

builder.AddProject<Projects.AI_HelloWorld_Console>("console")
    .WithReference(chat)
    .WaitFor(chat);

builder.Build().Run();