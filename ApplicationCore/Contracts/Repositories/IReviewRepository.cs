using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories {
    public interface IReviewRepository {
        bool AddReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(int userId, int movieId);
        Review GetUserReviewForMovie(int userId, int movieId);

        IEnumerable<Review> GetMovieReviews(int movieId);
    }
}
