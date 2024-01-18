using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Repositories;
using System.Collections;
using System.Net;

namespace Pokedex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainerController: ControllerBase
    {
        private readonly TrainerRepository _repository;

        private readonly ILogger<TrainerController> _logger;

        public TrainerController(ILogger<TrainerController> logger, TrainerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var trainers = await _repository.GetAll();

                return Ok(trainers);
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
                var trainer = await _repository.GetById(id);
                if (trainer == null)
                {
                    return NotFound("Tidak ada data dengan id tersebut");
                }

                return Ok(trainer);
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
                var trainer = await _repository.GetByName(name);
                if (trainer == null)
                {
                    return NotFound("Tidak ada data dengan nama tersebut");
                }

                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Trainer trainer)
        {
            try
            {
                var newTrainer = await _repository.Create(trainer);
                return Ok(newTrainer);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Trainer trainer)
        {
            try
            {
                var updatedTrainer = await _repository.Update(id, trainer);
                return Ok(updatedTrainer);
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
                var deletedTrainer = await _repository.Delete(id);
                return Ok(deletedTrainer);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
