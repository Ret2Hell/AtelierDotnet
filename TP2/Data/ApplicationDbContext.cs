using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TP2.Models;

namespace TP2.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<CustomerMovie> CustomerMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var membershipJson = File.ReadAllText("C:\\Users\\ASUS\\RiderProjects\\TP2\\TP2\\Data\\membership_data.json");
            var membershipData = JsonConvert.DeserializeObject<List<MembershipType>>(membershipJson);
            modelBuilder.Entity<MembershipType>().HasData(membershipData!);
            
            var genreJson = File.ReadAllText("C:\\Users\\ASUS\\RiderProjects\\TP2\\TP2\\Data\\genre_data.json");
            var genreData = JsonConvert.DeserializeObject<List<Genre>>(genreJson);
            modelBuilder.Entity<Genre>().HasData(genreData!);
            // Movie to Genre relationship
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany()
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer to MembershipType relationship
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.MembershipType)
                .WithMany(m => m.Customers)
                .HasForeignKey(c => c.MembershipTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many relationship between Customer and Movie through CustomerMovie
            modelBuilder.Entity<CustomerMovie>()
                .HasKey(cm => new { cm.CustomerId, cm.MovieId });

            modelBuilder.Entity<CustomerMovie>()
                .HasOne(cm => cm.Customer)
                .WithMany(c => c.CustomerMovies)
                .HasForeignKey(cm => cm.CustomerId);

            modelBuilder.Entity<CustomerMovie>()
                .HasOne(cm => cm.Movie)
                .WithMany(m => m.CustomerMovies)
                .HasForeignKey(cm => cm.MovieId);
        }
    }
}