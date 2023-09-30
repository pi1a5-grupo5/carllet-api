using Domain.Entities.Budget;
using Domain.Entities.Vehicle;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public string Password { get; set; }

        [Column("telefone")]
        public string? Cellphone { get; set; }

        [Column("deviceid")]
        public string? DeviceId { get; set; }

        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expiration")]
        public DateTime? RefreshTokenExpiration { get; set; }

        [Column("access_token")]
        public string? AccessToken { get; set; } = null;

        [Column("access_token_expiration")]
        public DateTime? AccessTokenExpiration { get; set; }

        public List<UserVehicle>? UserVehicles { get; set; }

        public List<Earning>? Earnings { get; set; }

        public List<Prevision>? Previsions { get; set; }
    }
}
