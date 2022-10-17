using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PetApi;
using Xunit;

namespace PetApiTest
{
    public class PetControllerTest
    {
        [Fact]
        public async void Should_add_new_pet_successfully()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var client = server.CreateClient();

            var pet = new Pet(name: "Baymax", type: "dog", color: "white", price: 1000);
            var stringContent = SerializeToJsonString(pet);

            var response = await client.PostAsync("api/pets", stringContent);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var savedPet = JsonConvert.DeserializeObject<Pet>(responseBody);
            Assert.Equal(pet, savedPet);
        }

        [Fact]
        public async void Should_get_all_pets_successfully()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var client = server.CreateClient();

            var petBaymax = new Pet(name: "Baymax", type: "dog", color: "white", price: 1000);
            await client.PostAsync("api/pets", SerializeToJsonString(petBaymax));
            var petJinMao = new Pet(name: "JinMao", type: "dog", color: "white", price: 5000);
            await client.PostAsync("api/pets", SerializeToJsonString(petJinMao));

            var response = await client.GetAsync("/api/pets");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var allPets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
            Assert.Equal(new List<Pet>() { petBaymax, petJinMao }, allPets);
        }

        private static StringContent SerializeToJsonString(Pet pet)
        {
            return new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");
        }
    }
}