using Microsoft.AspNetCore.Mvc.Testing;
using PetApi;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PetApiTest
{
    public class PetControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient client;
        private readonly CustomWebApplicationFactory<Program> factory;

        public PetControllerTest(CustomWebApplicationFactory<Program> factory)
        {
            this.factory = factory;
            client = factory.CreateClient(new WebApplicationFactoryClientOptions { });
        }

        [Fact]
        public async Task Should_return_hello_world_when_get_root_path()
        {
            //given
            //when
            var response = await client.GetAsync("/Hello");

            var returnString = await response.Content.ReadAsStringAsync();
            //then
            Assert.Equal("Hello World", returnString);
        }
    }
}
