using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace API.Services.Cloud.AWS;

public sealed class Secrets : ISecretsSuport
{
    public string AccessKey { get; private set; }
    public string SecretKey { get; private set; }
    public string Region { get; private set; }
    private const string DATABASESECRETS = "DatabseSecrets-quem-indica";

    public Secrets(string accessKey, string secretKey, string region)
    {
        AccessKey = accessKey;
        SecretKey = secretKey;
        Region = region;
    }

    public string GetDataBaseSecrets()
    {
        Amazon.RegionEndpoint regionObj = Amazon.RegionEndpoint.GetBySystemName(Region);
        using (var client = new AmazonSimpleSystemsManagementClient(AccessKey, SecretKey, regionObj))
        {
            var response = client.GetParameterAsync
                (
                    new GetParameterRequest()
                    {
                        Name = DATABASESECRETS
                    }
                ).Result;

            return response.Parameter.Value ?? "";
        }
    }
}
