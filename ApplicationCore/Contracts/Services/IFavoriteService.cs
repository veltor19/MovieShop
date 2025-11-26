using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public interface IFavoriteService {
        Task<bool> ToggleFavorite(int userId, int movieId);
        Task<bool> IsFavorite(int userId, int movieId);
        Task<IEnumerable<Favorite>> GetUserFavorites(int userId);
    }
}
