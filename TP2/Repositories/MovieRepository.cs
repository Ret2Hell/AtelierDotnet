using Microsoft.EntityFrameworkCore;
using TP2.Data;
using TP2.Interfaces;
using TP2.Models;

namespace TP2.Repositories;

public class MovieRepository(ApplicationDbContext context) : IMovieRepository
{
    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        return await context.Movies.Include(m => m.Genre).ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre)
    {
        return await context.Movies.Include(m => m.Genre)
            .Where(m => m.Genre.Name == genre)
            .ToListAsync();
    }


    public IEnumerable<Genre> GetAllGenresAsync()
    {
        return context.Genres.ToList();
    }

    public async Task AddMovie(Movie movie, IFormFile? photo)
    {
        movie.AddedDate = DateTime.Now;

        if (photo is { Length: > 0 })
        {
            var filePath = Path.Combine("wwwroot/images", photo.FileName);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            movie.Photo = "/images/" + photo.FileName;
        }

        context.Add(movie);
        await context.SaveChangesAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await context.Movies.FindAsync(id);
    }

    public async Task UpdateMovie(Movie movie, IFormFile? photo)
    {
        var existingMovie = await context.Movies.FindAsync(movie.Id);

        if (existingMovie != null)
        {
            existingMovie.Name = movie.Name;
            existingMovie.GenreId = movie.GenreId;
            existingMovie.AddedDate = movie.AddedDate;

            if (photo is { Length: > 0 })
            {
                if (!string.IsNullOrEmpty(existingMovie.Photo))
                {
                    var oldFilePath = Path.Combine("wwwroot", existingMovie.Photo.TrimStart('/'));
                    if (File.Exists(oldFilePath)) File.Delete(oldFilePath);
                }

                var filePath = Path.Combine("wwwroot/images", photo.FileName);
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                existingMovie.Photo = "/images/" + photo.FileName;
            }
        }

        await context.SaveChangesAsync();
    }

    public async Task DeleteMovie(int id)
    {
        var movie = await context.Movies.FindAsync(id);
        if (!string.IsNullOrEmpty(movie?.Photo))
        {
            var filePath = Path.Combine("wwwroot", movie.Photo.TrimStart('/'));
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        if (movie != null) context.Movies.Remove(movie);
        await context.SaveChangesAsync();
    }

    public bool MovieExists(int id)
    {
        return context.Movies.Any(e => e.Id == id);
    }
}