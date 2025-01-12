using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace API.Services.Cloud.AWS;

public sealed class Secrets : ISecretsSuport
{
    public string IDKey { get; private set; }
    public string SecretKey { get; private set; }
    public string Region { get; private set; }
    private const string DATABASESECRETS = "DATABASESECRETS";

    public Secrets(string iDKey, string secretKey, string region = "us-eats-1")
    {
        IDKey = iDKey;
        SecretKey = secretKey;
        Region = region;
    }

    public string GetDataBaseSecrets()
    {
        var client = new AmazonSecretsManagerClient(IDKey, SecretKey, Region);

        var request = new GetSecretValueRequest
        {
            SecretId = DATABASESECRETS
        };

        GetSecretValueResponse response = client.GetSecretValueAsync(request).Result;
        return DecodeString(response);
    }

    public static string DecodeString(GetSecretValueResponse response)
    {
        if (response.SecretString is not null)
        {
            var secret = response.SecretString;
            return secret;
        }
        else if (response.SecretBinary is not null)
        {
            var memoryStream = response.SecretBinary;
            StreamReader reader = new StreamReader(memoryStream);
            string decodedBinarySecret = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
            return decodedBinarySecret;
        }
        else
        {
            return string.Empty;
        }
    }
}
