using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdPracticeMob.Models;

namespace ProdPracticeMob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketOffersController : ControllerBase
    {
        private readonly ProdPracticeContext _context;

        public MarketOffersController(ProdPracticeContext context)
        {
            _context = context;
        }

        // GET: api/MarketOffers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketOffer>>> GetMarketOffers()
        {
            //return await _context.MarketOffers.ToListAsync();

            var marketOffers = await _context.MarketOffers
                .Include(p => p.Computer)           // Подключение таблицы Компы
                .Include(p => p.Seller)         // Подключение таблицы Продавцы
                .Select(p => new
                {
                    p.Id,
                    ComputerName = p.Computer.ModelName, // Модель компа
                    SellerName = p.Seller.Name, // Имя продавца
                    p.BatchSize,
                    p.BatchPrice
                })
                .ToListAsync();

            return Ok(marketOffers);

        }

        // GET: api/MarketOffers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MarketOffer>> GetMarketOffer(int id)
        {
            var marketOffers = await _context.MarketOffers
                .Include(p => p.Computer)
                .Include(p => p.Seller)
                .Select(p => new
                {
                    p.Id,
                    ComputerName = p.Computer.ModelName, 
                    SellerName = p.Seller.Name,
                    p.BatchSize,
                    p.BatchPrice
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (marketOffers == null)
            {
                return NotFound();
            }

            return Ok(marketOffers);
        }

        // PUT: api/MarketOffers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarketOffer(int id, MarketOffer marketOffer)
        {
            if (id != marketOffer.Id)
            {
                return BadRequest();
            }

            _context.Entry(marketOffer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarketOfferExists(id))
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

        // POST: api/MarketOffers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MarketOffer>> PostMarketOffer(MarketOffer marketOffer)
        {
            _context.MarketOffers.Add(marketOffer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarketOffer", new { id = marketOffer.Id }, marketOffer);
        }

        // DELETE: api/MarketOffers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarketOffer(int id)
        {
            var marketOffer = await _context.MarketOffers.FindAsync(id);
            if (marketOffer == null)
            {
                return NotFound();
            }

            _context.MarketOffers.Remove(marketOffer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarketOfferExists(int id)
        {
            return _context.MarketOffers.Any(e => e.Id == id);
        }
    }
}
