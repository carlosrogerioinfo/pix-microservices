using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace Pix.Gateway.Api.Configurations;

public static class PollyConfiguration
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryCount, context) =>
                {
                    var logger = context.ContainsKey("logger")
                        ? context["logger"] as ILogger
                        : null;
                    logger?.LogWarning("Retry {RetryCount} after {Delay}ms for {RequestUri}",
                        retryCount, timespan.TotalMilliseconds, outcome.Result?.RequestMessage?.RequestUri);
                });
    }

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(int failuresBeforeBreaking = 5, int breakDurationSeconds = 30)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: failuresBeforeBreaking,
                durationOfBreak: TimeSpan.FromSeconds(breakDurationSeconds),
                onBreak: (outcome, breakDelay) =>
                {
                    Console.WriteLine($"Circuit breaker OPEN for {breakDelay.TotalSeconds}s. Reason: {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
                },
                onReset: () => Console.WriteLine("Circuit breaker CLOSED - resuming normal operation"),
                onHalfOpen: () => Console.WriteLine("Circuit breaker HALF-OPEN - testing")
            );
    }
}
