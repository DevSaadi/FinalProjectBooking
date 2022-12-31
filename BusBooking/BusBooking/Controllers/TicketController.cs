using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusBooking.Models;

namespace BusBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly BusTicketContext _context;

        public TicketController(BusTicketContext context)
        {
            _context = context;
        }

        // GET: api/Ticket
        [HttpGet]
        public IEnumerable<TicketSale> GetTicketSale()
        {
            return _context.TicketSale;
        }

        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketSale([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticketSale = await _context.TicketSale.FindAsync(id);

            if (ticketSale == null)
            {
                return NotFound();
            }

            return Ok(ticketSale);
        }

        // PUT: api/Ticket/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketSale([FromRoute] int id, [FromBody] TicketSale ticketSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticketSale.TicketSaleId)
            {
                return BadRequest();
            }

            _context.Entry(ticketSale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketSaleExists(id))
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

        // POST: api/Ticket
        [HttpPost]
        public async Task<IActionResult> PostTicketSale([FromBody] TicketSale ticketSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TicketSale.Add(ticketSale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketSale", new { id = ticketSale.TicketSaleId }, ticketSale);
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketSale([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticketSale = await _context.TicketSale.FindAsync(id);
            if (ticketSale == null)
            {
                return NotFound();
            }

            _context.TicketSale.Remove(ticketSale);
            await _context.SaveChangesAsync();

            return Ok(ticketSale);
        }

        private bool TicketSaleExists(int id)
        {
            return _context.TicketSale.Any(e => e.TicketSaleId == id);
        }
    }
}