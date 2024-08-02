using DB_Design_Final_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DB_Design_Final_API.Controllers
{
    // Controllers/ProductsController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Prod_ID }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, Product product)
        {
            if (id != product.Prod_ID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.Prod_ID == id);
        }
    }

    // Controllers/StocksController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly DataContext _context;

        public StocksController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
            return await _context.Stocks.Include(s => s.Product).Include(s => s.Warehouse).ToListAsync();
        }

        // GET: api/Stocks/5
        [HttpGet("{prodId}/{wareId}")]
        public async Task<ActionResult<Stock>> GetStock(long prodId, long wareId)
        {
            var stock = await _context.Stocks.FindAsync(prodId, wareId);
            if (stock == null)
            {
                return NotFound();
            }
            return stock;
        }

        // POST: api/Stocks
        [HttpPost]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStock), new { prodId = stock.Prod_ID, wareId = stock.Ware_ID }, stock);
        }

        // PUT: api/Stocks/5
        [HttpPut("{prodId}/{wareId}")]
        public async Task<IActionResult> PutStock(long prodId, long wareId, Stock stock)
        {
            if (prodId != stock.Prod_ID || wareId != stock.Ware_ID)
            {
                return BadRequest();
            }

            _context.Entry(stock).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(prodId, wareId))
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

        // DELETE: api/Stocks/5
        [HttpDelete("{prodId}/{wareId}")]
        public async Task<IActionResult> DeleteStock(long prodId, long wareId)
        {
            var stock = await _context.Stocks.FindAsync(prodId, wareId);
            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockExists(long prodId, long wareId)
        {
            return _context.Stocks.Any(e => e.Prod_ID == prodId && e.Ware_ID == wareId);
        }
    }

}
