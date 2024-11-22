using TP2.Models;

namespace TP2.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    IEnumerable<MembershipType> GetAllMembershipTypes();
    Task AddCustomer(Customer customer);
    Task<Customer?> GetCustomerByIdAsync(int id);

    Task UpdateCustomer(Customer customer);

    Task DeleteCustomer(int id);
    bool CustomerExists(int id);
}