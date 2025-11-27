using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services {
    public interface IPurchaseService {

        Task<bool> PurchaseMovie(int userId, int movieId, DateTime purchaseDate);
        Task<bool> HasUserPurchasedMovie(int userId, int movieId);
        Task<IEnumerable<Purchase>> GetUserPurchases(int userId);
    }
}
