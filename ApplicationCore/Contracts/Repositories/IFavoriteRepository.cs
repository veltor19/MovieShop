using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories {
    public interface IFavoriteRepository {
        bool AddFavorite(int userId, int movieId);
        bool RemoveFavorite(int userId, int movieId);
        bool IsFavorite(int userId, int movieId);
        IEnumerable<Favorite> GetUserFavorites(int userId);
    }
}
