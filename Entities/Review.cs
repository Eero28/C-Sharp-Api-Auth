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
        public required string ReviewName { get; set; }

        [Column("reviewRating")]
        public required float ReviewRating { get; set; }

        [Column("reviewDescription")]
        public required string ReviewDescription { get; set; }
        
        [Column("imageUrl")]
        public required string ImageUrl { get; set; }

        [Column("category")]
        public required string Category { get; set; }

        [Column("createdAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // FK
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        

    }
}
