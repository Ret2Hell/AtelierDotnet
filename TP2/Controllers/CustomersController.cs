using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2.Data;
using TP2.Models;

namespace TP2.Controllers
{
    public class CustomersController(ApplicationDbContext context) : Controller
    {
        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var customers = await context.Customers
                .Include(c => c.MembershipType) // Include MembershipType to get DiscountRate
                .ToListAsync();

            return View(customers);
        }
        
        public IActionResult Create()
        {
            // Fetch the list of MembershipTypes to display in the dropdown
            ViewBag.MembershipTypes = context.MembershipTypes.ToList();
            return View();
        }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Customer customer)
{
    if (ModelState.IsValid)
    {
        // Add the customer to the database
        context.Add(customer);
        await context.SaveChangesAsync();

        // Redirect to the Index page after successful creation
        return RedirectToAction(nameof(Index));
    }

    // If model state is not valid, reload the MembershipTypes for the view
    ViewBag.MembershipTypes = context.MembershipTypes.ToList();
    return View(customer); // Return the form again with validation messages
}
        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await context.Customers
                .Include(c => c.MembershipType)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            // Load the membership types into ViewBag
            ViewBag.MembershipTypes = context.MembershipTypes.ToList();
            return View(customer);
        }

// POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, MembershipTypeId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(customer);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Reload the membership types if validation fails
            ViewBag.MembershipTypes = context.MembershipTypes.ToList();
            return View(customer);
        }

        private bool CustomerExists(int id)
        {
            return context.Customers.Any(e => e.Id == id);
        }


        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await context.Customers
                .Include(c => c.MembershipType)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

// POST: Customer/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
