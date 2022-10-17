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
            TestServer server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            HttpClient client = server.CreateClient();

            var pet = new Pet(name: "Baymax", type: "dog", color: "white", price: 1000);
            var stringContent =
                new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/pets", stringContent);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var savedPet = JsonConvert.DeserializeObject<Pet>(responseBody);
            Assert.Equal(pet, savedPet);
        }
    }
}