using System;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using TeacherConnect.API.Helpers;

namespace TeacherConnect.API.Extensions
{
    public static class SecretsManagerExtensions
    {
        public static IConfigurationBuilder AddJsonSecretsManager(this IConfigurationBuilder configurationBuilder, string secretKey)
        {
            return AddJsonSecretsManager(configurationBuilder, secretKey, optional: true);
        }

        public static IConfigurationBuilder AddJsonSecretsManager(this IConfigurationBuilder configurationBuilder, string secretName, bool optional)
        {
            try
            {
                var client = new AmazonSecretsManagerClient(Amazon.RegionEndpoint.GetBySystemName("us-east-1"));

                var request = new GetSecretValueRequest { SecretId = secretName, VersionStage = "AWSCURRENT" };

                var response = client.GetSecretValueAsync(request).GetAwaiter().GetResult();

                var secret = response?.SecretString;
                configurationBuilder.AddJsonFile(new InMemoryFileProvider(secret), "Not an empty string", optional, reloadOnChange: false);
            }
            catch (Exception e)
            {
                if (optional == false)
                {
                    Console.WriteLine($"An error occured reading secret [{secretName}]: {e.Message}, {e.StackTrace}");
                    throw;
                }
            }
            return configurationBuilder;
        }
    }
}