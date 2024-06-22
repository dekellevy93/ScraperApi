using Microsoft.AspNetCore.Mvc;
using ScraperApi.Models;
using ScraperApi.Services;

namespace ScraperApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet("actors/{pageNumber:int}/{pageSize:int}")]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetAllActors(string? nameFilter = null,
                                                                   int? minRank = null,
                                                                   int? maxRank = null,
                                                                   int? pageNumber = 1,
                                                                   int? pageSize = 10)
        {
            try
            {
                // Pass parameters to the service method
                var actors = await _actorService.GetAllActorsAsync(nameFilter, minRank, maxRank, pageNumber, pageSize);
                return Ok(actors);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (don't expose raw details in production)
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActorById(int id)
        {
            try
            {
                var actor = await _actorService.GetActorByIdAsync(id);
                if (actor == null)
                {
                    return NotFound();
                }
                return Ok(actor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Actor>> AddActor([FromBody] Actor actor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _actorService.AddActorAsync(actor);
                return CreatedAtAction(nameof(GetActorById), new { id = actor.Id }, actor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Actor>> UpdateActor(int id, [FromBody] Actor actor)
        {
            try
            {
                if (id != actor.Id)
                {
                    return BadRequest("Actor ID mismatch");
                }

                var existingActor = await _actorService.GetActorByIdAsync(id);
                if (existingActor == null)
                {
                    return NotFound();
                }

                await _actorService.UpdateActorAsync(actor);
                return Ok(actor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(int id)
        {
            try
            {
                var actor = await _actorService.GetActorByIdAsync(id);
                if (actor == null)
                {
                    return NotFound();
                }

                await _actorService.DeleteActorAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
