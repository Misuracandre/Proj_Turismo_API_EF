using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    public class TicketsController : ControllerBase
    {
        private readonly Proj_Turismo_API_EFContext _context;

        public TicketsController(Proj_Turismo_API_EFContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
          if (_context.Ticket == null)
          {
              return NotFound();
          }
            return await _context.Ticket.ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
          if (_context.Ticket == null)
          {
              return NotFound();
          }
            //var ticket = await _context.Ticket.FindAsync(id);
            var ticket = await _context.Ticket
                .Include(t => t.Origin)
                    .ThenInclude(a => a.City)
                .Include(t => t.Destination)
                    .ThenInclude(a => a.City)
                .Include(t => t.Client)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
          if (_context.Ticket == null)
          {
              return Problem("Entity set 'Proj_Turismo_API_EFContext.Ticket'  is null.");
          }
          var originAddress = await _context.Address.Include(a => a.City).FirstOrDefaultAsync(a => a.Id == ticket.Origin.Id);
            if(originAddress == null)
            {
                return NotFound("Origin address not found.");
            }
            ticket.Origin = originAddress;

            var destinationAddress = await _context.Address.Include(a => a.City).FirstOrDefaultAsync(a => a.Id == ticket.Destination.Id);
            if(destinationAddress == null)
            {
                return NotFound("Destination address not found.");
            }
            ticket.Destination = destinationAddress;

            var client = await _context.Client.FirstAsync(c => c.Id == ticket.Client.Id);
            if(client == null)
            {
                return NotFound("Client not found.");
            }
            ticket.Client = client;

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
