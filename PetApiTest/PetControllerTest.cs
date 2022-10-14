using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PetApi;
using Xunit;

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
        private static string url = "/Pets";

        [Fact]
        public async Task Should_return_success_when_call_put_given_pet()
        {
            //given

            var httpClient = GetClient();
            var pet = new Pet("petName", "Dog", "red", 20);
            var content = CoverPetToContent(pet);
            await DelAllPets(httpClient);
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
            await DelAllPets(httpClient);
            var pet = new Pet("petName", "Dog", "red", 20);
            var content = CoverPetToContent(pet);
            await httpClient.PostAsync(url, content);
            //when
            var httpResponseMessage = await httpClient.PostAsync(url, content);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Should_return_pets_when_call_get()
        {
            //given
            var httpClient = GetClient();
            await DelAllPets(httpClient);
            var pet1 = new Pet("pet1", "Dog", "red", 10);
            var pet2 = new Pet("pet2", "Cat", "white", 10);
            await httpClient.PostAsync(url, CoverPetToContent(pet1));
            await httpClient.PostAsync(url, CoverPetToContent(pet2));
            //when
            var httpResponseMessage = await httpClient.GetAsync(url);
            //then
            httpResponseMessage.EnsureSuccessStatusCode();
            var resultString = await httpResponseMessage.Content.ReadAsStringAsync();
            var pets = JsonConvert.DeserializeObject<List<Pet>>(resultString);
            Assert.Equal(pet1, pets[0]);
            Assert.Equal(pet2, pets[1]);
        }

        [Fact]
        public async Task Should_return_pets_by_name()
        {
            //given
            var httpClient = GetClient();
            await DelAllPets(httpClient);
            var pet1 = new Pet("pet1", "Dog", "red", 10);
            var pet2 = new Pet("pet2", "Cat", "white", 10);
            await httpClient.PostAsync(url, CoverPetToContent(pet1));

            await httpClient.PostAsync(url, CoverPetToContent(pet2));
            //when
            var httpResponseMessage = await httpClient.GetAsync(url + "/pet1");
            //then
            httpResponseMessage.EnsureSuccessStatusCode();
            var resultString = await httpResponseMessage.Content.ReadAsStringAsync();
            var pet = JsonConvert.DeserializeObject<Pet>(resultString);
            Assert.Equal(pet1, pet);
        }

        [Fact]
        public async Task Should_return_404_when_find_by_name_given_pet_not_in_store()
        {
            //given
            var httpClient = GetClient();
            await DelAllPets(httpClient);
            var pet1 = new Pet("pet1", "Dog", "red", 10);
            var pet2 = new Pet("pet2", "Cat", "white", 10);
            await httpClient.PostAsync(url, CoverPetToContent(pet1));
            await httpClient.PostAsync(url, CoverPetToContent(pet2));
            //when
            var responseMessage = await httpClient.GetAsync(url + "/not_in_pet_name");
            //then
            Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }

        [Fact]
        public async Task Should_can_del_pet_when_sell()
        {
            //given
            var httpClient = GetClient();
            await DelAllPets(httpClient);
            var pet1 = new Pet("pet1", "Dog", "red", 10);
            var pet2 = new Pet("pet2", "Cat", "white", 10);
            await httpClient.PostAsync(url, CoverPetToContent(pet1));
            await httpClient.PostAsync(url, CoverPetToContent(pet2));
            //when
            var delResult = await httpClient.DeleteAsync(url + "/pet1");
            //then
            delResult.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Should_remove_pet_when_del()
        {
            //given
            var httpClient = GetClient();
            await DelAllPets(httpClient);
            var pet1 = new Pet("pet1", "Dog", "red", 10);
            var pet2 = new Pet("pet2", "Cat", "white", 10);
            await httpClient.PostAsync(url, CoverPetToContent(pet1));
            await httpClient.PostAsync(url, CoverPetToContent(pet2));
            await httpClient.DeleteAsync(url + "/pet1");
            //when
            var searchByName = await httpClient.GetAsync(url + "/pet1");
            Assert.Equal(HttpStatusCode.NotFound, searchByName.StatusCode);
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

        private static async Task DelAllPets(HttpClient client)
        {
            await client.DeleteAsync(url);
        }
    }
}