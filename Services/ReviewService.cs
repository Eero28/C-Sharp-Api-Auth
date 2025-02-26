using Microsoft.EntityFrameworkCore;
using UserAuth.Database;
using UserAuth.Dto;
using UserAuth.Entities;

namespace UserAuth.Services
{
    public class ReviewService
    {
        private readonly DatabaseContext _dbContext;

        public ReviewService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Review>> GetReviewsAsync()
        {
            return await _dbContext.Reviews.ToListAsync();
        }

        public async Task<string> CreateReviewAsync(ReviewDto reviewDTO)
        {
            if (string.IsNullOrWhiteSpace(reviewDTO.ImageUrl) ||
                string.IsNullOrWhiteSpace(reviewDTO.ReviewDescription) ||
                string.IsNullOrWhiteSpace(reviewDTO.Category) ||
                string.IsNullOrWhiteSpace(reviewDTO.ReviewName))
            {
                return "All fields are required.";
            }

            var user = await _dbContext.Users.FindAsync(reviewDTO.UserId);
            if (user == null)
            {
                return "User not found.";
            }

            var review = new Review
            {
                ReviewName = reviewDTO.ReviewName,
                ReviewRating = reviewDTO.ReviewRating,
                ReviewDescription = reviewDTO.ReviewDescription,
                ImageUrl = reviewDTO.ImageUrl,
                Category = reviewDTO.Category,
                CreatedAt = DateTime.UtcNow,
                UserId = reviewDTO.UserId,
                User = user
            };

            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            return "Review created successfully";
        }
    }
}
