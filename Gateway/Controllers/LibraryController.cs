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
    public class LibraryController : ControllerBase
    {
        private readonly ConfigSettings configSettings;
        private readonly StatelessServiceContext serviceContext;
        private readonly HttpClient httpClient;
        private readonly FabricClient fabricClient;


        public LibraryController(ConfigSettings configSettings, StatelessServiceContext serviceContext, HttpClient httpClient, FabricClient fabricClient)
        {
            this.serviceContext = serviceContext;
            this.configSettings = configSettings;
            this.httpClient = httpClient;
            this.fabricClient = fabricClient;
        }

        [HttpGet("api/{route}")]
        public async Task<IActionResult> Get(string route)
        {
            try
            {
                string libraryServicesUri = this.serviceContext.CodePackageActivationContext.ApplicationName + "/" + this.configSettings.LibraryServiceName;

                string proxyUrl =
                      $"{this.configSettings.Hostname}:{this.configSettings.ReverseProxyPort}/{libraryServicesUri.Replace("fabric:/", "")}/api/{route}";
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
