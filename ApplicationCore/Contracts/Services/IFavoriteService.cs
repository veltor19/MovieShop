using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public interface IFavoriteService {
        bool ToggleFavorite(int userId, int movieId);
        bool IsFavorite(int userId, int movieId);
        IEnumerable<Favorite> GetUserFavorites(int userId);
    }
}
