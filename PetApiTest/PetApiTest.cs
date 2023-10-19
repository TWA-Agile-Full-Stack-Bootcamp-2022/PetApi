using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PetApi.Models;
using PetApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System.Net.Mime;
using PetApi.Controllers;

namespace PetApiTest
{
    public class PetApiTest
    {
        [Fact]
        public async void Should_add_new_pet_successful()
        {
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient client = server.CreateClient();

            var pet = new Pet(name: "Milu", type: "dog", color: "red", price: 100);
            var httpContent = new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/Pet", httpContent);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Should_get_all_pets_when_create_pets()
        {
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient client = server.CreateClient();

            var petController = new PetController();
            petController.Reset();

            var pet = new Pet(name: "Milu", type: "dog", color: "red", price: 100);
            var httpContent = new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/Pet", httpContent);
            await client.PostAsync("/Pet", httpContent);

            var response = await client.GetAsync("/Pet");
            var body = await response.Content.ReadAsStringAsync();
            var pets = JsonConvert.DeserializeObject<List<Pet>>(body);

            Assert.Equal(2, pets.Count);
            Assert.Equal(pet, pets[0]);
            Assert.Equal(pet, pets[1]);
        }
    }
}
