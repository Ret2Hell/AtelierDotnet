namespace TP2.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Name { get; set; }

    // Foreign key to Genre
    public Guid GenreId { get; set; }

    // Navigation property
    public Genre? Genre { get; set; }

    // Many-to-many relationship with Customer
    public ICollection<CustomerMovie> CustomerMovies { get; set; } = new List<CustomerMovie>();

    // New properties
    public DateTime AddedDate { get; set; } = DateTime.Now;
    public string? Photo { get; set; }
}