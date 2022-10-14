using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("pet")]
    public class PetController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Pet> AddPet(Pet pet)
        {
            return pet;
        }
    }
}