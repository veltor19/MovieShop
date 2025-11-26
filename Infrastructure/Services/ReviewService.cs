using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public class ReviewService : IReviewService {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository) {
            _reviewRepository = reviewRepository;
        }
        public async Task<bool> CreateReview(int userId, int movieId, decimal rating, string reviewText) {
            var existingReview = await _reviewRepository.GetUserReviewForMovie(userId, movieId);
            if (existingReview != null) return false;

            var review = new Review {
                UserId = userId,
                MovieId = movieId,
                Rating = rating,
                ReviewText = reviewText,
                CreatedDate = DateTime.Now
            };

            return await _reviewRepository.AddReview(review);
        }

        public async Task<IEnumerable<Review>> GetMovieReviews(int movieId) {
            return await _reviewRepository.GetMovieReviews(movieId);
        }

        public async Task<Review> GetUserReviewForMovie(int userId, int movieId) {
            return await _reviewRepository.GetUserReviewForMovie(userId, movieId);
        }
    }
}
