using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Application.ViewModels.User
{
    public class UpdateUserRequest  
    {
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        public string? Cnh { get; set; }
        public string? Email { get; set; }
        public string? Cellphone { get; set; }
        public string? DeviceId { get; set; }
        public int? DaysWorked { get; set; }
        public bool? Exclusive { get; set; }
        public double? Goal { get; set; }

          }
}