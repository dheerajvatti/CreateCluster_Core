using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CreateCluster_Core._code;
using Microsoft.Rest;

namespace CreateCluster_Core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var groupName = "ExampleResourceGroup";
            var storageName = "clustercreatetemplate";
            var location = "East US";
            var subscriptionId = "e4337d44-53e5-48eb-b1ba-6652656b470e";
            var deploymentName = "createlinuxclustertestdhee";

            DeployClass deploycall = new DeployClass();

           // var token =  deploycall.GetAccessTokenAsync();
            //var credential = new TokenCredentials(token.Result.AccessToken);


            //var rgResult = CreateResourceGroupAsync(credential, groupName, subscriptionId, location);
            //Console.WriteLine(rgResult.Result.Properties.ProvisioningState);
            ////Console.ReadLine();

            //var dpResult = CreateTemplateDeploymentAsync(credential, groupName, storageName, deploymentName, subscriptionId);
            //Console.WriteLine(dpResult.Result.Properties.ProvisioningState);
            //Console.ReadLine();


            // DeleteResourceGroupAsync(credential, groupName, subscriptionId);
            // Console.ReadLine();

            //var dpResult = DeleteResourceGroupAsync(credential, groupName, storageName, deploymentName, subscriptionId);
            //Console.WriteLine(dpResult.Result.Properties.ProvisioningState);
            //Console.ReadLine();




            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
