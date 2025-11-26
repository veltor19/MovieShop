using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services {
    public interface IReviewService {
        Task<bool> CreateReview(int userId, int movieId, decimal rating, string reviewText);
        Task<IEnumerable<Review>> GetMovieReviews(int movieId);
        Task<Review> GetUserReviewForMovie(int userId, int movieId);
    }
}
