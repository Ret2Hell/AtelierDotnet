namespace TP2.Models;

public class MembershipType
{
    public Guid Id { get; set; }
    public decimal SignUpFee { get; set; }
    public int DurationInMonth { get; set; }
    public decimal DiscountRate { get; set; }

    // Navigation property
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}