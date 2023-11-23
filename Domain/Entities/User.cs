using Domain.Entities.Budget;
using Domain.Entities.VehicleNS;
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

        [Column("nome_imagem")]
        public string? ImageName { get; set; }

        [Column("numero_cnh")]
        public string? Cnh { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        [JsonIgnore]
        public string Password { get; set; }

        [Column("telefone")]
        public string? Cellphone { get; set; }

        [Column("possui_plano")]
        public bool HavePlan { get; set; }

        [Column("deviceid")]
        public string? DeviceId { get; set; }

        [Column("dias_trabalhados")]
        public int? DaysWorked { get; set; }

        [Column("exclusivo")]
        public bool Exclusive { get; set; }

        [Column("meta")]
        public double ? Goal{ get; set; }

        [Column("reset_password")]
        public Boolean ResetPassword { get; set; }

        [Column("reset_password_token")]
        public string? ResetPasswordToken { get; set; }

        [Column("reset_password_token_expiration")]
        public DateTime? ResetPasswordTokenExpiration { get; set; }

        [Column("verified")]
        public Boolean Verified { get; set; }

        [Column("verification_token")]
        public string? VerificationToken { get; set; }

        [Column("verificationh_token_expiration")]
        public DateTime? VerificationTokenExpiration { get; set; }

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
