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
        public async Task<IEnumerable<Favorite>> GetUserFavorites(int userId) {
            return await _favoriteRepository.GetUserFavorites(userId);
        }

        public async Task<bool> IsFavorite(int userId, int movieId) {
            return await _favoriteRepository.IsFavorite(userId, movieId);
        }

        public async Task<bool> ToggleFavorite(int userId, int movieId) {
            var isFavorite = await _favoriteRepository.IsFavorite(userId, movieId);

            if (isFavorite) {
                return await _favoriteRepository.RemoveFavorite(userId, movieId);
            } else {
                return await _favoriteRepository.AddFavorite(userId, movieId);
            }
        }
    }
}
