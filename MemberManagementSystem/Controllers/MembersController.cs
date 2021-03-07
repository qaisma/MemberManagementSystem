using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemberManagementSystem.Models;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace MemberManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly MMSDbContext _context;

        public MembersController(MMSDbContext context)
        {
            _context = context;
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers(int userId)
        {
            return await _context.Members.Where(m => m.UserId == userId).ToListAsync();
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // POST: api/Members
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.Id }, member);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // POST: api/Members
        [HttpPost]
        [Route("Import")]
        [MemberManagementSystem.Utilities.AllowedExtensions(new string[] { ".json" })]
        public async Task<IActionResult> ImportMembers(IFormFile postedFile, [FromForm] int userId)
        {
            try
            {
                var jsonSB = new System.Text.StringBuilder();
                if (postedFile.Length > 0)
                {
                    using (var reader = new System.IO.StreamReader(postedFile.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                            jsonSB.AppendLine(reader.ReadLine());
                    }

                    List<Member> members = JsonConvert.DeserializeObject<List<Member>>(jsonSB.ToString());
                    members.ForEach(m => m.UserId = userId);

                    var accounts = members.SelectMany(m => m.Accounts).ToList();

                    //this list is created to stack companies that are retreived from db in order to decrease db hits
                    List<Comapny> globalCompanies = new List<Comapny>();

                    foreach (var account in accounts)
                    {
                        var company = globalCompanies.SingleOrDefault(c => c.Name.ToLower() == account.CompanyName.ToLower());
                        if (company == null)
                        {
                            company = _context.Comapnies.SingleOrDefault(c => c.Name.ToLower() == account.CompanyName.ToLower());

                            //case company doesn't exist in the system
                            // I chose adding the comapy to the system instead of throwing exception
                            if (company == null)
                            {
                                company = new Comapny()
                                {
                                    Name = account.CompanyName
                                };
                                _context.Comapnies.Add(company);
                                _context.SaveChanges();
                            }

                            globalCompanies.Add(company);
                        }
                        account.CompanyId = company.Id;
                    }

                    _context.Members.AddRange(members);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest();
                }

                return CreatedAtAction("GetMembers", new { UserId = userId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // POST: api/Members
        [HttpGet]
        [Route("Export")]
        public IActionResult ExportMembers([FromForm] int? userId, [FromForm] int? minPointsCount, [FromForm] int? maxPointCount, [FromForm] int? accountStatus)
        {
            try
            {
                if (!userId.HasValue)
                    return BadRequest();

                IQueryable<Member> data = _context.Members.Where(m => m.UserId == userId).Include(m => m.Accounts).AsQueryable();
                if (accountStatus.HasValue)
                {
                    data = data.Where(m => m.Accounts.Any(a => (int)a.Status == accountStatus));
                }
                if (minPointsCount.HasValue)
                {
                    // case maximum and minimum are defined
                    if (maxPointCount.HasValue)
                    {
                        data = data.Where(m => m.Accounts.Any(a => a.Balance <= maxPointCount && a.Balance >= minPointsCount));
                    }
                    else // case only minimum is defined
                    {
                        data = data.Where(m => m.Accounts.Any(a => a.Balance >= minPointsCount));
                    }
                }
                // case only maximum is defined
                if (maxPointCount.HasValue)
                {
                    data = data.Where(m => m.Accounts.Any(a => a.Balance <= maxPointCount));
                }

                var dataAsList = data.ToList();

                //this list is created to stack companies that are retreived from db in order to decrease db hits
                List<Comapny> globalCompanies = new List<Comapny>();

                foreach (var account in dataAsList.SelectMany(m => m.Accounts))
                {
                    var company = globalCompanies.SingleOrDefault(c => c.Id == account.CompanyId);
                    if (company == null)
                    {
                        company = _context.Comapnies.SingleOrDefault(c => c.Id == account.CompanyId);
                        account.CompanyName = company.Name;
                        globalCompanies.Add(company);
                    }
                }
                var jsonData = JsonConvert.SerializeObject(dataAsList, Formatting.Indented,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
                return File(System.Text.Encoding.UTF8.GetBytes(jsonData), "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
