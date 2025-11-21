using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public class PurchaseService : IPurchaseService {

        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieRepository _movieRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository, IMovieRepository movieRepository) {
            _purchaseRepository = purchaseRepository;
            _movieRepository = movieRepository;
        }
        public IEnumerable<Purchase> GetUserPurchases(int userId) {
            return _purchaseRepository.GetUserPurchases(userId);
        }

        public bool HasUserPurchasedMovie(int userId, int movieId) {
            return  _purchaseRepository.HasUserPurchasedMovie(userId, movieId);
        }

        public bool PurchaseMovie(int userId, int movieId) {
            var alreadyPurchased = _purchaseRepository.HasUserPurchasedMovie(userId, movieId);
            if (alreadyPurchased) return false;

            var movie = _movieRepository.GetById(movieId);
            if (movie == null) return false;

            var purchase = new Purchase {
                UserId = userId,
                MovieId = movieId,
                PurchaseDateTime = DateTime.Now,
                PurchaseNumber = "1",
                TotalPrice = movie.Price ?? 0
            };

            _purchaseRepository.Insert(purchase);
            return true;
        }
    }
}
