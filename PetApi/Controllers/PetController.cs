using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("api/pets")]
    public class PetController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(Pet pet)
        {
            return null;
        }
    }
}
