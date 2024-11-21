namespace TP2.Models
{
    public class CustomerMovie
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}