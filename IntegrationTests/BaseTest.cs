using IntegrationTests.Helpers;
using WebApiClient.Api;
using WebApiClient.Client;

namespace IntegrationTests
{
    public class BaseTest
    {
        protected readonly ICowsApi CowsApi;
        protected readonly ISensorsApi SensorsApi;
        protected readonly DataBaseHelper DataBaseHelper;
        public BaseTest()
        {
            var client = new ApiClient("http://localhost:5000/");
            CowsApi = new CowsApi(client);
            SensorsApi = new SensorsApi(client);
            DataBaseHelper = new DataBaseHelper("mongodb://localhost:27017", "App");
        }
    }
}
