using WebApi.Api;

namespace IntegrationTests
{
    public class BaseTest
    {
        protected readonly CowsApi CowsApi;
        protected readonly SensorsApi SensorsApi;
        public BaseTest()
        {
            CowsApi = new CowsApi();
            SensorsApi = new SensorsApi();
        }
    }
}
