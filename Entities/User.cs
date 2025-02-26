using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAuth.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_user")]
        public int Id_user { get; set; }

        [Column("email")]
        [Required] 
        public string Email { get; set; } = string.Empty;

        [Column("password")]
        [Required]  
        public string Password { get; set; } = string.Empty;

        [Column("username")]
        [Required]  
        public string Username { get; set; } = string.Empty;

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
