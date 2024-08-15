using System.Collections.Generic;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace Quizzie.RequestHelpers
{
    /// <summary>
    /// Provides methods to interact with Vault secrets.
    /// </summary>
    public class VaultSecretsManager : ISecretsManager
    {
        private readonly IVaultClient _vaultClient;
        private readonly string _mountPoint;
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="VaultSecretsManager"/> class.
        /// </summary>
        /// <param name="vaultUri">The URI of the Vault server.</param>
        /// <param name="vaultToken">The token used to authenticate with the Vault server.</param>
        /// <param name="mountPoint">The mount point of the secrets engine.</param>
        /// <param name="path">The path to the secret in the Vault.</param>
        public VaultSecretsManager(string vaultUri, string vaultToken, string mountPoint, string path)
        {
            var authMethod = new TokenAuthMethodInfo(vaultToken);
            var vaultClientSettings = new VaultClientSettings(vaultUri, authMethod);
            _vaultClient = new VaultClient(vaultClientSettings);
            _mountPoint = mountPoint;
            _path = path;
        }

        /// <summary>
        /// Retrieves a secret from the Vault server asynchronously.
        /// </summary>
        /// <param name="key">The key of the secret.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the secret data.</returns>
        public async Task<string> GetSecretAsync(string key)
        {
            var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: _path, mountPoint: _mountPoint);

            if (secret.Data.Data.TryGetValue(key, out var value))
            {
                return value.ToString();
            }

            throw new KeyNotFoundException($"Secret with key '{key}' not found in path '{_path}'");
        }
    }

    public interface ISecretsManager
    {
        Task<string> GetSecretAsync(string key);
    }
}
