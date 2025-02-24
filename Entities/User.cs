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
        public required string Email { get; set; }
        [Column("password")]
        public required string Password { get; set; }
        [Column("username")]
        public required string Username { get; set; }

    }
}
