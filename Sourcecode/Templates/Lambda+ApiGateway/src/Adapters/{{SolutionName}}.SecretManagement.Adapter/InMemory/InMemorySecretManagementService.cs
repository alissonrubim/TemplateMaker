using System.Threading.Tasks;
using {{SolutionName}}.Ports;

namespace {{SolutionName}}.SecretManagement.Adapter.InMemory
{
    internal sealed class InMemorySecretManagementService : ISecretManagementService
    {
        public Task<string> DecryptString(string value)
        {
            return Task.FromResult(value);
        }
    }
}
