using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services {
    public interface IAdminService {
        Task<IEnumerable<TopMovieViewModel>> GetTopMovies(DateTime? fromDate, DateTime? toDate);
        Task<Movie> CreateMovie(CreateMovieViewModel model, string createdBy);

    }
}
