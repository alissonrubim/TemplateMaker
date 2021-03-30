using System;
using System.IO;
using Coolblue.Utilities.MonitoringEvents;
using Coolblue.Utilities.MonitoringEvents.Aws.Lambda.Datadog;
using Coolblue.Utilities.MonitoringEvents.SimpleInjector;
using SuperNiceProject.Oracle.Adapter;
using SuperNiceProject.SecretManagement.Adapter;
using SuperNiceProject.ValueTypes;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Lambda.Base
{
    public static class Bootstrapper
    {

        private static IConfigurationRoot GetConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("parameters.json", true)
                .AddJsonFile("development.parameters.json", false)
                .AddEnvironmentVariables()
                .Build();

            return config;
        }

        internal static LambdaSettings GetLambdaSettings()
        {
            return GetConfiguration().Get<LambdaSettings>();
        }

        public static Container CreateDefaultApplication(LambdaSettings lambdaSettings)
        {
            var container = new Container();
            container.Options.DefaultLifestyle = new AsyncScopedLifestyle();
            container.Options.PropertySelectionBehavior = new MonitoringEventsPropertySelectionBehavior();
            container.Options.AllowOverridingRegistrations = true;
            RegisterTypes(container, lambdaSettings);
            return container;
        }

        private static void RegisterTypes(Container container, LambdaSettings settings)
        {
            var log = new LoggerConfiguration()
                              .Enrich.FromLogContext()
                              .MinimumLevel.Verbose()
                              .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                              .WriteTo.Console(new JsonFormatter())
                              .CreateLogger();
            var monitoringEvents = CreateMonitoringEvents(log);
            var secretManagementAdapter = SecretManagementAdapter.GetSecretManagementAdapter(settings.Environment);
        
            container.RegisterInstance(settings);
            container.RegisterInstance<IOracleSettings>(settings);
            settings.OracleUserName = settings.OracleUserName;
            if (settings.Environment != ApplicationEnvironment.Development)
            {
                settings.OraclePassword = secretManagementAdapter.DecryptString(settings.OraclePassword).Result;
            }
            else
            {
                settings.OraclePassword = settings.OraclePassword;
            }
            container.RegisterInstance(monitoringEvents);
            var persistenceAdapter = SetupPersistence(settings, monitoringEvents);
            persistenceAdapter.Register(container);
        }
        private static PersistenceAdapter SetupPersistence(LambdaSettings settings, MonitoringEvents monitoringEvents)
        {
            try
            {
                var persistenceSetting = new PersistenceAdapterSettings(settings.FullConnectionString) {
                    OracleResilience = settings.OracleResilience
                };
                return new PersistenceAdapter(persistenceSetting, monitoringEvents);
            }
            catch
            {
                Console.WriteLine("Error while attempting to connect to Oracle");
                throw;
            }
        }

        private static MonitoringEvents CreateMonitoringEvents(ILogger logger)
        {
            return new MonitoringEvents(logger, new AwsLambdaDatadogMetrics());
        }
    }
}
