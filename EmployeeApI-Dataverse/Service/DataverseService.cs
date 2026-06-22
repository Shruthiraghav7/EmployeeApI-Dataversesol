using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using EmployeeApI_Dataverse.DTO;
using System;

namespace EmployeeApI_Dataverse.Service
{
    public class DataverseService
    {
        private readonly IConfiguration _configuration;

        public DataverseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ServiceClient GetClient()
        {
            string connectionString =
         $@"AuthType=ClientSecret;
        Url={_configuration["Dataverse:Url"]};
        ClientId={_configuration["Dataverse:ClientId"]};
        ClientSecret={_configuration["Dataverse:ClientSecret"]};
        TenantId={_configuration["Dataverse:TenantId"]}";

            var client = new ServiceClient(connectionString);

            if (!client.IsReady)
            {
                throw new Exception(client.LastError);
            }

            return client;
        }
    }
}



