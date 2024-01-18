using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Repositories;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonTypeController: ControllerBase
    {
        private readonly PokemonTypeRepository _repository;

        private readonly ILogger<PokemonTypeController> _logger;

        public PokemonTypeController(ILogger<PokemonTypeController> logger, PokemonTypeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pokemontypes = await _repository.GetAll();
                return Ok(pokemontypes);
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
                var pokemontype = await _repository.GetById(id);
                if (pokemontype == null)
                {
                    return NotFound("Tidak ada data dengan id tersebut");
                }

                return Ok(pokemontype);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}/getByPokemonId")]
        public async Task<IActionResult> GetByPokemonId(int id)
        {
            try
            {
                var pokemontype = await _repository.GetByPokemonId(id);
                if (pokemontype == null)
                {
                    return NotFound("Tidak ada data dengan pokemonId tersebut");
                }

                return Ok(pokemontype);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PokemonType pokemontype)
        {
            try
            {
                var newPokemonType = await _repository.Create(pokemontype);
                return Ok(newPokemonType);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PokemonType pokemontype)
        {
            try
            {
                var updatedPokemonType = await _repository.Update(id, pokemontype);
                return Ok(updatedPokemonType);
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
                var deletedPokemonType = await _repository.Delete(id);
                return Ok(deletedPokemonType);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
