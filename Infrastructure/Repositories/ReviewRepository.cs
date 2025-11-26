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

        public async Task<bool> AddReview(Review review) {
            var exists = await GetUserReviewForMovie(review.UserId, review.MovieId);
            if (exists != null) return false;

            review.CreatedDate = DateTime.Now;
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReview(int userId, int movieId) {
            var review = await GetUserReviewForMovie(userId, movieId);
            if (review == null) return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Review>> GetMovieReviews(int movieId) {
            return await _context.Reviews
            .Where(r => r.MovieId == movieId)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedDate)
            .ToListAsync();
        }

        public async Task<Review> GetUserReviewForMovie(int userId, int movieId) { 
            return await _context.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
        }

        public async Task<bool> UpdateReview(Review review) {
            var existing = await GetUserReviewForMovie(review.UserId, review.MovieId);
            if (existing == null) return false;

            existing.Rating = review.Rating;
            existing.ReviewText = review.ReviewText;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
