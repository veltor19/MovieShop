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
        public async Task<IEnumerable<Purchase>> GetUserPurchases(int userId) {
            return await _purchaseRepository.GetUserPurchases(userId);
        }

        public async Task<bool> HasUserPurchasedMovie(int userId, int movieId) {
            return  await _purchaseRepository.HasUserPurchasedMovie(userId, movieId);
        }

        public async Task<bool> PurchaseMovie(int userId, int movieId) {
            var alreadyPurchased = await _purchaseRepository.HasUserPurchasedMovie(userId, movieId);
            if (alreadyPurchased) return false;

            var movie = await _movieRepository.GetById(movieId);
            if (movie == null) return false;

            var purchase = new Purchase {
                UserId = userId,
                MovieId = movieId,
                PurchaseDateTime = DateTime.Now,
                PurchaseNumber = "1",
                TotalPrice = movie.Price ?? 0
            };

            await _purchaseRepository.Insert(purchase);
            return true;
        }
    }
}
