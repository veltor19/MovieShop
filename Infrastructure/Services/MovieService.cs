using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public class MovieService : IMovieService {
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        public MovieService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository) {
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<bool> DeleteMovie(int id) {
            var movie = await _movieRepository.DeleteById(id);
            if (movie == null) {
                return false;
            }
            return true;
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int id, int? userId) {
            var movie = await _movieRepository.GetById(id);
            if (movie != null) {
                var movieDetails = new MovieDetailsModel() {
                    Id = movie.Id,
                    Title = movie.Title,
                    TagLine = movie.TagLine,
                    Overview = movie.Overview,
                    PosterUrl = movie.PosterUrl,
                    BackdropUrl = movie.BackdropUrl,
                    ImdbUrl = movie.ImdbURL,
                    TmdbUrl = movie.TmdbUrl,
                    OriginalLanguage = movie.OriginalLanguage,
                    Budget = movie.Budget,
                    Revenue = movie.Revenue,
                    Price = movie.Price,
                    ReleaseDate = movie.ReleaseDate,
                    RunTime = movie.RunTime,
                    Rating = await _movieRepository.GetAverageRating(id),
                    IsPurchased = userId.HasValue && await _purchaseRepository.HasUserPurchasedMovie(userId, id),
                    Genres = movie.MovieGenres.Select(mg => new GenreModel {
                        Id = mg.Genre.Id,
                        Name = mg.Genre.Name
                    }).ToList(),
                    Casts = movie.MovieCasts.Select(mc => new CastModel {
                        Id = mc.Cast.Id,
                        Name = mc.Cast.Name,
                        ProfilePath = mc.Cast.ProfilePath,
                        Character = mc.Character
                    }).ToList(),
                    Trailers = movie.Trailers.Select(t => new TrailerModel {
                        Id = t.Id,
                        Name = t.Name,
                        TrailerUrl = t.TrailerUrl
                    }).ToList()
                };
                return movieDetails;
            }
            return null;
        }

        public async Task<List<MovieCardModel>> GetTop20GrossingMovies() {
            var movies = await _movieRepository.GetTop20GrossingMovies();
            var movieCardModels = new List<MovieCardModel>();
            foreach (var movie in movies) {
                movieCardModels.Add(new MovieCardModel() {
                    Id = movie.Id,
                    PosterURL = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCardModels;
        }

        public async Task<List<MovieCardModel>> GetMovieByGenre(int? id) {
            var movies = await _movieRepository.GetMoviesByGenre(id);
            var movieCardModels = new List<MovieCardModel>();
            foreach (var movie in movies) {
                movieCardModels.Add(new MovieCardModel() {
                    Id = movie.Id,
                    PosterURL = movie.PosterUrl,
                    Title = movie.Title
                });
            }
            return movieCardModels;
        }

    }
}
