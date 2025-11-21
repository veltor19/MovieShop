using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services {
    public interface IPurchaseService {

        bool PurchaseMovie(int userId, int movieId);
        bool HasUserPurchasedMovie(int userId, int movieId);
        IEnumerable<Purchase> GetUserPurchases(int userId);
    }
}
