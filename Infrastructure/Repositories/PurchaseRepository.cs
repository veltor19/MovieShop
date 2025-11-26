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

        public async Task<IEnumerable<TopMovieViewModel>> GetTopMoviesByPurchase(DateTime? fromDate, DateTime? toDate) {
            var query = _movieShopDbContext.Purchases.AsQueryable();

            if (fromDate.HasValue) {
                query = query.Where(p => p.PurchaseDateTime >= fromDate.Value);
            }

            if (toDate.HasValue) {
                var endDate = toDate.Value.Date.AddDays(1).AddSeconds(-1);
                query = query.Where(p => p.PurchaseDateTime <= endDate);
            }

            var topMovies = await query
                .GroupBy(p => p.MovieId)
                .Select(g => new {
                    MovieId = g.Key,
                    TotalPurchases = g.Count()
                })
                .OrderByDescending(x => x.TotalPurchases)
                .Take(30) // Get top 30 movies
                .ToListAsync();

            var movieIds = topMovies.Select(tm => tm.MovieId).ToList();
            var movies = await _movieShopDbContext.Movies
                .Where(m => movieIds.Contains(m.Id))
                .ToDictionaryAsync(m => m.Id, m => m.Title);

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

        public async Task<IEnumerable<Purchase>> GetUserPurchases(int userId) {
            return await _movieShopDbContext.Purchases.Where(p => p.UserId == userId).Include(p => p.Movie).ToListAsync();
        }

        public async Task<bool> HasUserPurchasedMovie(int? userId, int movieId) {
            return await _movieShopDbContext.Purchases.AnyAsync(p => p.UserId == userId && p.MovieId == movieId);
        }
    }
}
