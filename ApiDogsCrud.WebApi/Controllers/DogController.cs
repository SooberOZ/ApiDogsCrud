using ApiDogsCrud.Contracts;
using ApiDogsCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDogsCrud.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogController : ControllerBase
    {
        private readonly IDogService _dogService;

        public DogController(IDogService dogService)
        {
            _dogService = dogService;
        }

        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            return "Dogs house service. Version 1.0.1";
        }

        [HttpGet("GetDogs")]
        public async Task<IActionResult> GetDogsAsync([FromQuery] DogsFilter filter)
        {
            try
            {
                var dogs = await _dogService.GetDogsAsync(filter);
                return Ok(dogs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CreateDog")]
        public async Task<IActionResult> CreateDogAsync([FromBody] DogDto dog)
        {
            try
            {
                await _dogService.CreateDogAsync(dog);
                return Ok("Dog created successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}