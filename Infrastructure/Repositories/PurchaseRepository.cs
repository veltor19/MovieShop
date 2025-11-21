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
    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository {

        public PurchaseRepository(MovieShopDbContext context): base(context) {

        }

        public IEnumerable<TopMovieViewModel> GetTopMoviesByPurchase(DateTime? fromDate, DateTime? toDate) {
            var query = _movieShopDbContext.Purchases.AsQueryable();

            if (fromDate.HasValue) {
                query = query.Where(p => p.PurchaseDateTime >= fromDate.Value);
            }

            if (toDate.HasValue) {
                var endDate = toDate.Value.Date.AddDays(1).AddSeconds(-1);
                query = query.Where(p => p.PurchaseDateTime <= endDate);
            }

            var topMovies = query
                .GroupBy(p => p.MovieId)
                .Select(g => new {
                    MovieId = g.Key,
                    TotalPurchases = g.Count()
                })
                .OrderByDescending(x => x.TotalPurchases)
                .Take(30) // Get top 30 movies
                .ToList();

            var movieIds = topMovies.Select(tm => tm.MovieId).ToList();
            var movies = _movieShopDbContext.Movies
                .Where(m => movieIds.Contains(m.Id))
                .ToDictionary(m => m.Id, m => m.Title);

            var result = topMovies
                .Select((item, index) => new TopMovieViewModel {
                    Rank = index + 1,
                    MovieId = item.MovieId,
                    Title = movies.ContainsKey(item.MovieId) ? movies[item.MovieId] : "Unknown",
                    TotalPurchases = item.TotalPurchases
                })
                .ToList();

            return result;
        }

        public IEnumerable<Purchase> GetUserPurchases(int userId) {
            return _movieShopDbContext.Purchases.Where(p => p.UserId == userId).Include(p => p.Movie).ToList();
        }

        public bool HasUserPurchasedMovie(int? userId, int movieId) {
            return _movieShopDbContext.Purchases.Any(p => p.UserId == userId && p.MovieId == movieId);
        }
    }
}
