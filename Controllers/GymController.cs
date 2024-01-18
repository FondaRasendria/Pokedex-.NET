using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Repositories;
using System.Net;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymController : ControllerBase
    {
        private readonly GymRepository _repository;

        private readonly ILogger<GymController> _logger;

        public GymController(ILogger<GymController> logger, GymRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var gyms = await _repository.GetAll();
                return Ok(gyms);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}/getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var gym = await _repository.GetById(id);
                if (gym == null)
                {
                    return NotFound("Tidak ada data dengan id tersebut");
                }

                return Ok(gym);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{name}/getByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var gym = await _repository.GetByName(name);
                if (gym == null)
                {
                    return NotFound("Tidak ada data dengan nama tersebut");
                }

                return Ok(gym);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Gym gym)
        {
            try
            {
                var newGym = await _repository.Create(gym);
                return Ok(newGym);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Gym gym)
        {
            try
            {
                var updatedGym = await _repository.Update(id, gym);
                return Ok(updatedGym);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedGym = await _repository.Delete(id);
                return Ok(deletedGym);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
