using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PetApi;
using PetApi.Models;
using Xunit;

namespace PetApiTest
{
    public class PetApiTest
    {
        [Fact]
        public async Task Should_add_new_pets_successful()
        {
            // Given
            TestServer testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient httpClient = testServer.CreateClient();
            Pet pet = new Pet(name: "Baymax", type: "dog", color: "white", price: 1000);
            StringContent stringContent =
                new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");
            // When
            HttpResponseMessage response = await httpClient.PostAsync("api/addPet", stringContent);
            // Then
            response.EnsureSuccessStatusCode();
            Pet actualPet = JsonConvert.DeserializeObject<Pet>(await response.Content.ReadAsStringAsync());
            Assert.Equal(pet, actualPet);
        }
    }
}
