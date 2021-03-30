
using Coolblue.Utilities.Resilience;

namespace SuperNiceProject.Oracle.Adapter
{
    public class PersistenceAdapterSettings
    {
        public string OracleConnectionString { get; private set; }
        public ResilienceSettings OracleResilience { get; set; }

        public PersistenceAdapterSettings(string connectionString)
        {
            OracleConnectionString = connectionString;
        }
    }
}