using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services {
    public interface IReviewService {
        bool CreateReview(int userId, int movieId, decimal rating, string reviewText);
        IEnumerable<Review> GetMovieReviews(int movieId);
        Review GetUserReviewForMovie(int userId, int movieId);
    }
}
