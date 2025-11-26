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
    public class FavoriteRepository: IFavoriteRepository {
        private readonly MovieShopDbContext _context;
        public FavoriteRepository(MovieShopDbContext context) {
            _context = context;
        }
        public async Task<bool> AddFavorite(int userId, int movieId) {
            var exists = await IsFavorite(userId, movieId);
            if (exists) {
                return false;
            }
            await _context.AddAsync(new Favorite {
                UserId = userId,
                MovieId = movieId
            });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Favorite>> GetUserFavorites(int userId) {
            return await _context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.Movie)
            .ToListAsync();
        }

        public async Task<bool> IsFavorite(int userId, int movieId) {
            return await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.MovieId == movieId);
        }

        public async Task<bool> RemoveFavorite(int userId, int movieId) {
            var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId);

            if (favorite == null) return false;

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
