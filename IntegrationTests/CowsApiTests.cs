using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class CowsApiTests : BaseTest
    {
        [Fact]
        public Task CreateCow()
        {
            CowsApi.ApiV1CowsPost();
        }
    }
}
