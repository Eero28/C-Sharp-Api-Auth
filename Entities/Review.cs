using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserAuth.Entities
{
    [Table("Review")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_review")]
        public int Id_review { get; set; }

        [Column("reviewName")]
        [Required]
        public string ReviewName { get; set; } = string.Empty;

        [Column("reviewRating")]
        [Required]
        public float ReviewRating { get; set; }

        [Column("reviewDescription")]
        [Required]
        public string ReviewDescription { get; set; } = string.Empty;

        [Column("imageUrl")]
        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Column("category")]
        [Required]
        public string Category { get; set; } = string.Empty;

        [Column("createdAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = new User();
    }
}
