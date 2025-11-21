using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services {
    public interface IAdminService {
        IEnumerable<TopMovieViewModel> GetTopMovies(DateTime? fromDate, DateTime? toDate);
        Movie CreateMovie(CreateMovieViewModel model, string createdBy);

    }
}
