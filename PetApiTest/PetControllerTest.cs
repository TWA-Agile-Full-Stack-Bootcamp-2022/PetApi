using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PetApi;
using PetTest;

namespace PetApiTest
{
    public class HelloControllerTest : TestBase
    {
        public HelloControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
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

    public class PetControllerTest : TestBase
    {
        private string url = "/Pet";

        public PetControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_return_success_when_call_put_given_pet()
        {
            //given
            var httpClient = GetClient();
            var pet = new Pet("petName", "Dog", "red", 20);
            var petString = JsonConvert.SerializeObject(pet);
            var content = new StringContent(petString, Encoding.UTF8, MediaTypeNames.Application.Json);
            //when
            var registerResponse = await httpClient.PostAsync(url, content);
            //then
            Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);
        }

        [Fact]
        public async Task Should_return_400_when_add_pet_given_pet_name_duplicated()
        {
            //given
            var httpClient = GetClient();
            var pet = new Pet("petName", "Dog", "red", 20);
            var petString = JsonConvert.SerializeObject(pet);
            var content = new StringContent(petString, Encoding.UTF8, MediaTypeNames.Application.Json);
            var fistCall = httpClient.PostAsync(url, content);
            //when
            while (fistCall.IsCompleted)
            {
                var httpResponseMessage = await httpClient.PostAsync(url, content);
                Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
            }
        }
    }
}