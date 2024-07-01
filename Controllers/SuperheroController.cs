using DotnetWebAPItesting.Data;
using DotnetWebAPItesting.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotnetWebAPItesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperheroController : ControllerBase
    {
        private readonly DataContext _context;

        // constructor
        public SuperheroController(DataContext context)
        {
            _context = context;
        }

        // GET: api/<SuperheroController>
        [HttpGet]
        public async Task<ActionResult<List<Superhero>>> GetAllHeroes()
        {
            var heroes = await _context.Superheroes.ToListAsync();

            return Ok(heroes);

        }

        // GET api/<SuperheroController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Superhero>> GetHero(int id)
        {
            var hero = await _context.Superheroes.FindAsync(id);
            if (hero is null)
            {
                return BadRequest("Hero not found.");
            }

            return Ok(hero);

        }

        // POST api/<SuperheroController>
        [HttpPost]
        public async Task<ActionResult<List<Superhero>>> CreateNewHero(Superhero hero)
        {
            _context.Superheroes.Add(hero);
            await _context.SaveChangesAsync();
            

            return Ok(await _context.Superheroes.ToListAsync());

        }

        // PUT api/<SuperheroController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Superhero>>> UpdateHero(Superhero updatedHero)
        {
            var dbHero = await _context.Superheroes.FindAsync(updatedHero.Id);
            if (dbHero is null)
            {
                return BadRequest("Hero not found. Unable to make update operation.");
            }

            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Place  = updatedHero.Place;

            return Ok(await _context.Superheroes.ToListAsync());

        }

        // DELETE api/<SuperheroController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Superhero>> DeleteHero(int id)
        {
            var hero = await _context.Superheroes.FindAsync(id);
            if (hero is null)
            {
                return BadRequest("Hero not found. Unable to make delete operation.");
            }

            _context.Superheroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Superheroes.ToListAsync());

        }
    }
}
