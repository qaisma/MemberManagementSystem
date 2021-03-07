using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemberManagementSystem.Models;

namespace MemberManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly MMSDbContext _context;

        public AccountsController(MMSDbContext context)
        {
            _context = context;
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // POST: api/Accounts/CollectPoints
        [HttpPost]
        [Route("CollectPoints")]
        public async Task<IActionResult> CollectPoints([FromForm] int memberId, [FromForm] int companyId, [FromForm] int pointsCount)
        {
            var account = _context.Accounts.SingleOrDefault(a => a.MemberId == memberId && a.CompanyId == companyId);
            if (account == null)
                return NotFound();

            account.Balance += pointsCount;

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Accounts/RedeemPoints
        [HttpPost]
        [Route("RedeemPoints")]
        public async Task<IActionResult> RedeemPoints([FromForm] int memberId, [FromForm] int companyId, [FromForm] int pointsCount)
        {
            var account = _context.Accounts.SingleOrDefault(a => a.MemberId == memberId && a.CompanyId == companyId);
            if (account == null)
                return NotFound();

            account.Balance -= pointsCount;

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        //POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            _context.Accounts.Add(account);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountExists(account.MemberId, account.CompanyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccount", new { id = account.MemberId }, account);
        }

        private bool AccountExists(int memberId, int companyId)
        {
            return _context.Accounts.Any(e => e.MemberId == memberId && e.CompanyId == companyId);
        }
    }
}
