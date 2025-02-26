namespace UserAuth.Dto
{
    using System.ComponentModel.DataAnnotations;

    public class ReviewDto
    {
        [Required(ErrorMessage = "Review name is required.")]
        public string ReviewName { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "Review rating must be between 1 and 5.")]
        [Required(ErrorMessage = "Review rating is required.")]
        public float ReviewRating { get; set; }

        [Required(ErrorMessage = "Review description is required.")]
        public string ReviewDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image URL is required.")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
    }
}
