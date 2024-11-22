using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2.Interfaces;
using TP2.Models;

namespace TP2.Controllers;

public class MovieController(IMovieRepository movieRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var movies = await movieRepository.GetAllMoviesAsync();
        return View(movies);
    }

    public IActionResult Create()
    {
        ViewBag.Genres = movieRepository.GetAllGenresAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name, GenreId")] Movie movie, IFormFile? photo)
    {
        if (ModelState.IsValid)
        {
            await movieRepository.AddMovie(movie, photo);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Genres = movieRepository.GetAllGenresAsync();
        return View(movie);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var movie = await movieRepository.GetMovieByIdAsync(id);
        ViewBag.Genres = movieRepository.GetAllGenresAsync();
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id, Name, GenreId, AddedDate")] Movie movie, IFormFile? photo)
    {
        if (id != movie.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await movieRepository.UpdateMovie(movie, photo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!movieRepository.MovieExists(movie.Id)) return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Genres = movieRepository.GetAllGenresAsync();
        return View(movie);
    }


    public async Task<IActionResult> Delete(int id)
    {
        var movie = await movieRepository.GetMovieByIdAsync(id);
        return View(movie);
    }

    [HttpPost]
    [ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await movieRepository.DeleteMovie(id);
        return RedirectToAction(nameof(Index));
    }
}