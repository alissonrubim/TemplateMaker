using System;
using System.Data.Common;
using System.IO;
using Coolblue.Utilities.Data.Timing;
using Coolblue.Utilities.MonitoringEvents;
using Coolblue.Utilities.Resilience.Oracle.Core;
using Gimme.Core;
using Oracle.ManagedDataAccess.Client;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Gimme.Oracle.Adapter
{
    public class PersistenceAdapter
    {
        private readonly PersistenceAdapterSettings _settings;
        private MonitoringEvents _monitoringEvent;
        private bool _isRegistered;
        private ITimingDbConnectionFactory _connectionFactory;

        public PersistenceAdapter(PersistenceAdapterSettings settings, MonitoringEvents monitoringEvents)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _monitoringEvent = monitoringEvents;
        }

        public void Register(Container container)
        {
            if (_isRegistered)
                throw new InvalidOperationException("Persistence adapter was already registered with a container.");
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            RegisterConnection(container);
            RegisterResilience(container);
            RegisterRepositories(container);
            ConfigureOracleWallet();

            _isRegistered = true;
        }

        private void RegisterRepositories(Container container)
        {
            container.Register<IExampleRepository,ExampleRepository>();
        }
        private void RegisterResilience(Container container)
        {
            container.RegisterSingleton(() => new OracleResiliencePolicy(_settings.OracleResilience));
        }

        private void RegisterConnection(Container container)
        {
            Func<DbConnection> connectionFactory = () => new OracleConnection(_settings.OracleConnectionString);
            _connectionFactory = new TimingDbConnectionFactory(connectionFactory, "Oracle") { MonitoringEvents = _monitoringEvent };
            container.RegisterInstance(_connectionFactory);
            container.Register(() => container.GetInstance<ITimingDbConnectionFactory>().CreateConnection(), new AsyncScopedLifestyle());
            container.Register<IUnitOfWork>(() => new UnitOfWorkTransactionScope());
        }
        private static void ConfigureOracleWallet()
        {
            var assemblyDirectory = Path.GetDirectoryName(typeof(PersistenceAdapter).Assembly.Location);

            OracleConfiguration.WalletLocation = $"(SOURCE=(METHOD=FILE)(METHOD_DATA=(DIRECTORY={assemblyDirectory})))";
        }
    }
}