using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories {
    public class ReviewRepository: IReviewRepository {

        private readonly MovieShopDbContext _context;
        public ReviewRepository(MovieShopDbContext context) {
            _context = context;
        }

        public bool AddReview(Review review) {
            var exists = GetUserReviewForMovie(review.UserId, review.MovieId);
            if (exists != null) return false;

            review.CreatedDate = DateTime.Now;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteReview(int userId, int movieId) {
            var review = GetUserReviewForMovie(userId, movieId);
            if (review == null) return false;

            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Review> GetMovieReviews(int movieId) {
            return _context.Reviews
            .Where(r => r.MovieId == movieId)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedDate)
            .ToList();
        }

        public Review GetUserReviewForMovie(int userId, int movieId) { 
            return _context.Reviews.FirstOrDefault(r => r.UserId == userId && r.MovieId == movieId);
        }

        public bool UpdateReview(Review review) {
            var existing = GetUserReviewForMovie(review.UserId, review.MovieId);
            if (existing == null) return false;

            existing.Rating = review.Rating;
            existing.ReviewText = review.ReviewText;
            _context.SaveChanges();
            return true;
        }
    }
}
