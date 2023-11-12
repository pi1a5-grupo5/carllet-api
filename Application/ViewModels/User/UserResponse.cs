using Domain.Entities.Budget;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.ViewModels.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Cnh { get; set; }
        public string Email { get; set; }
        public string? Cellphone { get; set; }
        public string? DeviceId { get; set; }
        public string? AccessToken { get; set; } = null;
        public DateTime? AccessTokenExpiration { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
