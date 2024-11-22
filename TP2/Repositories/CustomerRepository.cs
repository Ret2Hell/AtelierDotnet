using Microsoft.EntityFrameworkCore;
using TP2.Data;
using TP2.Interfaces;
using TP2.Models;

namespace TP2.Repositories;

public class CustomerRepository(ApplicationDbContext context) : ICustomerRepository
{
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await context.Customers.Include(c => c.MembershipType).ToListAsync();
    }

    public IEnumerable<MembershipType> GetAllMembershipTypes()
    {
        return context.MembershipTypes.ToList();
    }

    public async Task AddCustomer(Customer customer)
    {
        context.Add(customer);
        await context.SaveChangesAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await context.Customers
            .Include(c => c.MembershipType)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task UpdateCustomer(Customer customer)
    {
        context.Update(customer);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCustomer(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer == null) return;
        context.Customers.Remove(customer);
        await context.SaveChangesAsync();
    }

    public bool CustomerExists(int id)
    {
        return context.Customers.Any(e => e.Id == id);
    }
}