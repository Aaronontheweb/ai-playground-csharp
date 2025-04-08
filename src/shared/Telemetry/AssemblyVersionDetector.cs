﻿using System.Reflection;
using OpenTelemetry.Resources;

namespace AI.Telemetry;

public sealed class AssemblyVersionDetector : IResourceDetector
{
    public Resource Detect()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        IEnumerable<KeyValuePair<string, object>> attributes = Array.Empty<KeyValuePair<string, object>>();

        if (version != null)
        {
            attributes =
            [
                new KeyValuePair<string, object>("service.version", version)
            ];
        }
        
        return new Resource(attributes);
    }
}

public static class ServiceVersionDetectorExtensions
{
    public static ResourceBuilder AddServiceVersionDetector(this ResourceBuilder builder)
    {
        return builder.AddDetector(new AssemblyVersionDetector());
    }
}