using System.ComponentModel.DataAnnotations;

public class ReviewDto
{
    public required string ReviewName { get; set; }

    [Range(1, 5)] 
    public required float ReviewRating { get; set; }

    public required string ReviewDescription { get; set; }

    public required string ImageUrl { get; set; }
  
    public required string Category { get; set; }

    public required int UserId { get; set; }
}
