using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAuth.Database;
using UserAuth.Entities;

namespace UserAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase

    {
        private readonly DatabaseContext _dbContext;

        public ReviewController(DatabaseContext dbContext) =>
            _dbContext = dbContext;

        [HttpGet]
        public async Task<ActionResult> GetReviews()
        {
            var reviews = await _dbContext.Reviews.ToListAsync();

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReview([FromBody] ReviewDto reviewDTO)
        {
            if (string.IsNullOrWhiteSpace(reviewDTO.ImageUrl) ||
                string.IsNullOrWhiteSpace(reviewDTO.ReviewDescription) ||
                string.IsNullOrWhiteSpace(reviewDTO.Category) ||
                string.IsNullOrWhiteSpace(reviewDTO.ReviewName))
            {
                return BadRequest("All fields are required.");
            }
            // Find the user associated with the review using UserId
            var user = await _dbContext.Users.FindAsync(reviewDTO.UserId);
            if (user == null)
            {
                return BadRequest("User not found.");
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

            return CreatedAtAction(nameof(GetReviews), new { id = review.Id_review }, review);
        }


    }
}
