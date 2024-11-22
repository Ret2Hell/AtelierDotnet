using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2.Interfaces;
using TP2.Models;

namespace TP2.Controllers;

public class CustomerController(ICustomerRepository customerRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var customers = await customerRepository.GetAllCustomersAsync();

        return View(customers);
    }

    public IActionResult Create()
    {
        ViewBag.MembershipTypes = customerRepository.GetAllMembershipTypes();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer customer)
    {
        if (ModelState.IsValid)
        {
            await customerRepository.AddCustomer(customer);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.MembershipTypes = customerRepository.GetAllMembershipTypes();
        return View(customer);
    }

    public async Task<IActionResult> Details(int id)
    {
        var customer = await customerRepository.GetCustomerByIdAsync(id);

        if (customer == null) return NotFound();

        return View(customer);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var customer = await customerRepository.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();

        ViewBag.MembershipTypes = customerRepository.GetAllMembershipTypes();
        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id, Name, MembershipTypeId")] Customer customer)
    {
        if (id != customer.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await customerRepository.UpdateCustomer(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customerRepository.CustomerExists(customer.Id)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.MembershipTypes = customerRepository.GetAllMembershipTypes();
        return View(customer);
    }


    public async Task<IActionResult> Delete(int id)
    {
        var customer = await customerRepository.GetCustomerByIdAsync(id);

        if (customer == null) return NotFound();

        return View(customer);
    }

    [HttpPost]
    [ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await customerRepository.DeleteCustomer(id);
        return RedirectToAction(nameof(Index));
    }
}