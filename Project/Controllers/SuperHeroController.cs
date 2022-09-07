using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private static List<SuperHero> heroes = new List<SuperHero>
            {
               
                new SuperHero{
                Id=2,
                Name="IronMan",
                firstName="Tony",
                LastName="Stark",
                Place="Long Island"}
            };
        private readonly DataContext _context;

        public SuperHeroController(DataContext dataContext)
        {
            _context = dataContext;
        }



        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero Not Found");

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero superHero)
        {
            _context.SuperHeroes.Add(superHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var DbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (DbHero == null)
                return BadRequest("Hero Not Found");


            DbHero.Name = request.Name;
            DbHero.firstName = request.firstName;
            DbHero.LastName = request.LastName;
            DbHero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var DbHero = await _context.SuperHeroes.FindAsync(id);
            if (DbHero == null)
                return BadRequest("Hero Not Found");

            
            _context.SuperHeroes.Remove(DbHero);
            _context.SaveChanges();
            return Ok( await _context.SuperHeroes.ToListAsync());
        }

    }
}
