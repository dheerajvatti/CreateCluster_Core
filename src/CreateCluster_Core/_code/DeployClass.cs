using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateCluster_Core._code
{
    public class DeployClass
    {
        public  async Task<AuthenticationResult> GetAccessTokenAsync()
        {
            var cc = new ClientCredential("055c54b8-5b6f-4bc9-8a59-028d3f70f9b6", "PErJ/G/wvg+UGBUR6W8zQgkq1taS1mGRmVViM5oSXhg=");
            var context = new AuthenticationContext("https://login.windows.net/15b79de4-baff-453e-8df8-6865beac9b8c");
            var token = await context.AcquireTokenAsync("https://management.azure.com/", cc);
            if (token == null)
            {
                throw new InvalidOperationException("Could not get the token.");
            }
            return token;
        }


        public  async Task<ResourceGroup> CreateResourceGroupAsync(TokenCredentials credential, string groupName, string subscriptionId, string location)
        {
            Console.WriteLine("Creating the resource group...");
            var resourceManagementClient = new ResourceManagementClient(credential)
            { SubscriptionId = subscriptionId };
            var resourceGroup = new ResourceGroup { Location = location };
            return await resourceManagementClient.ResourceGroups.CreateOrUpdateAsync(groupName, resourceGroup);
        }

        public  async void DeleteResourceGroupAsync(TokenCredentials credential, string groupName, string subscriptionId)
        {
            Console.WriteLine("Deleting resource group...");
            var resourceManagementClient = new ResourceManagementClient(credential)
            { SubscriptionId = subscriptionId };
            await resourceManagementClient.ResourceGroups.DeleteAsync(groupName);
        }

        public  async Task<DeploymentExtended> CreateTemplateDeploymentAsync(TokenCredentials credential, string groupName, string storageName,
                                                    string deploymentName, string subscriptionId)
        {
            Console.WriteLine("Creating the template deployment...");
            var deployment = new Deployment();
            deployment.Properties = new DeploymentProperties
            {
                Mode = DeploymentMode.Incremental,
                TemplateLink = new TemplateLink
                {

                    //https://clustercreatetemplate.blob.core.windows.net/resourcetemplate/template.json
                    Uri = "https://" + storageName + ".blob.core.windows.net/resourcetemplate/template.json"

                },
                ParametersLink = new ParametersLink
                {
                    //https://clustercreatetemplate.blob.core.windows.net/resourcetemplate/parameters.json
                    Uri = "https://" + storageName + ".blob.core.windows.net/resourcetemplate/parameters.json"
                    //".blob.core.windows.net/templates/Parameters.json"
                }
            };
            var resourceManagementClient = new ResourceManagementClient(credential)
            { SubscriptionId = subscriptionId };
            return await resourceManagementClient.Deployments.CreateOrUpdateAsync(groupName, deploymentName, deployment);
        }

    }
}
