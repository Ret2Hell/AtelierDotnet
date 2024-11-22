namespace TP2.Models;

public class CustomerMovie
{
    public int CustomerId { get; set; }
    public required Customer Customer { get; set; }

    public int MovieId { get; set; }
    public required Movie Movie { get; set; }
}