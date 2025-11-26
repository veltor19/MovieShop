using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
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

        public async Task<PaginatedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1) {
            // Get total count for the genre
            var totalCount = await _movieShopDbContext.MovieGenres
                .Where(mg => mg.GenreId == genreId)
                .CountAsync();

            // Get paginated movies
            var movies = await _movieShopDbContext.MovieGenres
                .Where(mg => mg.GenreId == genreId)
                .Include(mg => mg.Movie)
                    .ThenInclude(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(mg => mg.Movie)
                .Distinct()
                .ToListAsync();

            return new PaginatedResultSet<Movie> {
                Data = movies,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalCount
            };
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
