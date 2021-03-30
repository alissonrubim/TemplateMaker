using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using {{SolutionName}}.Ports;

namespace {{SolutionName}}.SecretManagement.Adapter.Kms
{
    internal sealed class KmsSecretManagementService : ISecretManagementService
    {
        private readonly AmazonKeyManagementServiceClient _kmsClient;

        public KmsSecretManagementService(AmazonKeyManagementServiceClient kmsClient)
        {
            _kmsClient = kmsClient;
        }

        public async Task<string> DecryptString(string value)
        {
            string decryptedString;
            using (var stream = new MemoryStream(Convert.FromBase64String(value)))
            {
                var decryptRequest = new DecryptRequest
                {
                    CiphertextBlob = stream
                };

                var response = await _kmsClient.DecryptAsync(decryptRequest);

                using (var reader = new StreamReader(response.Plaintext))
                {
                    decryptedString = await reader.ReadToEndAsync();
                }
            }
            return decryptedString;
        }
    }
}
