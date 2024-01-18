using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Repositories;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonTrainerController: ControllerBase
    {
        private readonly ILogger<PokemonTrainerController> _logger;
        private readonly PokemonTrainerRepository _repository;

        public PokemonTrainerController(ILogger<PokemonTrainerController> logger, PokemonTrainerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pokemontrainers = await _repository.GetAll();
                return Ok(pokemontrainers);
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
                var pokemontrainer = await _repository.GetById(id);
                if (pokemontrainer == null)
                {
                    return NotFound("Tidak ada data dengan id tersebut");
                }

                return Ok(pokemontrainer);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}/getByTrainerId")]
        public async Task<IActionResult> GetByTrainerId(int id)
        {
            try
            {
                var pokemontrainer = await _repository.GetByTrainerId(id);
                if (pokemontrainer == null)
                {
                    return NotFound("Tidak ada data dengan pokemonId tersebut");
                }

                return Ok(pokemontrainer);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PokemonTrainer pokemontrainer)
        {
            try
            {
                var newpokemontrainer = await _repository.Create(pokemontrainer);
                return Ok(newpokemontrainer);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PokemonTrainer pokemontrainer)
        {
            try
            {
                var updatedpokemontrainer = await _repository.Update(id, pokemontrainer);
                return Ok(updatedpokemontrainer);
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
                var deletedpokemontrainer = await _repository.Delete(id);
                return Ok(deletedpokemontrainer);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
