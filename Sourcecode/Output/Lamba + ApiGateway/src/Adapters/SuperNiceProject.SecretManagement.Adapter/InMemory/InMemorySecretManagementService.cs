using System.Threading.Tasks;
using SuperNiceProject.Ports;

namespace SuperNiceProject.SecretManagement.Adapter.InMemory
{
    internal sealed class InMemorySecretManagementService : ISecretManagementService
    {
        public Task<string> DecryptString(string value)
        {
            return Task.FromResult(value);
        }
    }
}
