using TP2.Models;

namespace TP2.Interfaces;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);
    IEnumerable<Genre> GetAllGenresAsync();
    Task AddMovie(Movie movie, IFormFile? photo);
    Task<Movie?> GetMovieByIdAsync(int id);
    Task UpdateMovie(Movie movie, IFormFile? photo);

    bool MovieExists(int id);

    // delete movie
    Task DeleteMovie(int id);
}