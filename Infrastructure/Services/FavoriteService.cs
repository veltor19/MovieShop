using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public class FavoriteService : IFavoriteService {

        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository) {
            _favoriteRepository = favoriteRepository;
        }
        public IEnumerable<Favorite> GetUserFavorites(int userId) {
            return _favoriteRepository.GetUserFavorites(userId);
        }

        public bool IsFavorite(int userId, int movieId) {
            return _favoriteRepository.IsFavorite(userId, movieId);
        }

        public bool ToggleFavorite(int userId, int movieId) {
            var isFavorite = _favoriteRepository.IsFavorite(userId, movieId);

            if (isFavorite) {
                return _favoriteRepository.RemoveFavorite(userId, movieId);
            } else {
                return _favoriteRepository.AddFavorite(userId, movieId);
            }
        }
    }
}
