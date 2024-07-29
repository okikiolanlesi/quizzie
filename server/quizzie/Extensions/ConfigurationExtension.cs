using Microsoft.Extensions.Configuration;
using Quizzie.RequestHelpers;
using System.Collections.Generic;
using System.IO;

namespace Quizzie.Extensions
{
    /// <summary>
    /// Provides extension methods for configuration.
    /// </summary>
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Adds secrets from a vault to the configuration builder.
        /// </summary>
        /// <param name="builder">The configuration builder.</param>
        /// <param name="vaultSecretProvider">The vault secret provider.</param>
        /// <param name="path">The path to the secrets in the vault.</param>
        /// <param name="mountPoint"></param> The mount point of the secrets engine.
        /// <returns>The updated configuration builder.</returns>
        public static IConfigurationBuilder AddVaultSecrets(this IConfigurationBuilder builder, VaultSecretProvider vaultSecretProvider, string path, string mountPoint)
        {
            var secret = vaultSecretProvider.GetSecretAsync(path, mountPoint).GetAwaiter().GetResult();
            // Convert the secret data to a dictionary of with string keys and string values.
            var secrets = new Dictionary<string, string>();
            foreach (var kv in secret.Data.Data)
            {
                secrets.Add(kv.Key, kv.Value.ToString());
            }

            builder.AddInMemoryCollection(secrets);
            return builder;
        }
    }
}
