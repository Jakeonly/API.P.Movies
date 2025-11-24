using API.P.Movies.DAL;
using API.P.Movies.DAL.Models;
using API.P.Movies.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.P.Movies.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Movie>> GetMoviesAsync()
        {
            var movies = await _context.Movies
                .AsNoTracking()
                .OrderBy(m => m.Name)
                .ToListAsync();
            return movies;
        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            var movie = await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public async Task<bool> CreateMovieAsync(Movie movie)
        {
            movie.CreatedDate = DateTime.UtcNow;

            _context.Movies.Add(movie);
            return await SaveAsync();
        }

        public async Task<bool> UpdateMovieAsync(Movie movie)
        {
            movie.UpdateDate = DateTime.UtcNow;
            _context.Movies.Update(movie);
            return await SaveAsync();
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await GetMovieAsync(id);
            if (movie == null)
            {
                return false;
            }
            _context.Movies.Remove(movie);
            return await SaveAsync();
        }

        public async Task<bool> MovieExistsByIdAsync(int id)
        {
            return await _context.Movies
                .AsNoTracking()
                .AnyAsync(m => m.Id == id);
        }

        public async Task<bool> MovieExistsByNameAsync(string name)
        {
            return await _context.Movies
                .AsNoTracking()
                .AnyAsync(m => m.Name == name);
        }

        #region Private Methods
        private async Task<bool> SaveAsync()
        { 
            return await _context.SaveChangesAsync() >= 0 ? true : false;
        }
        #endregion

    }
}
