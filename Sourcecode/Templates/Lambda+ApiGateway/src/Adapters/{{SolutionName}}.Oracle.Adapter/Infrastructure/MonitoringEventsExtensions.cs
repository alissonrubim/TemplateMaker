using System;
using Coolblue.Utilities.MonitoringEvents;

namespace {{SolutionName}}.Oracle.Adapter.Infrastructure
{
    internal static class MonitoringEventsExtensions
    {
        public static void ErrorTestingOracleConnectivity(this MonitoringEvents monitoringEvents, Exception ex)
        {
            monitoringEvents.Logger.Error(ex, "Error testing Oracle connectivity");
        }

        public static void ErrorExecutingOracleQuery(this MonitoringEvents monitoringEvents, Exception ex,
            string queryType, TimeSpan? duration)
        {
            monitoringEvents.Logger.Error(ex, "Error executing Oracle query");
            monitoringEvents.Metrics.IncrementCounter("persistence.oracle.execute_query", "success:false",
                "type:" + queryType);

            if (duration.HasValue)
            {
                monitoringEvents.Metrics.Timer(
                    "persistence.oracle.execute_query.duration",
                    duration.Value,
                    "success:false",
                    "type:" + queryType
                );
            }
        }

        public static void ExecutedOracleQuery(this MonitoringEvents monitoringEvents, string queryType,
            TimeSpan? duration)
        {
            monitoringEvents.Metrics.IncrementCounter("persistence.oracle.execute_query", "success:true",
                "type:" + queryType);

            if (duration.HasValue)
            {
                monitoringEvents.Metrics.Timer(
                    "persistence.oracle.execute_query.duration",
                    duration.Value,
                    "success:true",
                    "type:" + queryType
                );
            }
        }
    }
}
