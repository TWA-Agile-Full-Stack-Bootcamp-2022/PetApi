using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PetApi;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PetApiTest
{
    public class PetControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient client;
        public PetControllerTest(WebApplicationFactory<Program> factory)
        {
            client = factory.CreateClient();
        }

        // Need clarify requirement
        // 1. return object ?
        // 2. return code ?
        // 3. type not a dog or cat ?
        // 4. duplicate name ?
        [Fact]
        public async Task Should_create_the_pet_when_post_to_pet_route_given_name_type_color_and_price()
        {
            // Given
            Pet givenPet = new Pet("Buddy", PetType.Dog, "Gold", 300);
            string petJsonString = JsonConvert.SerializeObject(givenPet);
            var requestContent = new StringContent(petJsonString, Encoding.UTF8, MediaTypeNames.Application.Json);

            // When
            var response = await client.PostAsync("api/pets", requestContent);
            var responseBody = await response.Content.ReadAsStringAsync();
            var savedPet = JsonConvert.DeserializeObject<Pet>(responseBody);

            // Then
            response.EnsureSuccessStatusCode();
            Assert.Equal(givenPet, savedPet);
        }
    }
}
