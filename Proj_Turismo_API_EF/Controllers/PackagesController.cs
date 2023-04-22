using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proj_Turismo_API_EF.Data;
using Proj_Turismo_API_EF.Models;

namespace Proj_Turismo_API_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly Proj_Turismo_API_EFContext _context;

        public PackagesController(Proj_Turismo_API_EFContext context)
        {
            _context = context;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackage()
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            return await _context.Package
                .Include(p => p.Hotel)
                    .ThenInclude(h => h.Address.City)
                .Include(p => p.Ticket)
                    .ThenInclude(t => t.Origin.City)
                .Include(p => p.Ticket.Destination.City)
                .Include(p => p.Ticket.Client)
                .Include(p => p.Client.Address.City)
                .ToListAsync();
        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            //var package = await _context.Package.FindAsync(id);
            var package = await _context.Package
                .Include(p => p.Hotel)
                    .ThenInclude(h => h.Address.City)
                //.ThenInclude(a => a.City)
                .Include(p => p.Ticket)
                    .ThenInclude(t => t.Origin.City)
                //.ThenInclude(a => a.City)
                .Include(p => p.Ticket)
                    .ThenInclude(t => t.Destination.City)
                //.ThenInclude(a => a.City)
                .Include(p => p.Client)
                    .ThenInclude(c => c.Address.City)
                        //.ThenInclude(a => a.City)
                        .FirstOrDefaultAsync(p => p.Id == id);

            if (package == null)
            {
                return NotFound();
            }
            return package;
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, Package package)
        {
            if (id != package.Id)
            {
                return BadRequest();
            }

            _context.Entry(package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            if (_context.Package == null)
            {
                return Problem("Entity set 'Proj_Turismo_API_EFContext.Package'  is null.");
            }
            var hotel = await _context.Hotel
                .Include(h => h.Address)
                    .ThenInclude(a => a.City)
                    .FirstAsync(h => h.Id == package.Hotel.Id);

            if (hotel == null)
            {
                return NotFound();
            }
            package.Hotel = hotel;


            var ticket = await _context.Ticket
                .Include(t => t.Origin)
                    .ThenInclude(a => a.City)
                    .FirstAsync(t => t.Id == package.Ticket.Id);

            if (ticket == null)
            {
                return NotFound();
            }
            package.Ticket = ticket;


            var client = await _context.Client
                .Include(c => c.Address)
                    .ThenInclude(a => a.City)
                    .FirstAsync(c => c.Id == package.Client.Id);

            if (client == null)
            {
                return NotFound();
            }
            package.Client = client;

            _context.Package.Add(package);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackage", new { id = package.Id }, package);
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Package.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            return (_context.Package?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
