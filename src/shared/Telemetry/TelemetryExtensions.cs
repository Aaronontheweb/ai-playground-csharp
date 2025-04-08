using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace AI.Telemetry;

public static class TelemetryExtensions
{
    public static ILoggingBuilder ConfigureAiLogging(this ILoggingBuilder builder)
    {
        var resourceBuilder = ResourceBuilder.CreateDefault();
        resourceBuilder
            .AddEnvironmentVariableDetector()
            .AddTelemetrySdk()
            .AddServiceVersionDetector();

        builder.AddOpenTelemetry(options => { options.SetResourceBuilder(resourceBuilder); });


        return builder;
    }
    
    public static IServiceCollection ConfigureAiTelemetry(this IServiceCollection services,
        Action<MeterProviderBuilder>? additionalMeters = null,
        Action<TracerProviderBuilder>? additionalTracers = null)
    {
        services
            .AddOpenTelemetry()
            .ConfigureResource(builder =>
            {
                builder
                    .AddEnvironmentVariableDetector()
                    .AddTelemetrySdk()
                    .AddServiceVersionDetector();
            })
            .UseOtlpExporter()
            .WithMetrics(c =>
            {
                c.AddRuntimeInstrumentation();

                additionalMeters?.Invoke(c);
            })
            .WithTracing(c => { additionalTracers?.Invoke(c); });

        return services;
    }
}