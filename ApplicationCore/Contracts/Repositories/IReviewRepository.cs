using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories {
    public interface IReviewRepository {
        Task<bool> AddReview(Review review);
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(int userId, int movieId);
        Task<Review> GetUserReviewForMovie(int userId, int movieId);

        Task<IEnumerable<Review>> GetMovieReviews(int movieId);
    }
}
