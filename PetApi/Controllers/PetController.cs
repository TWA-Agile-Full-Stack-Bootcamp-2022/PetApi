using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetApi.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetController : ControllerBase
    {
        public static List<Pet> Pets { get; set; } = new List<Pet>();

        [HttpPost]
        public Pet PostPet(Pet pet)
        {
            Pets.Add(pet);
            return pet;
        }

        [HttpGet]
        public List<Pet> GetAllPets()
        {
            return Pets;
        }
    }
}
