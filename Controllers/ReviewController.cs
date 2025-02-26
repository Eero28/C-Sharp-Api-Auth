using Microsoft.AspNetCore.Mvc;
using UserAuth.Dto;
using UserAuth.Services;

namespace UserAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult> GetReviews()
        {
            var reviews = await _reviewService.GetReviewsAsync();
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReview([FromBody] ReviewDto reviewDTO)
        {
            var result = await _reviewService.CreateReviewAsync(reviewDTO);
            if (result == "Review created successfully")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
