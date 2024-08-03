using Microsoft.AspNetCore.Mvc;
using DB_Design_Final_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DB_Design_Final_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("product")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok("Product created successfully.");
        }
        [HttpPost("warehouse")]
        public async Task<IActionResult> AddWarehouse([FromBody] Warehouse warehouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return Ok("Warehouse added successfully.");
        }

        [HttpPost("stock")]
        public async Task<IActionResult> AddStock([FromBody] StockDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(stockDto.Prod_ID);
            var warehouse = await _context.Warehouses.FindAsync(stockDto.Ware_ID);

            if (product == null || warehouse == null)
            {
                return BadRequest("Invalid Product ID or Warehouse ID.");
            }

            var stock = new Stock
            {
                Prod_ID = stockDto.Prod_ID,
                Ware_ID = stockDto.Ware_ID,
                Count = stockDto.Count,
                Product = product,
                Warehouse = warehouse
            };

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return Ok("Stock added successfully.");
        }

        [HttpPut("product/{id}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] Product product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Prod_ID == id);
            if (existingProduct == null)
                return NotFound("Product not found.");

            existingProduct.Name = product.Name;
            existingProduct.Type = product.Type;
            existingProduct.Brand = product.Brand;
            existingProduct.Description = product.Description;
            existingProduct.Size = product.Size;
            existingProduct.Price = product.Price;

            await _context.SaveChangesAsync();
            return Ok("Product updated successfully.");
        }

        [HttpPut("stock")]
        public async Task<IActionResult> UpdateStock([FromBody] Stock stock)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Prod_ID == stock.Prod_ID && s.Ware_ID == stock.Ware_ID);
            if (existingStock == null)
                return NotFound("Stock not found.");

            existingStock.Count = stock.Count;

            await _context.SaveChangesAsync();
            return Ok("Stock updated successfully.");
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Customers
                .Include(c => c.CCInfos)
                .Include(c => c.Addresses)
                .ToListAsync();

            return Ok(customers);
        }

        [HttpPost("processOrder")]
        public async Task<IActionResult> ProcessOrder([FromBody] Order order)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Ordr_ID == order.Ordr_ID);
            if (existingOrder == null)
                return NotFound("Order not found.");

            existingOrder.Count = order.Count;

            await _context.SaveChangesAsync();
            return Ok("Order processed successfully.");
        }
    }
}
