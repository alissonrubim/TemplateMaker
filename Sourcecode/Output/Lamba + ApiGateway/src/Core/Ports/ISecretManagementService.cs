using System.Threading.Tasks;

namespace SuperNiceProject.Ports
{
    public interface ISecretManagementService
    {
        Task<string> DecryptString(string value);
    }
}
