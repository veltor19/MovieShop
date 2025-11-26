using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services {
    public interface IMovieService {
        // all the business logic methods relating to movies

        Task<List<MovieCardModel>> GetTop20GrossingMovies();

        Task<MovieDetailsModel> GetMovieDetails(int id, int? userId);

        Task<bool> DeleteMovie(int id);

        Task<PaginatedResultSet<MovieCardModel>> GetMoviesByGenrePaginated(int genreId, int pageSize, int pageNumber);
    }
}
