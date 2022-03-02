using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; //ToListAsync
using WebApiCanciones.Entidades;

namespace WebApiCanciones.Controllers
{
    [ApiController]
    [Route("api/albumes")]

    public class AlbumesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public AlbumesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Album>>> GetAll()
        {
            return await dbContext.Albumes.ToListAsync();

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Album>> GetById(int id)
        {
            return await dbContext.Albumes.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Album album)
        //async: programación asíncrona
        //task: métodos asíncronos devuelven task
        {
            var existeCancion = await dbContext.Canciones.AnyAsync(x => x.Id == album.CancionId);
            if (!existeCancion)
            {
                return BadRequest($"No existe la canción con el id: {album.CancionId}");
            }
            dbContext.Add(album);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Cancion cancion, int id)
        {
            var existe = await dbContext.Albumes.AnyAsync(x => x.Id == id);
            
            if (cancion.Id != id)
            {
                return BadRequest("El id de la canción no coincide con el establecido en la url.");
            }

            dbContext.Update(cancion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Canciones.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado.");

            }

            dbContext.Remove(new Cancion()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
