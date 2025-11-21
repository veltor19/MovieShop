using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories {
    public interface IPurchaseRepository: IRepository<Purchase> {
        bool HasUserPurchasedMovie(int ?userId, int movieId);

        IEnumerable<Purchase> GetUserPurchases(int userId);

        IEnumerable<TopMovieViewModel> GetTopMoviesByPurchase(DateTime? fromDate, DateTime? toDate);
    }
}
