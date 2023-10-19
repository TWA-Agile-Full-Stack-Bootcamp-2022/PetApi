using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetApi.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private static List<Pet> pets = new List<Pet>();

        [HttpPost]
        public Pet PostPet(Pet pet)
        {
            pets.Add(pet);
            return pet;
        }

        [HttpGet]
        public List<Pet> GetAllPets()
        {
            return pets;
        }

        [HttpGet("{name}")]
        public Pet GetPetByName(string name)
        {
            return pets.FirstOrDefault(pet => name.Equals(pet.Name));
        }

        [HttpDelete("{name}")]
        public void DeletePetByName(string name)
        {
            pets.RemoveAll(pet => name.Equals(pet.Name));
        }

        [HttpGet("resetPets")]
        public void ResetPets()
        {
            pets.Clear();
        }
    }
}
