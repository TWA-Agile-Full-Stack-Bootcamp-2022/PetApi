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
            HttpClient httpClient = GenerateHttpClient();
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

        [Fact]
        public async Task Should_Return_All_Pets_When_Get_All_Pets()
        {
            // given
            HttpClient httpClient = GenerateHttpClient();

            Pet baymaxDog = new Pet(name: "Baymax", type: "dog", color: "white", price: 1000);
            await httpClient.PostAsync("api/addPet",
                new StringContent(JsonConvert.SerializeObject(baymaxDog), Encoding.UTF8, "application/json"));
            Pet oldblackCat = new Pet(name: "Old Black", type: "cat", color: "black", price: 500);
            await httpClient.PostAsync("api/addPet",
                new StringContent(JsonConvert.SerializeObject(oldblackCat), Encoding.UTF8, "application/json"));

            // when
            HttpResponseMessage response = await httpClient.GetAsync("api/getAllPets");

            // then
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            List<Pet> actualPets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
            Assert.Equal(new List<Pet>() { baymaxDog, oldblackCat }, actualPets);
        }

        private static HttpClient GenerateHttpClient()
        {
            TestServer testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            return testServer.CreateClient();
        }
    }
}
