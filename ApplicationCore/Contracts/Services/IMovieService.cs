using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services {
    public interface IMovieService {
        // all the business logic methods relating to movies

        List<MovieCardModel> GetTop20GrossingMovies();

        MovieDetailsModel GetMovieDetails(int id, int? userId);

        bool DeleteMovie(int id);

        List<MovieCardModel> GetMovieByGenre(int? id);
    }
}
