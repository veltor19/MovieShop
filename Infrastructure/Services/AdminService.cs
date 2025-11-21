using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public class AdminService : IAdminService {
        private readonly IPurchaseRepository _context;
        private readonly IMovieRepository _movieRepository;

        public AdminService(IPurchaseRepository context, IMovieRepository movieRepository) {
            _context = context;
            _movieRepository = movieRepository;
        }

        public IEnumerable<TopMovieViewModel> GetTopMovies(DateTime? fromDate, DateTime? toDate) {
            return _context.GetTopMoviesByPurchase(fromDate, toDate);
        }

        public Movie CreateMovie(CreateMovieViewModel model, string createdBy) {
            var movie = new Movie {
                Title = model.Title,
                Overview = model.Overview,
                TagLine = model.TagLine,
                Budget = model.Budget,
                Revenue = model.Revenue,
                ImdbURL = model.ImdbURL,
                TmdbUrl = model.TmdbUrl,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                OriginalLanguage = model.OriginalLanguage,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Price = model.Price,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now
            };

            return _movieRepository.CreateMovie(movie);
        }
    }
}
