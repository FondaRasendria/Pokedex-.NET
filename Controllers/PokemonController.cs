using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Repositories;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController: ControllerBase
    {
        private readonly PokemonRepository _repository;

        private readonly ILogger<PokemonController> _logger;

        public PokemonController(ILogger<PokemonController> logger, PokemonRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pokemons = await _repository.GetAll();
                return Ok(pokemons);
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
                var pokemon = await _repository.GetById(id);
                if (pokemon == null)
                {
                    return NotFound("Tidak ada data dengan id tersebut");
                }

                return Ok(pokemon);
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
                var pokemon = await _repository.GetByName(name);
                if (pokemon == null)
                {
                    return NotFound("Tidak ada data dengan nama tersebut");
                }

                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pokemon pokemon)
        {
            try
            {
                var newPokemon = await _repository.Create(pokemon);
                return Ok(newPokemon);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pokemon pokemon)
        {
            try
            {
                var updatedPokemon = await _repository.Update(id, pokemon);
                return Ok(updatedPokemon);
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
                var deletedPokemon = await _repository.Delete(id);
                return Ok(deletedPokemon);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
