using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PetApi;
using PetApi;

namespace PetApiTest
{
    public class HelloControllerTest
    {
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

        private static HttpClient GetClient()
        {
            TestServer server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            return client;
        }
    }

    public class PetControllerTest
    {
        private string url = "/Pets";

        [Fact]
        public async Task Should_return_success_when_call_put_given_pet()
        {
            //given
            var httpClient = GetClient();
            var pet = new Pet("petName", "Dog", "red", 20);
            var content = CoverPetToContent(pet);
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
            var content = CoverPetToContent(pet);
            var fistCall = httpClient.PostAsync(url, content);
            //when
            while (fistCall.IsCompleted)
            {
                var httpResponseMessage = await httpClient.PostAsync(url, content);
                Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
            }
        }

        [Fact]
        public async Task Should_return_pets_when_call_get()
        {
            //given
            var httpClient = GetClient();
            var pet1 = new Pet("pet1", "Dog", "red", 10);
            var pet2 = new Pet("pet2", "Cat", "white", 10);
            var postAsync1 = httpClient.PostAsync(url, CoverPetToContent(pet1));

            var postAsync2 = httpClient.PostAsync(url, CoverPetToContent(pet2));
            //when
            while (postAsync1.IsCompleted && postAsync2.IsCompleted)
            {
                var httpResponseMessage = await httpClient.GetAsync(url);
                //then
                httpResponseMessage.EnsureSuccessStatusCode();
                var resultString = await httpResponseMessage.Content.ReadAsStringAsync();
                var pets = JsonConvert.DeserializeObject<List<Pet>>(resultString);
                Assert.Equal(pet1, pets[0]);
                Assert.Equal(pet2, pets[1]);
            }
        }

        private static StringContent CoverPetToContent(Pet pet)
        {
            var petString = JsonConvert.SerializeObject(pet);
            var content = new StringContent(petString, Encoding.UTF8, MediaTypeNames.Application.Json);
            return content;
        }

        private static HttpClient GetClient()
        {
            TestServer server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            return client;
        }
    }
}