using CretaceousPark.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CretaceousPark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private CretaceousParkContext _db;

        public AnimalsController(CretaceousParkContext db)
        {
            _db = db;
        }

        //Index, essentially - gets list of animals
        // GET api/animals
        [HttpGet]
        public ActionResult<IEnumerable<Animal>> Get(string species, string gender, string name)
        //^^ nameing for string parameter is important as >NET will auto bind param values based on query string
        {
            var query = _db.Animals.AsQueryable();
            //^^ collects list of all animals from db & returns as queryable LINQ object so that LINQ methods can be used to build onto the query before finalizing our selection
            if (species != null)
            //^^check if there is a species parameter, & build onto query if there is with the Where() method
            {
                query = query.Where(entry => entry.Species == species);
                //^^Where() method accepts a fxn that will check whether each element passes the condition & does not get filtered out
            }

            if (gender != null)
            {
                query = query.Where(entry => entry.Gender == gender);
            }

            if (name != null)
            {
                query = query.Where(entry => entry.Name == name);
            }

            return query.ToList();
        }

        //Creates
        //POST api/animals
        [HttpPost]
        public void Post([FromBody] Animal animal)
        {
            _db.Animals.Add(animal);
            _db.SaveChanges();
        }

        //Gets Details (Read)
        //GET api/animals/5
        [HttpGet("{id}")]
        public ActionResult<Animal> Get(int id)
        {
            return _db.Animals.FirstOrDefault(entry => entry.AnimalId == id);
        }

        //Update
        //PUT api/animals/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Animal animal)
        {
            animal.AnimalId = id;
            _db.Entry(animal).State = EntityState.Modified;
            _db.SaveChanges();
        }

        //DELETE api/animals/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var animalToDelete = _db.Animals.FirstOrDefault(entry => entry.AnimalId == id);
            _db.Animals.Remove(animalToDelete);
            _db.SaveChanges();
        }
    }
}