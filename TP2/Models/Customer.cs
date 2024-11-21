namespace TP2.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
        
    // Foreign key to MembershipType
    public Guid MembershipTypeId { get; set; }

    // Navigation property
    public MembershipType? MembershipType { get; set; }

    // Many-to-many relationship with Movie
    public ICollection<CustomerMovie> CustomerMovies { get; set; } = new List<CustomerMovie>();
}