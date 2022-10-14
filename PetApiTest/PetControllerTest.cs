using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using PetApi;
using PetTest;

namespace PetApiTest
{
    public class PetControllerTest : TestBase
    {
        public PetControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_return_hello_world_when_get_root_path()
        {
            //given
            var client = GetClient();
            //when
            var response = await client.GetAsync("/Hello");

            var returnString = await response.Content.ReadAsStringAsync();
            //then
            Assert.Equal("Hello World", returnString);
        }
    }
}
