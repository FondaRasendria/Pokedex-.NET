using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Repositories;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController: ControllerBase
    {
        private readonly RegionRepository _repository;

        private readonly ILogger<RegionController> _logger;

        public RegionController(ILogger<RegionController> logger, RegionRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var regions = await _repository.GetAll();
                return Ok(regions);
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}/getById")]
        public async Task<IActionResult> GetById (int id)
        {
            try
            {
                var region = await _repository.GetById(id);
                return Ok(region);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{name}/getByName")]
        public async Task<IActionResult> GetByName (string name)
        {
            try
            {
                var region = await _repository.GetByName(name);
                return Ok(region);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create (Region region)
        {
            try
            {
                var createdRegion = await _repository.Create(region);
                return Ok(createdRegion);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update (int id, Region region)
        {
            try
            {
                var updatedRegion = await _repository.Update(id, region);
                return Ok(updatedRegion);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete (int id)
        {
            try
            {
                var deletedRegion = await _repository.Delete(id);
                return Ok(deletedRegion);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
