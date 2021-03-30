using System;
using Amazon.KeyManagementService;
using {{SolutionName}}.Ports;
using {{SolutionName}}.SecretManagement.Adapter.InMemory;
using {{SolutionName}}.SecretManagement.Adapter.Kms;
using {{SolutionName}}.ValueTypes;

namespace {{SolutionName}}.SecretManagement.Adapter
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
