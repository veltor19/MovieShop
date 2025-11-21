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

        public Movie CreateMovie(Movie movie) {
            movie.CreatedDate = DateTime.Now;
            _movieShopDbContext.Movies.Add(movie);
            _movieShopDbContext.SaveChanges();
            return movie;
        }
        public IEnumerable<Movie> GetTop20GrossingMovies() {
            var movies = _movieShopDbContext.Movies.OrderByDescending(x => x.Revenue).Take(20);
            return movies;
        }

        public Movie GetMovieById(int id) {
            return _movieShopDbContext.Movies
                .Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieCasts).ThenInclude(mc => mc.Cast)
                .Include(m => m.Trailers)
                .FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Movie> GetMoviesByGenre(int? Id) {
            var movies = _movieShopDbContext.Movies.Where(m => m.MovieGenres.Any(mg => mg.GenreId == Id));
            return movies;
        }

        public decimal GetAverageRating(int movieId) {
            var ratings = _movieShopDbContext.Reviews.Where(r => r.MovieId == movieId)
                .Select(r => r.Rating).ToList();
            if (!ratings.Any())
                return 0;
            return ratings.Average();
        }
    }
}
