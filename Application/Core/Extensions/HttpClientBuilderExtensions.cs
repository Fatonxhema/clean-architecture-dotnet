using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace Application.Core.Extensions
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder AddRetryAndCircuitBreakerPolicy(this IHttpClientBuilder builder, ILogger logger)
        {
            // Retry Policy: Retry once
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError() // Handles transient errors (5xx, 408, etc.)
                .RetryAsync(1, onRetry: (outcome, retryCount, context) =>
                {
                    logger.LogCritical("");
                });

            // Circuit Breaker Policy: Break the circuit after 2 consecutive failures
            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30),
                    onBreak: (outcome, timespan) =>
                    {
                        logger.LogCritical("");
                    },
                    onReset: () =>
                    {
                        logger.LogCritical("");
                    },
                    onHalfOpen: () =>
                    {
                        logger.LogCritical("");
                    });

            // Combine the Retry and Circuit Breaker Policies
            var policyWrap = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

            // Apply the combined policy to the HttpClientBuilder
            return builder.AddPolicyHandler(policyWrap);
        }
    }
}
