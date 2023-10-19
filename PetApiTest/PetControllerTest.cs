﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PetApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PetApiTest
{
    public class PetControllerTest
    {
        // Need clarify requirement
        // 1. return object ?
        // 2. return code ?
        // 3. type not a dog or cat ?
        // 4. duplicate name ?
        [Fact]
        public async Task Should_create_the_pet_when_post_to_pet_route_given_name_type_color_and_price()
        {
            HttpClient client = CreateContextAndGetHttpClient();

            // Given
            Pet givenPet = new Pet("Buddy", PetType.Dog, "Gold", 300);

            // When
            var response = await client.PostAsync("api/pets", SerializeContent(givenPet));
            var responseBody = await response.Content.ReadAsStringAsync();
            var savedPet = JsonConvert.DeserializeObject<Pet>(responseBody);

            // Then
            response.EnsureSuccessStatusCode();
            Assert.Equal(givenPet, savedPet);
        }

        [Fact]
        public async Task Should_return_all_pets_when_get_all_given_some_pets_already_existed()
        {
            HttpClient client = CreateContextAndGetHttpClient();

            // Given
            Pet givenPetDog = new Pet("Buddy", PetType.Dog, "Gold", 300);
            Pet givenPetCat = new Pet("Luna", PetType.Cat, "White", 200);
            await client.PostAsync("api/pets", SerializeContent(givenPetDog));
            await client.PostAsync("api/pets", SerializeContent(givenPetCat));

            // When
            var response = await client.GetAsync("api/pets");

            // Then
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var pets = JsonConvert.DeserializeObject<List<Pet>>(responseBody);
            Assert.Equal(givenPetDog, pets[0]);
            Assert.Equal(givenPetCat, pets[1]);
        }

        [Fact]
        public async Task Should_return_the_pet_when_find_by_name_given_a_existed_pet_and_its_name()
        {
            HttpClient client = CreateContextAndGetHttpClient();

            // Given
            Pet givenPetDog = new Pet("Buddy", PetType.Dog, "Gold", 300);
            await client.PostAsync("api/pets", SerializeContent(givenPetDog));

            // When
            var response = await client.GetAsync("api/pets/Buddy");

            // Then
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var petFound = JsonConvert.DeserializeObject<Pet>(responseBody);
            Assert.Equal(givenPetDog, petFound);
        }

        [Fact]
        public async Task Should_NOT_return_the_pet_when_find_by_name_given_a_not_existed_pet_name()
        {
            HttpClient client = CreateContextAndGetHttpClient();

            // Given
            // When
            const string urlWithNotExistedPetName = "api/pets/Daisy";
            var response = await client.GetAsync(urlWithNotExistedPetName);

            // Then
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var petFound = JsonConvert.DeserializeObject<Pet>(responseBody);
            Assert.Null(petFound);
        }

        [Fact]
        public async Task Should_can_delete_pet_when_pet_was_sold_by_given_name()
        {
            HttpClient client = CreateContextAndGetHttpClient();

            // Given
            Pet givenPetDog = new Pet("Buddy", PetType.Dog, "Gold", 300);
            await client.PostAsync("api/pets", SerializeContent(givenPetDog));

            // When
            const string urlWithPetName = "api/pets/Buddy";
            await client.DeleteAsync(urlWithPetName);
            var response = await client.GetAsync("api/pets/Buddy");

            // Then
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var petFound = JsonConvert.DeserializeObject<Pet>(responseBody);
            Assert.Null(petFound);
        }

        private static StringContent SerializeContent(Pet givenPet)
        {
            string petJsonString = JsonConvert.SerializeObject(givenPet);
            var requestContent = new StringContent(petJsonString, Encoding.UTF8, MediaTypeNames.Application.Json);
            return requestContent;
        }

        private static HttpClient CreateContextAndGetHttpClient()
        {
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var client = server.CreateClient();
            return client;
        }
    }
}
