using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

namespace Quizzie.RequestHelpers
{
    /// <summary>
    /// Provides methods to interact with Vault secrets.
    /// </summary>
    public class VaultSecretProvider
    {
        private readonly IVaultClient _vaultClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="VaultSecretProvider"/> class.
        /// </summary>
        /// <param name="vaultUri">The URI of the Vault server.</param>
        /// <param name="vaultToken">The token used to authenticate with the Vault server.</param>
        public VaultSecretProvider(string vaultUri, string vaultToken)
        {
            var authMethod = new TokenAuthMethodInfo(vaultToken);
            var vaultClientSettings = new VaultClientSettings(vaultUri, authMethod);
            _vaultClient = new VaultClient(vaultClientSettings);
        }

        /// <summary>
        /// Retrieves a secret from the Vault server asynchronously.
        /// </summary>
        /// <param name="path">The path to the secret in the Vault.</param>
        /// <param name="mountPoint"></param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the secret data.</returns>
        public async Task<Secret<SecretData>> GetSecretAsync(string path, string mountPoint)
        {
            return await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: path, mountPoint: mountPoint);
        }
    }
}
