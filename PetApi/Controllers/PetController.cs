using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetApi.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private List<Pet> pets = new List<Pet>();

        [HttpPost]
        public Pet PostPet(Pet pet)
        {
            pets.Add(pet);
            return pet;
        }
    }
}
