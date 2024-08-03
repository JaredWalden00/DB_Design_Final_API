using Microsoft.AspNetCore.Mvc;
using DB_Design_Final_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DB_Design_Final_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string query)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                .ToListAsync();

            return Ok(products);
        }

        [HttpPost("order")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok("Order placed successfully.");
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            var existingCustomer = await _context.Customers
                .Include(c => c.CCInfos)
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.Cust_ID == customer.Cust_ID);

            if (existingCustomer == null)
                return NotFound("Customer not found.");

            existingCustomer.Name = customer.Name;
            existingCustomer.Address = customer.Address;
            existingCustomer.Balance = customer.Balance;
            existingCustomer.CCInfos = customer.CCInfos;
            existingCustomer.Addresses = customer.Addresses;

            await _context.SaveChangesAsync();
            return Ok("Customer updated successfully.");
        }
    }
}
