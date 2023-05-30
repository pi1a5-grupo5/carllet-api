using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("usuarios")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("nome")]
        public string Name { get; set; }

        [Column("numero_cnh")]
        public string? Cnh { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        public string Password { get; set; }

        [Column("telefone")]
        public string? Cellphone { get; set; }

        [Column("deviceid")]
        public string? DeviceId { get; set; }

        [Column("refresh_token")]
        public string? RefreshToken{ get; set; }

        [Column("refresh_token_expiration")]
        public DateTime? RefreshTokenExpiration { get; set; }

        [Column("access_token")]
        public string? AccessToken { get; set; } = null;

        [Column("access_token_expiration")]
        public DateTime? AccessTokenExpiration { get; set; }

        public List<Course>? Courses { get; set; }
    }
}
