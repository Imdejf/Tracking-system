using TrackingSystem.Shared.Services.Interfaces;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace TrackingSystem.Shared.Services.Implementations
{
    internal sealed class LoggingTool : ILoggingTool
    {
        private readonly ILogger _SerilogLogger;
        public LoggingTool()
        {
            _SerilogLogger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                    outputTemplate: "Logger {Timestamp:HH:mm:ss} | {Level:u3} | {Message:lj} \n",
                    theme: SystemConsoleTheme.Colored,
                    standardErrorFromLevel: Serilog.Events.LogEventLevel.Information
                )
                .CreateLogger();
        }
        public void LogCritical(string message)
        {
            _SerilogLogger.Fatal(message);
        }

        public void LogError(string message)
        {
            _SerilogLogger.Error(message);
        }

        public void LogInformation(string message)
        {
            _SerilogLogger.Information(message);
        }

        public void LogWarning(string message)
        {
            _SerilogLogger.Warning(message);
        }
    }
}

