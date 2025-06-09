using Microsoft.AspNetCore.Mvc;
using Mibot.Domain.Entities;
using Mibot.Services.Features.Mangas;

namespace Mibot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangaController : ControllerBase
    {
        private readonly MangaService _mangaService;

        public MangaController(MangaService mangaService)
        {
            _mangaService = mangaService;
        }

        // GET: api/Manga
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manga>>> Get()
        {
            var mangas = await _mangaService.GetAll();
            return Ok(mangas);
        }

        // GET: api/Manga/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manga>> Get(int id)
        {
            var manga = await _mangaService.GetById(id);
            if (manga == null)
            {
                return NotFound();
            }
            return Ok(manga);
        }

        // POST: api/Manga
        [HttpPost]
        public async Task<ActionResult<Manga>> Post([FromBody] Manga manga)
        {
            var newManga = await _mangaService.Add(manga);
            return CreatedAtAction(nameof(Get), new { id = newManga.Id }, newManga);
        }

        // PUT: api/Manga/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Manga manga)
        {
            if (id != manga.Id)
            {
                return BadRequest();
            }

            if (await _mangaService.Update(manga))
            {
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/Manga/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _mangaService.Delete(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
} 