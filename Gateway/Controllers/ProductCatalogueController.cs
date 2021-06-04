using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCatalogueController : ControllerBase
    {
        private readonly ConfigSettings configSettings;
        private readonly StatelessServiceContext serviceContext;
        private readonly HttpClient httpClient;
        private readonly FabricClient fabricClient;


        public ProductCatalogueController(ConfigSettings configSettings, StatelessServiceContext serviceContext, HttpClient httpClient, FabricClient fabricClient)
        {
            this.serviceContext = serviceContext;
            this.configSettings = configSettings;
            this.httpClient = httpClient;
            this.fabricClient = fabricClient;
        }

        [HttpGet("api/{path}")]
        public async Task<IActionResult> Get(string path, string partitionKey, string partitionKind)
        {
            try
            {
                string ProdCatalogueServicesUri = this.serviceContext.CodePackageActivationContext.ApplicationName + "/" + this.configSettings.ProductCatalogueServiceName;

                string proxyUrl =
                      $"{this.configSettings.Hostname}:{this.configSettings.ReverseProxyPort}/{ProdCatalogueServicesUri.Replace("fabric:/", "")}/api/{path}?PartitionKey={partitionKey}&PartitionKind={partitionKind}";
                HttpResponseMessage response = await this.httpClient.GetAsync(proxyUrl);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return this.StatusCode((int)response.StatusCode);
                }

                return this.Ok(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
