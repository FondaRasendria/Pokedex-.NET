using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Repositories;
using Dapper;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeController: ControllerBase
    {
        private readonly TypeRepository _repository;

        private readonly ILogger<TypeController> _logger;

        public TypeController(ILogger<TypeController> logger, TypeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var types = await _repository.GetAll();
                return Ok(types);
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
                var type = await _repository.GetById(id);
                if (type == null)
                {
                    return NotFound("Tidak ada data dengan id tersebut");
                }

                return Ok(type);
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
                var type = await _repository.GetByName(name);
                if (type == null)
                {
                    return NotFound("Tidak ada data dengan nama tersebut");
                }

                return Ok(type);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Type type)
        {
            try
            {
                var newType = await _repository.Create(type);
                return Ok(newType);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Models.Type type)
        {
            try
            {
                var updatedType = await _repository.Update(id, type);
                return Ok(updatedType);
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
                var deletedType = await _repository.Delete(id);
                return Ok(deletedType);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
