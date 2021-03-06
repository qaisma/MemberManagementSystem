using MemberManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MemberManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly MMSDbContext _context;

        // GET: api/<CompaniesController>
        [HttpGet]
        public IEnumerable<Comapny> Get()
        {
            var result = new List<Comapny>();

            using (_context)
            {
                result = _context.Comapnies.ToList();
            }
            return result.ToArray();
        }

        // GET api/<CompaniesController>/5
        [HttpGet("{id}")]
        public Comapny Get(int id)
        {
            var result = new Comapny();

            using (_context)
            {
                result = _context.Comapnies.SingleOrDefault(obj => obj.Id == id);
            }
            if (result == null)
            {
                throw new KeyNotFoundException(string.Format("A company with the id value of {0} not found!", id));
            }
            return result;
        }

        // POST api/<CompaniesController>
        [HttpPost]
        public void Post([FromBody] Comapny value)
        {
        }

        // PUT api/<CompaniesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CompaniesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
