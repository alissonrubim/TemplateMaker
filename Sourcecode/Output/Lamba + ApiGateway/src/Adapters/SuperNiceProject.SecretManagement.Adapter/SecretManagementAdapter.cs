using System;
using Amazon.KeyManagementService;
using SuperNiceProject.Ports;
using SuperNiceProject.SecretManagement.Adapter.InMemory;
using SuperNiceProject.SecretManagement.Adapter.Kms;
using SuperNiceProject.ValueTypes;

namespace SuperNiceProject.SecretManagement.Adapter
{
    public static class SecretManagementAdapter
    {
        public static ISecretManagementService GetSecretManagementAdapter(ApplicationEnvironment environment)
        {
            switch (environment)
            {
                case ApplicationEnvironment.Testing:
                case ApplicationEnvironment.Acceptance:
                case ApplicationEnvironment.Production: return new KmsSecretManagementService(new AmazonKeyManagementServiceClient());
                case ApplicationEnvironment.Development: return new InMemorySecretManagementService();
                default: throw new ArgumentOutOfRangeException(nameof(environment), environment, null);
            }
        }
    }
}
