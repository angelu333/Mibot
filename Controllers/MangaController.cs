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
        public ActionResult<IEnumerable<Manga>> Get()
        {
            return Ok(_mangaService.GetAll());
        }

        // GET: api/Manga/5
        [HttpGet("{id}")]
        public ActionResult<Manga> Get(int id)
        {
            var manga = _mangaService.GetById(id);
            if (manga == null)
            {
                return NotFound();
            }
            return Ok(manga);
        }

        // POST: api/Manga
        [HttpPost]
        public ActionResult<Manga> Post([FromBody] Manga manga)
        {
            _mangaService.Add(manga);
            return CreatedAtAction(nameof(Get), new { id = manga.Id }, manga);
        }

        // PUT: api/Manga/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Manga manga)
        {
            if (id != manga.Id)
            {
                return BadRequest();
            }

            if (_mangaService.Update(manga))
            {
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/Manga/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_mangaService.Delete(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
} 