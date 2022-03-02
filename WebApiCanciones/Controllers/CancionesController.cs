using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; //ToListAsync
using WebApiCanciones.Entidades;

namespace WebApiCanciones.Controllers
{
    [ApiController]
    [Route("api/canciones")]
    public class CancionesController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public CancionesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }


        [HttpGet]
        [HttpGet("listado")] //api/canciones/listado
        [HttpGet("/listado")] // /listado
        public async Task<ActionResult<List<Cancion>>> Get()
        {
            return await dbContext.Canciones.Include(x => x.albumes).ToListAsync();

        }

        [HttpGet("primero")] //api/canciones/primero?
        public async Task<ActionResult<Cancion>> PrimeraCancion([FromHeader] int valor, [FromQuery] string cancion, [FromQuery] int cancionId)
        {
            return await dbContext.Canciones.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cancion>> Get(int id)
        {
            var cancion = await dbContext.Canciones.FirstOrDefaultAsync( x => x.Id == id);
            if (cancion == null)
            {
                return NotFound();
            }
            return cancion;
        }

        [HttpGet("{id:int}/{param?}")] //? ahorra escribir el nombre de la canción a buscar
        public async Task<ActionResult<Cancion>> Get(int id, string param)
        {
            var cancion = await dbContext.Canciones.FirstOrDefaultAsync(x => x.Id == id);
            if (param == null)
            {
                return NotFound();
            }
            return cancion;
        }

        [HttpGet("primero2")]
        public ActionResult<Cancion> PrimeraCancion() //Canción es el tipo de objeto
        {
            return new Cancion() { Nombre = "DOS" }; //Entonces regresa un objeto de ese tipo
        }

        /*[HttpGet("primero2")]
        public ActionResult<int> PrimeraCancion() //Int es el tipo de objeto
        {
            return 13; //Entonces regresa un objeto de ese tipo
        }*/

        /*[HttpGet("primero2")]
        public ActionResult<string> PrimeraCancion() //string es el tipo de objeto
        {
            return "Una cadena"; //Entonces regresa un objeto de ese tipo
        }*/

        /*[HttpGet("primero2")]
        public IActionResult PrimeraCancion() //IActionResult 
        {
            return Ok("Una cadena"); //Entonces regresa una acción 
        }*/

        /*[HttpGet("primero2")]
        public string PrimeraCancion() // Cadena
        {
            return "Una cadena"; //Entonces regresa una cadena
        }*/

        [HttpGet("{nombre}")] //Busca el primer registro que contenga el texto ingresado
        public async Task<ActionResult<Cancion>> Get([FromRoute] string nombre)
        {
            var cancion = await dbContext.Canciones.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));
            if (cancion == null)
            {
                return NotFound();
            }
            return cancion;
        }


 

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Cancion cancion) //FromBody, se manda desde el jason
        //async: programación asíncrona, pueden ejecutarse tareas en segundo espacio
        //task: métodos asíncronos devuelven task
        {
            dbContext.Add(cancion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Cancion cancion, int id)
        {
            if(cancion.Id != id)
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
