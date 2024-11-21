using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP2.Data;
using TP2.Models;

namespace TP2.Controllers
{
    public class MoviesController(ApplicationDbContext context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var movies = await context.Movies.Include(m => m.Genre).ToListAsync();
            return View(movies);
        }

        public IActionResult Create()
        {
            ViewBag.Genres = context.Genres.ToList(); // Load genres for dropdown
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, GenreId")] Movie movie, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                // Set the AddedDate to the current date
                movie.AddedDate = DateTime.Now;

                // Handle the uploaded file
                if (Photo != null && Photo.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/images", Photo.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Photo.CopyToAsync(stream);
                    }
                    // Save the file path to the database
                    movie.Photo = "/images/" + Photo.FileName;
                }

                context.Add(movie);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload genres in case model validation fails
            ViewBag.Genres = context.Genres.ToList();
            return View(movie);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var movie = await context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewBag.Genres = context.Genres.ToList();
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, GenreId, AddedDate")] Movie movie, IFormFile Photo)
{
    if (id != movie.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            // Find the existing movie in the database
            var existingMovie = await context.Movies.FindAsync(id);

            if (existingMovie == null)
            {
                return NotFound();
            }

            // Update properties
            existingMovie.Name = movie.Name;
            existingMovie.GenreId = movie.GenreId;
            existingMovie.AddedDate = movie.AddedDate;

            // Handle the uploaded file
            if (Photo != null && Photo.Length > 0)
            {
                // Delete old file if it exists
                if (!string.IsNullOrEmpty(existingMovie.Photo))
                {
                    var oldFilePath = Path.Combine("wwwroot", existingMovie.Photo.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Save the new file
                var filePath = Path.Combine("wwwroot/images", Photo.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Photo.CopyToAsync(stream);
                }
                existingMovie.Photo = "/images/" + Photo.FileName;
            }

            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(movie.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }

    // Reload genres in case of a validation error
    ViewBag.Genres = context.Genres.ToList();
    return View(movie);
}

private bool MovieExists(int id)
{
    return context.Movies.Any(e => e.Id == id);
}

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await context.Movies.Include(m => m.Genre).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            context.Movies.Remove(movie);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
