using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories {
    public interface IFavoriteRepository {
        Task<bool> AddFavorite(int userId, int movieId);
        Task<bool> RemoveFavorite(int userId, int movieId);
        Task<bool> IsFavorite(int userId, int movieId);
        Task<IEnumerable<Favorite>> GetUserFavorites(int userId);
    }
}
