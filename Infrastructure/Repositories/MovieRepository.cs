using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories {
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository {

        public MovieRepository(MovieShopDbContext movieShopDbContext): base(movieShopDbContext) {

        }

        public async Task<Movie> CreateMovie(Movie movie) {
            movie.CreatedDate = DateTime.Now;
            await _movieShopDbContext.Movies.AddAsync(movie);
            await _movieShopDbContext.SaveChangesAsync();
            return movie;
        }
        public async Task<IEnumerable<Movie>> GetTop20GrossingMovies() {
            var movies = await _movieShopDbContext.Movies.OrderByDescending(x => x.Revenue).Take(20).ToListAsync();
            return movies;
        }

        public async Task<Movie> GetMovieById(int id) {
            return await _movieShopDbContext.Movies
                .Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieCasts).ThenInclude(mc => mc.Cast)
                .Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int? Id) {
            var movies = await _movieShopDbContext.Movies.Where(m => m.MovieGenres.Any(mg => mg.GenreId == Id)).ToListAsync();
            return movies;
        }

        public async Task<decimal> GetAverageRating(int movieId) {
            var ratings = await _movieShopDbContext.Reviews.Where(r => r.MovieId == movieId)
                .Select(r => r.Rating).ToListAsync();
            if (!ratings.Any())
                return 0;
            return ratings.Average();
        }
    }
}
