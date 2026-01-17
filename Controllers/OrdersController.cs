using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Data;
using OrderManagementApp.Models;

namespace OrderManagementApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderDbContext _context;

        public OrdersController(OrderDbContext context)
        {
            _context = context;
        }

        // READ + PAGINATION
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;

            var orders = await _context.Orders
                .Include(o => o.Product)
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = Math.Ceiling(_context.Orders.Count() / (double)pageSize);

            return View(orders);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            var product = await _context.Products.FindAsync(order.ProductId);

            if (product == null)
                ModelState.AddModelError("ProductId", "Selected product does not exist.");

            if (order.Quantity > product.StockQuantity)
                ModelState.AddModelError("Quantity", "Quantity cannot exceed product stock quantity.");

            if (order.OrderDate > DateTime.Today)
                ModelState.AddModelError("OrderDate", "Order date cannot be in the future.");

            if (order.DeliveryDate.HasValue && order.DeliveryDate < order.OrderDate)
                ModelState.AddModelError("DeliveryDate", "Delivery date must be after order date.");

            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View(order);
        }

        // EDIT - GET
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id) return NotFound();

            var product = await _context.Products.FindAsync(order.ProductId);

            if (order.Quantity > product.StockQuantity)
                ModelState.AddModelError("Quantity", "Quantity cannot exceed product stock quantity.");

            if (order.DeliveryDate.HasValue && order.DeliveryDate < order.OrderDate)
                ModelState.AddModelError("DeliveryDate", "Delivery date must be after order date.");

            if (ModelState.IsValid)
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        // DELETE - GET
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
