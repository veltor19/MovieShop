using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories {
    public interface IMovieRepository: IRepository<Movie> {
        Task<IEnumerable<Movie>> GetTop20GrossingMovies();

        Task<PaginatedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1);

        Task<decimal> GetAverageRating(int movieId);

        public Task<Movie> GetMovieById(int id);

        public Task<Movie> CreateMovie(Movie movie);
    }
}
