# AI Hello World Project

This project demonstrates a simple "Hello, World!" application using [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview) and ASP.NET Core.

## Running the Project

To run this project, navigate to the `src/0-hello-world` directory in your terminal and execute the following command:

```bash
dotnet run --project AI.HelloWorld.AppHost/AI.HelloWorld.AppHost.csproj
```  

### Model

By default, this project will download and run the [`llama3.2:3b`](https://ollama.com/library/llama3.2:3b) model, which can run just fine even on a machine without any GPU support. You can configure this inside the `AI.HelloWorld.AppHost.Program.cs` file:

```csharp
var ollama =
    builder.AddOllama("ollama")
        .WithDataVolume()
        .WithOpenWebUI();

/*
 * The set of available models from Ollama: https://ollama.com/library
 */
var chat = ollama
    .AddModel("chat", "llama3.2:3b"); // 3b parameter model
```

## Accessing the Swagger Endpoint

Once the application is running, you can access the Swagger UI to explore the API endpoints. By default, the Swagger UI is available at:

```
http://localhost:5055/swagger/index.html
```

This interface allows you to interact with the Prompt API, view available endpoints, and send requests directly from the browser.