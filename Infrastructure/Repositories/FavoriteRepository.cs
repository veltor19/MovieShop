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
        public bool AddFavorite(int userId, int movieId) {
            var exists = IsFavorite(userId, movieId);
            if (exists) {
                return false;
            }
            _context.Add(new Favorite {
                UserId = userId,
                MovieId = movieId
            });
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Favorite> GetUserFavorites(int userId) {
            return _context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.Movie)
            .ToList();
        }

        public bool IsFavorite(int userId, int movieId) {
            return _context.Favorites
            .Any(f => f.UserId == userId && f.MovieId == movieId);
        }

        public bool RemoveFavorite(int userId, int movieId) {
            var favorite = _context.Favorites
            .FirstOrDefault(f => f.UserId == userId && f.MovieId == movieId);

            if (favorite == null) return false;

            _context.Favorites.Remove(favorite);
            _context.SaveChanges();
            return true;
        }
    }
}
