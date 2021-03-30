using Coolblue.Utilities.Data.Timing;
using Coolblue.Utilities.MonitoringEvents;
using Coolblue.Utilities.Resilience.Oracle.Core;
using Polly.CircuitBreaker;
using System;
using SuperNiceProject.Exceptions;
using SuperNiceProject.Oracle.Adapter.Infrastructure;

namespace SuperNiceProject.Oracle.Adapter
{
    public abstract class AbstractOracleRepository
    {
        private readonly OracleResiliencePolicy resiliencePolicy;
        private readonly ITimingDbConnectionFactory connectionFactory;

        protected ITimingDbConnection Connection { get => connectionFactory.CreateConnection(); }

        protected MonitoringEvents MonitoringEvents { get; set; }

        protected AbstractOracleRepository(ITimingDbConnectionFactory connectionFactory, OracleResiliencePolicy resiliencePolicy)
        {
            this.resiliencePolicy = resiliencePolicy ?? throw new ArgumentNullException(nameof(resiliencePolicy));
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        protected T ExecuteWithPolicy<T>(Func<T> func, string monitoringMetricName)
        {
            var startTime = DateTime.Now;

            var oraclePolicy = resiliencePolicy.Value;

            try
            {
                var result = oraclePolicy.Execute(func);
                MonitoringEvents.ExecutedOracleQuery(monitoringMetricName, DateTime.Now.Subtract(startTime));

                return result;
            }
            catch (BrokenCircuitException ex)
            {
                MonitoringEvents.ErrorExecutingOracleQuery(
                    ex,
                    monitoringMetricName,
                    DateTime.Now.Subtract(startTime)
                );
                throw new PersistenceUnavailableException("Oracle circuit breaker is currently open.", ex);
            }
            catch (Exception ex)
            {
                MonitoringEvents.ErrorExecutingOracleQuery(
                    ex,
                    monitoringMetricName,
                    DateTime.Now.Subtract(startTime)
                );
                throw new PersistenceException($"Exception occurred while executing Oracle query: {ex.Message}", ex);
            }
        }
    }
}
